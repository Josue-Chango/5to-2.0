using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Chango_Josue_Ex2
{
    internal class Clase_Lineas
    {
        public Clase_Lineas() { }

        public void Dibujar_Cuadrado(Graphics g, int alto, int ancho)
        {
            Pen lapiz1 = new Pen(Color.Black, 2);
            Pen lapiz2 = new Pen(Color.Black, 1);
            int x = ancho/4, y = alto/4, lado = 200;
            g.DrawLine(lapiz1, x, y, x + lado, y);
            g.DrawLine(lapiz1, x + lado, y, x + lado, y + lado);
            g.DrawLine(lapiz1, x + lado, y + lado, x, y + lado);
            g.DrawLine(lapiz1, x, y + lado, x, y);
            alto = alto/10; 
            ancho = ancho/10;
            int mallado = lado / 20;
            for (int i = 0; i < lado/10; i++)
            {
                g.DrawLine(lapiz2, x, y + (i*mallado), x + lado, y + (i*mallado));
                g.DrawLine(lapiz2, x + (i * mallado), y , x + (i * mallado) , y + lado);
            }
        }
    }
}
