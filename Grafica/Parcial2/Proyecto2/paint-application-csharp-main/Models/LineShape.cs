using System;
using System.Drawing;

namespace PaintApplicationAssignment.Models
{
    [Serializable]
    public class LineShape : Shape
    {
        public Point Start { get; set; }
        public Point End { get; set; }

        public LineShape() { }

        public LineShape(Point start, Point end, Color color, float width)
        {
            Start = start;
            End = end;
            PenColor = color;
            PenWidth = width;
        }

        public override void Draw(Graphics g)
        {
            using (Pen pen = new Pen(PenColor, PenWidth))
                g.DrawLine(pen, Start, End);
        }

        public override Point GetCenter()
        {
            return new Point((Start.X + End.X) / 2, (Start.Y + End.Y) / 2);
        }

        public override Rectangle GetBounds()
        {
            int x = Math.Min(Start.X, End.X);
            int y = Math.Min(Start.Y, End.Y);
            int w = Math.Abs(Start.X - End.X);
            int h = Math.Abs(Start.Y - End.Y);
            if (w == 0) w = 1;
            if (h == 0) h = 1;
            return new Rectangle(x, y, w, h);
        }

        public override void Translate(int dx, int dy)
        {
            Start = new Point(Start.X + dx, Start.Y + dy);
            End = new Point(End.X + dx, End.Y + dy);
        }

        public override void Rotate(float angleDegrees)
        {
            Point center = GetCenter();
            Start = RotatePoint(Start, center, angleDegrees);
            End = RotatePoint(End, center, angleDegrees);
        }

        public override void Scale(float factor)
        {
            Point center = GetCenter();
            Start = ScalePoint(Start, center, factor);
            End = ScalePoint(End, center, factor);
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
