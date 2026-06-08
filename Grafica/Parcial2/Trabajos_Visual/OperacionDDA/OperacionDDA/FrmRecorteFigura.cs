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
        private string algoritmoActual = "SUTHERLAND";
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
            algoritmoActual = "SUTHERLAND";

            EjecutarRecorte();
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

        private void btnVertex_Click(object sender, EventArgs e)
        {
            algoritmoActual = "VERTEX";

            EjecutarRecorte();
        }

        private void btnBounding_Click(object sender, EventArgs e)
        {
            algoritmoActual = "BOUNDING";

            EjecutarRecorte();
        }

        private void EjecutarRecorte()
        {
            if (!ValidarPuntos())
                return;

            if (!LeerPuntos())
                return;

            switch (algoritmoActual)
            {
                case "SUTHERLAND":

                    poligonoRecortado = recorte.SutherlandHodgman( poligonoOriginal, -100, -100, 100, 100);

                    MostrarFormula();

                    break;

                case "VERTEX":

                    poligonoRecortado = recorte.VertexClipping( poligonoOriginal, -100, -100, 100, 100);

                    MostrarFormulaVertex();

                    break;

                case "BOUNDING":

                    poligonoRecortado = recorte.BoundingBoxClip( poligonoOriginal, -100, -100, 100, 100);

                    MostrarFormulaBounding();

                    break;
            }

            dibujar = true;

            pictureBox1.Invalidate();
        }

        private void MostrarFormulaVertex()
        {
            rtbFormula.Text =
        @"VERTEX CLIPPING

Se analiza cada vértice.

Condición:

xmin <= x <= xmax

y

ymin <= y <= ymax

Si cumple:

Se conserva.

Si no cumple:

Se elimina.

Resultado:

Solo permanecen
los vértices que
están dentro de
la ventana.";
        }

        private void MostrarFormulaBounding()
        {
            rtbFormula.Text =
        @"BOUNDING BOX CLIPPING

Si:

x < xmin

x = xmin

Si:

x > xmax

x = xmax

Si:

y < ymin

y = ymin

Si:

y > ymax

y = ymax

Cada vértice es
ajustado para
permanecer dentro
de la ventana de
recorte.";
        }
    }
}
