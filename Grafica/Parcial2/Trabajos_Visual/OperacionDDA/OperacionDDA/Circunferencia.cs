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
        //Primer Algoritmo
        public Circunferencia() { }
        public System.Collections.Generic.List<string> puntosLista = new System.Collections.Generic.List<string>();
        public int puntos = 1;
        public int pipxeles = 0;
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
                puntos ++;
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
            pipxeles += 8;
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
            puntos = 1;
            pipxeles = 0;
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

        public int getPuntos()
        {
            return puntos;
        }

        public int getPixeles()
        {
            return pipxeles;
        }



        //Segundo Algoritmo Bresenham

        public List<string> puntosListaBresenham = new List<string>();
        public int puntosBresenham = 1;
        public int pipxelesBresenham = 0;

        public void CircleBresenham(int xc, int yc, int r)
        {
            int x = 0;
            int y = r;
            int d = 3 - 2 * r;  

            PlotPointBresenham(xc, yc, x, y);

            while (x < y)
            {
                x++;
                puntosBresenham++;

                if (d > 0)
                {
                    y--;
                    d = d + 4 * (x - y) + 10;
                }
                else
                {
                    d = d + 4 * x + 6;
                }

                PlotPointBresenham(xc, yc, x, y);
            }
        }

        public void PlotPointBresenham(int xc, int yc, int x, int y)
        {
            pipxelesBresenham += 8;
            puntosListaBresenham.Add($"({xc + x}, {yc + y})");
            puntosListaBresenham.Add($"({xc - x}, {yc + y})");
            puntosListaBresenham.Add($"({xc + x}, {yc - y})");
            puntosListaBresenham.Add($"({xc - x}, {yc - y})");
            puntosListaBresenham.Add($"({xc + y}, {yc + x})");
            puntosListaBresenham.Add($"({xc - y}, {yc + x})");
            puntosListaBresenham.Add($"({xc + y}, {yc - x})");
            puntosListaBresenham.Add($"({xc - y}, {yc - x})");
        }

        public void ClearPuntosListaBresenham()
        {
            puntosListaBresenham.Clear();
            puntosBresenham = 1;
            pipxelesBresenham = 0;
        }

        public void DibujarCircunferenciaBresenham(Graphics g, int xc, int yc, int r, Color color)
        {
            ClearPuntosListaBresenham();
            CircleBresenham(xc, yc, r);
            using (Brush brush = new SolidBrush(color))
            {
                foreach (var punto in puntosListaBresenham)
                {
                    string[] coords = punto.Trim('(', ')').Split(',');
                    int x = int.Parse(coords[0]);
                    int y = int.Parse(coords[1]);
                    g.FillRectangle(brush, x, y, 1, 1);
                }
            }
        }

        public int getPuntosBresenham() 
        { 
            return puntosBresenham; 
        }
        public int getPixelesBresenham() 
        { 
            return pipxelesBresenham; 
        }


        //Tercer Algoritmo Parametrica x = xc + r·cos(θ),  y = yc + r·sin(θ)

        public List<string> puntosListaParametrico = new List<string>();
        public int puntosParametrico = 0;
        public int pipxelesParametrico = 0;

        public void CircleParametrico(int xc, int yc, int r)
        {
            int pasos = (int)(2 * Math.PI * r); 
            double incremento = (2.0 * Math.PI) / pasos;

            for (int i = 0; i < pasos; i++)
            {
                double theta = i * incremento;
                int x = (int)Math.Round(xc + r * Math.Cos(theta));
                int y = (int)Math.Round(yc + r * Math.Sin(theta));

                PlotPointParametrico(x, y);
                puntosParametrico++;
            }
        }

        public void PlotPointParametrico(int x, int y)
        {
            pipxelesParametrico++;
            puntosListaParametrico.Add($"({x}, {y})");
        }

        public void ClearPuntosListaParametrico()
        {
            puntosListaParametrico.Clear();
            puntosParametrico = 0;
            pipxelesParametrico = 0;
        }

        public void DibujarCircunferenciaParametrico(Graphics g, int xc, int yc, int r, Color color)
        {
            ClearPuntosListaParametrico();
            CircleParametrico(xc, yc, r);
            using (Brush brush = new SolidBrush(color))
            {
                foreach (var punto in puntosListaParametrico)
                {
                    string[] coords = punto.Trim('(', ')').Split(',');
                    int x = int.Parse(coords[0]);
                    int y = int.Parse(coords[1]);
                    g.FillRectangle(brush, x, y, 1, 1);
                }
            }
        }

        public int getPuntosParametrico() 
        { 
            return puntosParametrico; 
        }
        public int getPixelesParametrico() 
        { 
            return pipxelesParametrico; 
        }

        public void DibujarAnimado(Graphics g, List<string> lista, Color color, int cantidad)
            {
                using (Brush brush = new SolidBrush(color))
                {
                    for (int i = 0; i < cantidad && i < lista.Count; i++)
                    {
                        string[] coords = lista[i]
                            .Trim('(', ')')
                            .Split(',');

                        int x = int.Parse(coords[0]);
                        int y = int.Parse(coords[1]);

                        g.FillRectangle(brush, x, y, 1, 1);
                    }
                }
            }


    }
}

