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
    public partial class FrmRecorteFigura : Form
    {

        private RecorteFigura recorte = new RecorteFigura();

        private List<PointF> poligonoOriginal;
        private List<PointF> poligonoRecortado;
        private bool dibujar = false;
        public FrmRecorteFigura()
        {
            InitializeComponent();
            
        }



       /* private void btnRecortar_Click( object sender, EventArgs e)
        {
            poligonoRecortado = recorte.SutherlandHodgman( poligonoOriginal, -100, -100, 100, 100);
            MostrarFormula();
            pictureBox1.Invalidate();
        }*/

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            int cx = pictureBox1.Width / 2;
            int cy = pictureBox1.Height / 2;

            Rectangle ventana = new Rectangle( cx - 100, cy - 100, 200, 200);

            e.Graphics.DrawRectangle( Pens.Black, ventana);

            if (poligonoOriginal != null)
            {
                PointF[] puntosOriginales = poligonoOriginal .Select(p => new PointF( cx + p.X, cy - p.Y)) .ToArray();

                using (SolidBrush rojo = new SolidBrush( Color.FromArgb( 120, Color.Red)))
                {
                    e.Graphics.FillPolygon( rojo, puntosOriginales);
                }

                e.Graphics.DrawPolygon( Pens.Red, puntosOriginales);
            }

            if (poligonoRecortado != null && poligonoRecortado.Count > 2)
            {
                PointF[] puntosRecortados = poligonoRecortado .Select(p => new PointF( cx + p.X, cy - p.Y)) .ToArray();

                using (SolidBrush verde = new SolidBrush( Color.FromArgb( 180, Color.LimeGreen)))
                {
                    e.Graphics.FillPolygon( verde, puntosRecortados);
                }

                e.Graphics.DrawPolygon( new Pen(Color.Green, 2), puntosRecortados);
            }
        }

        private void btnRecortra_Click(object sender, EventArgs e)
        {
            if (!ValidarPuntos())
                return;

            LeerPuntos();

            poligonoRecortado = recorte.SutherlandHodgman( poligonoOriginal, -100, -100, 100, 100);

            dibujar = true;
            MostrarFormula();
            pictureBox1.Invalidate();
        }

        private bool LeerPuntos()
        {
            try
            {
                poligonoOriginal = new List<PointF>()
        {
            new PointF( float.Parse(txtX1.Text), float.Parse(txtY1.Text)
            ),

            new PointF( float.Parse(txtX2.Text), float.Parse(txtY2.Text)
            ),

            new PointF( float.Parse(txtX3.Text), float.Parse(txtY3.Text)
            ),

            new PointF( float.Parse(txtX4.Text), float.Parse(txtY4.Text)
            ),

            new PointF( float.Parse(txtX5.Text), float.Parse(txtY5.Text)
            ),

            new PointF( float.Parse(txtX6.Text), float.Parse(txtY6.Text)
            ),

            new PointF( float.Parse(txtX7.Text), float.Parse(txtY7.Text)
            ),

            new PointF( float.Parse(txtX8.Text), float.Parse(txtY8.Text)
            )
        };

                return true;
            }
            catch
            {
                MessageBox.Show("Ingrese coordenadas válidas.");
                return false;
            }
        }

        private bool ValidarPuntos()
        {
            TextBox[] cajas = { txtX1, txtY1, txtX2, txtY2, txtX3, txtY3, txtX4, txtY4, txtX5, txtY5, txtX6, txtY6, txtX7, txtY7, txtX8, txtY8 };

            foreach (TextBox txt in cajas)
            {
                if (!shappes_2d.Validador.Validar<int>(txt.Text))
                {
                    MessageBox.Show( "Ingrese coordenadas válidas.");

                    return false;
                }
            }

            return true;
        }

        private void MostrarFormula()
        {
            rtbFormula.Text =
        @"SUTHERLAND-HODGMAN

Casos:

Dentro -> Dentro
Agregar punto final

Dentro -> Fuera
Agregar intersección

Fuera -> Dentro
Agregar intersección y punto final

Fuera -> Fuera
No agregar nada

Intersección Vertical:

y = y1 + (y2-y1)(xclip-x1)/(x2-x1)

Intersección Horizontal:

x = x1 + (x2-x1)(yclip-y1)/(y2-y1)

Proceso:
Izquierda -> Derecha ->
Inferior -> Superior";
        }
    }
}
