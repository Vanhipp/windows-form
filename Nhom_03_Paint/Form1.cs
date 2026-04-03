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
        Color colorBorder; //BIẾN LƯU MÀU VIỀN
        Color colorFill; //BIẾN LƯU MÀU TÔ

        private DrawingManager drawingManager;
        private bool isDrawing = false;
        private Point startPoint = Point.Empty;

        public Form1()
        {
            InitializeComponent();
            drawingManager = new DrawingManager();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.DoubleBuffered = true; // Kích hoạt Double Buffering cho Form
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            drawingManager?.Dispose();
            base.OnFormClosing(e);
        }

        private void colorBorder_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                colorBorder = colorDialog.Color;
                colorBorderSelect.BackColor = colorBorder;
            }
        }

        private void colorFillSelect_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                colorFill = colorDialog.Color;
                colorFillSelect.BackColor = colorFill;
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
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                string shapeType = shapeSelect.SelectedItem?.ToString();
                if (shapeType == "Text")
                {
                    // Show text input dialog
                    TextInputDialog dialog = new TextInputDialog();
                    if (dialog.ShowDialog() == DialogResult.OK && !string.IsNullOrEmpty(dialog.InputText))
                    {
                        TextShape textShape = new TextShape();
                        textShape.StartPoint = e.Location;
                        textShape.EndPoint = e.Location; // Same point for text
                        textShape.Text = dialog.InputText;
                        textShape.Font = dialog.SelectedFont;
                        textShape.TextColor = dialog.SelectedColor;
                        // Note: Border and fill not used for text, but set anyway for consistency
                        textShape.BorderColor = colorBorder;
                        textShape.BorderWidth = (int)sizeBorder.Value;
                        textShape.FillColor = colorFill;
                        SetShapeBrush(textShape); // Though not used

                        drawingManager.AddShape(textShape);
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

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDrawing)
            {
                Shape previewShape = CreateShape(startPoint, e.Location);
                if (previewShape != null)
                {
                    drawingManager.SetPreviewShape(previewShape);
                }
                panel1.Invalidate();
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && isDrawing)
            {
                isDrawing = false;
                Shape finalShape = CreateShape(startPoint, e.Location);
                if (finalShape != null)
                {
                    drawingManager.AddShape(finalShape);
                }
                drawingManager.ClearPreviewShape();
                panel1.Invalidate();
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
                
                // Set brush based on fill style
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
