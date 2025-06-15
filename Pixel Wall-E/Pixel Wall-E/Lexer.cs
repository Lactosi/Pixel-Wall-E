using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace PixelWallE
{
    public class Lexer
    {
        private readonly HashSet<string> reservedKeywords = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
{
    // Commands
    "Spawn", "Color", "Size", "DrawLine", "DrawCircle", "DrawRectangle", "Fill",
    // Functions
    "GetActualX", "GetActualY", "GetCanvasSize", "GetColorCount",
    "IsBrushColor", "IsBrushSize", "IsCanvasColor",
    // Colors
    "Red", "Blue", "Green", "Yellow", "Orange", "Purple", "Black", "White", "Transparent"
};

        public bool IsLabel(string line)
        {
            line = line.Trim();

            if (string.IsNullOrWhiteSpace(line)) return false;
            if (line.Contains(" ")) return false;

            foreach (string keyword in reservedKeywords)
            {
                if (line.StartsWith(keyword, StringComparison.OrdinalIgnoreCase))
                {
                    if (line.Length == keyword.Length ||
                        !char.IsLetterOrDigit(line[keyword.Length]))
                    {
                        return false;
                    }
                }
            }

            return Regex.IsMatch(line, @"^[a-zA-Z][a-zA-Z0-9_\-]*$");
        }

        public TokenType GetTokenType(string token)
        {
            if (reservedKeywords.Contains(token)) return TokenType.Keyword;
            if (Regex.IsMatch(token, @"^\d+$")) return TokenType.Number;
            if (Regex.IsMatch(token, @"^""[^""]*""$")) return TokenType.String;
            if (Regex.IsMatch(token, @"^[a-zA-Z][a-zA-Z0-9_]*$")) return TokenType.Identifier;
            if (Regex.IsMatch(token, @"^[+\-*/%()=<>!&|,]$")) return TokenType.Operator;

            return TokenType.Unknown;
        }

        public bool IsBooleanExpression(string expression)
        {
            expression = expression.Trim();

            if (expression.StartsWith("IsBrushColor(") ||
                expression.StartsWith("IsBrushSize(") ||
                expression.StartsWith("IsCanvasColor("))
                return true;

            if (expression.Contains("==") || expression.Contains("!=") ||
               expression.Contains("<") || expression.Contains(">") ||
               expression.Contains("&&") || expression.Contains("||"))
                return true;

            if (expression.Equals("true", StringComparison.OrdinalIgnoreCase) ||
                expression.Equals("false", StringComparison.OrdinalIgnoreCase))
                return true;

            return false;
        }
    }

    public enum TokenType
    {
        Keyword,
        Number,
        String,
        Label, 
        Identifier,
        Operator,
        Unknown
    }
}