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
            //frmcirculo.WindowState = FormWindowState.Maximized;
            frmcirculo.Show();
        }

        private void dDAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmPrincipal frmPrincipal = new FrmPrincipal();
            frmPrincipal.MdiParent = this;
            //frmPrincipal.WindowState = FormWindowState.Maximized;
            frmPrincipal.Show();
        }

        private void rellenoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmRelleno frmrelleno = new FrmRelleno();
            frmrelleno.MdiParent = this; 
            
            frmrelleno.Show();
        }

        private void recorteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void recorteLineaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmRecorte frmrecorte = new FrmRecorte();
            frmrecorte.MdiParent = this;

            frmrecorte.Show();
        }

        private void recorteFiguraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmRecorteFigura frmrecortefigura = new FrmRecorteFigura();
            frmrecortefigura.MdiParent = this;

            frmrecortefigura.Show();
        }
    }
}
