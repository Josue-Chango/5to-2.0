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
    public partial class FrmTriangulo : Form
    {
        float ladoA = 0;
        float ladoB = 0;
        float ladoC = 0;
        bool dibujar = false;
        public FrmTriangulo()
        {
            InitializeComponent();
        }

        private void FrmTriangulo_Paint(object sender, PaintEventArgs e)
        {
            if (!dibujar) return;
            Graphics g = e.Graphics;
            Figuras figuras = new Figuras();
            figuras.DibujarTriangulo(g, ladoA, ladoB, ladoC);
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            if (Validador.Validar<int>(txtLadoA.Text) && Validador.Validar<int>(txtLadoB.Text) && Validador.Validar<int>(txtLadoC.Text))
            {
                if (Validador.ValidarTriangulo(ladoA, ladoB, ladoC))
                {
                    warning.Text = "Los lados no forman un triángulo válido";
                    this.Invalidate();
                }
                else
                {
                    Calculos calculos = new Calculos();

                    ladoA = float.Parse(txtLadoA.Text);
                    ladoB = float.Parse(txtLadoB.Text);
                    ladoC = float.Parse(txtLadoC.Text);
                    lblArea.Text = "Area: " + calculos.CalcularAreaTriangulo(ladoA, ladoB, ladoC);
                    lblPerimetro.Text = "Perimetro: " + calculos.CalcularPerimetroTriangulo(ladoA, ladoB, ladoC);
                    dibujar = true;
                    warning.Text = "";

                    this.Invalidate();
                }
            }
            else
            {
                warning.Text = "Dato inválido, ingresa números mayores a 0 y sin espacios";
            }
            
        }

        private void btnResetear_Click(object sender, EventArgs e)
        {
            ladoA = 0;
            ladoB = 0;
            ladoC = 0;
            lblArea.Text = "Area: ";
            lblPerimetro.Text = "Perimetro: ";
            warning.Text = "";
            txtLadoA.Text = "";
            txtLadoB.Text = "";
            txtLadoC.Text = "";
            this.Invalidate();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
