using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

            if (texto.Contains(" "))
                return false;

            // validar según el tipo
            if (typeof(T) == typeof(int))
            {
                int num;
                // Allowing negative numbers and 0 for coordinate positions
                if (!int.TryParse(texto, out num))
                    return false;
            }
            else if (typeof(T) == typeof(float))
            {
                float num;
                // Allowing negative numbers and 0 for coordinate positions
                if (!float.TryParse(texto, out num))
                    return false;
            }
            else if (typeof(T) == typeof(double))
            {
                double num;
                if (!double.TryParse(texto, out num) || num <= 0)
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

        public static bool ValidarTriangulo(float ladoA, float ladoB, float ladoC)
        {
            if (ladoA + ladoB <= ladoC || ladoA + ladoC <= ladoB || ladoB + ladoC <= ladoA)
                return false;
            return true;
        }

        public static int ValidarEntrada(string entrada)
        {
            if (Validar<int>(entrada))
            {
                return int.Parse(entrada);
            }
            else
            {
                MessageBox.Show("Ingresa números enteros válidos para las coordenadas.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return 0;
            }
        }
    }
}
