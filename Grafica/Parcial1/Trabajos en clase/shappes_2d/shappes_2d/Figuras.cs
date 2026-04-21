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
                float redim_weight = weight * 4;
                float redim_height = height * 4;
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
                //g.DrawPolygon(Pens.Red, points);
                g.FillPolygon(Brushes.LightBlue, points);
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
                //g.DrawPolygon(Pens.Red, puntos);
                g.FillPolygon(Brushes.LightBlue, puntos);
            }
        }

        public void DibujarHexagono(Graphics g, float lado)
        {
            float a = lado;
            if (lado < 10)
            {
                float escala = 4;
                a = lado * escala;
                PointF centro = new PointF(350, 100);
                PointF[] puntos = new PointF[6];
                for (int i = 0; i < 6; i++)
                {
                    float angle = (float)(i * Math.PI / 3);
                    puntos[i] = new PointF(
                        centro.X + a * (float)Math.Cos(angle),
                        centro.Y + a * (float)Math.Sin(angle)
                    );
                }
                //g.DrawPolygon(Pens.Red, puntos);
                //g.FillPolygon(Brushes.LightBlue, puntos);
                Brush[] brushes = new Brush[]
                {
                    Brushes.Red,
                    Brushes.Green,
                    Brushes.Blue,
                    Brushes.Yellow,
                    Brushes.Orange,
                    Brushes.Purple
                };

                for (int i = 0; i < 6; i++)
                {
                    PointF[] triangulo = new PointF[3];
                    triangulo[0] = centro; 
                    triangulo[1] = puntos[i];
                    triangulo[2] = puntos[(i + 1) % 6];

                    g.FillPolygon(brushes[i % brushes.Length], triangulo);
                }
            }
            else
            {
                PointF centro = new PointF(350, 100);
                PointF[] puntos = new PointF[6];
                for (int i = 0; i < 6; i++)
                {
                    float angle = (float)(i * Math.PI / 3);
                    puntos[i] = new PointF(
                        centro.X + a * (float)Math.Cos(angle),
                        centro.Y + a * (float)Math.Sin(angle)
                    );
                }
                //g.DrawPolygon(Pens.Red, puntos);
                //g.FillPolygon(Brushes.LightBlue, puntos);

                Brush[] brushes = new Brush[]
                {
                    Brushes.Red,
                    Brushes.Green,
                    Brushes.Blue,
                    Brushes.Yellow,
                    Brushes.Orange,
                    Brushes.Purple
                };

                for (int i = 0; i < 6; i++)
                {
                    PointF[] triangulo = new PointF[3];
                    triangulo[0] = centro;
                    triangulo[1] = puntos[i];
                    triangulo[2] = puntos[(i + 1) % 6];

                    g.FillPolygon(brushes[i % brushes.Length], triangulo);
                }


            }
        }
    }
}
