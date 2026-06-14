using System;
using System.Drawing;
using System.Windows.Forms;

namespace OperacionDDA
{
    public partial class FrmCurva2P : Form
    {
        private Point p0, p1;
        private int clickCount = 0;
        private bool arrastrando = false;
        private int indiceArrastre = -1;
        private Point lastMouse;
        private CurvaBezier bezier = new CurvaBezier();

        public FrmCurva2P()
        {
            InitializeComponent();
            pictureBox1.MouseDown += pictureBox1_MouseDown;
            pictureBox1.MouseMove += pictureBox1_MouseMove;
            pictureBox1.MouseUp += pictureBox1_MouseUp;
            pictureBox1.Paint += pictureBox1_Paint;
            this.Text = "Curva Bézier - 2 Puntos de Control";
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            int cx = pictureBox1.Width / 2;
            int cy = pictureBox1.Height / 2;

            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            DibujarPlano(g, pictureBox1.Width, pictureBox1.Height, cx, cy);

            if (clickCount >= 2)
            {
                bezier.GenerarBezier2P(p0, p1);
                bezier.Dibujar(g, Color.Blue, cx, cy);
                bezier.DibujarPuntoControl(g, p0, Color.Red, cx, cy);
                bezier.DibujarPuntoControl(g, p1, Color.Red, cx, cy);

                using (Pen pen = new Pen(Color.Gray, 1) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dash })
                {
                    g.DrawLine(pen, p0.X + cx, cy - p0.Y, p1.X + cx, cy - p1.Y);
                }
            }
            else if (clickCount == 1)
            {
                bezier.DibujarPuntoControl(g, p0, Color.Red, cx, cy);
            }
        }

        private int PuntoCercano(Point mouse, Point p, int cx, int cy)
        {
            int mx = mouse.X;
            int my = mouse.Y;
            int px = p.X + cx;
            int py = cy - p.Y;
            int dist = (mx - px) * (mx - px) + (my - py) * (my - py);
            return dist <= 10 * 10 ? 0 : -1;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            int cx = pictureBox1.Width / 2;
            int cy = pictureBox1.Height / 2;
            int x = e.X - cx;
            int y = cy - e.Y;

            if (clickCount < 2)
            {
                if (clickCount == 0)
                {
                    p0 = new Point(x, y);
                    clickCount = 1;
                }
                else
                {
                    p1 = new Point(x, y);
                    clickCount = 2;
                }
                pictureBox1.Invalidate();
            }
            else
            {
                if (PuntoCercano(e.Location, p0, cx, cy) == 0)
                {
                    arrastrando = true;
                    indiceArrastre = 0;
                    lastMouse = e.Location;
                }
                else if (PuntoCercano(e.Location, p1, cx, cy) == 0)
                {
                    arrastrando = true;
                    indiceArrastre = 1;
                    lastMouse = e.Location;
                }
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (arrastrando && clickCount >= 2)
            {
                int dx = e.X - lastMouse.X;
                int dy = e.Y - lastMouse.Y;
                if (indiceArrastre == 0)
                    p0 = new Point(p0.X + dx, p0.Y - dy);
                else
                    p1 = new Point(p1.X + dx, p1.Y - dy);
                lastMouse = e.Location;
                pictureBox1.Invalidate();
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            arrastrando = false;
            indiceArrastre = -1;
        }

        private void DibujarPlano(Graphics g, int ancho, int alto, int cx, int cy)
        {
            int paso = 10;
            using (Pen cuadricula = new Pen(Color.LightGray, 1))
            {
                for (int x = cx; x < ancho; x += paso)
                    g.DrawLine(cuadricula, x, 0, x, alto);
                for (int x = cx - paso; x >= 0; x -= paso)
                    g.DrawLine(cuadricula, x, 0, x, alto);
                for (int y = cy; y < alto; y += paso)
                    g.DrawLine(cuadricula, 0, y, ancho, y);
                for (int y = cy - paso; y >= 0; y -= paso)
                    g.DrawLine(cuadricula, 0, y, ancho, y);
            }
            using (Pen ejeX = new Pen(Color.Red, 2))
            using (Pen ejeY = new Pen(Color.Blue, 2))
            {
                g.DrawLine(ejeX, 0, cy, ancho, cy);
                g.DrawLine(ejeY, cx, 0, cx, alto);
            }
        }
    }
}
