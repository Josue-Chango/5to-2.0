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
    public partial class FrmPrincipal : Form
    {
        
        int x1 = 20;
        int x2 = 70;
        int y1 = 30;
        int y2 = 120;
        private Color lineaColor = Color.Black;
        OperacionDDA operacionDDA = new OperacionDDA();
        bool dibujar = false;
        public FrmPrincipal()
        {
            InitializeComponent();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            int cx = pictureBox1.Width / 2;
            int cy = pictureBox1.Height / 2;
            
            operacionDDA.DibujarPlano(e.Graphics, pictureBox1.Width, pictureBox1.Height, cx, cy);

            if (dibujar)
            {
                //operacionDDA.DDA(e.Graphics, x1, y1, x2, y2, lineaColor);
                operacionDDA.DDACentrado(e.Graphics, x1, y1, x2, y2, lineaColor, cx, cy);
                lstPuntos.Items.Clear();
                foreach (var linea in operacionDDA.puntosLista)
                {
                    lstPuntos.Items.Add(linea);
                }
            }

        }

        private void lstPuntos_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            
        }

        private void lblPuntos_Click(object sender, EventArgs e)
        {
        }

        private void btnDibujar_Click(object sender, EventArgs e)
        {
            lstPuntos.Items.Clear();
            x1 = operacionDDA.Validar(txtX1.Text);
            y1 = operacionDDA.Validar(txtY1.Text);
            x2 = operacionDDA.Validar(txtX2.Text);
            y2 = operacionDDA.Validar(txtY2.Text);
            
            dibujar = true;
            pictureBox1.Invalidate();
            
        }
    }
}
