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

        public bool EsNumero(string texto)
        {
            int numero;
            return int.TryParse(texto, out numero);
        }

        private void btnsumar_Click(object sender, EventArgs e)
        {
            if (numero1.Text == "" || numero2.Text == ""){
                warning.Text = "WARNING: Por favor llene todas las casillas";
            }
            else if (!EsNumero(numero1.Text) || !EsNumero(numero2.Text))
            {
                warning.Text = "WARNING: Solo se permiten números";
            }
            else
            {
                int num1 = int.Parse(numero1.Text), num2 = int.Parse(numero2.Text);
                int respuesta = num1 + num2;
                Respuesta.Text = "Resultado: " + respuesta.ToString();
                warning.Text = "";
            }
        }

        private void btnrestar_Click(object sender, EventArgs e)
        {
            if (numero1.Text == "" || numero2.Text == "")
            {
                warning.Text = "WARNING: Por favor llene todas las casillas";
            }
            else if (!EsNumero(numero1.Text) || !EsNumero(numero2.Text))
            {
                warning.Text = "WARNING: Solo se permiten números";
            }
            else
            {
                int num1 = int.Parse(numero1.Text), num2 = int.Parse(numero2.Text);
                int respuesta = num1 - num2;
                Respuesta.Text = "Resultado: " + respuesta.ToString();
                warning.Text = "";
            }
        }

        private void btnmultiplicar_Click(object sender, EventArgs e)
        {
            if (numero1.Text == "" || numero2.Text == "")
            {
                warning.Text = "WARNING: Por favor llene todas las casillas";
            }
            else if (!EsNumero(numero1.Text) || !EsNumero(numero2.Text))
            {
                warning.Text = "WARNING: Solo se permiten números";
            }
            else
            {
                int num1 = int.Parse(numero1.Text), num2 = int.Parse(numero2.Text);
                int respuesta = num1 * num2;
                Respuesta.Text = "Resultado: " + respuesta.ToString();
                warning.Text = "";
            }
        }

        private void btndividir_Click(object sender, EventArgs e)
        {
            if (numero1.Text == "" || numero2.Text == "")
            {
                warning.Text = "WARNING: Por favor llene todas las casillas";
            }
            else if (!EsNumero(numero1.Text) || !EsNumero(numero2.Text))
            {
                warning.Text = "WARNING: Solo se permiten números";
            }
            else if ( int.Parse(numero1.Text) != 0)
            {
                int num1 = int.Parse(numero1.Text), num2 = int.Parse(numero2.Text);
                int respuesta = num1 / num2;
                Respuesta.Text = "Resultado: " + respuesta.ToString();
                warning.Text = "";
            }
            else
            {
                warning.Text = "WARNING: El dividendo tiene que ser mayor a 0";
            }
        }
    }
}
