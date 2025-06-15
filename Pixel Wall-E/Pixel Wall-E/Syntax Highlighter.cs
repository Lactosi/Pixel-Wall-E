using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace PixelWallE
{
    public class StableSyntaxHighlighter : IDisposable
    {
        private readonly RichTextBox _textBox;
        private readonly Lexer _lexer;
        private readonly Dictionary<TokenType, Color> _colorScheme;
        private readonly System.Windows.Forms.Timer _highlightTimer;
        private bool _isHighlighting = false;
        private bool _disposed = false;
        private int _lastCursorPos = 0;

        public StableSyntaxHighlighter(RichTextBox textBox)
        {
            _textBox = textBox ?? throw new ArgumentNullException(nameof(textBox));
            _lexer = new Lexer();

            _colorScheme = new Dictionary<TokenType, Color>
            {
                { TokenType.Keyword, Color.FromArgb(86, 156, 214) },
                { TokenType.Number, Color.FromArgb(181, 206, 168) },
                { TokenType.String, Color.FromArgb(206, 145, 120) },
                { TokenType.Label, Color.FromArgb(216, 160, 223) },
                { TokenType.Identifier, Color.FromArgb(220, 220, 170) },
                { TokenType.Operator, Color.FromArgb(180, 180, 180) },
                { TokenType.Unknown, _textBox.ForeColor }
            };

            _highlightTimer = new System.Windows.Forms.Timer { Interval = 100, Enabled = false };
            _highlightTimer.Tick += (s, e) => SafeHighlight();

            SetDoubleBuffering(_textBox, true);

            _textBox.TextChanged += OnTextChanged;
        }

        private void OnTextChanged(object sender, EventArgs e)
        {
            if (_isHighlighting) return;

            _lastCursorPos = _textBox.SelectionStart;

            _highlightTimer.Stop();
            _highlightTimer.Start();
        }

        private void SafeHighlight()
        {
            if (_isHighlighting || _textBox.IsDisposed || _disposed)
                return;

            _isHighlighting = true;

            try
            {
                var originalSelectionStart = _textBox.SelectionStart;
                var originalSelectionLength = _textBox.SelectionLength;
                var originalFirstVisibleLine = GetFirstVisibleLine();

                NativeMethods.SendMessage(_textBox.Handle, NativeMethods.WM_SETREDRAW, 0, IntPtr.Zero);

                ApplySyntaxHighlighting();

                _textBox.SelectionStart = originalSelectionStart;
                _textBox.SelectionLength = originalSelectionLength;

                _textBox.SelectionStart = _textBox.GetFirstCharIndexFromLine(originalFirstVisibleLine);
                _textBox.ScrollToCaret();
                _textBox.SelectionStart = originalSelectionStart;
            }
            finally
            {
                NativeMethods.SendMessage(_textBox.Handle, NativeMethods.WM_SETREDRAW, 1, IntPtr.Zero);
                _textBox.Invalidate();
                _isHighlighting = false;
            }
        }

        private void ApplySyntaxHighlighting()
        {
            int originalPosition = _textBox.SelectionStart;

            _textBox.Select(0, _textBox.TextLength);
            _textBox.SelectionColor = _colorScheme[TokenType.Unknown];

            foreach (var token in GetAllTokens())
            {
                _textBox.Select(token.Start, token.Length);
                _textBox.SelectionColor = _colorScheme[token.Type];
            }

            _textBox.Select(originalPosition, 0);
        }

        private IEnumerable<TextToken> GetAllTokens()
        {
            string text = _textBox.Text;
            if (string.IsNullOrEmpty(text)) yield break;

            int lineStart = 0;
            foreach (var line in _textBox.Lines)
            {
                if (_lexer.IsLabel(line.Trim()))
                {
                    yield return new TextToken(TokenType.Label, lineStart, line.Length);
                }
                else
                {
                    foreach (var token in GetTokensInLine(line, lineStart))
                        yield return token;
                }
                lineStart += line.Length + 1;
            }
        }

        private IEnumerable<TextToken> GetTokensInLine(string line, int lineStart)
        {
            var stringMatches = Regex.Matches(line, @"""(?:\\""|[^""])*""");
            foreach (Match match in stringMatches)
            {
                yield return new TextToken(TokenType.String, lineStart + match.Index, match.Length);
                line = line.Remove(match.Index, match.Length).Insert(match.Index, new string(' ', match.Length));
            }

            var wordMatches = Regex.Matches(line, @"(\b\w+\b|[^\w\s])");
            foreach (Match match in wordMatches)
            {
                if (!string.IsNullOrWhiteSpace(match.Value))
                {
                    yield return new TextToken(
                        _lexer.GetTokenType(match.Value),
                        lineStart + match.Index,
                        match.Length
                    );
                }
            }
        }

        private int GetFirstVisibleLine()
        {
            return _textBox.GetLineFromCharIndex(_textBox.GetCharIndexFromPosition(Point.Empty));
        }

        public static void SetDoubleBuffering(Control control, bool enable)
        {
            var method = typeof(Control).GetMethod("SetStyle",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            method?.Invoke(control, new object[] {
                ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, enable });
        }

        public void Dispose()
        {
            if (_disposed) return;
            _disposed = true;
            _highlightTimer?.Stop();
            _highlightTimer?.Dispose();
            if (_textBox != null && !_textBox.IsDisposed)
            {
                _textBox.TextChanged -= OnTextChanged;
            }
        }

        private struct TextToken
        {
            public TokenType Type { get; }
            public int Start { get; }
            public int Length { get; }

            public TextToken(TokenType type, int start, int length)
            {
                Type = type;
                Start = start;
                Length = length;
            }
        }

        private static class NativeMethods
        {
            public const int WM_SETREDRAW = 0x000B;

            [DllImport("user32.dll", CharSet = CharSet.Auto)]
            public static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, IntPtr lParam);
        }
    }
}