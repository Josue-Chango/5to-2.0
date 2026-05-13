using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace shappes_2d
{
    internal class CLine
    {
        private List<Point> points;

        private bool isDragging = false;
        private int selectedPointIndex = -1;

        private const int radio = 6;

        public CLine()
        {
            points = new List<Point>();
        }

        public void InitializeData(PictureBox picCanvas)
        {
            points.Clear();
            isDragging = false;
            selectedPointIndex = -1;

            picCanvas.Invalidate();
        }

        public void AddPoint(MouseEventArgs e, PictureBox picCanvas)
        {
            if (e.Button == MouseButtons.Left)
            {
                int index = GetPointAtPosition(e.Location);

                if (index >= 0)
                {
                    selectedPointIndex = index;
                    isDragging = true;
                }
                else
                {
                    points.Add(e.Location);
                    picCanvas.Invalidate();
                }
            }
        }

        public void MovePoint(MouseEventArgs e, PictureBox picCanvas)
        {
            if (isDragging && selectedPointIndex >= 0)
            {
                points[selectedPointIndex] = e.Location;
                picCanvas.Invalidate();
            }
        }

        public void StopDragging()
        {
            isDragging = false;
            selectedPointIndex = -1;
        }

        private int GetPointAtPosition(Point mousePos)
        {
            for (int i = 0; i < points.Count; i++)
            {
                Rectangle area = new Rectangle(
                    points[i].X - radio,
                    points[i].Y - radio,
                    radio * 2,
                    radio * 2
                );

                if (area.Contains(mousePos))
                    return i;
            }

            return -1;
        }

        public void Draw(Graphics g)
        {
            using (Pen linePen = new Pen(Color.Green, 2))
            using (Brush pointBrush = new SolidBrush(Color.Red))
            {
                for (int i = 0; i < points.Count - 1; i++)
                {
                    g.DrawLine(linePen, points[i], points[i + 1]);
                }

                foreach (var p in points)
                {
                    g.FillEllipse(pointBrush, p.X - 4, p.Y - 4, 8, 8);
                }
            }
        }
    }
}