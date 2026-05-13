using System;
using System.Drawing;
using System.Windows.Forms;

namespace shappes_2d
{
    public partial class frmLines : Form
    {
        private CLine ObjLine = new CLine();

        public frmLines()
        {
            InitializeComponent();

            // Eventos del PictureBox
            picCanvas.Paint += picCanvas_Paint;
            picCanvas.MouseDown += picCanvas_MouseDown;
            picCanvas.MouseMove += picCanvas_MouseMove;
            picCanvas.MouseUp += picCanvas_MouseUp;
        }

        private void frmLines_Load(object sender, EventArgs e)
        {
            picCanvas.BackColor = Color.White;
            ObjLine.InitializeData(picCanvas);
        }

        private void picCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            ObjLine.AddPoint(e, picCanvas);
        }

        private void picCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            ObjLine.MovePoint(e, picCanvas);
        }

        private void picCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            ObjLine.StopDragging();
        }

        private void picCanvas_Paint(object sender, PaintEventArgs e)
        {
            ObjLine.Draw(e.Graphics);
        }

        private void btnResetear_Click(object sender, EventArgs e)
        {
            ObjLine.InitializeData(picCanvas);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}