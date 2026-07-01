using System;
using System.Drawing;

namespace PaintApplicationAssignment.Models
{
    [Serializable]
    public class RectangleShape : Shape
    {
        public Point Location { get; set; }
        public Size Size { get; set; }

        public RectangleShape() { }

        public RectangleShape(Point location, Size size, Color color, float width)
        {
            Location = location;
            Size = size;
            PenColor = color;
            PenWidth = width;
        }

        public override void Draw(Graphics g)
        {
            using (Pen pen = new Pen(PenColor, PenWidth))
                g.DrawRectangle(pen, Location.X, Location.Y, Size.Width, Size.Height);
        }

        public override Point GetCenter()
        {
            return new Point(Location.X + Size.Width / 2, Location.Y + Size.Height / 2);
        }

        public override Rectangle GetBounds()
        {
            return new Rectangle(Location, Size);
        }

        public override void Translate(int dx, int dy)
        {
            Location = new Point(Location.X + dx, Location.Y + dy);
        }

        public override void Rotate(float angleDegrees)
        {
            Point center = GetCenter();
            Point topLeft = Location;
            Point topRight = new Point(Location.X + Size.Width, Location.Y);
            Point bottomRight = new Point(Location.X + Size.Width, Location.Y + Size.Height);
            Point bottomLeft = new Point(Location.X, Location.Y + Size.Height);

            Point[] corners = new Point[] { topLeft, topRight, bottomRight, bottomLeft };
            for (int i = 0; i < 4; i++)
                corners[i] = RotatePoint(corners[i], center, angleDegrees);

            int minX = Math.Min(corners[0].X, Math.Min(corners[1].X, Math.Min(corners[2].X, corners[3].X)));
            int minY = Math.Min(corners[0].Y, Math.Min(corners[1].Y, Math.Min(corners[2].Y, corners[3].Y)));
            int maxX = Math.Max(corners[0].X, Math.Max(corners[1].X, Math.Max(corners[2].X, corners[3].X)));
            int maxY = Math.Max(corners[0].Y, Math.Max(corners[1].Y, Math.Max(corners[2].Y, corners[3].Y)));

            Location = new Point(minX, minY);
            Size = new Size(maxX - minX, maxY - minY);
        }

        public override void Scale(float factor)
        {
            Point center = GetCenter();
            int newWidth = (int)(Size.Width * factor);
            int newHeight = (int)(Size.Height * factor);
            Location = new Point(center.X - newWidth / 2, center.Y - newHeight / 2);
            Size = new Size(newWidth, newHeight);
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
