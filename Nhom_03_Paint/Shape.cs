using System;
using System.Drawing;

namespace Nhom_03_Paint
{
    internal abstract class Shape
    {
        // Thuộc tính cơ bản
        public Point StartPoint { get; set; }
        public Point EndPoint { get; set; }
        public Color BorderColor { get; set; } = Color.Black;
        public Color FillColor { get; set; } = Color.White;
        public int BorderWidth { get; set; } = 1;
        public Brush Brush { get; set; }
        public int RotationAngle { get; set; } = 0; // Góc xoay
        // Selected state for interaction
        // [Khoa] Cờ cho biết hình đang được chọn hay không. Khi IsSelected = true
        // thì UI sẽ vẽ khung đánh dấu xung quanh hình (xem DrawSelection).
        public bool IsSelected { get; set; } = false;

        // Constructor
        protected Shape()
        {
            // [Khoa] Mặc định tạo một SolidBrush từ FillColor. Lưu ý rằng Brush có thể
            // được tái tạo lại sau này (ví dụ khi người dùng thay đổi màu tô hoặc kiểu
            // fill) -> các lớp khác sẽ gán lại property này khi cần.
            Brush = new SolidBrush(FillColor);
        }

        // Kiểm tra điểm có nằm trong hình hay không (mặc định dùng bounding rectangle)
        public virtual bool Contains(Point p)
        {
            return GetBoundingRectangle().Contains(p);
        }

        // Phương thức trừu tượng - các hình sẽ phải triển khai
        public abstract void Draw(Graphics g);

        // Phương thức hỗ trợ xoay
        public virtual void Rotate(int angle)
        {
            RotationAngle = (RotationAngle + angle) % 360;
        }

        // Phương thức hỗ trợ reset góc xoay
        public virtual void ResetRotation()
        {
            RotationAngle = 0;
        }

        // Tính giá trị tuyệt đối của chiều rộng
        public int GetWidth()
        {
            return Math.Abs(EndPoint.X - StartPoint.X);
        }

        // Tính giá trị tuyệt đối của chiều cao
        public int GetHeight()
        {
            return Math.Abs(EndPoint.Y - StartPoint.Y);
        }

        // Lấy Rectangle từ 2 điểm (xử lý vẽ ngược)
        public virtual Rectangle GetBoundingRectangle()
        {
            int x = Math.Min(StartPoint.X, EndPoint.X);
            int y = Math.Min(StartPoint.Y, EndPoint.Y);
            int width = GetWidth();
            int height = GetHeight();

            return new Rectangle(x, y, width, height);
        }

        // Vẽ khung đánh dấu khi được chọn
        public virtual void DrawSelection(Graphics g)
        {
            if (!IsSelected) return;

            var rect = GetBoundingRectangle();
            using (var pen = new Pen(Color.Blue))
            {
                pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                g.DrawRectangle(pen, rect);
            }
        }

        // Vẽ handles cho resize
        public virtual void DrawHandles(Graphics g)
        {
            if (!IsSelected) return;

            var rect = GetBoundingRectangle();
            int handleSize = 6;
            using (var brush = new SolidBrush(Color.White))
            using (var pen = new Pen(Color.Blue))
            {
                // 4 corners
                g.FillRectangle(brush, rect.X - handleSize / 2, rect.Y - handleSize / 2, handleSize, handleSize);
                g.DrawRectangle(pen, rect.X - handleSize / 2, rect.Y - handleSize / 2, handleSize, handleSize);
                g.FillRectangle(brush, rect.Right - handleSize / 2, rect.Y - handleSize / 2, handleSize, handleSize);
                g.DrawRectangle(pen, rect.Right - handleSize / 2, rect.Y - handleSize / 2, handleSize, handleSize);
                g.FillRectangle(brush, rect.X - handleSize / 2, rect.Bottom - handleSize / 2, handleSize, handleSize);
                g.DrawRectangle(pen, rect.X - handleSize / 2, rect.Bottom - handleSize / 2, handleSize, handleSize);
                g.FillRectangle(brush, rect.Right - handleSize / 2, rect.Bottom - handleSize / 2, handleSize, handleSize);
                g.DrawRectangle(pen, rect.Right - handleSize / 2, rect.Bottom - handleSize / 2, handleSize, handleSize);
                // 4 midpoints
                g.FillRectangle(brush, rect.X + rect.Width / 2 - handleSize / 2, rect.Y - handleSize / 2, handleSize, handleSize);
                g.DrawRectangle(pen, rect.X + rect.Width / 2 - handleSize / 2, rect.Y - handleSize / 2, handleSize, handleSize);
                g.FillRectangle(brush, rect.Right - handleSize / 2, rect.Y + rect.Height / 2 - handleSize / 2, handleSize, handleSize);
                g.DrawRectangle(pen, rect.Right - handleSize / 2, rect.Y + rect.Height / 2 - handleSize / 2, handleSize, handleSize);
                g.FillRectangle(brush, rect.X + rect.Width / 2 - handleSize / 2, rect.Bottom - handleSize / 2, handleSize, handleSize);
                g.DrawRectangle(pen, rect.X + rect.Width / 2 - handleSize / 2, rect.Bottom - handleSize / 2, handleSize, handleSize);
                g.FillRectangle(brush, rect.X - handleSize / 2, rect.Y + rect.Height / 2 - handleSize / 2, handleSize, handleSize);
                g.DrawRectangle(pen, rect.X - handleSize / 2, rect.Y + rect.Height / 2 - handleSize / 2, handleSize, handleSize);
            }
        }

        // [Khoa] DrawSelection là nơi duy nhất xử lý việc vẽ khung đánh dấu khi hình
        // được chọn. Việc tách ra giúp mọi lớp hình chỉ cần gọi DrawSelection(g)
        // ở cuối phương thức Draw của mình để hỗ trợ tương tác chọn/xóa.
    }
}
