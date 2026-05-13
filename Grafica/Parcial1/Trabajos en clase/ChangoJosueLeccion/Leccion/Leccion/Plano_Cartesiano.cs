using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leccion
{
    internal class Plano_Cartesiano
    {
        public Punto2D Punto1 { get; set; }
        public Punto2D Punto2 { get; set; }

        public int Ancho { get; set; }
        public int Alto { get; set; }

        public Plano_Cartesiano(int ancho, int alto)
        {
            Ancho = ancho;
            Alto = alto;
        }

        //centrar

        public int CentroX()
        {
            return Ancho / 2;
        }

        public int CentroY()
        {
            return Alto / 2;
        }

        //cuadricula
        public void DibujarPlano(Graphics g)
        {
            Pen cuadricula = new Pen(Color.LightGray, 1);

            for (int x = 0; x < Ancho; x += 20)
            {
                g.DrawLine(cuadricula, x, 0, x, Alto);
            }

            for (int y = 0; y < Alto; y += 20)
            {
                g.DrawLine(cuadricula, 0, y, Ancho, y);
            }

           
            g.DrawLine(
                new Pen(Color.Red, 2),
                0,
                CentroY(),
                Ancho,
                CentroY()
            );

            
            g.DrawLine(
                new Pen(Color.Blue, 2),
                CentroX(),
                0,
                CentroX(),
                Alto
            );
        }

       

        public void DibujarPuntos(Graphics g, Font font)
        {
            if (Punto1 != null)
            {
                float x1 = CentroX() + Punto1.X;
                float y1 = CentroY() - Punto1.Y;

                g.FillEllipse(
                    Brushes.Green,
                    x1 - 5,
                    y1 - 5,
                    10,
                    10
                );

                g.DrawString(
                    $"P1 ({Punto1.X}, {Punto1.Y})",
                    font,
                    Brushes.Black,
                    x1 + 10,
                    y1
                );
            }

            if (Punto2 != null)
            {
                float x2 = CentroX() + Punto2.X;
                float y2 = CentroY() - Punto2.Y;

                g.FillEllipse(
                    Brushes.Orange,
                    x2 - 5,
                    y2 - 5,
                    10,
                    10
                );

                g.DrawString(
                    $"P2 ({Punto2.X}, {Punto2.Y})",
                    font,
                    Brushes.Black,
                    x2 + 10,
                    y2
                );
            }
        }

        
        public void DibujarLinea(Graphics g)
        {
            if (Punto1 == null || Punto2 == null)
                return;

            float x1 = CentroX() + Punto1.X;
            float y1 = CentroY() - Punto1.Y;

            float x2 = CentroX() + Punto2.X;
            float y2 = CentroY() - Punto2.Y;

            g.DrawLine(
                new Pen(Color.Black, 2),
                x1,
                y1,
                x2,
                y2
            );
        }

        
        public float DeltaX()
        {
            if (Punto1 == null || Punto2 == null)
                return 0;

            return Punto2.X - Punto1.X;
        }

        
        public float DeltaY()
        {
            if (Punto1 == null || Punto2 == null)
                return 0;

            return Punto2.Y - Punto1.Y;
        }

       
        public double Distancia()
        {
            if (Punto1 == null || Punto2 == null)
                return 0;

            return Punto1.Distancia(Punto2);
        }

        

        public void DibujarResultados(Graphics g, Font font)
        {
            if (Punto1 == null || Punto2 == null)
                return;

            g.DrawString(
                $"Ax = {DeltaX()}   Ay = {DeltaY()}   Distancia = {Distancia():F2}",
                font,
                Brushes.DarkRed,
                20,
                20
            );
        }

        //
        public Punto2D ConvertirCoordenada(float mouseX, float mouseY)
        {
            float x = mouseX - CentroX();
            float y = CentroY() - mouseY;

            return new Punto2D(x, y);
        }
    }
}

