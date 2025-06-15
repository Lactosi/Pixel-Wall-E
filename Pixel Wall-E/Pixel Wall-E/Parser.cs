using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;

namespace PixelWallE
{
    public class Parser
    {
        private readonly Canvas canvas;
        private readonly WallE wallE;
        private readonly Dictionary<string, int> labels = new Dictionary<string, int>();
        private readonly Dictionary<string, dynamic> variables = new Dictionary<string, dynamic>();
        private readonly Lexer lexer = new Lexer();

        public int CurrentLine { get; set; }
        public bool SpawnExecuted { get; set; }

        public Parser(Canvas canvas, WallE wallE)
        {
            this.canvas = canvas;
            this.wallE = wallE;
        }

        public void ExecuteCode(string[] lines)
        {
  
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i].Trim();
                if (lexer.IsLabel(line))
                {
                    if (labels.ContainsKey(line))
                        throw new Exception($"Duplicate label: {line}");
                    labels[line] = i;
                }
            }

            while (CurrentLine < lines.Length)
            {
                string line = lines[CurrentLine].Trim();
                ProcessLine(line);
            }
        }

        private void ProcessLine(string line)
        {
            if (string.IsNullOrWhiteSpace(line) || line.StartsWith("//"))
            {
                CurrentLine++;
                return;
            }

            if (lexer.IsLabel(line))
            {
                CurrentLine++;
                return;
            }

            try
            {
                if (line.StartsWith("Spawn("))
                {
                    HandleSpawn(line);
                    SpawnExecuted = true;
                }
                else if (line.StartsWith("Color(")) HandleColor(line);
                else if (line.StartsWith("Size(")) HandleSize(line);
                else if (line.StartsWith("DrawLine("))
                {
                    if (!SpawnExecuted) throw new Exception("Must execute Spawn first");
                    HandleDrawLine(line);
                }
                else if (line.StartsWith("DrawCircle("))
                {
                    if (!SpawnExecuted) throw new Exception("Must execute Spawn first");
                    HandleDrawCircle(line);
                }
                else if (line.StartsWith("DrawRectangle("))
                {
                    if (!SpawnExecuted) throw new Exception("Must execute Spawn first");
                    HandleDrawRectangle(line);
                }
                else if (line.StartsWith("Fill("))
                {
                    if (!SpawnExecuted) throw new Exception("Must execute Spawn first");
                    wallE.Fill();
                }
                else if (line.StartsWith("GoTo [")) HandleGoTo(line);
                else if (line.Contains("<-")) HandleVariableAssignment(line);
                else throw new Exception($"Unrecognized command: {line}");

                if (!line.StartsWith("GoTo [")) CurrentLine++;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error on line {CurrentLine + 1}: {ex.Message}");
            }
        }

        private void HandleSpawn(string line)
        {
            var match = Regex.Match(line, @"Spawn\((\s*(\d+|\w+)\s*,\s*(\d+|\w+)\s*)\)");
            if (!match.Success)
                throw new Exception("Sintaxis incorrecta para Spawn. Formato esperado: Spawn(x, y)");

            int x = ParseExpression(match.Groups[2].Value);
            int y = ParseExpression(match.Groups[3].Value);

            wallE.Spawn(x, y);
        }

        private void HandleColor(string line)
        {
            var match = Regex.Match(line, @"Color\(\s*(""([^""]+)""|([a-zA-Z]+))\s*\)");
            if (!match.Success)
                throw new Exception("Sintaxis incorrecta para Color. Formato esperado: Color(\"color\") o Color(color)");

            string colorName = match.Groups[2].Success ? match.Groups[2].Value : match.Groups[3].Value;
            wallE.SetColor(colorName);
        }

        private void HandleSize(string line)
        {
            var match = Regex.Match(line, @"Size\((\s*(\d+|\w+)\s*)\)");
            if (!match.Success)
                throw new Exception("Sintaxis incorrecta para Size. Formato esperado: Size(k)");

            int size = ParseExpression(match.Groups[2].Value);
            wallE.SetSize(size);
        }

        private void HandleDrawLine(string line)
        {
            var match = Regex.Match(line, @"DrawLine\(\s*([-]?\d+|\w+)\s*,\s*([-]?\d+|\w+)\s*,\s*(\d+|\w+)\s*\)");
            if (!match.Success)
                throw new Exception("Sintaxis incorrecta para DrawLine. Formato esperado: DrawLine(dirX, dirY, distance)");

            int dirX = ParseExpression(match.Groups[1].Value);
            int dirY = ParseExpression(match.Groups[2].Value);
            int distance = ParseExpression(match.Groups[3].Value);

            wallE.DrawLine(dirX, dirY, distance);
        }

        private void HandleDrawCircle(string line)
        {
            var match = Regex.Match(line, @"DrawCircle\(\s*([-]?\d+|\w+)\s*,\s*([-]?\d+|\w+)\s*,\s*(\d+|\w+)\s*\)");
            if (!match.Success)
                throw new Exception("Sintaxis incorrecta para DrawCircle. Formato esperado: DrawCircle(dirX, dirY, radius)");

            int dirX = ParseExpression(match.Groups[1].Value);
            int dirY = ParseExpression(match.Groups[2].Value);
            int radius = ParseExpression(match.Groups[3].Value);

            wallE.DrawCircle(dirX, dirY, radius);
        }

        private void HandleDrawRectangle(string line)
        {
            var match = Regex.Match(line, @"DrawRectangle\(\s*([-]?\d+|\w+)\s*,\s*([-]?\d+|\w+)\s*,\s*(\d+|\w+)\s*,\s*(\d+|\w+)\s*,\s*(\d+|\w+)\s*\)");
            if (!match.Success)
                throw new Exception("Sintaxis incorrecta para DrawRectangle. Formato esperado: DrawRectangle(dirX, dirY, distance, width, height)");

            int dirX = ParseExpression(match.Groups[1].Value);
            int dirY = ParseExpression(match.Groups[2].Value);
            int distance = ParseExpression(match.Groups[3].Value);
            int width = ParseExpression(match.Groups[4].Value);
            int height = ParseExpression(match.Groups[5].Value);

            wallE.DrawRectangle(dirX, dirY, distance, width, height);
        }

        private void HandleVariableAssignment(string line)
        {
            var parts = line.Split(new[] { "<-" }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length != 2)
                throw new Exception("Sintaxis incorrecta para asignación de variable");

            string varName = parts[0].Trim();
            string expression = parts[1].Trim();

            if (!Regex.IsMatch(varName, @"^[a-zA-Z][a-zA-Z0-9_]*$"))
                throw new Exception($"Nombre de variable inválido: {varName}");

            variables[varName] = EvaluateExpression(expression);
        }

        private void HandleGoTo(string line)
        {
            var match = Regex.Match(line, @"GoTo\s*\[\s*([a-zA-Z][a-zA-Z0-9_\-]*)\s*\]\s*\(\s*([^()]*(?:\((?<depth>)[^()]*\)|[^()]*)*)\s*\)");
            if (!match.Success)
                throw new Exception("Invalid GoTo syntax. Expected format: GoTo [label] (condition)");

            string label = match.Groups[1].Value.Trim();
            string condition = match.Groups[2].Value.Trim();

            if (!labels.ContainsKey(label))
                throw new Exception($"Label not found: {label}");

            bool conditionResult = EvaluateCondition(condition);
            if (conditionResult)
            {
                CurrentLine = labels[label];
            }
            else
            {
                CurrentLine++;
            }
        }

        private int ParseExpression(string expression)
        {
            expression = expression.Trim();

            if (int.TryParse(expression, out int number))
                return number;

            if (variables.ContainsKey(expression))
                return variables[expression];

            if (expression.StartsWith("GetActualX(") && expression.EndsWith(")"))
                return wallE.GetActualX();
            if (expression.StartsWith("GetActualY(") && expression.EndsWith(")"))
                return wallE.GetActualY();
            if (expression.StartsWith("GetCanvasSize(") && expression.EndsWith(")"))
                return canvas.Size;
            if (expression.StartsWith("GetColorCount("))
            {
                var match = Regex.Match(expression, @"GetColorCount\(\s*""([^""]+)""\s*,\s*(\d+|\w+)\s*,\s*(\d+|\w+)\s*,\s*(\d+|\w+)\s*,\s*(\d+|\w+)\s*\)");
                if (!match.Success)
                    throw new Exception("Sintaxis incorrecta para GetColorCount");
                string color = match.Groups[1].Value;
                int x1 = ParseExpression(match.Groups[2].Value);
                int y1 = ParseExpression(match.Groups[3].Value);
                int x2 = ParseExpression(match.Groups[4].Value);
                int y2 = ParseExpression(match.Groups[5].Value);
                return canvas.GetColorCount(color, x1, y1, x2, y2);
            }

            try
            {
                string exprToEvaluate = expression;
                foreach (var var in variables)
                {
                    exprToEvaluate = exprToEvaluate.Replace(var.Key, var.Value.ToString());
                }
                var dataTable = new System.Data.DataTable();
                var result = dataTable.Compute(exprToEvaluate, null);
                return Convert.ToInt32(result);
            }
            catch
            {
                throw new Exception($"No se pudo evaluar la expresión: {expression}");
            }
        }

        private bool EvaluateCondition(string condition)
        {
            condition = condition.Trim();

            var comparisonMatch2 = Regex.Match(condition, @"^\s*(.+?)\s*(==|!=)\s*(.+?)\s*$");
            if (comparisonMatch2.Success)
            {
                string left = comparisonMatch2.Groups[1].Value;
                string op = comparisonMatch2.Groups[2].Value;
                string right = comparisonMatch2.Groups[3].Value;

                dynamic leftVal = EvaluateExpression(left);
                dynamic rightVal = EvaluateExpression(right);

                if (leftVal is bool && rightVal is int)
                    leftVal = (bool)leftVal ? 1 : 0;
                else if (leftVal is int && rightVal is bool)
                    rightVal = (bool)rightVal ? 1 : 0;

                return op == "==" ? leftVal == rightVal : leftVal != rightVal;
            }

            if (condition.StartsWith("IsBrushColor("))
            {
                var match2 = Regex.Match(condition, @"IsBrushColor\(\s*(""([^""]+)""|([a-zA-Z]+))\s*\)");
                if (!match2.Success)
                    throw new Exception("Sintaxis incorrecta para IsBrushColor");
                string color = match2.Groups[2].Success ? match2.Groups[2].Value : match2.Groups[3].Value;
                return wallE.IsBrushColor(color);
            }
            else if (condition.StartsWith("IsBrushSize"))
            {
                var match2 = Regex.Match(condition, @"IsBrushSize\(\s*(\d+|\w+)\s*\)");
                if (!match2.Success)
                    throw new Exception("Sintaxis incorrecta para IsBrushSize");
                int size = ParseExpression(match2.Groups[1].Value);
                return wallE.IsBrushSize(size);
            }
            else if (condition.StartsWith("IsCanvasColor"))
            {
                var match2 = Regex.Match(condition, @"IsCanvasColor\(\s*""([^""]+)""\s*,\s*([-]?\d+|\w+)\s*,\s*([-]?\d+|\w+)\s*\)");
                if (!match2.Success)
                    throw new Exception("Sintaxis incorrecta para IsCanvasColor");
                string color = match2.Groups[1].Value;
                int vertical = ParseExpression(match2.Groups[2].Value);
                int horizontal = ParseExpression(match2.Groups[3].Value);
                return wallE.IsCanvasColor(color, vertical, horizontal);
            }

            var comparisonMatch = Regex.Match(condition, @"^\s*([^\s=<>]+)\s*(==|>=|<=|>|<)\s*([^\s=<>]+)\s*$");
            if (comparisonMatch.Success)
            {
                string left = comparisonMatch.Groups[1].Value;
                string op = comparisonMatch.Groups[2].Value;
                string right = comparisonMatch.Groups[3].Value;

                int leftVal = ParseExpression(left);
                int rightVal = ParseExpression(right);

                switch (op)
                {
                    case "==": return leftVal == rightVal;
                    case ">=": return leftVal >= rightVal;
                    case "<=": return leftVal <= rightVal;
                    case ">": return leftVal > rightVal;
                    case "<": return leftVal < rightVal;
                }
            }

            if (condition.Contains("&&") || condition.Contains("||"))
            {
                var parts = condition.Split(new[] { "&&", "||" }, StringSplitOptions.RemoveEmptyEntries);
                var operators = Regex.Matches(condition, @"&&|\|\|").Cast<Match>().Select(m => m.Value).ToList();

                bool result = EvaluateCondition(parts[0].Trim());
                for (int i = 0; i < operators.Count; i++)
                {
                    bool next = EvaluateCondition(parts[i + 1].Trim());
                    if (operators[i] == "&&")
                        result = result && next;
                    else
                        result = result || next;
                }
                return result;
            }

            throw new Exception($"No se pudo evaluar la condicion: {condition}");
        }

        private dynamic EvaluateExpression(string expression)
        {
            expression = expression.Trim();

            if (expression.Equals("true", StringComparison.OrdinalIgnoreCase)) return true;
            if (expression.Equals("false", StringComparison.OrdinalIgnoreCase)) return false;

            if (int.TryParse(expression, out int intValue)) return intValue;

            if (variables.ContainsKey(expression)) return variables[expression];

            if (expression.StartsWith("IsBrushColor(") ||
                expression.StartsWith("IsBrushSize(") ||
                expression.StartsWith("IsCanvasColor("))
            {
                return EvaluateBoolExpression(expression);
            }

            if (expression.StartsWith("GetActualX(") ||
                expression.StartsWith("GetActualY(") ||
                expression.StartsWith("GetCanvasSize(") ||
                expression.StartsWith("GetColorCount("))
            {
                return ParseExpression(expression);
            }

            try
            {
                return ParseExpression(expression);
            }
            catch
            {
                throw new Exception($"No se pudo evaluar la expresión: {expression}");
            }
        }

        private bool EvaluateBoolExpression(string expr)
        {
            expr = expr.Trim();

            if (expr.Equals("true", StringComparison.OrdinalIgnoreCase)) return true;
            if (expr.Equals("false", StringComparison.OrdinalIgnoreCase)) return false;

            if (variables.TryGetValue(expr, out dynamic varValue))
            {
                if (varValue is bool boolVal) return boolVal;
                if (varValue is int intVal) return intVal != 0;
                throw new Exception($"Variable '{expr}' is not boolean");
            }

            var comparisonMatch = Regex.Match(expr,
                @"^\s*([^\s=<>]+)\s*(==|!=|>=|<=|>|<)\s*([^\s=<>]+)\s*$");
            if (comparisonMatch.Success)
            {
                string left = comparisonMatch.Groups[1].Value.Trim();
                string op = comparisonMatch.Groups[2].Value;
                string right = comparisonMatch.Groups[3].Value.Trim();

                if (op == "==" || op == "!=")
                {
                    if (int.TryParse(right, out int rightInt))
                    {
                        bool leftBool = EvaluateBoolExpression(left);
                        int leftInt = leftBool ? 1 : 0;
                        return op == "==" ? leftInt == rightInt : leftInt != rightInt;
                    }
                    else if (int.TryParse(left, out int leftInt))
                    {
                        bool rightBool = EvaluateBoolExpression(right);
                        int rightint = rightBool ? 1 : 0;
                        return op == "==" ? leftInt == rightint : leftInt != rightint;
                    }
                }

                int leftNum = ParseExpression(left);
                int rightNum = ParseExpression(right);

                return op switch
                {
                    "==" => leftNum == rightNum,
                    "!=" => leftNum != rightNum,
                    ">" => leftNum > rightNum,
                    "<" => leftNum < rightNum,
                    ">=" => leftNum >= rightNum,
                    "<=" => leftNum <= rightNum,
                    _ => throw new Exception($"Unknown operator: {op}")
                };
            }

            if (expr.StartsWith("IsBrushColor("))
            {
                var match = Regex.Match(expr, @"IsBrushColor\(\s*(""([^""]+)""|([a-zA-Z]+))\s*\)");
                if (!match.Success) throw new Exception("Invalid IsBrushColor syntax");
                string color = match.Groups[2].Success ? match.Groups[2].Value : match.Groups[3].Value;
                return wallE.IsBrushColor(color);
            }
            if (expr.StartsWith("IsBrushSize("))
            {
                var match = Regex.Match(expr, @"IsBrushSize\(\s*(\d+|\w+)\s*\)");
                if (!match.Success) throw new Exception("Invalid IsBrushSize syntax");
                int size = ParseExpression(match.Groups[1].Value);
                return wallE.IsBrushSize(size);
            }
            if (expr.StartsWith("IsCanvasColor("))
            {
                var match = Regex.Match(expr,
                    @"IsCanvasColor\(\s*""([^""]+)""\s*,\s*([-]?\d+|\w+)\s*,\s*([-]?\d+|\w+)\s*\)");
                if (!match.Success) throw new Exception("Invalid IsCanvasColor syntax");
                string color = match.Groups[1].Value;
                int vertical = ParseExpression(match.Groups[2].Value);
                int horizontal = ParseExpression(match.Groups[3].Value);
                return wallE.IsCanvasColor(color, vertical, horizontal);
            }

            if (expr.Contains("&&") || expr.Contains("||"))
            {
                string[] operators = { "&&", "||" };
                var parts = expr.Split(operators, StringSplitOptions.RemoveEmptyEntries);
                var ops = Regex.Matches(expr, @"&&|\|\|").Cast<Match>().Select(m => m.Value).ToList();

                bool result = EvaluateBoolExpression(parts[0].Trim());
                for (int i = 0; i < ops.Count; i++)
                {
                    bool next = EvaluateBoolExpression(parts[i + 1].Trim());
                    result = ops[i] == "&&" ? result && next : result || next;
                }
                return result;
            }

            try
            {
                int numValue = ParseExpression(expr);
                return numValue != 0;
            }
            catch
            {
                throw new Exception($"Cannot evaluate boolean expression: {expr}");
            }
        }
    }
}