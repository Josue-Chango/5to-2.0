using System;
using System.Collections.Generic;
using System.Linq;
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
        public float CalcularArea(float weight, float height)
        {
            return weight * height;
        }
        public float CalcularPerimetro(float weight, float height)
        {
            return 2 * (weight + height);
        }
    }
}
