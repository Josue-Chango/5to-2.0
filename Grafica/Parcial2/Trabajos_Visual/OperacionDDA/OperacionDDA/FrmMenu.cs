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
    public partial class FrmMenu : Form
    {
        public FrmMenu()
        {
            InitializeComponent();
        }

        private void circunferenciaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmCircunferencia frmcirculo = new FrmCircunferencia();
            frmcirculo.MdiParent = this;
            frmcirculo.Show();
        }

        private void dDAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmPrincipal frmPrincipal = new FrmPrincipal();
            frmPrincipal.MdiParent = this;
            frmPrincipal.Show();
        }
    }
}
