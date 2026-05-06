using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyThuVien.Helpers;
using System.Data.SqlClient;

namespace QuanLyThuVien
{
    public partial class FrmSearchBook : Form
    {
        string borrowStatusClause;
        private string _currentMasterId = null; // Tracks the current master ID for detail view

        public FrmSearchBook()
        {
            InitializeComponent();
            Load += FrmSearchBook_Load;
            btnTimKiem.Click += Button1_Click;
            btnTatCa.Click += Button2_Click;
            btnSanSang.Click += Button3_Click;
            btnDangMuon.Click += Button4_Click;
            cboTheLoaiFilter.SelectedIndexChanged += CboTheLoaiFilter_SelectedIndexChanged;
        }

        private void CboTheLoaiFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void FrmSearchBook_Load(object sender, EventArgs e)
        {
            LoadCategories();
            SetupGridColumnsThongTinSach(); // Set up columns for master view
            LoadDataThongTinSach(); // Load master data initially
            btnBack.Visible = false; // Hide btnBack in master view
        }

        private void LoadCategories()
        {
            try
            {
                cboTheLoaiFilter.SelectedIndexChanged -= CboTheLoaiFilter_SelectedIndexChanged;

                cboTheLoaiFilter.Items.Clear();
                cboTheLoaiFilter.Items.Add("Tất cả");

                var dt = DatabaseHelper.ExecuteQuery("SELECT IDDauSach, TenDauSach FROM DauSach", null);

                foreach (DataRow r in dt.Rows)
                {
                    cboTheLoaiFilter.Items.Add(new CategoryItem(r["IDDauSach"]?.ToString().Trim(), r["TenDauSach"]?.ToString().Trim()));
                }

                cboTheLoaiFilter.SelectedIndex = 0;

                cboTheLoaiFilter.SelectedIndexChanged += CboTheLoaiFilter_SelectedIndexChanged;
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

        private void SetupGridColumnsThongTinSach()
        {
            dgvBooks.Columns.Clear();
            dgvBooks.AutoGenerateColumns = false;
            dgvBooks.Columns.Add("IDSach", "Mã sách");
            dgvBooks.Columns.Add("TenSach", "Tên sách");
            dgvBooks.Columns.Add("TacGia", "Tác giả");
            dgvBooks.Columns.Add("NhaXuatBan", "Nhà xuất bản");
            dgvBooks.Columns.Add("NamXuatBan", "Năm XB");
            dgvBooks.Columns.Add("GiaBan", "Trị giá");
            dgvBooks.Columns.Add("GiaThue", "Giá thuê");
            dgvBooks.Columns.Add("DauSach", "Đầu Sách");
            dgvBooks.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBooks.MultiSelect = false;
            dgvBooks.DoubleClick += DataGridView1_DoubleClick;
        }

        private void SetupGridColumnsCaTheSach()
        {
            dgvBooks.Columns.Clear();
            dgvBooks.AutoGenerateColumns = false;
            dgvBooks.Columns.Add("IDCaTheSach", "Mã cá thể sách");
            dgvBooks.Columns.Add("IDSach", "Mã sách");
            dgvBooks.Columns.Add("TenSach", "Tên sách"); // Add TenSach explicitly
            dgvBooks.Columns.Add("TinhTrang", "Tình trạng");
            dgvBooks.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBooks.MultiSelect = false;
        }

        private void LoadDataThongTinSach()
        {
            try
            {
                string sql = "SELECT s.IDSach, s.TenSach, s.TacGia, s.NhaXuatBan, s.NamXuatBan, s.GiaBan, s.GiaThue, ISNULL(d.TenDauSach, s.IDDauSach) AS DauSach " +
                             "FROM ThongTinSach s " +
                             "LEFT JOIN DauSach d ON s.IDDauSach = d.IDDauSach";
                var dt = DatabaseHelper.ExecuteQuery(sql, null);
                dgvBooks.Rows.Clear();
                foreach (DataRow r in dt.Rows)
                {
                    dgvBooks.Rows.Add(
                        r["IDSach"]?.ToString()?.Trim(),
                        r["TenSach"]?.ToString()?.Trim(),
                        r["TacGia"]?.ToString()?.Trim(),
                        r["NhaXuatBan"]?.ToString()?.Trim(),
                        r["NamXuatBan"],
                        r["GiaBan"],
                        r["GiaThue"],
                        r["DauSach"]?.ToString()?.Trim()
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể truy vấn dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void LoadDataCaTheSach(string idSach)
        {
            try
            {
                string sql = "SELECT c.IDCaTheSach, c.IDSach, s.TenSach, c.TinhTrang " +
                             "FROM CaTheSach c " +
                             "INNER JOIN ThongTinSach s ON c.IDSach = s.IDSach " +
                             "WHERE c.IDSach = @idSach";
                var parameters = new SqlParameter[] { new SqlParameter("@idSach", idSach) };
                var dt = DatabaseHelper.ExecuteQuery(sql, parameters);
                dgvBooks.Rows.Clear();
                foreach (DataRow r in dt.Rows)
                {
                    dgvBooks.Rows.Add(
                        r["IDCaTheSach"]?.ToString()?.Trim(),
                        r["IDSach"]?.ToString()?.Trim(),
                        r["TenSach"]?.ToString()?.Trim(),
                        r["TinhTrang"]?.ToString()?.Trim()
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể truy vấn dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void LoadData(string whereClause = null, SqlParameter[] parameters = null)
        {
            try
            {
                // Kiểm tra cột GiaThue có tồn tại không
                bool hasGiaThue = false;
                try
                {
                    var check = DatabaseHelper.ExecuteQuery("SELECT TOP 0 GiaThue FROM ThongTinSach", null);
                    hasGiaThue = true;
                }
                catch { }

                string giaThueCol = hasGiaThue ? ", s.GiaThue" : ", 0 AS GiaThue";
                string sql = "SELECT c.IDCaTheSach, s.IDSach, s.TenSach, s.TacGia, s.NhaXuatBan, s.NamXuatBan, s.GiaBan" + giaThueCol + ", ISNULL(d.TenDauSach, s.IDDauSach) AS TheLoai, s.IDDauSach, c.TinhTrang FROM ThongTinSach s LEFT JOIN DauSach d ON s.IDDauSach = d.IDDauSach LEFT JOIN CaTheSach c ON s.IDSach = c.IDSach";

                string finalWhere = "";

                if (!string.IsNullOrWhiteSpace(whereClause))
                {
                    finalWhere = whereClause;
                }

                var selectedCategory = cboTheLoaiFilter.SelectedItem;
                List<SqlParameter> paramList = new List<SqlParameter>();
                if (parameters != null) paramList.AddRange(parameters);

                if (selectedCategory != null && selectedCategory.ToString() != "Tất cả" && selectedCategory is CategoryItem catItem)
                {
                    string categoryId = catItem.Id;

                    if (!string.IsNullOrWhiteSpace(categoryId))
                    {
                        string categoryFilter = "RTRIM(s.IDDauSach) = @categoryId";

                        if (!string.IsNullOrWhiteSpace(finalWhere))
                        {
                            finalWhere += " AND " + categoryFilter;
                        }
                        else
                        {
                            finalWhere = categoryFilter;
                        }
                        paramList.Add(new SqlParameter("@categoryId", categoryId));
                    }
                }

                if (!string.IsNullOrWhiteSpace(finalWhere))
                {
                    sql += " WHERE " + finalWhere;
                }

                var dt = DatabaseHelper.ExecuteQuery(sql, paramList.ToArray());
                dgvBooks.Rows.Clear();
                foreach (DataRow r in dt.Rows)
                {
                    var statusObj = r["TinhTrang"];
                    string statusText = statusObj?.ToString()?.Trim() ?? "";
                    if (!string.IsNullOrWhiteSpace(statusText))
                    {
                        statusText = GetStatusText(statusText);
                    }
                    var theLoai = r["TheLoai"]?.ToString()?.Trim() ?? r["IDDauSach"]?.ToString()?.Trim() ?? "";
                    dgvBooks.Rows.Add(
                        r["IDCaTheSach"]?.ToString()?.Trim(),
                        r["IDSach"]?.ToString()?.Trim(),
                        r["TenSach"]?.ToString()?.Trim(),
                        r["TacGia"]?.ToString()?.Trim(),
                        r["NhaXuatBan"]?.ToString()?.Trim(),
                        r["NamXuatBan"],
                        r["GiaBan"],
                        r["GiaThue"],
                        theLoai,
                        statusText);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể truy vấn dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void RefreshData()
        {
            LoadData();
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

        private void DataGridView1_DoubleClick(object sender, EventArgs e)
        {
            if (dgvBooks.SelectedRows.Count == 0 || _currentMasterId != null) return; // Disable double-click if already in detail view

            var selectedMasterId = dgvBooks.SelectedRows[0].Cells[0].Value?.ToString()?.Trim(); // Get IDSach from the first column
            if (string.IsNullOrWhiteSpace(selectedMasterId)) return;

            _currentMasterId = selectedMasterId; // Track the selected master ID

            SetupGridColumnsCaTheSach(); // Switch to detail view columns
            LoadDataCaTheSach(_currentMasterId); // Load detail data for the selected master ID
            btnBack.Visible = true; // Show btnBack in detail view
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaSach.Text))
            {
                LoadData(borrowStatusClause);
            }

            string searchValue = txtMaSach.Text.Trim();

            // Build the WHERE clause
            string whereClause = "s.TenSach LIKE @searchValue";
            SqlParameter[] parameters = { new SqlParameter("@searchValue", "%" + searchValue + "%") };

            try
            {
                LoadData(whereClause + (string.IsNullOrWhiteSpace(borrowStatusClause) ? "" : " AND " + borrowStatusClause), parameters);

                if (dgvBooks.Rows.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy kết quả nào!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            borrowStatusClause = null;
            _currentMasterId = null; // Reset to master view
            SetupGridColumnsThongTinSach(); // Reset columns to master view
            LoadDataThongTinSach(); // Reload master data
            btnBack.Visible = false; // Hide btnBack in master view
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            borrowStatusClause = "c.TinhTrang = N'Sẵn sàng'";
            LoadData(borrowStatusClause);
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            borrowStatusClause = "c.TinhTrang = N'Đang mượn'";
            LoadData(borrowStatusClause);
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            if (_currentMasterId == null) return; // Already in master view

            _currentMasterId = null; // Reset to master view
            SetupGridColumnsThongTinSach(); // Reset columns to master view
            LoadDataThongTinSach(); // Reload master data
            btnBack.Visible = false; // Hide btnBack in master view
        }
    }
}
