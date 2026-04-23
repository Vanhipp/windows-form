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
            SetupGridColumns();
            LoadData();
        }

        private void LoadCategories()
        {
            try
            {
                cboTheLoaiFilter.SelectedIndexChanged -= CboTheLoaiFilter_SelectedIndexChanged;

                cboTheLoaiFilter.Items.Clear();
                cboTheLoaiFilter.Items.Add("Tất cả");

                string[] orderSequence = { "Mathematics", "Physics", "Chemistry", "Biology", "History", "Geography", "English", "Literature" };
                var dt = DatabaseHelper.ExecuteQuery("SELECT IDDauSach, TenDauSach FROM DauSach", null);

                foreach (var name in orderSequence)
                {
                    foreach (DataRow r in dt.Rows)
                    {
                        if (r["TenDauSach"]?.ToString().Trim() == name)
                        {
                            cboTheLoaiFilter.Items.Add(new CategoryItem(r["IDDauSach"]?.ToString().Trim(), r["TenDauSach"]?.ToString().Trim()));
                            break;
                        }
                    }
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

        private void SetupGridColumns()
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
            dgvBooks.Columns.Add("TheLoai", "Thể loại");
            dgvBooks.Columns.Add("TinhTrang", "Tình trạng");
            dgvBooks.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBooks.MultiSelect = false;
            dgvBooks.DoubleClick += DataGridView1_DoubleClick;
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
                string sql = "SELECT s.IDSach, s.TenSach, s.TacGia, s.NhaXuatBan, s.NamXuatBan, s.GiaBan" + giaThueCol + ", ISNULL(d.TenDauSach, s.IDDauSach) AS TheLoai, s.IDDauSach, s.TinhTrang FROM ThongTinSach s LEFT JOIN DauSach d ON s.IDDauSach = d.IDDauSach";

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
                    return "Mất";
                case "người dùng làm mất":
                    return "Mất";
                default:
                    return status;
            }
        }

        private void DataGridView1_DoubleClick(object sender, EventArgs e)
        {
            if (dgvBooks.SelectedRows.Count == 0) return;
            var id = dgvBooks.SelectedRows[0].Cells[0].Value?.ToString()?.Trim();
            if (string.IsNullOrWhiteSpace(id)) return;
            var frm = new FrmLiquidation();
            frm.SelectAndHighlight(id);
            frm.ShowDialog(this);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtMaSach.Text))
            {
                var p = new SqlParameter[] { new SqlParameter("@id", txtMaSach.Text.Trim()) };
                LoadData("s.IDSach = @id", p);
            }
            else
            {
                LoadData();
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            LoadData("s.TinhTrang = N'OK'");
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            LoadData("s.TinhTrang = N'Đang mượn'");
        }
    }
}
