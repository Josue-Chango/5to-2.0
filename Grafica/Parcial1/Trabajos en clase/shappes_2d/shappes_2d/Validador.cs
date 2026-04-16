using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shappes_2d
{
    /*internal class Validador
    {
    }*/
    public static class Validador
    {
        public static bool Validar<T>(string texto)
        {
            // validar vacío o espacios
            if (string.IsNullOrWhiteSpace(texto))
                return false;

            // validar que no tenga espacios internos
            if (texto.Contains(" "))
                return false;

            // validar según el tipo
            if (typeof(T) == typeof(int))
            {
                int num;
                if (!int.TryParse(texto, out num) || num < 0)
                    return false;
            }
            else if (typeof(T) == typeof(float))
            {
                float num;
                if (!float.TryParse(texto, out num) || num < 0)
                    return false;
            }
            else if (typeof(T) == typeof(double))
            {
                double num;
                if (!double.TryParse(texto, out num) || num < 0)
                    return false;
            }
            else if (typeof(T) == typeof(string))
            {
                return true;
            }
            else
            {
                return false;
            }

            return true;
        }
    }
}
