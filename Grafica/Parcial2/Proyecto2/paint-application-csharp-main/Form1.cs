using System;
using System.Windows.Forms;
using PaintApplicationAssignment.Controllers;
using PaintApplicationAssignment.Models;

namespace PaintApplicationAssignment
{
    public partial class Form1 : Form
    {
        private PaintController _controller;

        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;

            PaintModel model = new PaintModel(DrawingArea.Width, DrawingArea.Height);
            _controller = new PaintController(model, this);

            model.Pen.SetLineCap(System.Drawing.Drawing2D.LineCap.Round, System.Drawing.Drawing2D.LineCap.Round, System.Drawing.Drawing2D.DashCap.Round);
            DrawingArea.Image = model.Bitmap;
        }

        public void RefreshCanvas() { DrawingArea.Refresh(); }
        public void SetStatusText(string text) { toolStripStatusLabel1.Text = text; }
        public void SetPickColorBack(System.Drawing.Color color) { pick_color.BackColor = color; }
        public void SetDrawingAreaBackColor(System.Drawing.Color color) { DrawingArea.BackColor = color; }
        public void SetDrawingImage(System.Drawing.Bitmap bm) { DrawingArea.Image = bm; }
        public void SetDrawingImageLocation(string path) { DrawingArea.ImageLocation = path; }

        private void DrawingArea_MouseDown(object sender, MouseEventArgs e) { _controller.HandleMouseDown(e.Location); }
        private void DrawingArea_MouseMove(object sender, MouseEventArgs e) { _controller.HandleMouseMove(e); }
        private void DrawingArea_MouseUp(object sender, MouseEventArgs e) { _controller.HandleMouseUp(e.Location); }
        private void DrawingArea_MouseClick(object sender, MouseEventArgs e) { _controller.HandleMouseClick(e.Location); }
        private void DrawingArea_Paint(object sender, PaintEventArgs e) { _controller.HandlePaint(e.Graphics); }

        private void Btn_pencil_Click(object sender, EventArgs e) { _controller.SelectTool(ToolType.PEN); }
        private void Btn_select_Click(object sender, EventArgs e) { _controller.SelectTool(ToolType.SELECT); }
        private void Btn_erase_Click(object sender, EventArgs e) { _controller.SelectTool(ToolType.ERASER); }
        private void Btn_line_Click(object sender, EventArgs e) { _controller.SelectTool(ToolType.LINE); }
        private void Btn_rectangle_Click(object sender, EventArgs e) { _controller.SelectTool(ToolType.RECTANGLE); }
        private void Btn_ellipse_Click(object sender, EventArgs e) { _controller.SelectTool(ToolType.ELLIPSE); }
        private void Btn_triangle_Click(object sender, EventArgs e) { _controller.SelectTool(ToolType.TRIANGLE); }
        private void Btn_fill_Click(object sender, EventArgs e) { _controller.SelectTool(ToolType.FILLCOLOR); }
        private void Btn_color_Click(object sender, EventArgs e) { _controller.HandleColorClick(); }
        private void Btn_save_Click(object sender, EventArgs e) { _controller.HandleSaveClick(); }
        private void Btn_clear_Click(object sender, EventArgs e) { _controller.HandleClearClick(); }

        private void Btn_translate_Click(object sender, EventArgs e) { _controller.HandleTranslateClick(); }
        private void Btn_rotate_Click(object sender, EventArgs e) { _controller.HandleRotateClick(); }
        private void Btn_scale_Click(object sender, EventArgs e) { _controller.HandleScaleClick(); }

        private void TranslateToolStripMenuItem_Click(object sender, EventArgs e) { _controller.HandleTranslateClick(); }
        private void RotateToolStripMenuItem_Click(object sender, EventArgs e) { _controller.HandleRotateClick(); }
        private void ScaleToolStripMenuItem_Click(object sender, EventArgs e) { _controller.HandleScaleClick(); }

        private void Pen_width_ValueChanged(object sender, EventArgs e)
        {
            if (_controller != null)
                _controller.HandlePenWidthChanged(pen_width.Value);
        }

        private void NewToolStripMenuItem_Click(object sender, EventArgs e) { _controller.HandleNewClick(); }
        private void LoadToolStripMenuItem_Click(object sender, EventArgs e) { _controller.HandleLoadClick(); }
        private void SaveToolStripMenuItem_Click(object sender, EventArgs e) { _controller.HandleSaveClick(); }
        private void ClearToolStripMenuItem_Click(object sender, EventArgs e) { _controller.HandleClearClick(); }
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e) { Application.Exit(); }
        private void SaveAsBinaryToolStripMenuItem1_Click(object sender, EventArgs e) { _controller.HandleSaveBinaryClick(); }
        private void OpenBinaryFileToolStripMenuItem_Click(object sender, EventArgs e) { _controller.HandleOpenBinaryClick(); }
        private void ColorsToolStripMenuItem_Click(object sender, EventArgs e) { _controller.HandleColorClick(); }
        private void HelpToolStripMenuItem_Click(object sender, EventArgs e) { _controller.HandleHelpClick(); }
        private void EditToolStripMenuItem_Click(object sender, EventArgs e) { _controller.HandleLoadClick(); }
        private void ViewToolStripMenuItem_Click(object sender, EventArgs e) { _controller.HandleLoadClick(); }
        private void ImageToolStripMenuItem_Click(object sender, EventArgs e) { _controller.HandleLoadClick(); }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = !_controller.HandleFormClosing();
        }

        private void Form1_Load(object sender, EventArgs e) { }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
