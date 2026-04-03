using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Nhom_03_Paint
{
    internal class DrawingManager
    {
        // Danh sách các hình được vẽ
        private List<Shape> shapes = new List<Shape>();

        // [Khoa] Hình tạm thời dùng để hiển thị preview khi đang kéo chuột (không thêm vào danh sách shapes)
        private Shape previewShape;

        // Biến hỗ trợ Double Buffering
        private Bitmap backBuffer;
        private Graphics backGraphics;

        public DrawingManager()
        {
        }

        // [Khoa] Thiết lập hình preview (sẽ được vẽ trên back buffer nhưng không được lưu vào danh sách shapes)
        public void SetPreviewShape(Shape s)
        {
            // [Khoa] Trước khi gán preview mới, giải phóng Brush của preview cũ nếu có
            if (previewShape != null)
            {
                try { previewShape.Brush?.Dispose(); } catch { }
                previewShape.IsSelected = false;
            }
            previewShape = s;
            if (previewShape != null)
            {
                previewShape.IsSelected = true;
            }
        }

        // [Khoa] Xóa hình preview khi kết thúc thao tác vẽ
        public void ClearPreviewShape()
        {
            // [Khoa] Giải phóng Brush của preview để tránh rò rỉ GDI+ khi tạo nhiều preview liên tục
            if (previewShape != null)
            {
                try { previewShape.Brush?.Dispose(); } catch { }
                previewShape.IsSelected = false;
                previewShape = null;
            }
        }

        /// <summary>
        /// Thêm hình mới vào danh sách
        /// </summary>
        public void AddShape(Shape shape)
        {
            if (shape != null)
            {
                shapes.Add(shape);
            }
        }

        /// <summary>
        /// Xóa hình cuối cùng
        /// </summary>
        public void RemoveLastShape()
        {
            if (shapes.Count > 0)
            {
                // [Khoa] Giải phóng Brush của shape bị xóa để tránh rò rỉ
                var idx = shapes.Count - 1;
                try { shapes[idx].Brush?.Dispose(); } catch { }
                shapes.RemoveAt(idx);
            }
        }

        /// <summary>
        /// Xóa tất cả hình
        /// </summary>
        public void ClearAll()
        {
            // [Khoa] Giải phóng Brush của tất cả shape trước khi clear danh sách
            foreach (var s in shapes)
            {
                try { s.Brush?.Dispose(); } catch { }
            }
            shapes.Clear();
        }

        /// <summary>
        /// Lấy danh sách hình hiện tại
        /// </summary>
        public List<Shape> GetShapes()
        {
            return shapes;
        }

        /// <summary>
        /// Vẽ tất cả hình với Double Buffering để tránh giật lag
        /// </summary>
        public void DrawAll(Graphics g, int width, int height)
        {
            if (backBuffer == null || backBuffer.Width != width || backBuffer.Height != height)
            {
                backBuffer?.Dispose();
                backBuffer = new Bitmap(width, height);
                backGraphics?.Dispose();
                backGraphics = Graphics.FromImage(backBuffer);
            }

            // Clear backbuffer
            backGraphics.Clear(Color.White);

            // Vẽ tất cả hình lên backbuffer
            foreach (var shape in shapes)
            {
                if (shape != null)
                {
                    // Lưu trạng thái Transform hiện tại
                    var state = backGraphics.Save();

                    // Nếu có góc xoay, apply RotateTransform
                    if (shape.RotationAngle != 0)
                    {
                        // Tìm điểm trung tâm của hình
                        Rectangle bounds = shape.GetBoundingRectangle();
                        float centerX = bounds.X + bounds.Width / 2f;
                        float centerY = bounds.Y + bounds.Height / 2f;

                        backGraphics.TranslateTransform(centerX, centerY);
                        backGraphics.RotateTransform(shape.RotationAngle);
                        backGraphics.TranslateTransform(-centerX, -centerY);
                    }

                    // Vẽ hình
                    shape.Draw(backGraphics);

                    // Restore trạng thái Transform
                    backGraphics.Restore(state);
                }
            }

            // Vẽ handles cho các hình được chọn (không xoay)
            foreach (var shape in shapes)
            {
                if (shape != null && shape.IsSelected)
                {
                    shape.DrawHandles(backGraphics);
                }
            }

            // [Khoa] Vẽ hình preview (nếu có) sau khi đã vẽ tất cả hình thực tế.
            if (previewShape != null)
            {
                var state = backGraphics.Save();
                if (previewShape.RotationAngle != 0)
                {
                    Rectangle bounds = previewShape.GetBoundingRectangle();
                    float centerX = bounds.X + bounds.Width / 2f;
                    float centerY = bounds.Y + bounds.Height / 2f;

                    backGraphics.TranslateTransform(centerX, centerY);
                    backGraphics.RotateTransform(previewShape.RotationAngle);
                    backGraphics.TranslateTransform(-centerX, -centerY);
                }

                previewShape.Draw(backGraphics);
                backGraphics.Restore(state);
            }

            // Sao chép từ backbuffer lên màn hình
            g.DrawImageUnscaled(backBuffer, 0, 0);
        }

        /// <summary>
        /// Lưu Panel thành file hình ảnh (JPG, PNG, BMP)
        /// </summary>
        public bool SaveImage(string filePath, Image imageToSave)
        {
            try
            {
                if (imageToSave == null)
                    return false;

                string ext = Path.GetExtension(filePath).ToLower();

                switch (ext)
                {
                    case ".jpg":
                    case ".jpeg":
                        imageToSave.Save(filePath, System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;
                    case ".png":
                        imageToSave.Save(filePath, System.Drawing.Imaging.ImageFormat.Png);
                        break;
                    case ".bmp":
                        imageToSave.Save(filePath, System.Drawing.Imaging.ImageFormat.Bmp);
                        break;
                    default:
                        return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi lưu file: {ex.Message}", "Error");
                return false;
            }
        }

        /// <summary>
        /// Cleanup resources
        /// </summary>
        public void Dispose()
        {
            // [Khoa] Giải phóng Brush cho preview và cho tất cả shapes
            if (previewShape != null)
            {
                try { previewShape.Brush?.Dispose(); } catch { }
                previewShape = null;
            }

            foreach (var s in shapes)
            {
                try { s.Brush?.Dispose(); } catch { }
            }

            backGraphics?.Dispose();
            backBuffer?.Dispose();
        }

        /// <summary>
        /// Lấy số lượng hình hiện tại
        /// </summary>
        public int GetShapeCount()
        {
            return shapes.Count;
        }
    }
}
