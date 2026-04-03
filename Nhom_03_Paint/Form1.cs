using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Nhom_03_Paint.Shapes;

namespace Nhom_03_Paint
{
    public partial class Form1 : Form
    {
        Color colorBorder;
        Color colorFill;

        private DrawingManager drawingManager;
        private bool isDrawing = false;
        private Point startPoint = Point.Empty;
        private bool isMoving = false;
        private Shape movingShape = null;
        private Point moveOffset = Point.Empty;
        private Point originalDelta = Point.Empty;
        private Point lastEndPoint = Point.Empty;

        private enum ResizeHandle { None, TopLeft, TopCenter, TopRight, MiddleLeft, MiddleRight, BottomLeft, BottomCenter, BottomRight, LineStart, LineEnd }
        private ResizeHandle currentResizeHandle = ResizeHandle.None;
        private bool isResizing = false;
        private Point resizeStartPoint = Point.Empty;
        private int origLeft = 0, origTop = 0, origRight = 0, origBottom = 0;
        private Point origLineStart = Point.Empty;
        private Point origLineEnd = Point.Empty;

        private Shape SelectedShape
        {
            get { return drawingManager.GetShapes().FirstOrDefault(s => s.IsSelected); }
        }

        private Point[] GetResizeHandles(Shape shape)
        {
            if (shape == null) return new Point[0];

            if (shape is LineShape)
            {
                return new Point[]
                {
                    shape.StartPoint,
                    shape.EndPoint
                };
            }

            Rectangle rect = shape.GetBoundingRectangle();
            return new Point[]
            {
                new Point(rect.Left, rect.Top),
                new Point(rect.Left + rect.Width / 2, rect.Top),
                new Point(rect.Right, rect.Top),
                new Point(rect.Left, rect.Top + rect.Height / 2),
                new Point(rect.Right, rect.Top + rect.Height / 2),
                new Point(rect.Left, rect.Bottom),
                new Point(rect.Left + rect.Width / 2, rect.Bottom),
                new Point(rect.Right, rect.Bottom)
            };
        }

        private void SetCursorForHandle(ResizeHandle handle)
        {
            switch (handle)
            {
                case ResizeHandle.TopLeft:
                case ResizeHandle.BottomRight:
                case ResizeHandle.LineStart:
                case ResizeHandle.LineEnd:
                    panel1.Cursor = Cursors.SizeAll;
                    break;
                case ResizeHandle.TopRight:
                case ResizeHandle.BottomLeft:
                    panel1.Cursor = Cursors.SizeNESW;
                    break;
                case ResizeHandle.TopCenter:
                case ResizeHandle.BottomCenter:
                    panel1.Cursor = Cursors.SizeNS;
                    break;
                case ResizeHandle.MiddleLeft:
                case ResizeHandle.MiddleRight:
                    panel1.Cursor = Cursors.SizeWE;
                    break;
                default:
                    panel1.Cursor = Cursors.Default;
                    break;
            }
        }
        

        private void ResizeShape(Point currentPoint)
        {
            if (movingShape == null) return;

            if (movingShape is LineShape)
            {
                switch (currentResizeHandle)
                {
                    case ResizeHandle.LineStart:
                        movingShape.StartPoint = currentPoint;
                        break;
                    case ResizeHandle.LineEnd:
                        movingShape.EndPoint = currentPoint;
                        break;
                }
                return;
            }

            int minSize = 10;
            int left = origLeft, top = origTop, right = origRight, bottom = origBottom;

            switch (currentResizeHandle)
            {
                case ResizeHandle.TopLeft:
                    left = Math.Min(currentPoint.X, origRight - minSize);
                    top = Math.Min(currentPoint.Y, origBottom - minSize);
                    break;
                case ResizeHandle.TopCenter:
                    top = Math.Min(currentPoint.Y, origBottom - minSize);
                    break;
                case ResizeHandle.TopRight:
                    top = Math.Min(currentPoint.Y, origBottom - minSize);
                    right = Math.Max(currentPoint.X, origLeft + minSize);
                    break;
                case ResizeHandle.MiddleLeft:
                    left = Math.Min(currentPoint.X, origRight - minSize);
                    break;
                case ResizeHandle.MiddleRight:
                    right = Math.Max(currentPoint.X, origLeft + minSize);
                    break;
                case ResizeHandle.BottomLeft:
                    left = Math.Min(currentPoint.X, origRight - minSize);
                    bottom = Math.Max(currentPoint.Y, origTop + minSize);
                    break;
                case ResizeHandle.BottomCenter:
                    bottom = Math.Max(currentPoint.Y, origTop + minSize);
                    break;
                case ResizeHandle.BottomRight:
                    right = Math.Max(currentPoint.X, origLeft + minSize);
                    bottom = Math.Max(currentPoint.Y, origTop + minSize);
                    break;
            }

            int newLeft = Math.Min(left, right);
            int newTop = Math.Min(top, bottom);
            int newRight = Math.Max(left, right);
            int newBottom = Math.Max(top, bottom);

            if (newRight - newLeft < minSize)
            {
                if (currentResizeHandle == ResizeHandle.TopLeft || currentResizeHandle == ResizeHandle.MiddleLeft || currentResizeHandle == ResizeHandle.BottomLeft)
                    newLeft = newRight - minSize;
                else
                    newRight = newLeft + minSize;
            }
            if (newBottom - newTop < minSize)
            {
                if (currentResizeHandle == ResizeHandle.TopLeft || currentResizeHandle == ResizeHandle.TopCenter || currentResizeHandle == ResizeHandle.TopRight)
                    newTop = newBottom - minSize;
                else
                    newBottom = newTop + minSize;
            }

            movingShape.StartPoint = new Point(newLeft, newTop);
            movingShape.EndPoint = new Point(newRight, newBottom);
        }

