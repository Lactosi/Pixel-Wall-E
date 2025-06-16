using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace PixelWallE
{
    public class WallE
    {
        private Canvas canvas;
        private int x;
        private int y;
        private Color currentColor = Color.Transparent;
        private int brushSize = 1;

        public WallE(Canvas canvas)
        {
            this.canvas = canvas;
        }

        public void Spawn(int x, int y)
        {
            if (x < 0 || x >= canvas.Size || y < 0 || y >= canvas.Size)
                throw new Exception("Posición inicial de Wall-E fuera de los límites del canvas");

            this.x = x;
            this.y = y;
            canvas.SetWallEPosition(x, y);
        }

        public void SetColor(string colorName)
        {
            currentColor = Canvas.ColorFromName(colorName);
        }

        public void SetSize(int size)
        {
            if (size <= 0)
                throw new Exception("El tamaño del pincel debe ser positivo");

            brushSize = size % 2 == 0 ? size - 1 : size;
        }

        public void DrawLine(int dirX, int dirY, int distance)
        {
            if (distance <= 0)
                throw new Exception("La distancia debe ser positiva");

            if (Math.Abs(dirX) > 1 || Math.Abs(dirY) > 1)
                throw new Exception("Las direcciones deben estar entre -1 y 1");

            int endX = x + dirX * distance;
            int endY = y + dirY * distance;

            // Algoritmo de Bresenham para dibujar líneas
            int dx = Math.Abs(endX - x);
            int dy = Math.Abs(endY - y);
            int sx = x < endX ? 1 : -1;
            int sy = y < endY ? 1 : -1;
            int err = dx - dy;

            while (true)
            {
                DrawWithBrush(x, y);

                if (x == endX && y == endY)
                    break;

                int e2 = 2 * err;
                if (e2 > -dy)
                {
                    err -= dy;
                    x += sx;
                }
                if (e2 < dx)
                {
                    err += dx;
                    y += sy;
                }
            }
            canvas.SetWallEPosition(x, y);
        }

        public void DrawCircle(int dirX, int dirY, int radius)
        {
            if (radius <= 0)
                throw new Exception("El radio debe ser positivo");

            if (Math.Abs(dirX) > 1 || Math.Abs(dirY) > 1)
                throw new Exception("Las direcciones deben estar entre -1 y 1");

            int centerX = x + dirX * radius;
            int centerY = y + dirY * radius;

            int d = 3 - 2 * radius;
            int currentX = 0;
            int currentY = radius;

            while (currentX <= currentY)
            {
                DrawCirclePoints(centerX, centerY, currentX, currentY);
                currentX++;

                if (d > 0)
                {
                    currentY--;
                    d = d + 4 * (currentX - currentY) + 10;
                }
                else
                {
                    d = d + 4 * currentX + 6;
                }
            }

            x = centerX;
            y = centerY;
            canvas.SetWallEPosition(x, y);
        }

        private void DrawCirclePoints(int centerX, int centerY, int x, int y)
        {
            DrawWithBrush(centerX + x, centerY + y);
            DrawWithBrush(centerX - x, centerY + y);
            DrawWithBrush(centerX + x, centerY - y);
            DrawWithBrush(centerX - x, centerY - y);
            DrawWithBrush(centerX + y, centerY + x);
            DrawWithBrush(centerX - y, centerY + x);
            DrawWithBrush(centerX + y, centerY - x);
            DrawWithBrush(centerX - y, centerY - x);
        }

        public void DrawRectangle(int dirX, int dirY, int distance, int width, int height)
        {
            if (width <= 0 || height <= 0)
                throw new Exception("El ancho y alto del rectángulo deben ser positivos");

            if (Math.Abs(dirX) > 1 || Math.Abs(dirY) > 1)
                throw new Exception("Las direcciones deben estar entre -1 y 1");

            int startX = x + dirX * distance;
            int startY = y + dirY * distance;

            int endX = startX + width - 1;
            int endY = startY + height - 1;

            startX = Math.Max(0, Math.Min(canvas.Size - 1, startX));
            startY = Math.Max(0, Math.Min(canvas.Size - 1, startY));
            endX = Math.Max(0, Math.Min(canvas.Size - 1, endX));
            endY = Math.Max(0, Math.Min(canvas.Size - 1, endY));

            for (int px = startX; px <= endX; px++)
            {
                DrawWithBrush(px, startY);
                DrawWithBrush(px, endY); 
            }

            for (int py = startY + 1; py < endY; py++)
            {
                DrawWithBrush(startX, py);
                DrawWithBrush(endX, py); 
            }

            x = startX + width / 2;
            y = startY + height / 2;
            canvas.SetWallEPosition(x, y);
        }

        public void Fill()
        {
            if (currentColor == Color.Transparent)
                return;

            Color targetColor = canvas.GetPixel(x, y);
            if (targetColor == currentColor)
                return;

            FloodFill(x, y, targetColor);
            canvas.SetWallEPosition(x, y);
        }

        private void FloodFill(int startX, int startY, Color targetColor)
        {
            if (startX < 0 || startX >= canvas.Size || startY < 0 || startY >= canvas.Size)
                return;

            if (canvas.GetPixel(startX, startY) != targetColor)
                return;

            Queue<Point> queue = new Queue<Point>();
            queue.Enqueue(new Point(startX, startY));

            while (queue.Count > 0)
            {
                Point p = queue.Dequeue();
                if (p.X < 0 || p.X >= canvas.Size || p.Y < 0 || p.Y >= canvas.Size)
                    continue;

                if (canvas.GetPixel(p.X, p.Y) == targetColor)
                {
                    DrawWithBrush(p.X, p.Y);

                    queue.Enqueue(new Point(p.X + 1, p.Y));
                    queue.Enqueue(new Point(p.X - 1, p.Y));
                    queue.Enqueue(new Point(p.X, p.Y + 1));
                    queue.Enqueue(new Point(p.X, p.Y - 1));
                }
            }
        }

        private void DrawWithBrush(int centerX, int centerY)
        {
            if (currentColor == Color.Transparent)
                return;

            int halfSize = brushSize / 2;
            for (int dx = -halfSize; dx <= halfSize; dx++)
            {
                for (int dy = -halfSize; dy <= halfSize; dy++)
                {
                    int px = centerX + dx;
                    int py = centerY + dy;
                    canvas.SetPixel(px, py, currentColor);
                }
            }
        }

        public int GetActualX() => x;
        public int GetActualY() => y;

        public bool IsBrushColor(string colorName)
        {
            return currentColor == Canvas.ColorFromName(colorName);
        }

        public bool IsBrushSize(int size)
        {
            return brushSize == size;
        }

        public bool IsCanvasColor(string colorName, int vertical, int horizontal)
        {
            int checkX = x + horizontal;
            int checkY = y + vertical;

            if (checkX < 0 || checkX >= canvas.Size || checkY < 0 || checkY >= canvas.Size)
                return false;

            Color targetColor = Canvas.ColorFromName(colorName);
            return canvas.GetPixel(checkX, checkY) == targetColor;
        }
        public int BoolToInt(bool value)
        {
            return value ? 1 : 0;
        }
    }
    }