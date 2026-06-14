using System;
using System.Collections.Generic;
using System.Drawing;

namespace OperacionDDA
{
    internal class CurvaBezier
    {
        public List<Point> puntosBezier = new List<Point>();

        public void Clear()
        {
            puntosBezier.Clear();
        }

        public void GenerarBezier2P(Point p0, Point p1, int paso = 1000)
        {
            puntosBezier.Clear();
            for (int i = 0; i <= paso; i++)
            {
                double t = (double)i / paso;
                double x = (1 - t) * p0.X + t * p1.X;
                double y = (1 - t) * p0.Y + t * p1.Y;
                puntosBezier.Add(new Point((int)Math.Round(x), (int)Math.Round(y)));
            }
        }

        public void GenerarBezier3P(Point p0, Point p1, Point p2, int paso = 1000)
        {
            puntosBezier.Clear();
            for (int i = 0; i <= paso; i++)
            {
                double t = (double)i / paso;
                double x = (1 - t) * (1 - t) * p0.X + 2 * (1 - t) * t * p1.X + t * t * p2.X;
                double y = (1 - t) * (1 - t) * p0.Y + 2 * (1 - t) * t * p1.Y + t * t * p2.Y;
                puntosBezier.Add(new Point((int)Math.Round(x), (int)Math.Round(y)));
            }
        }

        public void Dibujar(Graphics g, Color color, int centroX, int centroY)
        {
            using (Brush brush = new SolidBrush(color))
            {
                foreach (var p in puntosBezier)
                {
                    g.FillRectangle(brush, p.X + centroX, centroY - p.Y, 2, 2);
                }
            }
        }

        public void DibujarPuntoControl(Graphics g, Point p, Color color, int centroX, int centroY)
        {
            using (Brush brush = new SolidBrush(color))
            {
                g.FillEllipse(brush, p.X + centroX - 4, centroY - p.Y - 4, 8, 8);
            }
        }
    }
}
