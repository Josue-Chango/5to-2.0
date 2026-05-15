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
    public partial class FrmPrincipal : Form
    {
        public FrmPrincipal()
        {
            InitializeComponent();
            this.IsMdiContainer = true;
        }

        private void miSegundaFiguraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSegundaFigura frm2 = new FrmSegundaFigura();
            frm2.MdiParent = this;
            frm2.Show();
        }

        private void miPrimeraFiguraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmPrimeraFigura fr1 = new FrmPrimeraFigura();
            fr1.MdiParent = this;
            fr1.Show();
        }
    }
}
