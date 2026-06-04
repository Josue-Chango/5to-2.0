using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using shappes_2d;

namespace OperacionDDA
{
    public partial class FrmCircunferencia : Form
    {
        int centroX = 0;
        int centroY = 0;
        int radio = 0;

        // 0 = ninguno, 1 = Punto Medio, 2 = Bresenham, 3 = Paramétrico
        int algoritmoActivo = 0;
        private Timer timerAnimacion = new Timer();
        private int pixelesMostrados = 0;

        OperacionDDA operacionDDA = new OperacionDDA();
        Circunferencia circunferencia = new Circunferencia();

        public FrmCircunferencia()
        {
            InitializeComponent();
            timerAnimacion.Interval = 1; // velocidad
            timerAnimacion.Tick += TimerAnimacion_Tick;
        }

        private void TimerAnimacion_Tick(object sender, EventArgs e)
        {
            pixelesMostrados += 10;

            int total = 0;

            if (algoritmoActivo == 1)
                total = circunferencia.puntosLista.Count;

            else if (algoritmoActivo == 2)
                total = circunferencia.puntosListaBresenham.Count;

            else if (algoritmoActivo == 3)
                total = circunferencia.puntosListaParametrico.Count;

            if (pixelesMostrados >= total)
            {
                pixelesMostrados = total;
                timerAnimacion.Stop();
            }

            pctGrafico.Invalidate();
        }

        private void pctGrafico_Paint(object sender, PaintEventArgs e)
        {
            int cx = pctGrafico.Width / 2;
            int cy = pctGrafico.Height / 2;
            operacionDDA.DibujarPlano(e.Graphics, pctGrafico.Width, pctGrafico.Height, cx, cy);

            if (algoritmoActivo == 0) return;

            lstPuntos.Items.Clear();

            /*if (algoritmoActivo == 1)
            {

                //  Algoritmo 1: Punto Medio 
                circunferencia.DibujarCircunferencia(e.Graphics, cx + centroX, cy - centroY, radio, Color.Blue);
                lblPuntos.Text = $"Puntos: {circunferencia.getPuntos()}";
                lblPixeles.Text = $"Pixeles: {circunferencia.getPixeles()}";
                foreach (var linea in circunferencia.puntosLista)
                    lstPuntos.Items.Add(linea);
            }
            else if (algoritmoActivo == 2)
            {
                //  Algoritmo 2: Bresenham 
                circunferencia.DibujarCircunferenciaBresenham(e.Graphics, cx + centroX, cy - centroY, radio, Color.Red);
                lblPuntos.Text = $"Puntos: {circunferencia.getPuntosBresenham()}";
                lblPixeles.Text = $"Pixeles: {circunferencia.getPixelesBresenham()}";
                foreach (var linea in circunferencia.puntosListaBresenham)
                    lstPuntos.Items.Add(linea);
            }
            else if (algoritmoActivo == 3)
            {
                //  Algoritmo 3: Paramétrico 
                circunferencia.DibujarCircunferenciaParametrico(e.Graphics, cx + centroX, cy - centroY, radio, Color.Green);
                lblPuntos.Text = $"Puntos: {circunferencia.getPuntosParametrico()}";
                lblPixeles.Text = $"Pixeles: {circunferencia.getPixelesParametrico()}";
                foreach (var linea in circunferencia.puntosListaParametrico)
                    lstPuntos.Items.Add(linea);
            }*/

            if (algoritmoActivo == 1)
            {
                
                circunferencia.DibujarAnimado(
                    e.Graphics,
                    circunferencia.puntosLista,
                    Color.Blue,
                    pixelesMostrados);

                lblPuntos.Text = $"Puntos: {circunferencia.getPuntos()}";
                lblPixeles.Text = $"Pixeles: {circunferencia.getPixeles()}";

                lstPuntos.Items.Clear();

                foreach (var linea in circunferencia.puntosLista)
                    lstPuntos.Items.Add(linea);
                MostrarFormulaCircunferencia();

            }
            else if (algoritmoActivo == 2)
            {
                
                circunferencia.DibujarAnimado(
                    e.Graphics,
                    circunferencia.puntosListaBresenham,
                    Color.Red,
                    pixelesMostrados);

                lblPuntos.Text = $"Puntos: {circunferencia.getPuntosBresenham()}";
                lblPixeles.Text = $"Pixeles: {circunferencia.getPixelesBresenham()}";

                lstPuntos.Items.Clear();

                foreach (var linea in circunferencia.puntosListaBresenham)
                    lstPuntos.Items.Add(linea);
                MostrarFormulaCircunferencia();
            }
            else if (algoritmoActivo == 3)
            {
                circunferencia.DibujarAnimado(
                    e.Graphics,
                    circunferencia.puntosListaParametrico,
                    Color.Green,
                    pixelesMostrados);

                lblPuntos.Text = $"Puntos: {circunferencia.getPuntosParametrico()}";
                lblPixeles.Text = $"Pixeles: {circunferencia.getPixelesParametrico()}";

                lstPuntos.Items.Clear();

                foreach (var linea in circunferencia.puntosListaParametrico)
                    lstPuntos.Items.Add(linea);
                MostrarFormulaCircunferencia();
            }
        }

