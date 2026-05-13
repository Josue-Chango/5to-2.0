using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Leccion
{
    public partial class Plano : Form
    {
        Plano_Cartesiano plano;

        bool moverP1 = false;
        bool moverP2 = false;

        public Plano()
        {
            InitializeComponent();

            this.DoubleBuffered = true;

            plano = new Plano_Cartesiano(
                this.ClientSize.Width,
                this.ClientSize.Height
            );

            this.Paint += Form1_Paint;
            this.MouseClick += Form1_MouseClick;
            this.MouseDown += Form1_MouseDown;
            this.MouseMove += Form1_MouseMove;
            this.MouseUp += Form1_MouseUp;
        }

        //dibujar
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            plano.Ancho = this.ClientSize.Width;
            plano.Alto = this.ClientSize.Height;

            plano.DibujarPlano(e.Graphics);

            plano.DibujarPuntos(e.Graphics, this.Font);

            plano.DibujarLinea(e.Graphics);

            plano.DibujarResultados(e.Graphics, this.Font);
        }

        //puntos

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            Punto2D nuevo = plano.ConvertirCoordenada(e.X, e.Y);

            if (plano.Punto1 == null)
            {
                plano.Punto1 = nuevo;
            }
            else if (plano.Punto2 == null)
            {
                plano.Punto2 = nuevo;
            }

            Invalidate();
        }

        // tocar punto

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (plano.Punto1 != null)
            {
                float x1 = plano.CentroX() + plano.Punto1.X;
                float y1 = plano.CentroY() - plano.Punto1.Y;

                if (Math.Abs(e.X - x1) < 10 &&
                    Math.Abs(e.Y - y1) < 10)
                {
                    moverP1 = true;
                }
            }

            if (plano.Punto2 != null)
            {
                float x2 = plano.CentroX() + plano.Punto2.X;
                float y2 = plano.CentroY() - plano.Punto2.Y;

                if (Math.Abs(e.X - x2) < 10 &&
                    Math.Abs(e.Y - y2) < 10)
                {
                    moverP2 = true;
                }
            }
        }

        //mover

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            Punto2D nuevo = plano.ConvertirCoordenada(e.X, e.Y);

            if (moverP1)
            {
                plano.Punto1.X = nuevo.X;
                plano.Punto1.Y = nuevo.Y;
            }

            if (moverP2)
            {
                plano.Punto2.X = nuevo.X;
                plano.Punto2.Y = nuevo.Y;
            }

            Invalidate();
        }

        //dejar
        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            moverP1 = false;
            moverP2 = false;
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
