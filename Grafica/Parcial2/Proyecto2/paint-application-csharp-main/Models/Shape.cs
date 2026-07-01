using System;
using System.Drawing;

namespace PaintApplicationAssignment.Models
{
    [Serializable]
    public abstract class Shape
    {
        public Color PenColor { get; set; }
        public float PenWidth { get; set; }

        public abstract void Draw(Graphics g);
        public abstract Point GetCenter();
        public abstract Rectangle GetBounds();
        public abstract void Translate(int dx, int dy);
        public abstract void Rotate(float angleDegrees);
        public abstract void Scale(float factor);
        public abstract bool ContainsPoint(Point p);

        protected static Point RotatePoint(Point point, Point center, float angleDegrees)
        {
            double angleRad = angleDegrees * Math.PI / 180.0;
            double cos = Math.Cos(angleRad);
            double sin = Math.Sin(angleRad);
            int x = (int)(cos * (point.X - center.X) - sin * (point.Y - center.Y) + center.X);
            int y = (int)(sin * (point.X - center.X) + cos * (point.Y - center.Y) + center.Y);
            return new Point(x, y);
        }

        protected static Point ScalePoint(Point point, Point center, float factor)
        {
            int x = (int)((point.X - center.X) * factor + center.X);
            int y = (int)((point.Y - center.Y) * factor + center.Y);
            return new Point(x, y);
        }
    }
}
