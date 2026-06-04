using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperacionDDA
{
    internal class FloodFillStepper
    {
        private Bitmap _bitmap;
        private Stack<Point> _stack;
        private Color _targetColor;
        private Color _fillColor;

        public bool IsFinished => _stack == null || _stack.Count == 0;

        public FloodFillStepper(Bitmap bitmap)
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

        public void ProcessStep(int pixelsPerStep = 50)
        {
            if (IsFinished) return;

            int processed = 0;

            while (_stack.Count > 0 && processed < pixelsPerStep)
            {
                Point p = _stack.Pop();

                if (p.X < 0 || p.X >= _bitmap.Width || p.Y < 0 || p.Y >= _bitmap.Height)
                    continue;

                if (_bitmap.GetPixel(p.X, p.Y).ToArgb() == _targetColor.ToArgb())
                {
                    _bitmap.SetPixel(p.X, p.Y, _fillColor);
                    _stack.Push(new Point(p.X - 1, p.Y));
                    _stack.Push(new Point(p.X, p.Y + 1));
                    _stack.Push(new Point(p.X + 1, p.Y));
                    _stack.Push(new Point(p.X, p.Y - 1));

                    processed++;
                }
            }
        }
    }
}
