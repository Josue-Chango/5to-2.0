using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperacionDDA
{
    internal class Relleno
    {
        private Stack<Point> pila = new Stack<Point>();
        private Color colorObjetivo;
        private Color colorRelleno;
        private Bitmap bitmap;
        private bool activo = false;

        // Inicializa el flood fill sin ejecutarlo
        public void IniciarFloodFill(Bitmap bmp, int x, int y, Color colorRell)
        {
            bitmap = bmp;
            colorRelleno = colorRell;
            colorObjetivo = bmp.GetPixel(x, y);

            pila.Clear();
            activo = false;

            if (colorObjetivo.ToArgb() == colorRelleno.ToArgb())
                return;

            pila.Push(new Point(x, y));
            activo = true;
        }

        // Ejecuta N pasos del flood fill (llamar desde el Timer)
        public bool PasoFloodFill(int pasosPorTick = 50)
        {
            if (!activo || pila.Count == 0)
                return false; // terminó

            int contador = 0;

            while (pila.Count > 0 && contador < pasosPorTick)
            {
                Point punto = pila.Pop();
                int px = punto.X;
                int py = punto.Y;

                if (px < 0 || px >= bitmap.Width || py < 0 || py >= bitmap.Height)
                    continue;

                if (bitmap.GetPixel(px, py).ToArgb() != colorObjetivo.ToArgb())
                    continue;

                bitmap.SetPixel(px, py, colorRelleno);

                /*pila.Push(new Point(px + 1, py));
                pila.Push(new Point(px - 1, py));
                pila.Push(new Point(px, py + 1));
                pila.Push(new Point(px, py - 1));*/
                pila.Push(new Point(px - 1, py)); // Oeste  ← entra último en salir
                pila.Push(new Point(px, py + 1)); // Sur
                pila.Push(new Point(px + 1, py)); // Este
                pila.Push(new Point(px, py - 1)); // Norte  ← sale primero

                contador++;
            }

            if (pila.Count == 0)
                activo = false;

            return activo; // true = sigue en progreso
        }

        public bool EstaActivo() => activo;

        public void Resetear()
        {
            pila.Clear();
            activo = false;
        }
    }
}
