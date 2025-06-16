using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Pixel_Wall_E.Properties;

namespace PixelWallE
{
    public class Canvas : Control
    {
        private Color[,] pixels;
        private int size;
        private Color gridColor = Color.LightGray;
        private Point wallEPosition = new Point(-1, -1);

        public int Size => size;

        public Canvas(int size)
        {
            this.size = size;
            pixels = new Color[size, size];
            Clear();
            this.DoubleBuffered = true;
        }

        public void Clear()
        {
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    pixels[x, y] = Color.White;
                }
            }
            wallEPosition = new Point(-1, -1); // Resetear la posición de Wall-E
            this.Invalidate();
        }

        public void SetPixel(int x, int y, Color color)
        {
            if (x >= 0 && x < size && y >= 0 && y < size)
            {
                pixels[x, y] = color;
            }
        }

        public Color GetPixel(int x, int y)
        {
            if (x >= 0 && x < size && y >= 0 && y < size)
            {
                return pixels[x, y];
            }
            return Color.Transparent;
        }

        public void SetWallEPosition(int x, int y)
        {
            if (x >= 0 && x < size && y >= 0 && y < size)
            {
                wallEPosition = new Point(x, y);
            }
            else
            {
                wallEPosition = new Point(-1, -1);
            }
            this.Invalidate();
        }

        public int GetColorCount(string colorName, int x1, int y1, int x2, int y2)
        {
            Color targetColor = ColorFromName(colorName);
            if (targetColor == Color.Transparent) return 0;

            // Asegurarse de que las coordenadas estén dentro del canvas
            x1 = Math.Max(0, Math.Min(size - 1, x1));
            y1 = Math.Max(0, Math.Min(size - 1, y1));
            x2 = Math.Max(0, Math.Min(size - 1, x2));
            y2 = Math.Max(0, Math.Min(size - 1, y2));

            int minX = Math.Min(x1, x2);
            int maxX = Math.Max(x1, x2);
            int minY = Math.Min(y1, y2);
            int maxY = Math.Max(y1, y2);

            int count = 0;
            for (int x = minX; x <= maxX; x++)
            {
                for (int y = minY; y <= maxY; y++)
                {
                    if (pixels[x, y] == targetColor)
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;

            float cellWidth = (float)ClientSize.Width / size;
            float cellHeight = (float)ClientSize.Height / size;

            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    using (Brush brush = new SolidBrush(pixels[x, y]))
                    {
                        g.FillRectangle(brush, x * cellWidth, y * cellHeight, cellWidth, cellHeight);
                    }
                }
            }

            using (Pen pen = new Pen(gridColor))
            {
                for (int i = 0; i <= size; i++)
                {
                    // Líneas verticales
                    g.DrawLine(pen, i * cellWidth, 0, i * cellWidth, size * cellHeight);
                    // Líneas horizontales
                    g.DrawLine(pen, 0, i * cellHeight, size * cellWidth, i * cellHeight);
                }
            }

            if (wallEPosition.X >= 0 && wallEPosition.Y >= 0)
            {
                float xPos = wallEPosition.X * cellWidth;
                float yPos = wallEPosition.Y * cellHeight;

                Image wallEImage = Image.FromFile(@"D:\Proyecto 2\Pixel Wall-E\Pixel Wall-E\Resources\WallE.jpg");
                
                float scale = 0.8f; 
                float imageWidth = cellWidth * scale;
                float imageHeight = cellHeight * scale;
                
                float offsetX = (cellWidth - imageWidth) / 2;
                float offsetY = (cellHeight - imageHeight) / 2;

                g.DrawImage(wallEImage, xPos + offsetX, yPos + offsetY, imageWidth, imageHeight);
            }
        }

        public static Color ColorFromName(string colorName)
        {
            switch (colorName.ToLower())
            {
                case "red": return Color.Red;
                case "blue": return Color.Blue;
                case "green": return Color.Green;
                case "yellow": return Color.Yellow;
                case "orange": return Color.Orange;
                case "purple": return Color.Purple;
                case "black": return Color.Black;
                case "white": return Color.White;
                case "transparent": return Color.Transparent;
                default: return Color.Transparent;
            }
        }
    }
}