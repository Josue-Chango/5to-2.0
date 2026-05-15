using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenChangoJosue
{
    internal class Decagono
    {
        public void GraficarDecagono(Graphics g, int x, int y, float radio, bool rotado)
        {
            Pen lapiz = new Pen(Color.Black, 2);

            int lados = 10;
            PointF[] puntos = new PointF[lados];

            float cx = x + radio;
            float cy = y + radio;

            double offsetAngulo = rotado ? (Math.PI / lados) : 0;

            for (int i = 0; i < lados; i++)
            {
                double angulo = (2 * Math.PI * i / lados) - (Math.PI / 2) + offsetAngulo;
                puntos[i] = new PointF(
                    cx + radio * (float)Math.Cos(angulo),
                    cy + radio * (float)Math.Sin(angulo)
                );
            }
            //g.FillPolygon(Brushes.Purple, puntos);
            g.DrawPolygon(lapiz, puntos);
            lapiz.Dispose();
        }

        public void GraficarEstrella(Graphics g, int x, int y, float radio, int verticeInicial, Color color, bool rotado)
        {
            int indiceInicio = verticeInicial - 1;

            Pen lapiz = new Pen(Color.Blue, 2);
            SolidBrush relleno = new SolidBrush(color);
            int lados = 10;
            PointF[] puntos = new PointF[lados];

            float cx = x + radio;
            float cy = y + radio;
            double offsetAngulo = rotado ? (Math.PI / lados) : 0;
            for (int i = 0; i < lados; i++)
            {
                double angulo = (2 * Math.PI * (indiceInicio + i * 3) / lados) - (Math.PI / 2) + offsetAngulo;
                puntos[i] = new PointF(
                    cx + radio * (float)Math.Cos(angulo),
                    cy + radio * (float)Math.Sin(angulo)
                );
            }

            //g.FillPolygon(relleno, puntos, System.Drawing.Drawing2D.FillMode.Winding);
            g.DrawPolygon(lapiz, puntos);

            lapiz.Dispose();
        }

        
    }
}
