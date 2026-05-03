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
using ClosedXML.Excel;

namespace QuanLyThuVien
{
    public partial class FrmReports : Form
    {
        // Ghi chú: File này chứa logic hiển thị 3 loại báo cáo - thống kê
        // - "Tình hình mượn sách theo thể loại": thống kê theo tháng (dựa trên dateTimePicker1)
        // - "Sách trả trễ": lọc theo ngày (chỉ những sách trả trễ trong ngày được chọn)
        // - "Độc giả nợ tiền phạt": lọc theo ngày (tổng tiền phạt phát sinh trong ngày)

        public FrmReports()
        {
            InitializeComponent();
        }

        private void FrmReports_Load(object sender, EventArgs e)
        {
            comboBoxTongQuan.SelectedIndex = 0;

        }

        private void comboBoxTongQuan_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Cập nhật tiêu đề báo cáo theo lựa chọn
            labelBaoCao.Text = comboBoxTongQuan.SelectedItem.ToString();
        }

        private void ButtonTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                string selection = comboBoxTongQuan.SelectedItem?.ToString() ?? string.Empty;
                DataTable dt = null;

                // Sử dụng bộ lọc ngày: báo cáo 1 theo tháng, 2 và 3 theo ngày
                DateTime selectedDate = dateTimePicker1.Value.Date;
                DateTime dayStart = selectedDate;
                DateTime dayEnd = selectedDate.AddDays(1).AddSeconds(-1);

                // Hiển thị thông tin thời gian trên tiêu đề báo cáo
                if (selection == "Tình hình mượn sách theo thể loại")
                {
                    // Hiển thị tháng/năm
                    labelBaoCao.Text = $"{selection} - Tháng {selectedDate.Month}/{selectedDate.Year}";
                }
                else
                {
                    // Hiển thị ngày
                    labelBaoCao.Text = $"{selection} - Ngày {selectedDate:dd/MM/yyyy}";
                }

