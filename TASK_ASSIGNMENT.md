# PHÂN CÔNG CÔNG VIỆC DỰ ÁN QUẢN LÝ THƯ VIỆN

> Dự án: Windows Forms App .NET Framework
> Số lượng phát triển: 5 thành viên
> Đề tài: Quản lý Thư viện từ tài liệu DSDeTaiThamKhao.pdf

---

## 📌 DEVELOPER 1 - CHỊU TRÁCH NHIỆM CƠ SỞ DỮ LIỆU & CORE ARCHITECTURE

> Hoàng

✅ **Nhiệm vụ chính:**
- Thiết kế Database Schema
- Xây dựng lớp Models, Enums, Constants
- Xây dựng kiến trúc nền, Helpers chung, Extension methods

✅ **File được phân công:**
```
QuanLyThuVien/
├── Models/
│   ├── Book.cs
│   ├── BookLiquidation.cs
│   ├── BorrowSLip.cs
│   ├── Fine.cs
│   ├── Reader.cs
│   ├── ReturnSlip.cs
│   └── Staff.cs
├── Enums/
│   ├── BookCategory.cs
│   ├── BookStatus.cs
│   ├── Department.cs
│   ├── LiquidationReason.cs
│   ├── Position.cs
│   ├── ReaderType.cs
│   └── UserRole.cs
├── Constants/
│   ├── AppSettings.cs
│   └── UIConstants.cs
├── Extensions/
│   └── ControlExtensions.cs
├── Helpers/
└── Validators/
    └── InputValidator.cs
```

---

## 📌 DEVELOPER 2 - CHỊU TRÁCH NHIỆM QUẢN LÝ NGƯỜI DÙNG & XÁC THỰC

> Hiệp

✅ **Nhiệm vụ chính:**
- Form Đăng nhập, Phân quyền //Oke 
- Quản lý Nhân viên, Thiết lập hệ thống
- Xây dựng Permission system, quản lý vai trò người dùng //Oke

✅ **File được phân công:**
```
QuanLyThuVien/
├── FrmLogin.cs
├── FrmLogin.Designer.cs
├── FrmLogin.resx
├── FrmStaff.cs
├── FrmStaff.Designer.cs
├── FrmStaff.resx
├── FrmSettings.cs
├── FrmSettings.Designer.cs
├── FrmSettings.resx
├── Attributes/
│   └── RequiresRoleAttribute.cs
├── Interfaces/
│   └── IPermissionForm.cs
└── Managers/
    └── FormManager.cs
```

---

## 📌 DEVELOPER 3 - CHỊU TRÁCH NHIỆM QUẢN LÝ SÁCH

> Giàu

✅ **Nhiệm vụ chính:**
- Quản lý danh mục sách
- Nhập sách, Tra cứu sách
- Thanh lý sách

✅ **File được phân công:**
```
QuanLyThuVien/
├── FrmBookEntry.cs
├── FrmBookEntry.Designer.cs
├── FrmBookEntry.resx
├── FrmSearchBook.cs
├── FrmSearchBook.Designer.cs
├── FrmSearchBook.resx
├── FrmLiquidation.cs
├── FrmLiquidation.Designer.cs
└── FrmLiquidation.resx
```

---

## 📌 DEVELOPER 4 - CHỊU TRÁCH NHIỆM GIAO DỊCH MƯỢN TRẢ

> Dũng

✅ **Nhiệm vụ chính:**
- Lập thẻ độc giả
- Cho mượn sách, Nhận trả sách
- Ghi nhận mất sách, Thu tiền phạt

✅ **File được phân công:**
```
QuanLyThuVien/
├── Helpers/
│   └── ✅ DatabaseHelper.cs # Cơ chễ hố trợ tương tác với Database
├── FrmReaderCard.cs
├── FrmReaderCard.Designer.cs
├── FrmReaderCard.resx
├── FrmBorrow.cs
├── FrmBorrow.Designer.cs
├── FrmBorrow.resx
├── FrmReturn.cs
├── FrmReturn.Designer.cs
├── FrmReturn.resx
├── FrmFineCollection.cs
├── FrmFineCollection.Designer.cs
└── FrmFineCollection.resx
```

---

## 📌 DEVELOPER 5 - CHỊU TRÁCH NHIỆM BÁO CÁO & MÀN HÌNH CHÍNH

> Khoa

✅ **Nhiệm vụ chính:**
- Form chính Menu hệ thống
- Tất cả các module báo cáo, thống kê
- Xây dựng giao diện chung, điều hướng

✅ **File được phân công:**
```
QuanLyThuVien/
├── FrmMain.cs
├── FrmMain.Designer.cs
├── FrmMain.resx
├── FrmReports.cs
├── FrmReports.Designer.cs
├── FrmReports.resx
├── Program.cs
├── App.config
├── Properties/
│   ├── AssemblyInfo.cs
│   ├── Resources.resx
│   ├── Resources.Designer.cs
│   ├── Settings.settings
│   └── Settings.Designer.cs
└── QuanLyThuVien.csproj
```

---

## 📋 TRẠNG THÁI CÔNG VIỆC

| Developer   | Tiến độ          | Deadline   | Ghi chú |
|-------------|------------------|------------|---|
| Developer 1 | `Đang tiến hành` | 26-04-2025 | Core Layer |
| Developer 2 | `Đang tiến hành` | 26-04-2025 | Authentication Layer |
| Developer 3 | `Đang tiến hành` | 26-04-2025 | Book Management |
| Developer 4 | `Cần kiểm thử` | 26-04-2025 | Transaction Layer |
| Developer 5 | `Đang tiến hành` | 26-04-2025 | Presentation & Reporting |

---

## ✅ QUY ĐỊNH PHÂN CÔNG

1. Mỗi Developer chỉ được commit vào file được phân công cho mình
2. Không tự ý sửa file của người khác, nếu cần thay đổi tạo Pull Request hoặc trao đổi trực tiếp
3. Các thành phần chung (Interfaces, Constants, Enums) chỉ được thay đổi sau khi thống nhất cả nhóm
4. Mỗi Form bao gồm đầy đủ 3 file: `.cs`, `.Designer.cs`, `.resx`
5. Tất cả nghiệp vụ được tuân thủ chính xác theo tài liệu `DSDeTaiThamKhao.pdf`