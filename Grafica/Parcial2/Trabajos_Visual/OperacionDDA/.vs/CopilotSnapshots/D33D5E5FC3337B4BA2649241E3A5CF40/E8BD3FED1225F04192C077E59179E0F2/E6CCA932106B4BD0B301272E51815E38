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

        // 0 = ninguno, 1 = Punto Medio, 2 = Bresenham, 3 = Paramétrico
        int algoritmoActivo = 0;

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

            if (algoritmoActivo == 0) return;

            lstPuntos.Items.Clear();

            if (algoritmoActivo == 1)
            {
                // ── Algoritmo 1: Punto Medio ──────────────────────
                circunferencia.DibujarCircunferencia(e.Graphics, cx, cy, radio, Color.Blue);
                lblPuntos.Text = $"Puntos: {circunferencia.getPuntos()}";
                lblPixeles.Text = $"Pixeles: {circunferencia.getPixeles()}";
                foreach (var linea in circunferencia.puntosLista)
                    lstPuntos.Items.Add(linea);
            }
            else if (algoritmoActivo == 2)
            {
                // ── Algoritmo 2: Bresenham ────────────────────────
                circunferencia.DibujarCircunferenciaBresenham(e.Graphics, cx, cy, radio, Color.Red);
                lblPuntos.Text = $"Puntos: {circunferencia.getPuntosBresenham()}";
                lblPixeles.Text = $"Pixeles: {circunferencia.getPixelesBresenham()}";
                foreach (var linea in circunferencia.puntosListaBresenham)
                    lstPuntos.Items.Add(linea);
            }
            else if (algoritmoActivo == 3)
            {
                // ── Algoritmo 3: Paramétrico ──────────────────────
                circunferencia.DibujarCircunferenciaParametrico(e.Graphics, cx, cy, radio, Color.Green);
                lblPuntos.Text = $"Puntos: {circunferencia.getPuntosParametrico()}";
                lblPixeles.Text = $"Pixeles: {circunferencia.getPixelesParametrico()}";
                foreach (var linea in circunferencia.puntosListaParametrico)
                    lstPuntos.Items.Add(linea);
            }
        }

        // boton de punto medio
        private void btnDibujar_Click(object sender, EventArgs e)
        {
            radio = operacionDDA.Validar(txtRadio.Text);
            if (radio == 0) return;

            lstPuntos.Items.Clear();
            algoritmoActivo = 1;
            pctGrafico.Invalidate();
        }

        // Boton de brasenham
        private void btnDibujar2_Click(object sender, EventArgs e)
        {
            radio = operacionDDA.Validar(txtRadio.Text);
            if (radio == 0) return;

            lstPuntos.Items.Clear();
            algoritmoActivo = 2;
            pctGrafico.Invalidate();
        }

        // boton parametrico
        private void btnDibujar3_Click(object sender, EventArgs e)
        {
            radio = operacionDDA.Validar(txtRadio.Text);
            if (radio == 0) return;

            lstPuntos.Items.Clear();
            algoritmoActivo = 3;
            pctGrafico.Invalidate();
        }
    }
}