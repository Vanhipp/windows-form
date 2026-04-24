using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyThuVien.Enums;
using QuanLyThuVien.Models;
using QuanLyThuVien.Helpers;
using System.Data.SqlClient;

namespace QuanLyThuVien
{
    public partial class FrmBookEntry : Form
    {
        public event EventHandler BookSaved;
        private string _editingId = null;
        public FrmBookEntry()
        {
            InitializeComponent();
            Load += FrmBookEntry_Load;
            btnLuu.Click += btnLuu_Click;
            btnDong.Click += btnDong_Click;
        }

        private void FrmBookEntry_Load(object sender, EventArgs e)
        {
            EnsureDatabase();
            EnsureDauSachData();
            LoadCategories();
        }

        private void EnsureDatabase()
        {
            try
            {
                // Thêm cột GiaThue nếu chưa có
                DatabaseHelper.ExecuteNonQuery(@"
                IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('ThongTinSach') AND name = 'GiaThue')
                BEGIN
                    ALTER TABLE ThongTinSach ADD GiaThue money NOT NULL DEFAULT ((0))
                END", null);

                // Xóa constraint năm xuất bản sai
                DatabaseHelper.ExecuteNonQuery(@"
                IF EXISTS (SELECT * FROM sys.check_constraints WHERE name = 'CK_Sach_NamXuatBan')
                BEGIN
                    ALTER TABLE ThongTinSach DROP CONSTRAINT CK_Sach_NamXuatBan
                END", null);

                // Sửa IDTheLoai cho phép NULL
                DatabaseHelper.ExecuteNonQuery(@"
                IF EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('DauSach') AND name = 'IDTheLoai' AND is_nullable = 0)
                BEGIN
                    ALTER TABLE DauSach ALTER COLUMN IDTheLoai nchar(10) NULL
                END", null);

                // Bỏ FK DauSach -> TheLoai nếu gây lỗi
                DatabaseHelper.ExecuteNonQuery(@"
                IF EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_DauSach_TheLoai')
                BEGIN
                    ALTER TABLE DauSach DROP CONSTRAINT FK_DauSach_TheLoai
                END", null);
            }
            catch { }
        }

        private void EnsureDauSachData()
        {
            try
            {
                // Chỉ insert nếu chưa có dữ liệu
                var dt = DatabaseHelper.ExecuteQuery("SELECT COUNT(*) AS cnt FROM DauSach", null);
                if (dt.Rows.Count > 0 && Convert.ToInt32(dt.Rows[0]["cnt"]) > 0)
                    return;

                string[] names = { "Mathematics", "Physics", "Chemistry", "Biology", "History", "Geography", "English", "Literature" };
                for (int i = 0; i < names.Length; i++)
                {
                    string name = names[i];
                    DatabaseHelper.ExecuteNonQuery(
                        "INSERT INTO DauSach (IDDauSach, IDTheLoai, TenDauSach) VALUES (@id, NULL, @name)",
                        new[] { new SqlParameter("@id", name), new SqlParameter("@name", name) });
                }
            }
            catch { }
        }

        private void LoadCategories()
        {
            try
            {
                cboTheLoai.Items.Clear();
                var dt = DatabaseHelper.ExecuteQuery("SELECT IDDauSach, TenDauSach FROM DauSach", null);

                foreach (DataRow r in dt.Rows)
                {
                    cboTheLoai.Items.Add(new CategoryItem(r["IDDauSach"]?.ToString().Trim(), r["TenDauSach"]?.ToString().Trim()));
                }

                if (cboTheLoai.Items.Count > 0) 
                    cboTheLoai.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load danh mục: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private class CategoryItem
        {
            public string Id { get; }
            public string Name { get; }
            public CategoryItem(string id, string name) { Id = id; Name = name; }
            public override string ToString() => Name ?? "";
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            txtTenSach.Text = string.Empty;
            txtTacGia.Text = string.Empty;
            txtNhaXuatBan.Text = string.Empty;
            txtNamXuatBan.Text = string.Empty;
            txtGiaBan.Text = string.Empty;
            txtGiaThue.Text = string.Empty;
            dtpNgayNhap.Value = DateTime.Now;
            if (cboTheLoai.Items.Count > 0) cboTheLoai.SelectedIndex = 0;
            _editingId = null;
            btnLuu.Text = "Lưu";
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenSach.Text))
            {
                MessageBox.Show("Vui lòng nhập tên sách.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenSach.Focus();
                return;
            }

            if (!int.TryParse(txtNamXuatBan.Text.Trim(), out int namXB) || namXB < 1000 || namXB > DateTime.Now.Year)
            {
                MessageBox.Show("Năm xuất bản không hợp lệ.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNamXuatBan.Focus();
                return;
            }

            if (!int.TryParse(txtGiaBan.Text.Trim(), out int giaBan) || giaBan < 0)
            {
                MessageBox.Show("Giá bán (trị giá) không hợp lệ.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtGiaBan.Focus();
                return;
            }

            int giaThue = 0;
            if (!string.IsNullOrWhiteSpace(txtGiaThue.Text.Trim()))
            {
                if (!int.TryParse(txtGiaThue.Text.Trim(), out giaThue) || giaThue < 0)
                {
                    MessageBox.Show("Giá thuê không hợp lệ.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtGiaThue.Focus();
                    return;
                }
            }

            try
            {
                var book = new Book
                {
                    TenSach = txtTenSach.Text.Trim(),
                    TacGia = txtTacGia.Text.Trim(),
                    NhaXuatBan = txtNhaXuatBan.Text.Trim(),
                    NgayNhap = dtpNgayNhap.Value,
                    NamXuatBan = namXB,
                    GiaBan = giaBan,
                    IDDauSach = (cboTheLoai.SelectedItem is CategoryItem cat) ? cat.Id : null
                };

                if (!string.IsNullOrEmpty(_editingId))
                {
                    string updateSql = "UPDATE ThongTinSach SET TenSach=@TenSach, TacGia=@TacGia, NhaXuatBan=@NhaXuatBan, NamXuatBan=@NamXuatBan, NgayNhap=@NgayNhap, GiaBan=@GiaBan, GiaThue=@GiaThue, IDDauSach=@IDDauSach WHERE IDSach=@id";
                    var updateParams = new SqlParameter[]
                    {
                        new SqlParameter("@TenSach", book.TenSach),
                        new SqlParameter("@TacGia", book.TacGia),
                        new SqlParameter("@NhaXuatBan", book.NhaXuatBan),
                        new SqlParameter("@NamXuatBan", book.NamXuatBan),
                        new SqlParameter("@NgayNhap", book.NgayNhap.Date),
                        new SqlParameter("@GiaBan", book.GiaBan),
                        new SqlParameter("@GiaThue", giaThue),
                        new SqlParameter("@IDDauSach", (object)book.IDDauSach ?? DBNull.Value),
                        new SqlParameter("@id", _editingId)
                    };

                    DatabaseHelper.ExecuteNonQuery(updateSql, updateParams);
                    MessageBox.Show("Cập nhật sách thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RefreshAllForms();
                    BookSaved?.Invoke(this, EventArgs.Empty);
                    this.Close();
                }
                else
                {
                    var newId = GenerateNewBookId();
                    string sql = "INSERT INTO ThongTinSach (IDSach, TenSach, TacGia, NhaXuatBan, NamXuatBan, NgayNhap, GiaBan, GiaThue, IDDauSach, TinhTrang) " +
                                 "VALUES (@IDSach, @TenSach, @TacGia, @NhaXuatBan, @NamXuatBan, @NgayNhap, @GiaBan, @GiaThue, @IDDauSach, N'Sẵn sàng');";

                    var parameters = new SqlParameter[]
                    {
                        new SqlParameter("@IDSach", newId),
                        new SqlParameter("@TenSach", book.TenSach),
                        new SqlParameter("@TacGia", book.TacGia),
                        new SqlParameter("@NhaXuatBan", book.NhaXuatBan),
                        new SqlParameter("@NamXuatBan", (object)book.NamXuatBan),
                        new SqlParameter("@NgayNhap", (object)book.NgayNhap.Date),
                        new SqlParameter("@GiaBan", (object)book.GiaBan),
                        new SqlParameter("@GiaThue", giaThue),
                        new SqlParameter("@IDDauSach", (object)book.IDDauSach ?? DBNull.Value)
                    };

                    DatabaseHelper.ExecuteNonQuery(sql, parameters);
                    MessageBox.Show($"Thêm sách thành công. Mã sách: {newId}", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RefreshAllForms();
                    BookSaved?.Invoke(this, EventArgs.Empty);

                    var result = MessageBox.Show("Bạn có muốn tiếp tục nhập thêm sách mới?", "Tiếp tục?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        btnDong_Click(null, null);
                    }
                    else
                    {
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu dữ liệu: " + ex.Message, "Lỗi DB", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RefreshAllForms()
        {
            foreach (Form frm in Application.OpenForms)
            {
                if (frm is FrmSearchBook searchForm)
                {
                    searchForm.RefreshData();
                }
                if (frm is FrmLiquidation liqForm)
                {
                    liqForm.RefreshData();
                }
            }
        }

        private string GenerateNewBookId()
        {
            try
            {
                var dt = DatabaseHelper.ExecuteQuery("SELECT IDSach FROM ThongTinSach", null);
                int maxId = 0;
                foreach (DataRow row in dt.Rows)
                {
                    string idStr = row["IDSach"]?.ToString().Trim();
                    if (!string.IsNullOrWhiteSpace(idStr))
                    {
                        // Lấy riêng phần số từ chuỗi (ví dụ: S015 -> 15, 3 -> 3)
                        string numPart = new string(idStr.Where(char.IsDigit).ToArray());
                        if (int.TryParse(numPart, out int num))
                        {
                            if (num > maxId)
                            {
                                maxId = num;
                            }
                        }
                    }
                }
                return "S" + (maxId + 1).ToString("D3");
            }
            catch { }
            return "S001";
        }

        public void OpenForEdit(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) return;
            try
            {
                var p = new SqlParameter[] { new SqlParameter("@id", id) };
                var dt = DatabaseHelper.ExecuteQuery("SELECT TOP 1 * FROM ThongTinSach WHERE IDSach = @id", p);
                if (dt.Rows.Count == 0) return;
                var r = dt.Rows[0];
                _editingId = r["IDSach"]?.ToString().Trim();
                txtTenSach.Text = r["TenSach"]?.ToString().Trim();
                txtTacGia.Text = r["TacGia"]?.ToString().Trim();
                txtNhaXuatBan.Text = r["NhaXuatBan"]?.ToString().Trim();
                txtNamXuatBan.Text = r["NamXuatBan"]?.ToString().Trim();
                if (DateTime.TryParse(r["NgayNhap"]?.ToString(), out var dn)) dtpNgayNhap.Value = dn;
                txtGiaBan.Text = r["GiaBan"]?.ToString().Trim();
                txtGiaThue.Text = r.Table.Columns.Contains("GiaThue") ? r["GiaThue"]?.ToString().Trim() : "0";
                var cat = r.Table.Columns.Contains("IDDauSach") ? r["IDDauSach"]?.ToString().Trim() : null;
                if (!string.IsNullOrWhiteSpace(cat))
                {
                    for (int i = 0; i < cboTheLoai.Items.Count; i++)
                    {
                        if (cboTheLoai.Items[i] is CategoryItem item && item.Id == cat)
                        {
                            cboTheLoai.SelectedIndex = i;
                            break;
                        }
                    }
                }
                btnLuu.Text = "Cập nhật";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể nạp thông tin sách: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
