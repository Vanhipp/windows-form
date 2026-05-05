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
        private string _viewingMasterId = null; // ID sách gốc đang được xem chi tiết
        public FrmBookEntry()
        {
            InitializeComponent();
            Load += FrmBookEntry_Load;
            btnLuu.Click += btnLuu_Click;
            btnDong.Click += btnDong_Click;
            btnBack.Click += btnBack_Click;
        }

        private void FrmBookEntry_Load(object sender, EventArgs e)
        {
            EnsureDatabase();
            EnsureDauSachData();
            LoadCategories();
            SetupGridColumns();
            LoadData();
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

                DatabaseHelper.ExecuteNonQuery(@"
                IF EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_DauSach_TheLoai')
                BEGIN
                    ALTER TABLE DauSach DROP CONSTRAINT FK_DauSach_TheLoai
                END", null);

                // Thêm cột SoLuong nếu chưa có
                DatabaseHelper.ExecuteNonQuery(@"
                IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('ThongTinSach') AND name = 'SoLuong')
                BEGIN
                    ALTER TABLE ThongTinSach ADD SoLuong int NOT NULL DEFAULT ((0))
                END", null);
            }
            catch { }
        }

        private void EnsureDauSachData()
        {
            try
            {
                string[] names = { "Mathematics", "Physics", "Chemistry", "Biology", "History", "Geography", "English", "Literature" };
                for (int i = 0; i < names.Length; i++)
                {
                    string name = names[i];
                    string newId = "DS" + (i + 1).ToString("D2");
                    
                    // Kiểm tra xem môn này đã tồn tại chưa (theo tên hoặc ID mới)
                    var dtCheck = DatabaseHelper.ExecuteQuery("SELECT IDDauSach FROM DauSach WHERE TenDauSach = @name OR IDDauSach = @id", 
                        new[] { new SqlParameter("@name", name), new SqlParameter("@id", newId) });
                    
                    if (dtCheck.Rows.Count > 0)
                    {
                        string oldId = dtCheck.Rows[0]["IDDauSach"].ToString().Trim();
                        if (oldId != newId)
                        {
                            // Cập nhật ID cũ thành ID mới (cần tắt constraint nếu có hoặc cập nhật cả bảng liên quan)
                            // Ở đây ta đơn giản là cập nhật IDDauSach trong cả 2 bảng
                            DatabaseHelper.ExecuteNonQuery("UPDATE ThongTinSach SET IDDauSach = @newId WHERE IDDauSach = @oldId",
                                new[] { new SqlParameter("@newId", newId), new SqlParameter("@oldId", oldId) });
                            DatabaseHelper.ExecuteNonQuery("UPDATE DauSach SET IDDauSach = @newId WHERE IDDauSach = @oldId",
                                new[] { new SqlParameter("@newId", newId), new SqlParameter("@oldId", oldId) });
                        }
                    }
                    else
                    {
                        // Thêm mới nếu chưa có
                        DatabaseHelper.ExecuteNonQuery(
                            "INSERT INTO DauSach (IDDauSach, IDTheLoai, TenDauSach) VALUES (@id, NULL, @name)",
                            new[] { new SqlParameter("@id", newId), new SqlParameter("@name", name) });
                    }
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
            if (!string.IsNullOrEmpty(_editingId))
            {
                // Nếu đang sửa một cuốn cụ thể, nhấn Đóng sẽ hủy việc sửa và quay về chế độ "Thêm mới/Thêm bản sao"
                if (!string.IsNullOrEmpty(_viewingMasterId))
                {
                    LoadMasterInfoIntoFields(_viewingMasterId);
                }
                else
                {
                    _editingId = null;
                    btnLuu.Text = "Lưu";
                    txtSoLuong.Enabled = true;
                }
                return;
            }

            // Nếu không đang sửa, thì xóa trắng để nhập mới hoàn toàn
            txtTenSach.Text = string.Empty;
            txtTacGia.Text = string.Empty;
            txtNhaXuatBan.Text = string.Empty;
            txtNamXuatBan.Text = string.Empty;
            txtGiaBan.Text = string.Empty;
            txtGiaThue.Text = string.Empty;
            txtSoLuong.Value = 1;
            dtpNgayNhap.Value = DateTime.Now;
            if (cboTheLoai.Items.Count > 0) cboTheLoai.SelectedIndex = 0;
            
            _editingId = null;
            _viewingMasterId = null; // Thoát khỏi chế độ xem chi tiết
            btnLuu.Text = "Lưu";
            txtSoLuong.Enabled = true;
            
            SetupGridColumns();
            LoadData();
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

            // QĐ3: Chỉ nhận các sách xuất bản trong vòng 8 năm
            if (DateTime.Now.Year - namXB > 8)
            {
                MessageBox.Show("Chỉ nhận các sách xuất bản trong vòng 8 năm gần đây!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNamXuatBan.Focus();
                return;
            }

            if (!decimal.TryParse(txtGiaBan.Text.Trim(), out decimal giaBan) || giaBan < 0)
            {
                MessageBox.Show("Giá bán (trị giá) không hợp lệ.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtGiaBan.Focus();
                return;
            }

            decimal giaThue = 0;
            if (!string.IsNullOrWhiteSpace(txtGiaThue.Text.Trim()))
            {
                if (!decimal.TryParse(txtGiaThue.Text.Trim(), out giaThue) || giaThue < 0)
                {
                    MessageBox.Show("Giá thuê không hợp lệ.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtGiaThue.Focus();
                    return;
                }
            }

            try
            {
                int quantity = (int)txtSoLuong.Value;
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
                    // CHẾ ĐỘ SỬA: Chỉ sửa thông tin của 1 cuốn cụ thể hoặc đầu sách
                    DatabaseHelper.ExecuteWithTransaction(cmd =>
                    {
                        // 1. Update ThongTinSach (Dùng IDSach từ _editingId - thường là S001_0001)
                        // Lấy ID gốc (S001) từ mã con (S001_0001)
                        string masterId = _editingId.Contains("_") ? _editingId.Split('_')[0] : _editingId;

                        cmd.CommandText = "UPDATE ThongTinSach SET TenSach=@TenSach, TacGia=@TacGia, NhaXuatBan=@NhaXuatBan, NamXuatBan=@NamXuatBan, GiaBan=@GiaBan, GiaThue=@GiaThue, IDDauSach=@IDDauSach WHERE IDSach=@masterId";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@TenSach", book.TenSach);
                        cmd.Parameters.AddWithValue("@TacGia", book.TacGia);
                        cmd.Parameters.AddWithValue("@NhaXuatBan", book.NhaXuatBan);
                        cmd.Parameters.AddWithValue("@NamXuatBan", book.NamXuatBan);
                        cmd.Parameters.AddWithValue("@GiaBan", book.GiaBan);
                        cmd.Parameters.AddWithValue("@GiaThue", giaThue);
                        cmd.Parameters.AddWithValue("@IDDauSach", (object)book.IDDauSach ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@masterId", masterId);
                        cmd.ExecuteNonQuery();

                        // 2. Update CaTheSach (Cập nhật ngày nhập cho cuốn đang chọn)
                        cmd.CommandText = "UPDATE CaTheSach SET NgayNhap=@NgayNhap, TenCaTheSach=@TenSach WHERE IDCaTheSach=@id";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@NgayNhap", book.NgayNhap.Date);
                        cmd.Parameters.AddWithValue("@TenSach", book.TenSach);
                        cmd.Parameters.AddWithValue("@id", _editingId);
                        cmd.ExecuteNonQuery();
                    });

                    MessageBox.Show("Cập nhật thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // CHẾ ĐỘ THÊM MỚI (Hỗ trợ nhập số lượng)
                    string masterId = "";
                    
                    // Kiểm tra xem Đầu Sách đã tồn tại chưa
                    var dtMaster = DatabaseHelper.ExecuteQuery(
                        "SELECT IDSach FROM ThongTinSach WHERE TenSach=@t AND TacGia=@tg AND NamXuatBan=@n",
                        new[] { 
                            new SqlParameter("@t", book.TenSach), 
                            new SqlParameter("@tg", book.TacGia),
                            new SqlParameter("@n", book.NamXuatBan)
                        });

                    DatabaseHelper.ExecuteWithTransaction(cmd =>
                    {
                        if (dtMaster.Rows.Count > 0)
                        {
                            // Sách ĐÃ CÓ -> Lấy ID cũ
                            masterId = dtMaster.Rows[0]["IDSach"].ToString().Trim();
                            
                            // Cập nhật số lượng tổng trong ThongTinSach
                            cmd.CommandText = "UPDATE ThongTinSach SET SoLuong = SoLuong + @q WHERE IDSach = @mid";
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@q", quantity);
                            cmd.Parameters.AddWithValue("@mid", masterId);
                            cmd.ExecuteNonQuery();
                        }
                        else
                        {
                            // Sách MỚI HOÀN TOÀN -> Tạo ID gốc mới (S001)
                            masterId = GenerateNewMasterId();
                            
                            cmd.CommandText = "INSERT INTO ThongTinSach (IDSach, TenSach, TacGia, NhaXuatBan, NamXuatBan, GiaBan, GiaThue, IDDauSach, SoLuong) " +
                                             "VALUES (@mid, @TenSach, @TacGia, @NhaXuatBan, @NamXuatBan, @GiaBan, @GiaThue, @IDDauSach, @q);";
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@mid", masterId);
                            cmd.Parameters.AddWithValue("@TenSach", book.TenSach);
                            cmd.Parameters.AddWithValue("@TacGia", book.TacGia);
                            cmd.Parameters.AddWithValue("@NhaXuatBan", book.NhaXuatBan);
                            cmd.Parameters.AddWithValue("@NamXuatBan", book.NamXuatBan);
                            cmd.Parameters.AddWithValue("@GiaBan", book.GiaBan);
                            cmd.Parameters.AddWithValue("@GiaThue", giaThue);
                            cmd.Parameters.AddWithValue("@IDDauSach", (object)book.IDDauSach ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@q", quantity);
                            cmd.ExecuteNonQuery();
                        }

                        // Lấy STT lớn nhất hiện tại của các cuốn con
                        int maxSeq = GetMaxSequence(masterId);

                        // Vòng lặp tạo các cuốn con (CaTheSach)
                        for (int i = 1; i <= quantity; i++)
                        {
                            string childId = $"{masterId}_{ (maxSeq + i).ToString("D4") }";
                            
                            cmd.CommandText = "INSERT INTO CaTheSach (IDCaTheSach, TenCaTheSach, IDSach, NgayNhap, TinhTrang) " +
                                             "VALUES (@cid, @TenSach, @mid, @NgayNhap, N'Sẵn sàng');";
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@cid", childId);
                            cmd.Parameters.AddWithValue("@TenSach", book.TenSach);
                            cmd.Parameters.AddWithValue("@mid", masterId);
                            cmd.Parameters.AddWithValue("@NgayNhap", book.NgayNhap.Date);
                            cmd.ExecuteNonQuery();
                        }
                    });

                    MessageBox.Show($"Đã nhập thêm {quantity} cuốn sách thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                RefreshAllForms();
                BookSaved?.Invoke(this, EventArgs.Empty);
                
                if (string.IsNullOrEmpty(_editingId))
                {
                    var result = MessageBox.Show("Bạn có muốn tiếp tục nhập thêm sách khác?", "Tiếp tục?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes) btnDong_Click(null, null);
                    else this.Close();
                }
                else this.Close();
            }
            catch (Exception ex)
            {
                // Nếu lỗi do duplicate key S001, ta thử gợi ý người dùng hoặc tự động thử lại với ID khác
                if (ex.Message.Contains("duplicate key") || ex.Message.Contains("PRIMARY KEY"))
                {
                    MessageBox.Show("Lỗi trùng mã ID. Hệ thống sẽ thử cập nhật lại mã ID mới nhất. Vui lòng thử Lưu lại lần nữa.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private string GenerateNewMasterId()
        {
            // Lấy tất cả ID gốc (dạng Sxxx)
            var dt = DatabaseHelper.ExecuteQuery("SELECT IDSach FROM ThongTinSach");
            int max = 0;
            foreach (DataRow r in dt.Rows)
            {
                string id = r["IDSach"].ToString().Trim();
                // Nếu có ID con (S001_0001) thì lấy phần đầu
                if (id.Contains("_")) id = id.Split('_')[0];
                
                if (id.StartsWith("S") && int.TryParse(id.Substring(1), out int val))
                {
                    if (val > max) max = val;
                }
            }
            return "S" + (max + 1).ToString("D3");
        }

        private int GetMaxSequence(string masterId)
        {
            string prefix = masterId + "_";
            var dt = DatabaseHelper.ExecuteQuery("SELECT IDCaTheSach FROM CaTheSach WHERE IDCaTheSach LIKE @p", 
                new[] { new SqlParameter("@p", prefix + "%") });
            
            int max = 0;
            foreach (DataRow r in dt.Rows)
            {
                string id = r["IDCaTheSach"].ToString().Trim();
                string seqStr = id.Substring(prefix.Length);
                if (int.TryParse(seqStr, out int val))
                {
                    if (val > max) max = val;
                }
            }
            return max;
        }

        private void SetupGridColumns()
        {
            dgvBooks.Columns.Clear();
            dgvBooks.AutoGenerateColumns = false;

            if (_viewingMasterId == null)
            {
                // CHẾ ĐỘ MASTER: Hiển thị ThongTinSach
                dgvBooks.Columns.Add("IDSach", "Mã Sách");
                dgvBooks.Columns.Add("TenSach", "Tên Sách");
                dgvBooks.Columns.Add("TacGia", "Tác Giả");
                dgvBooks.Columns.Add("NhaXuatBan", "Nhà XB");
                dgvBooks.Columns.Add("NamXuatBan", "Năm XB");
                dgvBooks.Columns.Add("SoLuong", "SL");
                dgvBooks.Columns.Add("TheLoai", "Đầu Sách");

                dgvBooks.Columns["IDSach"].DataPropertyName = "IDSach";
                dgvBooks.Columns["TenSach"].DataPropertyName = "TenSach";
                dgvBooks.Columns["TacGia"].DataPropertyName = "TacGia";
                dgvBooks.Columns["NhaXuatBan"].DataPropertyName = "NhaXuatBan";
                dgvBooks.Columns["NamXuatBan"].DataPropertyName = "NamXuatBan";
                dgvBooks.Columns["SoLuong"].DataPropertyName = "SoLuong";
                dgvBooks.Columns["TheLoai"].DataPropertyName = "TheLoai";
                
                btnBack.Visible = false;
            }
            else
            {
                // CHẾ ĐỘ DETAIL: Hiển thị CaTheSach của 1 cuốn
                dgvBooks.Columns.Add("IDCaTheSach", "Mã Cá Thể");
                dgvBooks.Columns.Add("TenCaTheSach", "Tên Sách");
                dgvBooks.Columns.Add("NgayNhap", "Ngày Nhập");
                dgvBooks.Columns.Add("TinhTrang", "Tình Trạng");

                dgvBooks.Columns["IDCaTheSach"].DataPropertyName = "IDCaTheSach";
                dgvBooks.Columns["TenCaTheSach"].DataPropertyName = "TenCaTheSach";
                dgvBooks.Columns["NgayNhap"].DataPropertyName = "NgayNhap";
                dgvBooks.Columns["TinhTrang"].DataPropertyName = "TinhTrang";

                btnBack.Visible = true;
                btnBack.Text = "← Quay lại: " + _viewingMasterId;
            }

            dgvBooks.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvBooks.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBooks.MultiSelect = false;
            dgvBooks.ReadOnly = true;
            dgvBooks.DoubleClick -= dgvBooks_DoubleClick;
            dgvBooks.DoubleClick += dgvBooks_DoubleClick;
        }

        private void dgvBooks_DoubleClick(object sender, EventArgs e)
        {
            if (dgvBooks.SelectedRows.Count > 0)
            {
                if (_viewingMasterId == null)
                {
                    // TỪ MASTER -> DETAIL
                    _viewingMasterId = dgvBooks.SelectedRows[0].Cells["IDSach"].Value?.ToString();
                    
                    // Nạp thông tin sách lên panel nhập liệu để người dùng có thể thêm số lượng (nhận sách)
                    LoadMasterInfoIntoFields(_viewingMasterId);
                    
                    SetupGridColumns();
                    LoadData();
                }
                else
                {
                    // TỪ DETAIL -> MỞ FORM SỬA CÁ THỂ
                    var id = dgvBooks.SelectedRows[0].Cells["IDCaTheSach"].Value?.ToString();
                    OpenForEdit(id);
                }
            }
        }

        private void LoadMasterInfoIntoFields(string masterId)
        {
            try
            {
                var dt = DatabaseHelper.ExecuteQuery("SELECT * FROM ThongTinSach WHERE IDSach = @id", 
                    new[] { new SqlParameter("@id", masterId) });
                
                if (dt.Rows.Count > 0)
                {
                    var r = dt.Rows[0];
                    txtTenSach.Text = r["TenSach"]?.ToString().Trim();
                    txtTacGia.Text = r["TacGia"]?.ToString().Trim();
                    txtNhaXuatBan.Text = r["NhaXuatBan"]?.ToString().Trim();
                    txtNamXuatBan.Text = r["NamXuatBan"]?.ToString().Trim();
                    txtGiaBan.Text = r["GiaBan"]?.ToString().Trim();
                    txtGiaThue.Text = r.Table.Columns.Contains("GiaThue") ? r["GiaThue"]?.ToString().Trim() : "0";
                    
                    // Chọn thể loại (đầu sách) tương ứng
                    var catId = r.Table.Columns.Contains("IDDauSach") ? r["IDDauSach"]?.ToString().Trim() : null;
                    if (!string.IsNullOrWhiteSpace(catId))
                    {
                        for (int i = 0; i < cboTheLoai.Items.Count; i++)
                        {
                            if (cboTheLoai.Items[i] is CategoryItem item && item.Id == catId)
                            {
                                cboTheLoai.SelectedIndex = i;
                                break;
                            }
                        }
                    }

                    txtSoLuong.Value = 1;
                    txtSoLuong.Enabled = true;
                    _editingId = null; // Để chế độ thêm mới (nhưng sẽ tự khớp với ID gốc cũ)
                    btnLuu.Text = "Thêm bản sao";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi nạp master info: " + ex.Message);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            _viewingMasterId = null;
            SetupGridColumns();
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                DataTable dt;
                if (_viewingMasterId == null)
                {
                    // LOAD MASTER
                    string sql = @"SELECT s.IDSach, s.TenSach, s.TacGia, s.NhaXuatBan, s.NamXuatBan, s.SoLuong, 
                                 d.TenDauSach AS TheLoai
                                 FROM ThongTinSach s 
                                 LEFT JOIN DauSach d ON s.IDDauSach = d.IDDauSach 
                                 ORDER BY s.IDSach DESC";
                    dt = DatabaseHelper.ExecuteQuery(sql);
                }
                else
                {
                    // LOAD DETAIL
                    string sql = @"SELECT IDCaTheSach, TenCaTheSach, NgayNhap, TinhTrang 
                                 FROM CaTheSach 
                                 WHERE IDSach = @mid 
                                 ORDER BY IDCaTheSach ASC";
                    dt = DatabaseHelper.ExecuteQuery(sql, new[] { new SqlParameter("@mid", _viewingMasterId) });
                }
                
                var dtCount = DatabaseHelper.ExecuteQuery("SELECT COUNT(*) FROM CaTheSach", null);
                if (dtCount.Rows.Count > 0)
                {
                    lblTotalBooksValue.Text = dtCount.Rows[0][0].ToString();
                }

                dgvBooks.Rows.Clear();
                foreach (DataRow r in dt.Rows)
                {
                    if (_viewingMasterId == null)
                    {
                        dgvBooks.Rows.Add(
                            r["IDSach"]?.ToString()?.Trim(),
                            r["TenSach"]?.ToString()?.Trim(),
                            r["TacGia"]?.ToString()?.Trim(),
                            r["NhaXuatBan"]?.ToString()?.Trim(),
                            r["NamXuatBan"],
                            r["SoLuong"],
                            r["TheLoai"]?.ToString()?.Trim());
                    }
                    else
                    {
                        string status = GetStatusText(r["TinhTrang"]?.ToString());
                        dgvBooks.Rows.Add(
                            r["IDCaTheSach"]?.ToString()?.Trim(),
                            r["TenCaTheSach"]?.ToString()?.Trim(),
                            Convert.ToDateTime(r["NgayNhap"]).ToString("dd/MM/yyyy"),
                            status);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi load grid: " + ex.Message);
            }
        }

        private string GetStatusText(string status)
        {
            if (string.IsNullOrWhiteSpace(status)) return status;
            var normalized = status.Trim().ToLower();
            switch (normalized)
            {
                case "ok":
                case "sẵn sàng":
                case "ready":
                case "san sang":
                    return "Sẵn sàng";
                case "đang mượn":
                case "muon":
                case "borrowed":
                case "dang muon":
                    return "Đang mượn";
                case "hỏng":
                case "hong":
                case "damaged":
                    return "Hỏng";
                case "mất":
                case "mat":
                case "lost":
                case "lost by user":
                case "lostbyuser":
                    return "Mất";
                case "người dùng làm mất":
                    return "Mất";
                default:
                    return status;
            }
        }

        private void RefreshAllForms()
        {
            LoadData(); 
            foreach (Form frm in Application.OpenForms)
            {
                if (frm is FrmSearchBook searchForm) searchForm.RefreshData();
                if (frm is FrmLiquidation liqForm) liqForm.RefreshData();
            }
        }

        public void OpenForEdit(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) return;
            try
            {
                var p = new SqlParameter[] { new SqlParameter("@id", id) };
                string sql = "SELECT s.*, c.NgayNhap, c.IDCaTheSach FROM ThongTinSach s JOIN CaTheSach c ON s.IDSach = c.IDSach WHERE c.IDCaTheSach = @id";
                var dt = DatabaseHelper.ExecuteQuery(sql, p);
                if (dt.Rows.Count == 0) return;
                var r = dt.Rows[0];
                
                _editingId = r["IDCaTheSach"]?.ToString().Trim();
                txtTenSach.Text = r["TenSach"]?.ToString().Trim();
                txtTacGia.Text = r["TacGia"]?.ToString().Trim();
                txtNhaXuatBan.Text = r["NhaXuatBan"]?.ToString().Trim();
                txtNamXuatBan.Text = r["NamXuatBan"]?.ToString().Trim();
                if (DateTime.TryParse(r["NgayNhap"]?.ToString(), out var dn)) dtpNgayNhap.Value = dn;
                txtGiaBan.Text = r["GiaBan"]?.ToString().Trim();
                txtGiaThue.Text = r.Table.Columns.Contains("GiaThue") ? r["GiaThue"]?.ToString().Trim() : "0";
                
                txtSoLuong.Value = 1;
                txtSoLuong.Enabled = false;

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
