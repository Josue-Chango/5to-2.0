using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperacionDDA
{
    internal class ScanlineFloodFillStepper
    {
        private Bitmap _bitmap;
        private Stack<Point> _stack;
        private Color _targetColor;
        private Color _fillColor;

        public bool IsFinished => _stack == null || _stack.Count == 0;

        public ScanlineFloodFillStepper(Bitmap bitmap)
        {
            _bitmap = bitmap;
        }

        public void StartAlgorithm(Point startPoint, Color fillColor)
        {
            _targetColor = _bitmap.GetPixel(startPoint.X, startPoint.Y);
            _fillColor = fillColor;

            if (_targetColor.ToArgb() == _fillColor.ToArgb()) return;

            _stack = new Stack<Point>();
            _stack.Push(startPoint);
        }

        public void ProcessStep(int linesPerStep = 5) 
        {
            if (IsFinished) return;

            int processedLines = 0;

            while (_stack.Count > 0 && processedLines < linesPerStep)
            {
                Point p = _stack.Pop();
                int x = p.X;
                int y = p.Y;

                while (x > 0 && _bitmap.GetPixel(x - 1, y).ToArgb() == _targetColor.ToArgb())
                {
                    x--;
                }
                int leftX = x;

                int rightX = p.X;
                while (rightX < _bitmap.Width - 1 && _bitmap.GetPixel(rightX + 1, y).ToArgb() == _targetColor.ToArgb())
                {
                    rightX++;
                }

                for (int i = leftX; i <= rightX; i++)
                {
                    _bitmap.SetPixel(i, y, _fillColor);
                }

                ScanLine(leftX, rightX, y - 1);

                ScanLine(leftX, rightX, y + 1);

                processedLines++;
            }
        }

        private void ScanLine(int xLeft, int xRight, int y)
        {
            if (y < 0 || y >= _bitmap.Height) return;

            bool spanAdded = false;
            for (int x = xLeft; x <= xRight; x++)
            {
                if (_bitmap.GetPixel(x, y).ToArgb() == _targetColor.ToArgb())
                {
                    if (!spanAdded)
                    {
                        _stack.Push(new Point(x, y));
                        spanAdded = true;
                    }
                }
                else
                {
                    spanAdded = false;
                }
            }
        }
    }
}
