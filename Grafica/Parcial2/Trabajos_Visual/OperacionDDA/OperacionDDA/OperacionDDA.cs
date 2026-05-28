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
    internal class OperacionDDA
    {

        public OperacionDDA() { }
        int x1, y1, x2, y2, pasos;
        public System.Collections.Generic.List<string> puntosLista = new System.Collections.Generic.List<string>();

        public void ClearPuntosLista()
        {
            puntosLista.Clear();
        }


        public int calcularX(int x1, int x2)
        {
            return x2 - x1;
        }
        public int calcularY(int y1, int y2)
        {
            return y2 - y1;
        }

        public void DDA(Graphics g, int x1, int y1, int x2, int y2, Color color)
        {
            ClearPuntosLista();
            int dx = calcularX(x1, x2);
            int dy = calcularY(y1, y2);
            int pasos = Math.Max(Math.Abs(dx), Math.Abs(dy));
            using (Brush brush = new SolidBrush(color))
            {
                if (pasos == 0)
                {
                    puntosLista.Add($"({x1}, {y1})");
                    g.FillRectangle(brush, x1, y1, 1, 1);
                    return;
                }

                float xIncrement = (float)dx / pasos;
                float yIncrement = (float)dy / pasos;
                float x = x1;
                float y = y1;
                for (int i = 0; i <= pasos; i++)
                {
                    int xr = (int)Math.Round(x);
                    int yr = (int)Math.Round(y);
                    puntosLista.Add($"({xr}, {yr})");
                    g.FillRectangle(brush, xr, yr, 1, 1);
                    x += xIncrement;
                    y += yIncrement;
                }
            }
        }

        public int Validar(string coordenada)
        {
            if (Validador.Validar<int>(coordenada))
            {
                return int.Parse(coordenada);
            }
            else
            {
                MessageBox.Show("Ingresa un número válido para la coordenada.");
            }
            return 0;
        }

        public void DibujarPlano(Graphics g, int Ancho, int Alto, int CentroX, int CentroY)
        {
            int paso = 10;

            using (Pen cuadricula = new Pen(Color.LightGray, 1))
            {
                for (int x = CentroX; x < Ancho; x += paso)
                {
                    g.DrawLine(cuadricula, x, 0, x, Alto);
                }
                for (int x = CentroX - paso; x >= 0; x -= paso)
                {
                    g.DrawLine(cuadricula, x, 0, x, Alto);
                }

                for (int y = CentroY; y < Alto; y += paso)
                {
                    g.DrawLine(cuadricula, 0, y, Ancho, y);
                }
                for (int y = CentroY - paso; y >= 0; y -= paso)
                {
                    g.DrawLine(cuadricula, 0, y, Ancho, y);
                }
            }

            using (Pen ejeX = new Pen(Color.Red, 2))
            using (Pen ejeY = new Pen(Color.Blue, 2))
            {
                g.DrawLine(ejeX, 0, CentroY, Ancho, CentroY);
                g.DrawLine(ejeY, CentroX, 0, CentroX, Alto);
            }
        }


        public void DDACentrado(Graphics g, int x1, int y1, int x2, int y2, Color color, int CentroX, int CentroY)
        {
            ClearPuntosLista();
            int dx = calcularX(x1, x2);
            int dy = calcularY(y1, y2);
            pasos = Math.Max(Math.Abs(dx), Math.Abs(dy));
            using (Brush brush = new SolidBrush(color))
            {
                if (pasos == 0)
                {
                    puntosLista.Add($"({x1}, {y1})");
                    g.FillRectangle(brush, x1 + CentroX, y1 + CentroY, 1, 1);
                    return;
                }

                float xIncrement = (float)dx / pasos;
                float yIncrement = (float)dy / pasos;
                float x = x1;
                float y = y1;
                for (int i = 0; i <= pasos; i++)
                {
                    int xr = (int)Math.Round(x);
                    int yr = (int)Math.Round(y);
                    puntosLista.Add($"({xr}, {yr})");
                    g.FillRectangle(brush, xr + CentroX, yr + CentroY, 1, 1);
                    x += xIncrement;
                    y += yIncrement;
                }



            }
        }

        public int getPasos()
        {
            return pasos;
        }


    }
}
