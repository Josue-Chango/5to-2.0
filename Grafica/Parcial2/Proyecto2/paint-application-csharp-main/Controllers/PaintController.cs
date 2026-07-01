using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using PaintApplicationAssignment.Models;
using Microsoft.VisualBasic;

namespace PaintApplicationAssignment.Controllers
{
    public class PaintController
    {
        private readonly PaintModel _model;
        private readonly Form1 _view;
        private readonly ColorDialog _colorDialog;

        public PaintController(PaintModel model, Form1 view)
        {
            _model = model;
            _view = view;
            _colorDialog = new ColorDialog();
        }

        public void SelectTool(ToolType tool)
        {
            _model.CurrentTool = tool;
            if (tool != ToolType.SELECT)
                _model.SelectedShapeIndex = -1;
            _view.RefreshCanvas();
        }

        public void HandleMouseDown(Point location)
        {
            _model.IsDrawing = true;
            _model.Start = location;
            _model.End = location;
            _model.Py = location;
            _model.CX = location.X;
            _model.CY = location.Y;
        }

        public void HandleMouseMove(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && _model.IsDrawing)
            {
                if (_model.CurrentTool == ToolType.PEN)
                {
                    _model.Px = e.Location;
                    _model.Graphics.DrawLine(_model.Pen, _model.Px, _model.Py);
                    _model.Py = _model.Px;
                }
                else if (_model.CurrentTool == ToolType.ERASER)
                {
                    _model.Px = e.Location;
                    _model.Graphics.DrawLine(_model.EraserPen, _model.Px, _model.Py);
                    _model.Py = _model.Px;
                }
            }

            if (_model.CurrentTool == ToolType.TRIANGLE)
            {
                _model.End = e.Location;
            }

            _model.X = e.X;
            _model.Y = e.Y;
            _model.SX = e.X - _model.CX;
            _model.SY = e.Y - _model.CY;

            _view.RefreshCanvas();

            if (_model.CurrentTool == ToolType.ELLIPSE ||
                _model.CurrentTool == ToolType.RECTANGLE ||
                _model.CurrentTool == ToolType.LINE)
            {
                _view.SetStatusText(string.Format(" Pos: {0},{1}", e.X, e.Y));
            }
        }

        public void HandleMouseUp(Point location)
        {
            _model.IsDrawing = false;

            _model.SX = _model.X - _model.CX;
            _model.SY = _model.Y - _model.CY;

            Shape shape = null;
            Color color = _model.Pen.Color;
            float width = _model.Pen.Width;

            switch (_model.CurrentTool)
            {
                case ToolType.LINE:
                    shape = new LineShape(
                        new Point(_model.CX, _model.CY),
                        new Point(_model.X, _model.Y),
                        color, width);
                    break;

                case ToolType.RECTANGLE:
                    {
                        int x = Math.Min(_model.CX, _model.X);
                        int y = Math.Min(_model.CY, _model.Y);
                        int w = Math.Abs(_model.SX);
                        int h = Math.Abs(_model.SY);
                        shape = new RectangleShape(new Point(x, y), new Size(w, h), color, width);
                    }
                    break;

                case ToolType.ELLIPSE:
                    {
                        int x = Math.Min(_model.CX, _model.X);
                        int y = Math.Min(_model.CY, _model.Y);
                        int w = Math.Abs(_model.SX);
                        int h = Math.Abs(_model.SY);
                        shape = new EllipseShape(new Point(x, y), new Size(w, h), color, width);
                    }
                    break;

                case ToolType.TRIANGLE:
                    {
                        _model.End = location;
                        Point p1 = _model.Start;
                        Point p2 = new Point((_model.Start.X + _model.End.X) / 2, _model.End.Y);
                        Point p3 = _model.End;
                        shape = new TriangleShape(p1, p2, p3, color, width);
                    }
                    break;
            }

            if (shape != null)
            {
                _model.ShapesList.Add(shape);
                _model.SelectedShapeIndex = -1;
            }

            _view.RefreshCanvas();
        }

        public void HandlePaint(Graphics g)
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            foreach (Shape shape in _model.ShapesList)
                shape.Draw(g);

