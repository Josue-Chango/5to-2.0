using shappes_2d;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OperacionDDA
{
    internal class Circunferencia
    {

        public Circunferencia() { }
        public System.Collections.Generic.List<string> puntosLista = new System.Collections.Generic.List<string>();

        public void CircleMidPoint(int xc, int yc, int r) 
        {
            int x, y, p;
            x=0;
            y = r;
            p = 1 - r;
            PlotPoint(xc, yc, x, y);
            while (x < y)
            {
                x++;
                if (p < 0)
                {
                    p = p + 2 * x + 1;
                }
                else
                {
                    y--;
                    p = p + 2 * (x - y) + 1;
                }
                PlotPoint(xc, yc, x, y);
            }
        }

        public void PlotPoint(int xc, int yc, int x, int y)
        {
            puntosLista.Add($"({xc + x}, {yc + y})");
            puntosLista.Add($"({xc - x}, {yc + y})");
            puntosLista.Add($"({xc + x}, {yc - y})");
            puntosLista.Add($"({xc - x}, {yc - y})");
            puntosLista.Add($"({xc + y}, {yc + x})");
            puntosLista.Add($"({xc - y}, {yc + x})");
            puntosLista.Add($"({xc + y}, {yc - x})");
            puntosLista.Add($"({xc - y}, {yc - x})");
        }

        public void ClearPuntosLista()
        {
            puntosLista.Clear();
        }

        public int Validar(string radio)
        {
            if (Validador.Validar<int>(radio))
            {
                return int.Parse(radio);
            }
            else
            {
                MessageBox.Show("Ingresa un número válido para el radio.");
            }
            return 0;
        }

        public void DibujarCircunferencia(Graphics g, int xc, int yc, int r, Color color)
        {
            ClearPuntosLista();
            CircleMidPoint(xc, yc, r);
            using (Brush brush = new SolidBrush(color))
            {
                foreach (var punto in puntosLista)
                {
                    string[] coords = punto.Trim('(', ')').Split(',');
                    int x = int.Parse(coords[0]);
                    int y = int.Parse(coords[1]);
                    g.FillRectangle(brush, x, y, 1, 1);
                }
            }
        }
    }
}
