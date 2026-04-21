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
    public partial class FrmHexagono : Form
    {
        int radio = 0;
        bool dibujar = false;
        public FrmHexagono()
        {
            InitializeComponent();
        }

        private void FrmHexagono_Paint(object sender, PaintEventArgs e)
        {
            if (!dibujar) return;
            Graphics g = e.Graphics;
            Figuras figuras = new Figuras();
            figuras.DibujarHexagono(g, radio);
        }

        private void txtRadio_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnDibujar_Click(object sender, EventArgs e)
        {
            if (Validador.Validar<int>(txtRadio.Text) )
            {
                radio = int.Parse(txtRadio.Text);
                radio = radio > 0 ? radio : radio + 1;
                lblArea.Text = "Área: " + new Calculos().CalcularAreaHexagono(radio) ;
                lblPerimetro.Text = "Perímetro: " + new Calculos().CalcularPerimetroHexagono(radio) ;
                warning.Text = "";
                dibujar = true;
                this.Invalidate();
            }
            else
            {
                warning.Text = "Dato inválido, ingresa números mayores a 0 y sin espacios";
            }
        }
    }
}
