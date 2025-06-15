using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace PixelWallE
{
    public partial class Interface : Form
    {
        private WallE wallE;
        private Canvas canvas;
        private Parser parser;
        private int canvasSize = 20;
        private System.Windows.Forms.Panel panelLineNumbers;
        private System.Windows.Forms.Panel panelCodeEditor;
        private StableSyntaxHighlighter _syntaxHighlighter;

        public Interface()
        {
            InitializeComponent();
            InitializeCanvas();
            InitializeEditor();
        }

        private void InitializeCanvas()
        {
            canvas = new Canvas(canvasSize);
            wallE = new WallE(canvas);
            parser = new Parser(canvas, wallE);
            pnlCanvas.Controls.Clear();
            pnlCanvas.Controls.Add(canvas);
            canvas.Dock = DockStyle.Fill;
            canvas.BackColor = Color.White;
        }

        private void InitializeEditor()
        {
            txtLineNumbers.BackColor = SystemColors.ControlLight;
            txtLineNumbers.ForeColor = SystemColors.GrayText;
            txtLineNumbers.TabStop = false;
            txtLineNumbers.ReadOnly = true;

            _syntaxHighlighter = new StableSyntaxHighlighter(txtCode);

            txtCode.Text = " ";
            UpdateLineNumbers();
            txtCode.Text = "";
            txtCode.TextChanged += (s, e) => UpdateLineNumbers();
            txtCode.TextChanged += TxtCode_TextChanged;
            txtCode.Font = new Font("Consolas", 10);
            txtCode.WordWrap = false;

            txtCode.TextChanged += (s, e) => SyncLineNumbersScroll();
            txtCode.KeyUp += (s, e) => SyncLineNumbersScroll();
            txtCode.MouseUp += (s, e) => SyncLineNumbersScroll();

            panelCodeEditor.Resize += panelCodeEditor_Resize;
        }

        private void TxtCode_TextChanged(object sender, EventArgs e)
        {
            UpdateLineNumbers();
            SyncLineNumbersScroll();
        }

        private void SyncLineNumbersScroll()
        {
            if (txtCode.Focused)
            {
                int firstVisibleLine = txtCode.GetLineFromCharIndex(txtCode.GetCharIndexFromPosition(new Point(0, 0)));
                int targetLineNumberScrollPos = txtLineNumbers.GetFirstCharIndexFromLine(firstVisibleLine);

                if (txtLineNumbers.SelectionStart != targetLineNumberScrollPos)
                {
                    txtLineNumbers.SelectionStart = targetLineNumberScrollPos;
                    txtLineNumbers.ScrollToCaret();
                }
            }
        }

        private void UpdateLineNumbers()
        {
            int firstVisibleLine = txtCode.GetLineFromCharIndex(txtCode.GetCharIndexFromPosition(new Point(0, 0)));
            var lines = txtCode.Lines;

            if (lines.Length == 0)
            {
                txtLineNumbers.Text = "1";
                return;
            }

            int lineCount = lines.Length;
            int digitCount = lineCount.ToString().Length;
            int newWidth = 10 + digitCount * 7;

            txtLineNumbers.Width = Math.Max(30, newWidth);
            txtCode.Left = txtLineNumbers.Width;
            txtCode.Width = panelCodeEditor.Width - txtLineNumbers.Width;

            string lineNumbers = string.Join(Environment.NewLine, Enumerable.Range(1, lineCount));
            txtLineNumbers.Text = lineNumbers;

            int targetLineNumberScrollPos = txtLineNumbers.GetFirstCharIndexFromLine(firstVisibleLine);
            txtLineNumbers.SelectionStart = targetLineNumberScrollPos;
            txtLineNumbers.ScrollToCaret();
        }

        private void panelCodeEditor_Resize(object sender, EventArgs e)
        {
            UpdateLineNumbers();
        }

        private void txtCode_Scroll(object sender, EventArgs e)
        {
            txtLineNumbers.ScrollToCaret();
        }

        private void btnExecute_Click(object sender, EventArgs e)
        {
            ExecuteCode();
        }

        private void ExecuteCode()
        {
            try
            {
                canvas.Clear();
                wallE = new WallE(canvas);
                parser = new Parser(canvas, wallE);
                parser.ExecuteCode(txtCode.Lines);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Execution Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Archivos Pixel Wall-E (*.pw)|*.pw|Todos los archivos (*.*)|*.*",
                Title = "Abrir archivo de código"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    txtCode.Text = File.ReadAllText(openFileDialog.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al cargar el archivo: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Archivos Pixel Wall-E (*.pw)|*.pw|Todos los archivos (*.*)|*.*",
                Title = "Guardar archivo de código"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    File.WriteAllText(saveFileDialog.FileName, txtCode.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al guardar el archivo: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnResize_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtCanvasSize.Text, out int newSize) && newSize > 0)
            {
                canvasSize = newSize;
                InitializeCanvas();
            }
            else
            {
                MessageBox.Show("Por favor ingrese un tamaño válido para el canvas (número entero positivo).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtLineNumbers_TextChanged(object sender, EventArgs e) { }
        private void txtCode_TextChanged(object sender, EventArgs e) { }
    }
}