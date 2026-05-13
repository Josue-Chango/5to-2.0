using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leccion
{
    internal class Punto2D
    {
        public float X { get; set; }
        public float Y { get; set; }

        public Punto2D(float x, float y)
        {
            X = x;
            Y = y;
        }

        public double Distancia(Punto2D otro)
        {
            double dx = otro.X - X;
            double dy = otro.Y - Y;

            return Math.Sqrt(dx * dx + dy * dy);
        }
    }
}
