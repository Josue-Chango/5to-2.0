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
    public partial class FrmCircunferencia : Form
    {

        int centroX = 0;
        int centroY = 0;
        int radio = 0;
        bool dibujar = false;
        OperacionDDA operacionDDA = new OperacionDDA();
        Circunferencia circunferencia = new Circunferencia();

        public FrmCircunferencia()
        {
            InitializeComponent();
        }

        private void pctGrafico_Paint(object sender, PaintEventArgs e)
        {
            int cx = pctGrafico.Width / 2;
            int cy = pctGrafico.Height / 2;
            operacionDDA.DibujarPlano(e.Graphics, pctGrafico.Width, pctGrafico.Height, cx, cy);
            
            
            if (dibujar)
            {
                circunferencia.DibujarCircunferencia(e.Graphics, cx, cy, radio, Color.Blue);
                lstPuntos.Items.Clear();
                lblPuntos.Text = $"Puntos: {circunferencia.getPuntos()}";
                lblPixeles.Text = $"Pixeles: {circunferencia.getPixeles()}";
                foreach (var linea in circunferencia.puntosLista)
                {
                    lstPuntos.Items.Add(linea);
                }
            }
            
        }

        private void btnDibujar_Click(object sender, EventArgs e)
        {
            lstPuntos.Items.Clear();
            radio = operacionDDA.Validar(txtRadio.Text);

            dibujar = true;
            pctGrafico.Invalidate();
        }
    }
}