        // boton de punto medio
        private void btnDibujar_Click(object sender, EventArgs e)
        {
            if (Validador.Validar<int>(txtRadio.Text) && Validador.Validar<int>(txtX.Text) && Validador.Validar<int>(txtY.Text))
            {
                radio = int.Parse(txtRadio.Text);
                centroX = int.Parse(txtX.Text);
                centroY = int.Parse(txtY.Text);
            }
            else
            {
                MessageBox.Show("Ingresa un número entero válido para las coordenadas.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                radio = 0;
            }

            if (radio == 0) return;

            /*lstPuntos.Items.Clear();
            algoritmoActivo = 1;
            pctGrafico.Invalidate();*/
            lstPuntos.Items.Clear();

            circunferencia.ClearPuntosLista();

            int cx = pctGrafico.Width / 2;
            int cy = pctGrafico.Height / 2;

            circunferencia.CircleMidPoint(
                cx + centroX,
                cy - centroY,
                radio);

            algoritmoActivo = 1;
            pixelesMostrados = 0;

            timerAnimacion.Start();
            pctGrafico.Invalidate();
        }

        // Boton de brasenham
        private void btnDibujar2_Click(object sender, EventArgs e)
        {
            if (Validador.Validar<int>(txtRadio.Text) && Validador.Validar<int>(txtX.Text) && Validador.Validar<int>(txtY.Text))
            {
                radio = int.Parse(txtRadio.Text);
                centroX = int.Parse(txtX.Text);
                centroY = int.Parse(txtY.Text);
            }
            else
            {
                MessageBox.Show("Ingresa un número entero válido para las coordenadas.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                radio = 0;
            }

            if (radio == 0) return;

            /*lstPuntos.Items.Clear();
            algoritmoActivo = 2;
            pctGrafico.Invalidate();*/

            lstPuntos.Items.Clear();

            circunferencia.ClearPuntosListaBresenham();

            int cx = pctGrafico.Width / 2;
            int cy = pctGrafico.Height / 2;

            circunferencia.CircleBresenham(
                cx + centroX,
                cy - centroY,
                radio);

            algoritmoActivo = 2;
            pixelesMostrados = 0;

            timerAnimacion.Start();
            pctGrafico.Invalidate();
        }

        // boton parametrico
        private void btnDibujar3_Click(object sender, EventArgs e)
        {
            if (Validador.Validar<int>(txtRadio.Text) && Validador.Validar<int>(txtX.Text) && Validador.Validar<int>(txtY.Text))
            {
                radio = int.Parse(txtRadio.Text);
                centroX = int.Parse(txtX.Text);
                centroY = int.Parse(txtY.Text);
            }
            else
            {
                MessageBox.Show("Ingresa un número entero válido para las coordenadas.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                radio = 0;
            }

            if (radio == 0) return;

            /*lstPuntos.Items.Clear();
            algoritmoActivo = 3;
            pctGrafico.Invalidate();*/

            lstPuntos.Items.Clear();

            circunferencia.ClearPuntosListaParametrico();

            int cx = pctGrafico.Width / 2;
            int cy = pctGrafico.Height / 2;

            circunferencia.CircleParametrico(
                cx + centroX,
                cy - centroY,
                radio);

            algoritmoActivo = 3;
            pixelesMostrados = 0;

            timerAnimacion.Start();
            pctGrafico.Invalidate();
        }

        private void MostrarFormulaCircunferencia()
        {
            switch (algoritmoActivo)
            {
                case 1:
                    rtbFormula.Text =
        @"CIRCUNFERENCIA PUNTO MEDIO

x = 0
y = r

p = 1 - r

Si p < 0

   p = p + 2x + 1

Si p >= 0

   y = y - 1

   p = p + 2(x-y)+1";
                    break;

                case 2:
                    rtbFormula.Text =
        @"CIRCUNFERENCIA BRESENHAM

x = 0
y = r

d = 3 - 2r

Si d > 0

   y = y - 1

   d = d + 4(x-y)+10

Si d <= 0

   d = d + 4x + 6";
                    break;

                case 3:
                    rtbFormula.Text =
        @"CIRCUNFERENCIA PARAMÉTRICA

x = xc + r cos(θ)

y = yc + r sin(θ)

θ = θ + Δθ";
                    break;
            }
        }
    }
}