        public Form1()
        {
            InitializeComponent();
            drawingManager = new DrawingManager();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.DoubleBuffered = true;
            panel1.MouseDown += panel1_MouseDown;
            panel1.MouseMove += panel1_MouseMove;
            panel1.MouseUp += panel1_MouseUp;
            panel1.Paint += panel1_Paint;
            panel1.DoubleClick += panel1_DoubleClick;
            gradientDirectionSelect.SelectedIndexChanged += gradientDirectionSelect_SelectedIndexChanged;
            sizeBorder.ValueChanged += sizeBorder_ValueChanged;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            drawingManager?.Dispose();
            base.OnFormClosing(e);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.Z))
            {
                Undo_Click(null, null);
                return true;
            }
            if (keyData == (Keys.Control | Keys.Y))
            {
                Redo_Click(null, null);
                return true;
            }
            if (keyData == Keys.Delete)
            {
                Delete_Click(null, null);
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        public void Undo_Click(object sender, EventArgs e)
        {
            if (drawingManager.Undo())
            {
                ClearSelection();
                panel1.Invalidate();
            }
        }

        public void Redo_Click(object sender, EventArgs e)
        {
            if (drawingManager.Redo())
            {
                ClearSelection();
                panel1.Invalidate();
            }
        }

        public void Delete_Click(object sender, EventArgs e)
        {
            if (drawingManager.DeleteSelectedShape())
            {
                panel1.Invalidate();
            }
        }

        private void ClearSelection()
        {
            foreach (var s in drawingManager.GetShapes())
            {
                s.IsSelected = false;
            }
        }

        private void colorBorder_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                colorBorder = colorDialog.Color;
                colorBorderSelect.BackColor = colorBorder;
                if (SelectedShape != null && !(SelectedShape is TextShape))
                {
                    SelectedShape.BorderColor = colorBorder;
                    if (!(SelectedShape is LineShape))
                    {
                        SetShapeBrush(SelectedShape);
                    }
                    panel1.Invalidate();
                }
            }
        }

        private void colorFillSelect_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                colorFill = colorDialog.Color;
                colorFillSelect.BackColor = colorFill;
                if (SelectedShape != null && !(SelectedShape is TextShape) && !(SelectedShape is LineShape))
                {
                    SelectedShape.FillColor = colorFill;
                    SetShapeBrush(SelectedShape);
                    panel1.Invalidate();
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            shapeSelect.SelectedIndex = 0;
            fillStyleSelect.SelectedIndex = 0;
            gradientDirectionSelect.SelectedIndex = 0;
            colorBorder = Color.Black;
            colorBorderSelect.BackColor = colorBorder;
            colorFill = Color.Black;
            colorFillSelect.BackColor = colorFill;
        }

        private void fillStyleSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (fillStyleSelect.SelectedIndex != 1)
            {
                labelGrad.Visible = false;
                gradientDirectionSelect.Visible = false;
            }
            else
            {
                labelGrad.Visible = true;
                gradientDirectionSelect.Visible = true;
            }
            if (SelectedShape != null && !(SelectedShape is TextShape) && !(SelectedShape is LineShape))
            {
                SetShapeBrush(SelectedShape);
                panel1.Invalidate();
            }
        }

        private void gradientDirectionSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SelectedShape != null && !(SelectedShape is TextShape) && !(SelectedShape is LineShape) && fillStyleSelect.SelectedItem?.ToString() == "LinearGradientMode")
            {
                SetShapeBrush(SelectedShape);
                panel1.Invalidate();
            }
        }

        private void sizeBorder_ValueChanged(object sender, EventArgs e)
        {
            if (SelectedShape != null && !(SelectedShape is TextShape))
            {
                SelectedShape.BorderWidth = (int)sizeBorder.Value;
                panel1.Invalidate();
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Ứng dụng vẽ đơn giản được phát triển bởi Nhóm 03.\n\n" +
                "Các thành viên:\n" +
                "- Nguyễn Lê Văn Dũng\n" +
                "- Nguyễn Lê Khánh Hoàng\n" +
                "- Nguyễn Đăng Khoa\n" +
                "- Đỗ Văn Hiệp\n" +
                "- Nguyễn Hữu Giàu\n" +
                "\nCảm ơn bạn đã sử dụng ứng dụng!", 
                "Thông tin về ứng dụng", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            drawingManager.DrawAll(e.Graphics, panel1.Width, panel1.Height);

            var selectedShape = SelectedShape;
            if (selectedShape != null && !(selectedShape is TextShape))
            {
                Point[] handles = GetResizeHandles(selectedShape);
                foreach (var handle in handles)
                {
                    Rectangle handleRect = new Rectangle(handle.X - 5, handle.Y - 5, 10, 10);
                    e.Graphics.FillRectangle(new SolidBrush(Color.White), handleRect);
                    e.Graphics.DrawRectangle(new Pen(Color.Black), handleRect);
                }
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                bool wasEditing = isResizing || isMoving;

                var shapes = drawingManager.GetShapes();
                Shape selectedShape = null;
                for (int i = shapes.Count - 1; i >= 0; i--)
                {
                    var rect = shapes[i].GetBoundingRectangle();
                    rect.Inflate(3, 3);
                    if (rect.Contains(e.Location))
                    {
                        selectedShape = shapes[i];
                        break;
                    }
                }
                foreach (var s in shapes)
                {
                    s.IsSelected = (s == selectedShape);
                }
                if (selectedShape != null && !(selectedShape is TextShape) && !(selectedShape is LineShape))
                {
                    colorBorder = selectedShape.BorderColor;
                    colorBorderSelect.BackColor = colorBorder;
                    colorFill = selectedShape.FillColor;
                    colorFillSelect.BackColor = colorFill;
                    sizeBorder.Value = selectedShape.BorderWidth;
                }
                else if (selectedShape != null && selectedShape is LineShape)
                {
                    colorBorder = selectedShape.BorderColor;
                    colorBorderSelect.BackColor = colorBorder;
                    sizeBorder.Value = selectedShape.BorderWidth;
                }
                panel1.Invalidate();

                if (selectedShape != null && !(selectedShape is TextShape))
                {
                    Point[] handles = GetResizeHandles(selectedShape);
                    bool onHandle = false;
                    for (int i = 0; i < handles.Length; i++)
                    {
                        Rectangle handleRect = new Rectangle(handles[i].X - 6, handles[i].Y - 6, 12, 12);
                        if (handleRect.Contains(e.Location))
                        {
                            onHandle = true;
                            if (selectedShape is LineShape)
                            {
                                currentResizeHandle = i == 0 ? ResizeHandle.LineStart : ResizeHandle.LineEnd;
                            }
                            else
                            {
                                currentResizeHandle = (ResizeHandle)(i + 1);
                            }
                            isResizing = true;
                            movingShape = selectedShape;
                            resizeStartPoint = e.Location;
                            if (selectedShape is LineShape)
                            {
                                origLineStart = selectedShape.StartPoint;
                                origLineEnd = selectedShape.EndPoint;
                            }
                            else
                            {
                                Rectangle r = selectedShape.GetBoundingRectangle();
                                origLeft = r.Left;
                                origTop = r.Top;
                                origRight = r.Right;
                                origBottom = r.Bottom;
                            }
                            SetCursorForHandle(currentResizeHandle);
                            break;
                        }
                    }
                    if (!onHandle)
                    {
                        isMoving = true;
                        movingShape = selectedShape;
                        moveOffset = new Point(e.X - selectedShape.StartPoint.X, e.Y - selectedShape.StartPoint.Y);
                        originalDelta = new Point(selectedShape.EndPoint.X - selectedShape.StartPoint.X, selectedShape.EndPoint.Y - selectedShape.StartPoint.Y);
                    }
                }
                else if (selectedShape != null && selectedShape is TextShape)
                {
                    isMoving = true;
                    movingShape = selectedShape;
                    moveOffset = new Point(e.X - selectedShape.StartPoint.X, e.Y - selectedShape.StartPoint.Y);
                    originalDelta = new Point(selectedShape.EndPoint.X - selectedShape.StartPoint.X, selectedShape.EndPoint.Y - selectedShape.StartPoint.Y);
                }
                else
                {
                    if (!wasEditing)
                    {
                        string shapeType = shapeSelect.SelectedItem?.ToString();
                        if (shapeType == "Text")
                        {
                            TextInputDialog dialog = new TextInputDialog();
                            if (dialog.ShowDialog() == DialogResult.OK && !string.IsNullOrEmpty(dialog.InputText))
                            {
                                TextShape textShape = new TextShape();
                                textShape.StartPoint = e.Location;
                                textShape.EndPoint = e.Location;
                                textShape.Text = dialog.InputText;
                                textShape.Font = dialog.SelectedFont;
                                textShape.TextColor = dialog.SelectedColor;
                                textShape.BorderColor = colorBorder;
                                textShape.BorderWidth = (int)sizeBorder.Value;
                                textShape.FillColor = colorFill;
                                SetShapeBrush(textShape);

                                drawingManager.AddShape(textShape);
                                var allShapes = drawingManager.GetShapes();
                                foreach (var s in allShapes)
                                {
                                    s.IsSelected = (s == textShape);
                                }
                                panel1.Invalidate();
                            }
                        }
                        else
                        {
                            isDrawing = true;
                            startPoint = e.Location;
                        }
                    }
                }
            }
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isResizing && movingShape != null)
            {
                ResizeShape(e.Location);
                if (!(movingShape is LineShape))
                {
                    SetShapeBrush(movingShape);
                }
                SetCursorForHandle(currentResizeHandle);
                panel1.Invalidate();
            }
            else if (isMoving && movingShape != null)
            {
                Point newStart = new Point(e.X - moveOffset.X, e.Y - moveOffset.Y);
                movingShape.StartPoint = newStart;
                movingShape.EndPoint = new Point(newStart.X + originalDelta.X, newStart.Y + originalDelta.Y);
                panel1.Cursor = Cursors.SizeAll;
                panel1.Invalidate();
            }
            else if (isDrawing)
            {
                Point endPoint = e.Location;
                lastEndPoint = endPoint;
                Shape previewShape = CreateShape(startPoint, endPoint);
                if (previewShape != null)
                {
                    drawingManager.SetPreviewShape(previewShape);
                }
                panel1.Cursor = Cursors.Cross;
                panel1.Invalidate();
            }

            if (!isResizing && !isMoving && !isDrawing)
            {
                var selectedShape = SelectedShape;
                if (selectedShape != null && !(selectedShape is TextShape) && !(selectedShape is LineShape))
                {
                    colorBorder = selectedShape.BorderColor;
                    colorBorderSelect.BackColor = colorBorder;
                    colorFill = selectedShape.FillColor;
                    colorFillSelect.BackColor = colorFill;
                    sizeBorder.Value = selectedShape.BorderWidth;
                }
                else if (selectedShape != null && selectedShape is LineShape)
                {
                    colorBorder = selectedShape.BorderColor;
                    colorBorderSelect.BackColor = colorBorder;
                    sizeBorder.Value = selectedShape.BorderWidth;
                }
                else
                {
                    currentResizeHandle = ResizeHandle.None;
                    panel1.Cursor = Cursors.Default;
                }
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (isResizing)
                {
                    isResizing = false;
                    movingShape = null;
                    currentResizeHandle = ResizeHandle.None;
                }
                else if (isMoving)
                {
                    isMoving = false;
                    movingShape = null;
                }
                else if (isDrawing)
                {
                    isDrawing = false;
                    Shape finalShape = CreateShape(startPoint, lastEndPoint);
                    if (finalShape != null)
                    {
                        drawingManager.AddShape(finalShape);
                        var allShapes = drawingManager.GetShapes();
                        foreach (var s in allShapes)
                        {
                            s.IsSelected = (s == finalShape);
                        }
                    }
                    drawingManager.ClearPreviewShape();
                    panel1.Invalidate();
                }
            }
        }

        private void panel1_DoubleClick(object sender, EventArgs e)
        {
            var allShapes = drawingManager.GetShapes();
            foreach (var shape in allShapes)
            {
                if (shape.IsSelected && shape is TextShape textShape)
                {
                    TextInputDialog dialog = new TextInputDialog();
                    dialog.InputText = textShape.Text;
                    dialog.SelectedFont = textShape.Font;
                    dialog.SelectedColor = textShape.TextColor;
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        textShape.Text = dialog.InputText;
                        textShape.Font = dialog.SelectedFont;
                        textShape.TextColor = dialog.SelectedColor;
                        panel1.Invalidate();
                    }
                    break;
                }
            }
        }

        private Shape CreateShape(Point start, Point end)
        {
            if (shapeSelect.SelectedIndex < 0)
                return null;

            string shapeType = shapeSelect.SelectedItem.ToString();
            Shape shape = null;

            switch (shapeType)
            {
                case "Line":
                    shape = new LineShape();
                    break;
                case "Rectangle":
                    shape = new RectangleShape();
                    break;
                case "Ellipse":
                    shape = new EllipseShape();
                    break;
                case "Triangle":
                    shape = new TriangleShape();
                    break;
                case "Parallelogram":
                    shape = new ParallelogramShape();
                    break;
                case "Text":
                    shape = new TextShape();
                    break;
            }

            if (shape != null)
            {
                shape.StartPoint = start;
                shape.EndPoint = end;
                shape.BorderColor = colorBorder;
                shape.BorderWidth = (int)sizeBorder.Value;
                shape.FillColor = colorFill;
                SetShapeBrush(shape);
            }

            return shape;
        }

        private void SetShapeBrush(Shape shape)
        {
            string fillStyle = fillStyleSelect.SelectedItem?.ToString() ?? "Solid";
            
            switch (fillStyle)
            {
                case "Solid":
                    shape.Brush = new SolidBrush(colorFill);
                    break;
                case "LinearGradientMode":
                    var rect = shape.GetBoundingRectangle();
                    if (rect.Width > 0 && rect.Height > 0)
                    {
                        LinearGradientMode mode = GetGradientMode();
                        shape.Brush = new LinearGradientBrush(rect, colorBorder, colorFill, mode);
                    }
                    else
                    {
                        shape.Brush = new SolidBrush(colorFill);
                    }
                    break;
                case "PathGradientBrush":
                    var rect2 = shape.GetBoundingRectangle();
                    if (rect2.Width > 0 && rect2.Height > 0)
                    {
                        Point centerPoint = new Point(rect2.X + rect2.Width / 2, rect2.Y + rect2.Height / 2);
                        Point[] points = new Point[] {
                            new Point(rect2.X, rect2.Y),
                            new Point(rect2.Right, rect2.Y),
                            new Point(rect2.Right, rect2.Bottom),
                            new Point(rect2.X, rect2.Bottom)
                        };
                        shape.Brush = new PathGradientBrush(points);
                        ((PathGradientBrush)shape.Brush).CenterColor = colorBorder;
                        ((PathGradientBrush)shape.Brush).SurroundColors = new Color[] { colorFill };
                    }
                    else
                    {
                        shape.Brush = new SolidBrush(colorFill);
                    }
                    break;
                case "HatchBrush":
                    shape.Brush = new HatchBrush(HatchStyle.Cross, colorBorder, colorFill);
                    break;
                default:
                    shape.Brush = new SolidBrush(colorFill);
                    break;
            }
        }

        private LinearGradientMode GetGradientMode()
        {
            string direction = gradientDirectionSelect.SelectedItem?.ToString() ?? "Horizontal";
            
            switch (direction)
            {
                case "BackwardDiagonal":
                    return LinearGradientMode.BackwardDiagonal;
                case "ForwardDiagonal":
                    return LinearGradientMode.ForwardDiagonal;
                case "Vertical":
                    return LinearGradientMode.Vertical;
                default:
                    return LinearGradientMode.Horizontal;
            }
        }
    }
}
