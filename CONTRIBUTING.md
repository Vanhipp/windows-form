# Hướng dẫn đóng góp (Contributing)

## Quy ước Commit Message

Dự án tuân theo chuẩn [Conventional Commits](https://www.conventionalcommits.org/). Mỗi commit message phải có định dạng:

```
<type>(<scope>): <description>

[body]

[footer]
```

### Các loại commit (type)

| Loại       | Mô tả                                              |
|------------|-----------------------------------------------------|
| `feat`     | Thêm tính năng mới                                  |
| `fix`      | Sửa lỗi                                             |
| `docs`     | Thay đổi tài liệu                                   |
| `style`    | Thay đổi format code (không ảnh hưởng logic)        |
| `refactor` | Tái cấu trúc code (không sửa lỗi, không thêm tính năng) |
| `perf`     | Cải thiện hiệu năng                                |
| `test`     | Thêm hoặc sửa test                                  |
| `build`    | Thay đổi hệ thống build                            |
| `ci`       | Thay đổi cấu hình CI/CD                            |
| `chore`    | Các thay đổi khác (quản lý package, v.v.)          |
| `revert`   | Hoàn tác commit trước đó                           |

### Ví dụ

```
feat: thêm chức năng in đậm văn bản
fix(font): sửa lỗi chọn font không áp dụng đúng
docs: cập nhật README hướng dẫn cài đặt
refactor: tách logic lưu file thành phương thức riêng
```

### Quy tắc

- Giới hạn tiêu đề trong **72 ký tự**
- Viết bằng **tiếng Việt** hoặc **tiếng Anh**
- Dùng thì **hiện tại** ("thêm" không phải "đã thêm")
- Không kết thúc tiêu đề bằng dấu chấm
- Nếu commit liên quan đến issue, thêm `Closes #<số-issue>` vào footer

### Branch

- `main` — nhánh chính, code ổn định
- `feat/<tên-tính-năng>` — nhánh phát triển tính năng mới
- `fix/<tên-lỗi>` — nhánh sửa lỗi

### Quy trình đóng góp

1. Fork repository
2. Tạo nhánh mới từ `main`: `git checkout -b feat/ten-tinh-nang`
3. Thực hiện thay đổi và commit theo quy ước
4. Push nhánh lên GitHub
5. Tạo Pull Request về nhánh `main`
