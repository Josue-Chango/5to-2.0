using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamenChangoJosue
{
    
    internal class FlorMargarita
    {
        private int petalos;

        public FlorMargarita(int petalos = 5)
        {
            this.petalos = petalos;
        }

        public void GraficarPentagono(
            Graphics g,
            int x,
            int y,
            float radio,
            int aux,
            Color color,
            bool rotado,
            int nivel)
        {
            if (nivel <= 0 || radio < 5)
                return;

            int indiceInicio = aux - 1;

            Pen lapiz = new Pen(Color.Black, 2);
            SolidBrush relleno = new SolidBrush(color);

            int lados = 10;
            PointF[] puntos = new PointF[5];

            float cx = x + radio;
            float cy = y + radio;

            double offsetAngulo = rotado ? (Math.PI / lados) : 0;

            for (int i = 0; i < 5; i++)
            {
                double angulo =
                    (2 * Math.PI * (indiceInicio + i * 2) / lados)
                    - (Math.PI / 2)
                    + offsetAngulo;

                puntos[i] = new PointF(
                    cx + radio * (float)Math.Cos(angulo),
                    cy + radio * (float)Math.Sin(angulo)
                );
            }

            g.DrawPolygon(lapiz, puntos);

            g.FillPolygon(relleno, puntos);

            foreach (PointF p in puntos)
            {
                GraficarPentagono(
                    g,
                    (int)(p.X - radio / 2),
                    (int)(p.Y - radio / 2),
                    radio / 2,
                    aux,
                    Color.Red,
                    rotado,
                    nivel - 1
                );
            }

            lapiz.Dispose();
            relleno.Dispose();
        }
    }

}
