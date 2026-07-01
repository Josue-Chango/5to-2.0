using System.Collections.Generic;
using System.Drawing;

namespace PaintApplicationAssignment.Models
{
    public class PaintModel
    {
        public Point Start { get; set; }
        public Point End { get; set; }
        public List<Shape> ShapesList { get; set; }
        public Bitmap Bitmap { get; set; }
        public Graphics Graphics { get; set; }
        public bool IsDrawing { get; set; }
        public Point Px { get; set; }
        public Point Py { get; set; }
        public Pen Pen { get; set; }
        public Pen EraserPen { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int SX { get; set; }
        public int SY { get; set; }
        public int CX { get; set; }
        public int CY { get; set; }
        public Color NewColor { get; set; }
        public ToolType CurrentTool { get; set; }
        public int SelectedShapeIndex { get; set; } = -1;

        public PaintModel(int width, int height)
        {
            Pen = new Pen(Color.Black, 1);
            EraserPen = new Pen(Color.White, 10);
            Bitmap = new Bitmap(width, height);
            Graphics = Graphics.FromImage(Bitmap);
            Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Graphics.Clear(Color.White);
            ShapesList = new List<Shape>();
            CurrentTool = ToolType.SELECT;
        }

        public void ClearCanvas()
        {
            Graphics.Clear(Color.White);
            ShapesList.Clear();
            SelectedShapeIndex = -1;
        }
    }
}