            if (_model.SelectedShapeIndex >= 0 && _model.SelectedShapeIndex < _model.ShapesList.Count)
            {
                Shape selected = _model.ShapesList[_model.SelectedShapeIndex];
                Rectangle bounds = selected.GetBounds();
                bounds.Inflate(5, 5);
                using (Pen selPen = new Pen(Color.Blue, 2)
                {
                    DashStyle = System.Drawing.Drawing2D.DashStyle.Dash
                })
                {
                    g.DrawRectangle(selPen, bounds);
                }
            }

            if (!_model.IsDrawing) return;

            switch (_model.CurrentTool)
            {
                case ToolType.ELLIPSE:
                    g.DrawEllipse(_model.Pen, _model.CX, _model.CY, _model.SX, _model.SY);
                    break;
                case ToolType.RECTANGLE:
                    g.DrawRectangle(_model.Pen, _model.CX, _model.CY, _model.SX, _model.SY);
                    break;
                case ToolType.LINE:
                    g.DrawLine(_model.Pen, _model.CX, _model.CY, _model.X, _model.Y);
                    break;
                case ToolType.TRIANGLE:
                    Point point1 = _model.Start;
                    Point point2 = new Point((_model.Start.X + _model.End.X) / 2, _model.End.Y);
                    Point point3 = _model.End;
                    g.DrawPolygon(_model.Pen, new Point[] { point1, point2, point3 });
                    break;
            }
        }

        public void HandleMouseClick(Point location)
        {
            if (_model.CurrentTool == ToolType.FILLCOLOR)
            {
                Fill(_model.Bitmap, location.X, location.Y, _model.NewColor);
            }
            else if (_model.CurrentTool == ToolType.SELECT)
            {
                for (int i = _model.ShapesList.Count - 1; i >= 0; i--)
                {
                    if (_model.ShapesList[i].ContainsPoint(location))
                    {
                        _model.SelectedShapeIndex = i;
                        _view.RefreshCanvas();
                        return;
                    }
                }
                _model.SelectedShapeIndex = -1;
                _view.RefreshCanvas();
            }
        }

        public void HandleColorClick()
        {
            if (_colorDialog.ShowDialog() == DialogResult.OK)
            {
                _model.NewColor = _colorDialog.Color;
                _model.Pen.Color = _colorDialog.Color;
                _view.SetDrawingAreaBackColor(_colorDialog.Color);
                _view.SetPickColorBack(_colorDialog.Color);
            }
        }

        public void HandleSaveClick()
        {
            if (_model.Bitmap != null)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Image(*.png)|*.png";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    Bitmap btm = _model.Bitmap.Clone(new Rectangle(0, 0, _model.Bitmap.Width, _model.Bitmap.Height), _model.Bitmap.PixelFormat);
                    using (Graphics g = Graphics.FromImage(btm))
                    {
                        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                        foreach (Shape shape in _model.ShapesList)
                            shape.Draw(g);
                    }
                    btm.Save(sfd.FileName, ImageFormat.Png);
                    MessageBox.Show("Image Saved Sucessully");
                }
            }
        }

        public void HandleClearClick()
        {
            _model.ClearCanvas();
            _view.SetDrawingImage(_model.Bitmap);
            _view.RefreshCanvas();
            SelectTool(ToolType.SELECT);
        }

        public void HandleNewClick()
        {
            DialogResult result = MessageBox.Show("Do you want to save currrent drawing?", "Close Window", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                HandleSaveClick();
            }
            else
            {
                HandleClearClick();
            }
        }

        public void HandleLoadClick()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.tif;...";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                _view.SetDrawingImageLocation(ofd.FileName);
            }
        }

        public void HandlePenWidthChanged(decimal value)
        {
            _model.Pen.Width = (float)value;
        }

        public void HandleSaveBinaryClick()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Binary(*.bin)|*.bin";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                using (Stream stream = File.Open(sfd.FileName, FileMode.Create))
                {
                    BinaryFormatter bin = new BinaryFormatter();
                    bin.Serialize(stream, _model.ShapesList);
                    MessageBox.Show("File Saved as Binary");
                }
            }
        }

        public void HandleOpenBinaryClick()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Bin Files|*.bin";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string path = ofd.FileName;
                using (Stream stream = File.Open(path, FileMode.Open))
                {
                    BinaryFormatter bin = new BinaryFormatter();
                    _model.ShapesList = (List<Shape>)bin.Deserialize(stream);
                }
                _view.RefreshCanvas();
            }
        }

        public void HandleTranslateClick()
        {
            if (_model.SelectedShapeIndex < 0 || _model.SelectedShapeIndex >= _model.ShapesList.Count)
            {
                MessageBox.Show("Please select a shape first.", "No Selection");
                return;
            }
            string dxStr = Interaction.InputBox("Enter X offset (dx):", "Translate", "10");
            if (string.IsNullOrEmpty(dxStr)) return;
            string dyStr = Interaction.InputBox("Enter Y offset (dy):", "Translate", "10");
            if (string.IsNullOrEmpty(dyStr)) return;
            if (int.TryParse(dxStr, out int dx) && int.TryParse(dyStr, out int dy))
            {
                _model.ShapesList[_model.SelectedShapeIndex].Translate(dx, dy);
                _view.RefreshCanvas();
            }
            else
            {
                MessageBox.Show("Invalid numbers.", "Error");
            }
        }

        public void HandleRotateClick()
        {
            if (_model.SelectedShapeIndex < 0 || _model.SelectedShapeIndex >= _model.ShapesList.Count)
            {
                MessageBox.Show("Please select a shape first.", "No Selection");
                return;
            }
            string angleStr = Interaction.InputBox("Enter rotation angle (degrees):", "Rotate", "45");
            if (string.IsNullOrEmpty(angleStr)) return;
            if (float.TryParse(angleStr, out float angle))
            {
                _model.ShapesList[_model.SelectedShapeIndex].Rotate(angle);
                _view.RefreshCanvas();
            }
            else
            {
                MessageBox.Show("Invalid angle.", "Error");
            }
        }

        public void HandleScaleClick()
        {
            if (_model.SelectedShapeIndex < 0 || _model.SelectedShapeIndex >= _model.ShapesList.Count)
            {
                MessageBox.Show("Please select a shape first.", "No Selection");
                return;
            }
            string factorStr = Interaction.InputBox("Enter scale factor (e.g. 1.5 to enlarge, 0.5 to shrink):", "Scale", "1.5");
            if (string.IsNullOrEmpty(factorStr)) return;
            if (float.TryParse(factorStr, out float factor) && factor > 0)
            {
                _model.ShapesList[_model.SelectedShapeIndex].Scale(factor);
                _view.RefreshCanvas();
            }
            else
            {
                MessageBox.Show("Invalid factor. Must be a positive number.", "Error");
            }
        }

        public void HandleHelpClick()
        {
            MessageBox.Show("Some people aren't good at asking for help because they're so used to being 'the helper.' Throughtout thier life they've experienced an unbalanced give and take, so their instinct is usually I'll figure it out on own. The self-reliance is all they've ever known.");
        }

        public bool HandleFormClosing()
        {
            DialogResult result = MessageBox.Show("Are you sure you want to exit?", "Exit Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            return result == DialogResult.Yes;
        }

        private void Fill(Bitmap bm, int x, int y, Color newClr)
        {
            Color oldColor = bm.GetPixel(x, y);
            if (oldColor == newClr) return;

            Stack<Point> pixel = new Stack<Point>();
            pixel.Push(new Point(x, y));
            bm.SetPixel(x, y, newClr);

            while (pixel.Count > 0)
            {
                Point pt = pixel.Pop();
                if (pt.X > 0 && pt.Y > 0 && pt.X < bm.Width - 1 && pt.Y < bm.Height - 1)
                {
                    Validate(bm, pixel, pt.X - 1, pt.Y, oldColor, newClr);
                    Validate(bm, pixel, pt.X, pt.Y - 1, oldColor, newClr);
                    Validate(bm, pixel, pt.X + 1, pt.Y, oldColor, newClr);
                    Validate(bm, pixel, pt.X, pt.Y + 1, oldColor, newClr);
                }
            }
        }

        private static void Validate(Bitmap bm, Stack<Point> sp, int x, int y, Color oldColor, Color newColor)
        {
            if (x < 0 || x >= bm.Width || y < 0 || y >= bm.Height) return;
            Color cx = bm.GetPixel(x, y);
            if (cx == oldColor)
            {
                sp.Push(new Point(x, y));
                bm.SetPixel(x, y, newColor);
            }
        }
    }
}
