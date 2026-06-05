using shappes_2d;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OperacionDDA
{
    public partial class FrmRecorte : Form
    {
        private Recorte recorte = new Recorte();

        double x1, y1, x2, y2;

        bool dibujar = false;
        bool lineaVisible = false;
        const int xmin = -100;
        const int ymin = -100;
        double x1Original;
        double y1Original;
        double x2Original;
        double y2Original;

        private void pctGrafico_Paint(object sender, PaintEventArgs e)
        {
            int cx = pctGrafico.Width / 2;
            int cy = pctGrafico.Height / 2;

            // Dibujar ejes
            e.Graphics.DrawLine(Pens.Red, 0, cy, pctGrafico.Width, cy);
            e.Graphics.DrawLine(Pens.Blue, cx, 0, cx, pctGrafico.Height);

            // Dibujar ventana de recorte
            Rectangle ventana = new Rectangle( cx + xmin, cy - ymax, xmax - xmin, ymax - ymin);

            e.Graphics.DrawRectangle( Pens.Black, ventana);

            if (dibujar)
            {
                if (lineaVisible)
                {
                    e.Graphics.DrawLine( Pens.Red, (float)(cx + x1Original), (float)(cy - y1Original), (float)(cx + x2Original), (float)(cy - y2Original));
                    e.Graphics.DrawLine( Pens.Green, (float)(cx + x1), (float)(cy - y1), (float)(cx + x2), (float)(cy - y2));
                        
                }
            }
        }

        const int xmax = 100;

        private void btnRecortar_Click(object sender, EventArgs e)
        {
            if (!ValidarEntradas())
                return;
            /*x1 = int.Parse(txtX1.Text);
            y1 = int.Parse(txtY1.Text);
            x2 = int.Parse(txtX2.Text);
            y2 = int.Parse(txtY2.Text);*/
            x1Original = x1;
            y1Original = y1;
            x2Original = x2;
            y2Original = y2;
            lineaVisible = recorte.CohenSutherlandClip( ref x1, ref y1, ref x2, ref y2, xmin, ymin, xmax, ymax);
            
            dibujar = true;

            MostrarFormula();

            pctGrafico.Invalidate();
        }

        const int ymax = 100;
        public FrmRecorte()
        {
            InitializeComponent();
        }

        private bool ValidarEntradas()
        {
            if (Validador.Validar<int>(txtX1.Text) && Validador.Validar<int>(txtY1.Text) && Validador.Validar<int>(txtX2.Text) && Validador.Validar<int>(txtY2.Text))
            {
                x1 = int.Parse(txtX1.Text);
                y1 = int.Parse(txtY1.Text);
                x2 = int.Parse(txtX2.Text);
                y2 = int.Parse(txtY2.Text);

                return true;
            }

            MessageBox.Show( "Ingresa números enteros válidos para las coordenadas.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            return false;
        }


        private void MostrarFormula()
        {
            rtbFormula.Text =
        @"ALGORITMO COHEN-SUTHERLAND

INSIDE = 0000
LEFT   = 0001
RIGHT  = 0010
BOTTOM = 0100
TOP    = 1000

Aceptación:
(code1 | code2) == 0

Rechazo:
(code1 & code2) != 0

Intersección superior:

x = x1 + (x2-x1)
    * (ymax-y1)
    / (y2-y1)

Intersección inferior:

x = x1 + (x2-x1)
    * (ymin-y1)
    / (y2-y1)

Intersección derecha:

y = y1 + (y2-y1)
    * (xmax-x1)
    / (x2-x1)

Intersección izquierda:

y = y1 + (y2-y1)
    * (xmin-x1)
    / (x2-x1)";
        }
    }
}
