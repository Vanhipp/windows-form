# 🎨 Kế hoạch Phân chia Công việc - Dự án My Paint (Assignment 03)

**Tên Project:** `Nhom_03_Paint`  

> Lưu ý: Dự án tập chung khá nhiều vào hướng đối tương, nên là Dev 1 cần hoàn thành và cải thiện trước để cho các Dev khác có thể thực hiện nhiệm vụ!

---

## 👥 Danh sách Thành viên & Vai trò dự kiến

| Thành viên        | Nhiệm vụ chính |
|:------------------| :--- |
| **Dev 1**         | **Kiến trúc hệ thống, Lớp Shape gốc, Quản lý vẽ (DrawingManager), Lưu file, Xoay hình.** |
| **Dev 2**          | **Thuật toán đổ màu nâng cao (Brushes), Các hình vẽ phức tạp (Đa giác, Bình hành).** |
| **Dev 3**          | **Thiết kế Giao diện chính (UI), Form About, Xử lý sự kiện Control, ColorDialog.** |
| **Dev 4**          | **Cài đặt các hình cơ bản (Line, Rectangle, Ellipse), Logic tính toán tọa độ.** |
| **Dev 5**         | **Chức năng vẽ chữ (Text drawing), Quản lý Font/Size, Hệ thống Enums và Constant.** |

---

## 🛠 Chi tiết Nhiệm vụ cụ thể

### 1. Dev 1 (Dũng) - [X]
- **Kiến trúc:** Thiết kế lớp trừu tượng `Shape.cs` (Base class) với các phương thức đa hình như `Draw()`.
- **Quản lý đồ họa:** Viết lớp `DrawingManager.cs` để lưu trữ `List<Shape>`, xử lý việc vẽ đè (Double Buffering) để tránh giật lag và mất hình khi Invalidate.
- **Tính năng cao cấp:** 
    - Cài đặt `RotateTransform` và `ResetTransform` để xoay vật thể.
    - Chức năng **Save Image** (Lưu Panel thành file JPG, PNG, BMP).
- **Tổng hợp:** Review mã nguồn, xử lý xung đột (nếu dùng Git) và kiểm thử cuối cùng.

**Files cần tạo/sửa:**
- [X] `Nhom_03_Paint/Shape.cs`
- [X] `Nhom_03_Paint/DrawingManager.cs`

---

### 2. Dev 2 (Khoa) - [ ]
- **Đổ màu nâng cao (Advanced Brushes):** Triển khai các lớp hỗ trợ: `HatchBrush`, `LinearGradientBrush`, `PathGradientBrush`, `TextureBrush`.
- **Hình học nâng cao:** Cài đặt các lớp `TriangleShape.cs`, `ParallelogramShape.cs` (Hình bình hành).
- **Tương tác:** Cài đặt logic "Chọn" (Select) hình đã vẽ để thay đổi màu hoặc xóa (yêu cầu nâng cao).

**Files cần tạo:**
- [ ] `Nhom_03_Paint/Shapes/TriangleShape.cs`
- [ ] `Nhom_03_Paint/Shapes/ParallelogramShape.cs`
- [ ] `Nhom_03_Paint/Brushes/GradientBrushes.cs`

---

### 3. Dev 3 (Hoàng) - [X]
- **Giao diện chính (Main UI):** Thiết kế `Form1.cs` chuyên nghiệp, bố trí MenuStrip/ToolStrip hợp lý.
- **Cài đặt Control:** Kết nối các sự kiện từ Button chọn màu, NumericUpDown (độ dày viền), ComboBox (chọn hình) vào logic của Lead.
- **Form giới thiệu:** Hoàn thiện `FormAbout.cs` hiển thị thông tin nhóm và hình ảnh minh họa.

**Files cần tạo/sửa:**
- [X] `Nhom_03_Paint/Form1.cs` (sửa - thêm controls)
- [X] `Nhom_03_Paint/Form1.Designer.cs` (sửa - thiết kế UI)
- [X] `Nhom_03_Paint/FormAbout.cs` (tạo mới)

---

### 4. Dev 4 (Giàu) - [ ]
- **Hình học cơ bản:** Triển khai các lớp `LineShape.cs`, `RectangleShape.cs`, `EllipseShape.cs`, `SquareShape.cs`.
- **Logic vẽ:** Đảm bảo tính toán đúng Rectangle từ 2 điểm MouseDown và MouseUp (hỗ trợ vẽ ngược hướng từ phải sang trái hoặc từ dưới lên trên).

**Files cần tạo:**
- [ ] `Nhom_03_Paint/Shapes/LineShape.cs`
- [ ] `Nhom_03_Paint/Shapes/RectangleShape.cs`
- [ ] `Nhom_03_Paint/Shapes/EllipseShape.cs`
- [ ] `Nhom_03_Paint/Shapes/SquareShape.cs`

---

### 5. Dev 5 (Hiệp) - [ ]
- **Hệ thống Văn bản:** Xử lý cho phép người dùng nhập text, chọn Font và Size thông qua `FontDialog`, sau đó vẽ lên tọa độ đã chọn.
- **Quản lý hệ thống:** Xây dựng `AppEnums.cs` để định nghĩa tập trung các kiểu `ShapeType`, `BrushStyle`, giúp code nhất quán giữa các thành viên.

**Files cần tạo:**
- [ ] `Nhom_03_Paint/TextShape.cs`
- [ ] `Nhom_03_Paint/AppEnums.cs`

---

## 📅 Deadline

Ngày nộp: 04/04/2026

Lưu ý: Mọi người nhớ commit code thường xuyên và test kỹ trước khi merge!
