using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExamenChangoJosue
{
    public partial class FrmSegundaFigura : Form
    {
        int radioCirculo1 = 100;
        int x1 = 200, y1 = 20;
        bool dibujar = false, rotar = true;
        public FrmSegundaFigura()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            float cx = x1 + radioCirculo1;
            float cy = y1 + radioCirculo1;
            float radioCirculo2 = 50;
            int x2 = (int)(cx - radioCirculo2);
            int y2 = (int)(cy - radioCirculo2);
            Graphics g = e.Graphics;
            Decagono decagono = new Decagono();
            decagono.GraficarDecagono(g, x1, y1, radioCirculo1, true);
            decagono.GraficarEstrella(g, x1, y1, radioCirculo1, 1, Color.Orange, rotar);
            decagono.GraficarEstrella(g, x2, y2, radioCirculo2, 1, Color.Orange, rotar);
        }
    }
}
