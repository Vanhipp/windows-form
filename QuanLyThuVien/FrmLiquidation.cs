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
            cboLyDo.SelectedIndexChanged += CboLyDo_SelectedIndexChanged;
            dgvBooks.SelectionChanged += DgvBooks_SelectionChanged;
            btnDatimthay.Click += Button1_Click_KhoiPhuc;
        }

        private class ReasonItem
        {
            public LiquidationReason Value { get; set; }
            public string Text { get; set; }
            public override string ToString() { return Text; }
        }

        private bool isUpdatingCombo = false;

        private void FrmLiquidation_Load(object sender, EventArgs e)
        {
            cboLyDo.Items.Clear();
            cboLyDo.Items.Add(new ReasonItem { Value = LiquidationReason.Lost, Text = "Lost" });
            cboLyDo.Items.Add(new ReasonItem { Value = LiquidationReason.Damaged, Text = "Damaged" });
            cboLyDo.Items.Add(new ReasonItem { Value = LiquidationReason.LostByUser, Text = "LostByUser" });
            if (cboLyDo.Items.Count > 0) cboLyDo.SelectedIndex = 0;

            try
            {
                DatabaseHelper.ExecuteNonQuery(@"
                    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('ThanhLySach') AND name = 'TienPhat')
                    BEGIN
                        ALTER TABLE ThanhLySach ADD TienPhat money NOT NULL DEFAULT ((0))
                    END");
            }
            catch { }

            dgvBooks.Columns.Clear();
            dgvBooks.AutoGenerateColumns = false;
            dgvBooks.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBooks.MultiSelect = false;
            dgvBooks.Columns.Add("IDSach", "Mã sách");
            dgvBooks.Columns.Add("TenSach", "Tên sách");
            dgvBooks.Columns.Add("TacGia", "Tác giả");
            dgvBooks.Columns.Add("GiaBan", "Trị giá");
            dgvBooks.Columns.Add("GiaThue", "Giá thuê");
            dgvBooks.Columns.Add("TienPhat", "Tiền phạt");
            dgvBooks.Columns.Add("TinhTrang", "Tình trạng");
            
            dgvBooks.Columns.Add("LyDoThanhLy_SQL", "LyDo_SQL");
            dgvBooks.Columns["LyDoThanhLy_SQL"].Visible = false;
            dgvBooks.Columns.Add("TienPhat_SQL", "TienPhat_SQL");
            dgvBooks.Columns["TienPhat_SQL"].Visible = false;

            dgvBooks.DoubleClick += DataGridView1_DoubleClick;

            LoadData();
        }

        public void LoadData(string whereClause = null, SqlParameter[] parameters = null)
        {
            try
            {
                string sql = @"
                    SELECT s.IDSach, s.TenSach, s.TacGia, s.GiaBan, s.GiaThue, s.TinhTrang, t.LyDoThanhLy, t.TienPhat 
                    FROM ThongTinSach s 
                    LEFT JOIN ThanhLySach t ON s.IDSach = t.IDSach";

                if (!string.IsNullOrWhiteSpace(whereClause))
                {
                    sql += " WHERE " + whereClause;
                }

                decimal currentPct = 0;
                if (cboLyDo.SelectedItem is ReasonItem ri)
                {
                    if (ri.Value == LiquidationReason.Damaged) currentPct = 0.5m;
                    else if (ri.Value == LiquidationReason.LostByUser) currentPct = 1.0m;
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
                    
                    decimal giaBan = r["GiaBan"] != DBNull.Value ? Convert.ToDecimal(r["GiaBan"]) : 0;
                    decimal rowPenalty = r["TienPhat"] != DBNull.Value ? Convert.ToDecimal(r["TienPhat"]) : (giaBan * currentPct);

                    dgvBooks.Rows.Add(
                        r["IDSach"]?.ToString()?.Trim(),
                        r["TenSach"]?.ToString()?.Trim(),
                        r["TacGia"]?.ToString()?.Trim(),
                        r["GiaBan"],
                        r["GiaThue"],
                        rowPenalty,
                        statusText,
                        r["LyDoThanhLy"]?.ToString()?.Trim(),
                        r["TienPhat"] != DBNull.Value ? r["TienPhat"] : null
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
            LoadData("IDSach = @id", p);
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
                LoadData("IDSach = @id", p);
            }
            else
            {
                LoadData();
            }
        }

        private void CboLyDo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isUpdatingCombo) return;
            if (dgvBooks.SelectedRows.Count == 0) return;

            decimal pct = 0;
            if (cboLyDo.SelectedItem is ReasonItem ri)
            {
                if (ri.Value == LiquidationReason.Damaged) pct = 0.5m;
                else if (ri.Value == LiquidationReason.LostByUser) pct = 1.0m;
            }

            var row = dgvBooks.SelectedRows[0];
            if (row.Cells[3].Value != null && decimal.TryParse(row.Cells[3].Value.ToString(), out decimal giaBan))
            {
                decimal penalty = giaBan * pct;
                if (penalty > nudTienPhat.Maximum) nudTienPhat.Maximum = penalty;
                nudTienPhat.Value = penalty;
                
                // Đồng bộ số tiền lên cột lưới của đúng dòng đang chọn
                row.Cells[5].Value = penalty;
            }
        }

        private void DgvBooks_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvBooks.SelectedRows.Count == 0) return;
            
            isUpdatingCombo = true;
            try
            {
                var row = dgvBooks.SelectedRows[0];
                if (row.Cells.Count > 7 && row.Cells[7].Value != null && !string.IsNullOrWhiteSpace(row.Cells[7].Value.ToString()))
                {
                    string lydo = row.Cells[7].Value.ToString();
                    foreach (ReasonItem ri in cboLyDo.Items)
                    {
                        if (ri.Value.ToString() == lydo)
                        {
                            cboLyDo.SelectedItem = ri;
                            break;
                        }
                    }
                }

                if (row.Cells[5].Value != null)
                {
                    decimal penalty = Convert.ToDecimal(row.Cells[5].Value);
                    if (penalty > nudTienPhat.Maximum) nudTienPhat.Maximum = penalty;
                    nudTienPhat.Value = penalty;
                }
            }
            finally
            {
                isUpdatingCombo = false;
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

            var result = MessageBox.Show($"Bạn có chắc chắn muốn thanh lý sách mã {id}?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes) return;

            try
            {
                string lydo = null;
                if (cboLyDo.SelectedItem is ReasonItem ri) lydo = ri.Value.ToString();

                var tien = (int)nudTienPhat.Value;

                try
                {
                    string insertSql = @"
                        IF EXISTS (SELECT 1 FROM ThanhLySach WHERE IDSach = @id)
                        BEGIN
                            UPDATE ThanhLySach SET NgayThanhLy = @ngay, LyDoThanhLy = @lydo, TienPhat = @tien WHERE IDSach = @id
                        END
                        ELSE
                        BEGIN
                            INSERT INTO ThanhLySach (IDSach, NgayThanhLy, LyDoThanhLy, TienPhat) VALUES (@id, @ngay, @lydo, @tien)
                        END";
                    var insParams = new SqlParameter[] {
                        new SqlParameter("@id", id),
                        new SqlParameter("@ngay", DateTime.Now.Date),
                        new SqlParameter("@lydo", lydo ?? (object)DBNull.Value),
                        new SqlParameter("@tien", tien)
                    };
                    DatabaseHelper.ExecuteNonQuery(insertSql, insParams);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi lưu phiếu thanh lý: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string newStatus = lydo ?? "Damaged";

                string updateSql = "UPDATE ThongTinSach SET TinhTrang = @t WHERE IDSach = @id";
                var p = new SqlParameter[] {
                    new SqlParameter("@t", newStatus),
                    new SqlParameter("@id", id)
                };
                DatabaseHelper.ExecuteNonQuery(updateSql, p);

                MessageBox.Show("Đã thanh lý sách thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Giữ nguyên filter hiện tại, tránh việc grid bị auto-select hàng đầu tiên
                Timkiem_Click(null, null);
                dgvBooks.ClearSelection();

                // Refresh form tra cứu sách nếu đang mở
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
            nudTienPhat.Value = nudTienPhat.Minimum;
            if (cboLyDo.Items.Count > 0) cboLyDo.SelectedIndex = 0;
            LoadData();
        }

        private void Button1_Click_KhoiPhuc(object sender, EventArgs e)
        {
            if (dgvBooks.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn sách để khôi phục trạng thái.", "Chú ý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var id = dgvBooks.SelectedRows[0].Cells[0].Value?.ToString()?.Trim();
            if (string.IsNullOrWhiteSpace(id)) return;

            var result = MessageBox.Show($"Bạn có chắc muốn khôi phục sách mã {id} về trạng thái 'Sẵn sàng' và đảo ngược lịch sử thanh lý?", "Xác nhận khôi phục", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes) return;

            try
            {
                // Cập nhật lại trạng thái thành Sẵn sàng
                DatabaseHelper.ExecuteNonQuery("UPDATE ThongTinSach SET TinhTrang = N'Sẵn sàng' WHERE IDSach = @id", 
                    new[] { new SqlParameter("@id", id) });

                // Xóa khỏi bảng ThanhLySach để dọn sạch lịch sử (undo thanh lý)
                DatabaseHelper.ExecuteNonQuery("DELETE FROM ThanhLySach WHERE IDSach = @id", 
                    new[] { new SqlParameter("@id", id) });

                MessageBox.Show("Khôi phục trạng thái sách thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Timkiem_Click(null, null);
                dgvBooks.ClearSelection();

                foreach (Form frm in Application.OpenForms)
                {
                    if (frm is FrmSearchBook searchForm) searchForm.RefreshData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi khôi phục sách: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
