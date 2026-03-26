# MyWordPad - Phân công công việc

## Tổng quan dự án

Dự án MyWordPad là ứng dụng soạn thảo văn bản Windows Forms với các chức năng cơ bản và nâng cao. Dự án đã hoàn thành khoảng 50%, cần chia công việc cho 5 developers để hoàn thiện.

---

## 📋 Danh sách công việc còn lại

### Dev 1: File Operations (Hiệp)
- [X] **New File**: Tạo mới document, xóa nội dung RichTextBox
- [ ] **Save As**: Lưu file với tên mới (hiện đã có XuLySave)
- [ ] **Close**: Đóng file hiện tại, xóa nội dung
- [ ] **Exit**: Thoát ứng dụng

**File cần sửa**: `MyWordPad/Form1.cs`

---

### Dev 2: Edit Operations (Giàu)
- [ ] **Undo**: Sử dụng RichTextBox.Undo()
- [ ] **Redo**: Sử dụng RichTextBox.Redo()
- [ ] **Copy**: Sử dụng RichTextBox.Copy()
- [ ] **Cut**: Sử dụng RichTextBox.Cut()
- [ ] **Paste**: Sử dụng RichTextBox.Paste()
- [ ] **Select All**: Sử dụng RichTextBox.SelectAll()

**File cần sửa**: `MyWordPad/Form1.cs`, `MyWordPad/Form1.Designer.cs`

---

### Dev 3: Find & Replace (Khoa)
- [x] **Find**: Tìm kiếm text trong RichTextBox, highlight kết quả
- [x] **Find and Replace**: Tìm và thay thế text
- [x] Hỗ trợ Next/Previous, Case sensitive option

**Cần tạo**: `MyWordPad/FindReplaceForm.cs` (Form mới)

---

### Dev 4: Paragraph & Alignment (Dũng)
- [x] **None** (Indent): Xóa indent
- [x] **5 pts**: Thụt lề 5 points
- [x] **10 pts**: Thụt lề 10 points
- [x] **15 pts**: Thụt lề 15 points
- [x] **20 pts**: Thụt lề 20 points
- [x] **Left Align**: Căn trái
- [x] **Center Align**: Căn giữa
- [x] **Right Align**: Căn phải

**File cần sửa**: `MyWordPad/Form1.cs`, `MyWordPad/Form1.Designer.cs`

---

### Dev 5: Features & UI (Hoàng)
- [X] **Page Color**: Sửa handler, dùng ColorDialog đổi BackColor của RichTextBox
- [X] **Normal**: Bỏ tất cả định dạng (Font, Color, Bold, Italic, Underline)
- [X] **Remove Bullets**: Xóa bullet khỏi văn bản
- [X] **About**: Hiển thị thông tin ứng dụng (MessageBox)
- [X] **Group Information**: Hiển thị thông tin nhóm (MSSV, Họ tên, Lớp)
- [X] **Print**: In tài liệu (sử dụng PrintDocument)
- [ ] **ToolStrip Buttons**: Gán event handlers cho các nút trên toolbar

**File cần sửa**: `MyWordPad/Form1.cs`, `MyWordPad/Form1.Designer.cs`

---

## 🔧 Hướng dẫn

### Quy tắc đặt tên handler:
```csharp
private void newToolStripMenuItem_Click(object sender, EventArgs e)
{
    // Xử lý New
}
```

### Quy tắc gán sự kiện trong Designer.cs:
```csharp
this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
```

### Tham khảo code có sẵn:
- [`XuLySave()`](MyWordPad/Form1.cs:115) - Save file
- [`XuLyOpen()`](MyWordPad/Form1.cs:146) - Open file
- [`boldToolStripMenuItem_Click()`](MyWordPad/Form1.cs:193) - Bold text

---

## ✅ Tiến độ hoàn thành

| Dev | Công việc | Trạng thái |
|-----|-----------|------------|
| Dev 1 | File Operations | ⏳          |
| Dev 2 | Edit Operations | ⏳          |
| Dev 3 | Find & Replace | ⏳          |
| Dev 4 | Paragraph & Alignment | ⏳          |
| Dev 5 | Features & UI | ⏳          |

---

## 📅 Deadline

**Ngày nộp**: 28/03/2026

**Lưu ý**: Mọi người nhớ commit code thường xuyên và test kỹ trước khi merge!
