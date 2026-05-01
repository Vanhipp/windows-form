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
using QuanLyThuVien.Helpers;
using System.Data.SqlClient;

namespace QuanLyThuVien
{
    public partial class FrmLiquidation : Form
    {
        public FrmLiquidation()
        {
            InitializeComponent();
            Load += FrmLiquidation_Load;
            btnTimKiem.Click += Timkiem_Click;
            btnXacNhan.Click += XacNhan_Click;
            btnDong.Click += Dong_Click;
            dgvBooks.SelectionChanged += DgvBooks_SelectionChanged;
        }

        private class ReasonItem
        {
            public LiquidationReason Value { get; set; }
            public string Text { get; set; }
            public override string ToString() { return Text; }
        }

        private void FrmLiquidation_Load(object sender, EventArgs e)
        {
            cboLyDo.Items.Clear();
            cboLyDo.Items.Add(new ReasonItem { Value = LiquidationReason.Lost, Text = "Mất" });
            cboLyDo.Items.Add(new ReasonItem { Value = LiquidationReason.Damaged, Text = "Hỏng" });
            cboLyDo.Items.Add(new ReasonItem { Value = LiquidationReason.LostByUser, Text = "Mất do người dùng" });
            if (cboLyDo.Items.Count > 0) cboLyDo.SelectedIndex = 0;

            dgvBooks.Columns.Clear();
            dgvBooks.AutoGenerateColumns = false;
            dgvBooks.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBooks.MultiSelect = false;
            dgvBooks.Columns.Add("IDSach", "Mã sách");
            dgvBooks.Columns.Add("TenSach", "Tên sách");
            dgvBooks.Columns.Add("TacGia", "Tác giả");
            dgvBooks.Columns.Add("GiaBan", "Trị giá");
            dgvBooks.Columns.Add("GiaThue", "Giá thuê");
            dgvBooks.Columns.Add("TinhTrang", "Tình trạng");

            dgvBooks.Columns.Add("LyDoThanhLy_SQL", "Lý do thanh lý");
            dgvBooks.Columns["LyDoThanhLy_SQL"].Visible = true;

            dgvBooks.DoubleClick += DataGridView1_DoubleClick;

            LoadData();
        }

        public void LoadData(string whereClause = null, SqlParameter[] parameters = null)
        {
            try
            {
                string sql = @"
                    SELECT s.IDSach, s.TenSach, s.TacGia, s.GiaBan, s.GiaThue, s.TinhTrang, t.LyDoThanhLy
                    FROM ThongTinSach s
                    LEFT JOIN ThanhLySach t ON s.IDSach = t.IDSach";

                if (!string.IsNullOrWhiteSpace(whereClause))
                {
                    sql += " WHERE " + whereClause;
                }

                var dt = DatabaseHelper.ExecuteQuery(sql, parameters);
                dgvBooks.Rows.Clear();
                foreach (DataRow r in dt.Rows)
                {
                    var statusObj = r["TinhTrang"];
                    string statusText = statusObj?.ToString()?.Trim() ?? "";
                    if (!string.IsNullOrWhiteSpace(statusText))
                    {
                        statusText = GetStatusText(statusText);
                    }

                    dgvBooks.Rows.Add(
                        r["IDSach"]?.ToString()?.Trim(),
                        r["TenSach"]?.ToString()?.Trim(),
                        r["TacGia"]?.ToString()?.Trim(),
                        r["GiaBan"],
                        r["GiaThue"],
                        statusText,
                        r["LyDoThanhLy"]?.ToString()?.Trim()
                    );
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
                    return "Mất";
                case "lostbyuser":
                    return "Mất do người dùng";
                default:
                    return status;
            }
        }

        public void SelectAndHighlight(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) return;
            txtMaSach.Text = id;
            var p = new SqlParameter[] { new SqlParameter("@id", id) };
            LoadData("s.IDSach = @id", p);
            foreach (DataGridViewRow row in dgvBooks.Rows)
            {
                if (row.Cells.Count > 0 && row.Cells[0].Value != null && row.Cells[0].Value.ToString().Trim() == id)
                {
                    row.Selected = true;
                    dgvBooks.CurrentCell = row.Cells[0];
                    break;
                }
            }
        }

