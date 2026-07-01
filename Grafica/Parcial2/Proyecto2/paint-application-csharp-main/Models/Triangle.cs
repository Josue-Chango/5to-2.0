using System;
using System.Drawing;

namespace PaintApplicationAssignment.Models
{
    [Serializable]
    public class TriangleShape : Shape
    {
        public Point Point1 { get; set; }
        public Point Point2 { get; set; }
        public Point Point3 { get; set; }

        public TriangleShape() { }

        public TriangleShape(Point p1, Point p2, Point p3, Color color, float width)
        {
            Point1 = p1;
            Point2 = p2;
            Point3 = p3;
            PenColor = color;
            PenWidth = width;
        }

        public override void Draw(Graphics g)
        {
            using (Pen pen = new Pen(PenColor, PenWidth))
                g.DrawPolygon(pen, new[] { Point1, Point2, Point3 });
        }

        public override Point GetCenter()
        {
            return new Point(
                (Point1.X + Point2.X + Point3.X) / 3,
                (Point1.Y + Point2.Y + Point3.Y) / 3);
        }

        public override Rectangle GetBounds()
        {
            int minX = Math.Min(Point1.X, Math.Min(Point2.X, Point3.X));
            int minY = Math.Min(Point1.Y, Math.Min(Point2.Y, Point3.Y));
            int maxX = Math.Max(Point1.X, Math.Max(Point2.X, Point3.X));
            int maxY = Math.Max(Point1.Y, Math.Max(Point2.Y, Point3.Y));
            return new Rectangle(minX, minY, Math.Max(1, maxX - minX), Math.Max(1, maxY - minY));
        }

        public override void Translate(int dx, int dy)
        {
            Point1 = new Point(Point1.X + dx, Point1.Y + dy);
            Point2 = new Point(Point2.X + dx, Point2.Y + dy);
            Point3 = new Point(Point3.X + dx, Point3.Y + dy);
        }

        public override void Rotate(float angleDegrees)
        {
            Point center = GetCenter();
            Point1 = RotatePoint(Point1, center, angleDegrees);
            Point2 = RotatePoint(Point2, center, angleDegrees);
            Point3 = RotatePoint(Point3, center, angleDegrees);
        }

        public override void Scale(float factor)
        {
            Point center = GetCenter();
            Point1 = ScalePoint(Point1, center, factor);
            Point2 = ScalePoint(Point2, center, factor);
            Point3 = ScalePoint(Point3, center, factor);
        }

        public override bool ContainsPoint(Point p)
        {
            int margin = 5;
            Rectangle bounds = GetBounds();
            bounds.Inflate(margin, margin);
            return bounds.Contains(p);
        }
    }
}
