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
    public partial class FrmRelleno : Form
    {
        int radio = 110;
        bool dibujar = false;

        Circunferencia circunferencia = new Circunferencia();
        Relleno relleno = new Relleno();
        Bitmap canvas;
        Timer timer = new Timer();

        private void Timer_Tick(object sender, EventArgs e)
        {
            bool enProgreso = relleno.PasoFloodFill(pasosPorTick: 30);
            pictureBox1.Invalidate();

            if (!enProgreso)
                timer.Stop(); // Relleno completado
        }

        public FrmRelleno()
        {
            InitializeComponent();
            timer.Interval = 1; // milisegundos entre cada tick
            timer.Tick += Timer_Tick;
            this.Load += FrmRelleno_Load;
        }
        
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(canvas, 0, 0);

        }


        private void btnCirculo_Click(object sender, EventArgs e)
        {
            timer.Stop();
            relleno.Resetear();
            dibujar = true;

            int cx = pictureBox1.Width / 2;
            int cy = pictureBox1.Height / 2;

            using (Graphics g = Graphics.FromImage(canvas))
            {
                g.Clear(Color.White); // Limpiar fondo
                circunferencia.DibujarCircunferencia(g, cx, cy, radio, Color.Blue);
            }

            pictureBox1.Invalidate();
        }

        private void pictureBox1_MouseClick_1(object sender, MouseEventArgs e)
        {
            if (!dibujar) return;
            if (relleno.EstaActivo()) return; 

            relleno.IniciarFloodFill(canvas, e.X, e.Y, Color.Red);
            timer.Start();
        }

        private void FrmRelleno_Load(object sender, EventArgs e)
        {
            canvas = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            using (Graphics g = Graphics.FromImage(canvas))
            {
                g.Clear(Color.White);
            }
        }

        private void btnCuadrado_Click(object sender, EventArgs e)
        {
            timer.Stop();
            relleno.Resetear();
            dibujar = true;

            int cx = pictureBox1.Width / 2;
            int cy = pictureBox1.Height / 2;

            using (Graphics g = Graphics.FromImage(canvas))
            {
                g.Clear(Color.White); // Limpiar fondo
                g.DrawRectangle(Pens.Blue, cx - radio, cy - radio, radio * 2, radio * 2);
            }

            pictureBox1.Invalidate();
        }
    }
}
