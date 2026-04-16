using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace shappes_2d
{
    /*internal class Calculos
    {
    }*/
    public class Calculos
    {
        public Calculos() { }
        public float CalcularAreaRectangulo(float weight, float height)
        {
            return weight * height;
        }
        public float CalcularPerimetroRectangulo(float weight, float height)
        {
            return 2 * (weight + height);
        }

        public float CalcularPerimetroTriangulo(float ladoA, float ladoB, float ladoC)
        {
            return ladoA + ladoB + ladoC;
        }

        public float CalcularAreaTriangulo(float ladoA, float ladoB, float ladoC)
        {
            float suma = ladoA + ladoB + ladoC;
            return (float)(Math.Sqrt(suma * (suma - ladoA) * (suma - ladoB) * (suma - ladoC)));
        }
    }
}
