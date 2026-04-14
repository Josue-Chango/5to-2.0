using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HolaMundo
{
    public partial class FrmOperaciones : Form
    {
        public FrmOperaciones()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void numero1_TextChanged(object sender, EventArgs e)
        {
            

        }

        private void btnsumar_Click(object sender, EventArgs e)
        {
            if (numero1.Text == "" || numero2.Text == ""){
                warning.Text = "WARNING: Alguna casilla esta vacioa, por favor llene todas las casillas";
            }
            else
            {
                int num1 = int.Parse(numero1.Text), num2 = int.Parse(numero2.Text);
                int respuesta = num1 + num2;
                Respuesta.Text = "Resultado: " + respuesta.ToString();
            }
        }

        private void btnrestar_Click(object sender, EventArgs e)
        {
            if (numero1.Text == "" || numero2.Text == "")
            {
                warning.Text = "WARNING: Alguna casilla esta vacioa, por favor llene todas las casillas";
            }
            else
            {
                int num1 = int.Parse(numero1.Text), num2 = int.Parse(numero2.Text);
                int respuesta = num1 - num2;
                Respuesta.Text = "Resultado: " + respuesta.ToString();
            }
        }

        private void btnmultiplicar_Click(object sender, EventArgs e)
        {
            if (numero1.Text == "" || numero2.Text == "")
            {
                warning.Text = "WARNING: Alguna casilla esta vacioa, por favor llene todas las casillas";
            }
            else
            {
                int num1 = int.Parse(numero1.Text), num2 = int.Parse(numero2.Text);
                int respuesta = num1 * num2;
                Respuesta.Text = "Resultado: " + respuesta.ToString();
            }
        }

        private void btndividir_Click(object sender, EventArgs e)
        {
            if (numero1.Text == "" || numero2.Text == "")
            {
                warning.Text = "WARNING: Alguna casilla esta vacioa, por favor llene todas las casillas";
            }
            else if ( int.Parse(numero1.Text) != 0)
            {
                int num1 = int.Parse(numero1.Text), num2 = int.Parse(numero2.Text);
                int respuesta = num1 / num2;
                Respuesta.Text = "Resultado: " + respuesta.ToString();
            }
            else
            {
                warning.Text = "WARNING: El dividendo tiene que ser mayor a 0";
            }
        }
    }
}
