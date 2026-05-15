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
    public partial class FrmPrimeraFigura : Form
    {

        private int x1 = 200;
        private int y1 = 80;
        private int radioCirculo1 = 50;
        bool rotar = false;

        public FrmPrimeraFigura()
        {
            InitializeComponent();
        }

        private void FrmPrimeraFigura_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            FlorMargarita flor = new FlorMargarita();
            flor.GraficarPentagono(g, x1, y1, radioCirculo1, 2, Color.White, rotar, 2);
        }
    }
}
