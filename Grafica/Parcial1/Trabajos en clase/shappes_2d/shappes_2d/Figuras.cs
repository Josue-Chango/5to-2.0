using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shappes_2d
{
    /*internal class Figuras
    {
    }*/
    public class Figuras
    {
        public float Weight { get; set; }
        public float Height { get; set; }
        public Figuras(float weight, float height)
        {
            Weight = weight;
            Height = height;
        }
        public Figuras()
        {
        }

        public void DibujarRectangulo(Graphics g, float weight, float height)
        {
            if (weight < 6 && height < 6)
            {
                float redim_weight = weight * 10;
                float redim_height = height * 10;
                Pen pen = new Pen(Color.Blue, 2);
                g.DrawRectangle(Pens.Red, 350, 100, redim_weight, redim_height);
            }
            else
            {
                Pen pen = new Pen(Color.Blue, 2);
                g.DrawRectangle(Pens.Red, 350, 100, weight, height);
            }
        }

        public void DibujarTriangulo(Graphics g, float ladoA, float ladoB, float ladoC)
        {
            float a;
            float b;
            float c;
            if (ladoA < 10 && ladoB < 10 && ladoC < 10)
            {
                float escala = 10;
                a = ladoA * escala;
                b = ladoB * escala;
                c = ladoC * escala;
                PointF punto1 = new PointF(350, 100);
                PointF punto2 = new PointF(350 + a, 100);
                float cx = (a * a + b * b - c * c) / (2 * a);
                float cy = (float)Math.Sqrt(Math.Max(0, b * b - cx * cx));
                PointF punto3 = new PointF(350 + cx, 100 - cy);
                PointF[] points = { punto1, punto2, punto3 };
                g.DrawPolygon(Pens.Red, points);
            }
            else
            {
                a = ladoA;
                b = ladoB;
                c = ladoC;
                PointF punto1 = new PointF(350, 100);
                PointF punto2 = new PointF(350 + a, 100);
                float cx = (a * a + b * b - c * c) / (2 * a);
                float cy = (float)Math.Sqrt(Math.Max(0, b * b - cx * cx));
                PointF punto3 = new PointF(350 + cx, 100 - cy);
                PointF[] puntos = { punto1, punto2, punto3 };
                g.DrawPolygon(Pens.Red, puntos);
            }
        }
    }
}
