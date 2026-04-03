using System;
using System.Drawing;
using System.Windows.Forms;

namespace Nhom_03_Paint
{
    internal class TextShape : Shape
    {
        public string Text { get; set; } = "";
        public Font Font { get; set; }
        public Color TextColor { get; set; } = Color.Black;

        public TextShape()
        {
            Font = new Font("Arial", 12);
        }

        public override void Draw(Graphics g)
        {
            if (!string.IsNullOrEmpty(Text))
            {
                using (var brush = new SolidBrush(TextColor))
                {
                    g.DrawString(Text, Font, brush, StartPoint);
                }
            }
            DrawSelection(g);
        }

        public override bool Contains(Point p)
        {
            if (string.IsNullOrEmpty(Text)) return false;

            // Measure the text size using a dummy graphics if needed, but for simplicity, use a basic rectangle
            // For accurate measurement, we might need to cache the size from Draw, but this is approximate
            SizeF size = TextRenderer.MeasureText(Text, Font);
            Rectangle textRect = new Rectangle(StartPoint, size.ToSize());
            return textRect.Contains(p);
        }

        public override Rectangle GetBoundingRectangle()
        {
            if (string.IsNullOrEmpty(Text))
                return new Rectangle(StartPoint, Size.Empty);

            SizeF size = TextRenderer.MeasureText(Text, Font);
            return new Rectangle(StartPoint, size.ToSize());
        }
    }
}
