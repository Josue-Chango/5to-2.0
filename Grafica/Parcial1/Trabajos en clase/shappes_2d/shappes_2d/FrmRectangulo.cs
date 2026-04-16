using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace shappes_2d
{
    public partial class FrmRectangulo : Form
    {
        float weight = 0;
        float height = 0;
        bool dibujar = false;
        public FrmRectangulo()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtAncho_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txtLargo_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmRectangulo_Paint(object sender, PaintEventArgs e)
        {
            if (!dibujar) return;
            Graphics g = e.Graphics;
            /*Pen pen = new Pen(Color.Blue, 2);
            g.DrawRectangle(Pens.Red, 300, 100, weight, height);*/
            Figuras figuras = new Figuras();
            figuras.DibujarRectangulo(g, weight, height);
        }

        private void btnDibujar_Click(object sender, EventArgs e)
        {
            if (Validador.Validar<int>(txtAncho.Text) && Validador.Validar<int>(txtLargo.Text))
            {
                Calculos calculos = new Calculos();
                
                weight = float.Parse(txtAncho.Text);
                height = float.Parse(txtLargo.Text);
                lblArea.Text = "Area: " + calculos.CalcularArea(weight, height);
                lblPerimetro.Text = "Perimetro: " + calculos.CalcularPerimetro(weight, height);
                dibujar = true;
                warning.Text = "";

                this.Invalidate();
            }
            else
            {
                warning.Text = "Dato inválido, ingresa números mayores a 0 y sin espacios";
            }
            
        }

        private void btnResetear_Click(object sender, EventArgs e)
        {
            weight = 0;
            height = 0;
            lblArea.Text = "Area: ";
            lblPerimetro.Text = "Perimetro: ";
            warning.Text = "";
            txtLargo.Text = "";
            txtAncho.Text = "";
            this.Invalidate();

        }
    }
}
