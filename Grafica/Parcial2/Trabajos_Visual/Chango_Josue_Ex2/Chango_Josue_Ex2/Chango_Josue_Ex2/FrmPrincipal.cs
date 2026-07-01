using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chango_Josue_Ex2
{
    public partial class FrmPrincipal : Form
    {

        bool dibujar = false;

        Clase_Lineas claseLineas = new Clase_Lineas();
        public FrmPrincipal()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dibujar = true;

            if (dibujar)
            {
                Graphics g = pictureBox1.CreateGraphics();
                int alto = pictureBox1.Height;
                int ancho = pictureBox1.Width;
                claseLineas.Dibujar_Cuadrado(g, alto, ancho);
                //pictureBox1.Invalidate();
            }

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }

        

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            pictureBox1.Invalidate();
        }
    }
}
