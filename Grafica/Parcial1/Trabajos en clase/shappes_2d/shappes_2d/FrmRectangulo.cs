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
        int weight = 0;
        int height = 0;
        public FrmRectangulo()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        public bool EsNumero(string texto)
        {
            int numero;
            return int.TryParse(texto, out numero);
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
            Graphics g = e.Graphics;
            Pen pen = new Pen(Color.Blue, 2);
            g.DrawRectangle(Pens.Red, 300, 100, weight, height);
        }

        private void btnDibujar_Click(object sender, EventArgs e)
        {
            if (txtAncho.Text == "" && txtLargo.Text == "")
            {
                warning.Text = "WARNING: Por favor llene todas las casillas";
            }
            else if (!EsNumero(txtAncho.Text) || !EsNumero(txtLargo.Text))
            {
                warning.Text = "WARNING: Solo se permiten números";
            }
            else if (int.Parse(txtAncho.Text) < 0 || int.Parse(txtLargo.Text) < 0)
            {
                warning.Text = "WARNING: Solo se permiten números mayores a 0";
            }
            else
            {
                if (int.Parse(txtAncho.Text) < 6 || int.Parse(txtLargo.Text) < 6)
                {

                    weight = int.Parse(txtAncho.Text) * 10;
                    height = int.Parse(txtLargo.Text) * 10;
                    warning.Text = "";
                    lblArea.Text = "Area: " + ((weight / 10) * (height / 10));
                    lblPerimetro.Text = "Perimetro: " + ((2 * weight / 10) + (2 * height / 10));
                    this.Invalidate();
                }
                else
                {
                    weight = int.Parse(txtAncho.Text);
                    height = int.Parse(txtLargo.Text);
                    lblArea.Text = "Area: " + (weight * height);
                    lblPerimetro.Text = "Perimetro: " + ((2 * weight) + (2 * height));
                    warning.Text = "";
                    this.Invalidate();
                }
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