        private void DataGridView1_DoubleClick(object sender, EventArgs e)
        {
            if (dgvBooks.SelectedRows.Count > 0)
            {
                var id = dgvBooks.SelectedRows[0].Cells[0].Value?.ToString()?.Trim();
                if (!string.IsNullOrWhiteSpace(id)) txtMaSach.Text = id;
            }
        }

        private void Timkiem_Click(object sender, EventArgs e)
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

        private void DgvBooks_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvBooks.SelectedRows.Count == 0) return;

            var row = dgvBooks.SelectedRows[0];
            if (row.Cells.Count > 6 && row.Cells[6].Value != null && !string.IsNullOrWhiteSpace(row.Cells[6].Value.ToString()))
            {
                string lydo = row.Cells[6].Value.ToString();
                foreach (ReasonItem ri in cboLyDo.Items)
                {
                    if (ri.Value.ToString() == lydo)
                    {
                        cboLyDo.SelectedItem = ri;
                        break;
                    }
                }
            }
        }

        private void XacNhan_Click(object sender, EventArgs e)
        {
            if (dgvBooks.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn sách từ danh sách để thanh lý.", "Chú ý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var id = dgvBooks.SelectedRows[0].Cells[0].Value?.ToString()?.Trim();
            if (string.IsNullOrWhiteSpace(id)) return;

            // Kiểm tra trạng thái sách trước khi thanh lý
            string currentStatus = dgvBooks.SelectedRows[0].Cells[5].Value?.ToString()?.Trim();
            if (currentStatus == "Đang mượn")
            {
                MessageBox.Show("Không thể thanh lý sách đang được mượn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show($"Bạn có chắc chắn muốn thanh lý sách mã {id}?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes) return;

            try
            {
                string lydo = null;
                if (cboLyDo.SelectedItem is ReasonItem ri) 
                    lydo = ri.Value.GetDisplayName();

                try
                {
                    string insertSql = @"
                        IF EXISTS (SELECT 1 FROM ThanhLySach WHERE IDSach = @id)
                        BEGIN
                            UPDATE ThanhLySach SET NgayThanhLy = @ngay, LyDoThanhLy = @lydo WHERE IDSach = @id
                        END
                        ELSE
                        BEGIN
                            INSERT INTO ThanhLySach (IDSach, NgayThanhLy, LyDoThanhLy) VALUES (@id, @ngay, @lydo)
                        END";
                    var insParams = new SqlParameter[] {
                        new SqlParameter("@id", id),
                        new SqlParameter("@ngay", DateTime.Now.Date),
                        new SqlParameter("@lydo", lydo ?? (object)DBNull.Value)
                    };
                    DatabaseHelper.ExecuteNonQuery(insertSql, insParams);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi lưu phiếu thanh lý: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string newStatus;
                if (cboLyDo.SelectedItem is ReasonItem reasonItem)
                {
                    newStatus = reasonItem.Value == LiquidationReason.Damaged ? "Hỏng" : "Mất";
                }
                else
                {
                    newStatus = "Hỏng";
                }

                string updateSql = "UPDATE ThongTinSach SET TinhTrang = @t WHERE IDSach = @id";
                var p = new SqlParameter[] {
                    new SqlParameter("@t", newStatus),
                    new SqlParameter("@id", id)
                };
                DatabaseHelper.ExecuteNonQuery(updateSql, p);

                MessageBox.Show("Đã thanh lý sách thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Timkiem_Click(null, null);
                dgvBooks.ClearSelection();

                foreach (Form frm in Application.OpenForms)
                {
                    if (frm is FrmSearchBook searchForm)
                    {
                        searchForm.RefreshData();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thanh lý sách: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Dong_Click(object sender, EventArgs e)
        {
            txtMaSach.Text = string.Empty;
            if (cboLyDo.Items.Count > 0) cboLyDo.SelectedIndex = 0;
            LoadData();
        }
    }
}