                switch (selection)
                {
                    case "Tình hình mượn sách theo thể loại":
                        {
                            // Báo mẫu BM10.1: Thống kê theo tháng
                            // Lấy dữ liệu trong tháng được chọn (dựa vào dateTimePicker1)
                            // Trả về: Thể loại | Số lượt mượn | Tỉ lệ (%) và thêm hàng tổng
                            DateTime monthStart = new DateTime(selectedDate.Year, selectedDate.Month, 1);
                            DateTime monthEnd = monthStart.AddMonths(1).AddSeconds(-1);

                            // Include percentage (tỉ lệ) using window function and limit by month
                            string sql =
                                "SELECT tl.TenTheLoai AS [Thể loại], COUNT(*) AS [Số lượt mượn], " +
                                "CAST(100.0 * COUNT(*) / SUM(COUNT(*)) OVER() AS DECIMAL(5,2)) AS [Tỉ lệ (%)] " +
                                "FROM PhieuMuon pm " +
                                "JOIN ChiTietMuon ct ON pm.IDPhieuMuon = ct.IDPhieuMuon " +
                                "JOIN ThongTinSach s ON ct.IDSach = s.IDSach " +
                                "JOIN DauSach ds ON s.IDDauSach = ds.IDDauSach " +
                                "JOIN TheLoai tl ON ds.IDTheLoai = tl.IDTheLoai " +
                                "WHERE ct.NgayMuon BETWEEN @Start AND @End " +
                                "GROUP BY tl.TenTheLoai";

                            dt = DatabaseHelper.ExecuteQuery(sql, new System.Data.SqlClient.SqlParameter[] {
                                new System.Data.SqlClient.SqlParameter("@Start", monthStart),
                                new System.Data.SqlClient.SqlParameter("@End", monthEnd)
                            });

                            // Thêm hàng tổng vào cuối bảng (giao diện hiển thị giống mẫu báo cáo)
                            try
                            {
                                int total = 0;
                                if (dt != null && dt.Rows.Count > 0)
                                {
                                    foreach (DataRow r in dt.Rows)
                                    {
                                        if (int.TryParse(r["Số lượt mượn"].ToString(), out int v))
                                            total += v;
                                    }

                                    DataRow tot = dt.NewRow();
                                    tot["Thể loại"] = "Tổng số lượt mượn:";
                                    // set total in second column
                                    tot["Số lượt mượn"] = total;
                                    tot["Tỉ lệ (%)"] = DBNull.Value;
                                    dt.Rows.Add(tot);
                                }
                            }
                            catch
                            {
                                // ignore footer creation errors
                            }
                        }
                        break;
                    case "Sách trả trễ":
                        {
                            // Báo mẫu BM10.2: Sách trả trễ (theo ngày)
                            // Lọc các phiếu trả có "Ngày trả" trong ngày được chọn và NgàyTra > NgayHenTra
                            string sql =
                                "SELECT ds.TenDauSach AS [Tên sách], ct.NgayMuon AS [Ngày mượn], DATEDIFF(day, ct.HanTra, ct.NgayTra) AS [Số ngày trả trễ] " +
                                "FROM PhieuMuon pm " +
                                "JOIN ChiTietMuon ct ON pm.IDPhieuMuon = ct.IDPhieuMuon " +
                                "JOIN ThongTinSach s ON ct.IDSach = s.IDSach " +
                                "JOIN DauSach ds ON s.IDDauSach = ds.IDDauSach " +
                                "WHERE ct.NgayTra BETWEEN @Start AND @End " +
                                "AND ct.NgayTra > ct.HanTra";

                            dt = DatabaseHelper.ExecuteQuery(sql, new System.Data.SqlClient.SqlParameter[] {
                                new System.Data.SqlClient.SqlParameter("@Start", dayStart),
                                new System.Data.SqlClient.SqlParameter("@End", dayEnd)
                            });
                        }
                        break;
                    case "Độc giả nợ tiền phạt":
                        {
                            // Báo mẫu BM10.3: Độc giả nợ tiền phạt (theo ngày)
                            // Lọc các phiếu trả có phát sinh tiền phạt trong ngày được chọn, nhóm theo độc giả
                            string sql =
                                "SELECT dg.IDDocGia AS [Mã độc giả], dg.HoTen AS [Họ tên], SUM(ct.TienPhat) AS [Tiền nợ] " +
                                "FROM TheDocGia dg " +
                                "JOIN PhieuMuon pm ON dg.IDDocGia = pm.IDNguoiMuon " +
                                "JOIN ChiTietMuon ct ON pm.IDPhieuMuon = ct.IDPhieuMuon " +
                                "WHERE ct.NgayTra BETWEEN @Start AND @End " +
                                "GROUP BY dg.IDDocGia, dg.HoTen " +
                                "HAVING SUM(ct.TienPhat) > 0";

                            dt = DatabaseHelper.ExecuteQuery(sql, new System.Data.SqlClient.SqlParameter[] {
                                new System.Data.SqlClient.SqlParameter("@Start", dayStart),
                                new System.Data.SqlClient.SqlParameter("@End", dayEnd)
                            });

                            // Thêm hàng tổng tiền nợ ở cuối báo cáo
                            try
                            {
                                decimal totalMoney = 0m;
                                if (dt != null && dt.Rows.Count > 0)
                                {
                                    foreach (DataRow r in dt.Rows)
                                    {
                                        if (r["Tiền nợ"] != DBNull.Value && decimal.TryParse(r["Tiền nợ"].ToString(), out decimal val))
                                            totalMoney += val;
                                    }

                                    DataRow tot = dt.NewRow();
                                    tot["Mã độc giả"] = "Tổng tiền nợ:";
                                    // đặt tổng vào cột Tiền nợ
                                    tot["Tiền nợ"] = totalMoney;
                                    // nếu có cột Họ tên thì để trống
                                    if (dt.Columns.Contains("Họ tên"))
                                        tot["Họ tên"] = DBNull.Value;
                                    dt.Rows.Add(tot);
                                }
                            }
                            catch
                            {
                                // ignore footer creation errors
                            }
                        }
                        break;
                    default:
                        MessageBox.Show("Vui lòng chọn loại báo cáo.", "Thống kê", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                }

                dataGridView1.DataSource = dt;
            }
            catch (Exception)
            {
                MessageBox.Show("Có lỗi xảy ra khi truy vấn dữ liệu báo cáo. Vui lòng liên hệ quản trị viên.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ButtonHuy_Click(object sender, EventArgs e)
        {
            // Reset filters / results
            try
            {
                dataGridView1.DataSource = null;
                dateTimePicker1.Value = DateTime.Today;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // =========================
        // BUTTON XUẤT EXCEL
        // =========================
        private void btnXuatFileExcel_Click(object sender, EventArgs e)
        {
            // =========================
            // KIỂM TRA CHƯA CÓ DỮ LIỆU (chưa tìm kiếm)
            // =========================
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("Vui lòng tìm kiếm trước khi xuất file!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            // =========================
            // KIỂM TRA CÓ DỮ LIỆU THỰC SỰ HAY KHÔNG
            // (trường hợp tìm kiếm nhưng không có kết quả)
            // =========================
            bool hasData = false;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (!row.IsNewRow)
                {
                    hasData = true;
                    break;
                }
            }

            if (!hasData)
            {
                MessageBox.Show("Không có dữ liệu sau khi tìm kiếm!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "Excel File|*.xlsx";
            save.Title = "Chọn nơi lưu file Excel";

            if (save.ShowDialog() == DialogResult.OK)
            {
                ExportToExcel(dataGridView1, save.FileName);
            }
        }

        // =========================
        // HÀM EXPORT EXCEL
        // =========================
        public void ExportToExcel(DataGridView dgv, string filePath)
        {
            try
            {
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("BaoCao");

                    // ===== HEADER =====
                    for (int i = 0; i < dgv.Columns.Count; i++)
                    {
                        var cell = worksheet.Cell(1, i + 1);
                        cell.Value = dgv.Columns[i].HeaderText;

                        cell.Style.Font.Bold = true;
                        cell.Style.Fill.BackgroundColor = XLColor.LightGray;
                        cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        cell.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    }

                    // ===== DATA =====
                    for (int i = 0; i < dgv.Rows.Count; i++)
                    {
                        for (int j = 0; j < dgv.Columns.Count; j++)
                        {
                            var value = dgv.Rows[i].Cells[j].Value;

                            var cell = worksheet.Cell(i + 2, j + 1);
                            cell.Value = value?.ToString() ?? "";

                            cell.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        }
                    }

                    // ===== AUTO FIT =====
                    worksheet.Columns().AdjustToContents();

                    workbook.SaveAs(filePath);
                }

                MessageBox.Show("Xuất Excel thành công!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xuất Excel:\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnMoFileExcel_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();

            // chỉ cho chọn file Excel
            open.Filter = "Excel Files|*.xlsx;*.xls";
            open.Title = "Chọn file Excel để mở";

            if (open.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    System.Diagnostics.Process.Start(open.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không mở được file:\n" + ex.Message,
                        "Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }
    }
    
}
