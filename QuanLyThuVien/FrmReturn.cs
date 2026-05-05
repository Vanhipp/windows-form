using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyThuVien.Models;
using QuanLyThuVien.Helpers;
using System.Data.SqlClient;

namespace QuanLyThuVien
{
    public partial class FrmReturn : Form
    {
        private const decimal FINE_PER_DAY = 1000;

        public FrmReturn()
        {
            InitializeComponent();
            
            this.btnSearchBorrowing.Click += BtnSearchBorrowing_Click;
            this.btnConfirmReturn.Click += BtnConfirmReturn_Click;
            this.btnCancel.Click += BtnCancel_Click;
            this.dtpReturnDate.ValueChanged += DtpReturnDate_ValueChanged;
            this.chkIsLost.CheckedChanged += (s, ev) => CalculateFine();
            
            txtPricePerMonth.Text = FINE_PER_DAY.ToString();
            txtPricePerMonth.ReadOnly = true;
            txtLateDays.ReadOnly = true;
            txtTotalFine.ReadOnly = true;
        }

        private void BtnSearchBorrowing_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtReaderID.Text))
            {
                MessageBox.Show("Vui lòng nhập mã độc giả!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query = "SELECT CT.IDChiTietMuon, CT.IDPhieuMuon, CT.IDSach, S.TenSach, CT.NgayMuon, CT.HanTra " +
                           "FROM ChiTietMuon CT " +
                           "JOIN PhieuMuon PM ON CT.IDPhieuMuon = PM.IDPhieuMuon " +
                           "JOIN ThongTinSach S ON CT.IDSach = S.IDSach " +
                           "WHERE PM.IDNguoiMuon = @ReaderID AND CT.NgayTra IS NULL";
            
            SqlParameter[] parameters = { new SqlParameter("@ReaderID", txtReaderID.Text) };
            
            try
            {
                DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters);
                dgvBorrowingList.DataSource = dt;
                
                if (dt.Rows.Count > 0)
                {
                    dtpBorrowDate.Value = Convert.ToDateTime(dt.Rows[0]["NgayMuon"]);
                    CalculateFine();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy sách đang mượn của độc giả này!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm phiếu mượn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DtpReturnDate_ValueChanged(object sender, EventArgs e)
        {
            CalculateFine();
        }

        private void CalculateFine()
        {
            if (dgvBorrowingList.SelectedRows.Count == 0) return;

            decimal totalFine = 0;
            DateTime returnDate = dtpReturnDate.Value.Date;

            foreach (DataGridViewRow row in dgvBorrowingList.SelectedRows)
            {
                if (row.Cells["HanTra"].Value == null) continue;
                
                DateTime dueDate = Convert.ToDateTime(row.Cells["HanTra"].Value).Date;
                decimal rowFine = 0;

                if (returnDate > dueDate)
                {
                    int lateDays = (returnDate - dueDate).Days;
                    rowFine = lateDays * FINE_PER_DAY;
                }

                if (chkIsLost.Checked)
                {
                    // QĐ8: Tiền phạt không nhỏ hơn trị giá quyển sách
                    string sachID = row.Cells["IDSach"].Value.ToString();
                    string queryGia = "SELECT GiaBan FROM ThongTinSach WHERE IDSach = @SachID";
                    object resultGia = DatabaseHelper.ExecuteScalar(queryGia, new SqlParameter[] { new SqlParameter("@SachID", sachID) });
                    decimal giaBan = resultGia != null ? Convert.ToDecimal(resultGia) : 0;
                    
                    rowFine = Math.Max(rowFine, giaBan);
                }
                totalFine += rowFine;
            }

            txtTotalFine.Text = totalFine.ToString();
            // Cập nhật số ngày trễ của dòng đầu tiên đang chọn để tham khảo
            if (dgvBorrowingList.SelectedRows[0].Cells["HanTra"].Value != null)
            {
                DateTime firstDueDate = Convert.ToDateTime(dgvBorrowingList.SelectedRows[0].Cells["HanTra"].Value).Date;
                txtLateDays.Text = returnDate > firstDueDate ? (returnDate - firstDueDate).Days.ToString() : "0";
            }
        }

        private void BtnConfirmReturn_Click(object sender, EventArgs e)
        {
            if (dgvBorrowingList.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn sách cần trả!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                decimal totalFineIssued = 0;
                DateTime returnDate = dtpReturnDate.Value.Date;

                foreach (DataGridViewRow row in dgvBorrowingList.SelectedRows)
                {
                    string ctID = row.Cells["IDChiTietMuon"].Value.ToString();
                    string sachID = row.Cells["IDSach"].Value.ToString();
                    DateTime dueDate = Convert.ToDateTime(row.Cells["HanTra"].Value).Date;
                    
                    decimal rowFine = 0;
                    if (returnDate > dueDate)
                    {
                        rowFine = (returnDate - dueDate).Days * FINE_PER_DAY;
                    }

                    if (chkIsLost.Checked)
                    {
                        string queryGia = "SELECT GiaBan FROM ThongTinSach WHERE IDSach = @SachID";
                        object resultGia = DatabaseHelper.ExecuteScalar(queryGia, new SqlParameter[] { new SqlParameter("@SachID", sachID) });
                        decimal giaBan = resultGia != null ? Convert.ToDecimal(resultGia) : 0;
                        rowFine = Math.Max(rowFine, giaBan);

                        // 1. Ghi nhận mất sách
                        string matSachID = "MS" + DateTime.Now.Ticks.ToString().Substring(10);
                        string queryMat = "INSERT INTO GhiNhanMatSach (IDPhieuMatSach, IDNguoiMuon, IDSach, NgayGhiNhan, TienPhat) " +
                                          "VALUES (@MSID, @ReaderID, @SachID, @Ngay, @Fine)";
                        DatabaseHelper.ExecuteNonQuery(queryMat, new SqlParameter[] {
                            new SqlParameter("@MSID", matSachID),
                            new SqlParameter("@ReaderID", txtReaderID.Text),
                            new SqlParameter("@SachID", sachID),
                            new SqlParameter("@Ngay", returnDate),
                            new SqlParameter("@Fine", rowFine)
                        });

                        // 2. Cập nhật trạng thái sách
                        DatabaseHelper.ExecuteNonQuery("UPDATE ThongTinSach SET TinhTrang = N'Mất' WHERE IDSach = @SachID", 
                            new SqlParameter[] { new SqlParameter("@SachID", sachID) });
                    }
                    else
                    {
                        // 1. Cập nhật ChiTietMuon
                        string queryUpdateCT = "UPDATE ChiTietMuon SET NgayTra = @ReturnDate, TienPhat = @Fine, TinhTrangTra = N'Đã trả' " +
                                               "WHERE IDChiTietMuon = @CTID";
                        DatabaseHelper.ExecuteNonQuery(queryUpdateCT, new SqlParameter[] {
                            new SqlParameter("@ReturnDate", returnDate),
                            new SqlParameter("@Fine", rowFine),
                            new SqlParameter("@CTID", ctID)
                        });

                        // 2. Trả sách về trạng thái Sẵn sàng
                        DatabaseHelper.ExecuteNonQuery("UPDATE ThongTinSach SET TinhTrang = N'Sẵn sàng' WHERE IDSach = @SachID", 
                            new SqlParameter[] { new SqlParameter("@SachID", sachID) });
                    }
                    totalFineIssued += rowFine;
                }

                // 3. CẬP NHẬT NỢ CHO ĐỘC GIẢ
                if (totalFineIssued > 0)
                {
                    string queryUpdateDebt = "UPDATE TheDocGia SET TienNo = TienNo + @TotalFine WHERE IDDocGia = @ReaderID";
                    DatabaseHelper.ExecuteNonQuery(queryUpdateDebt, new SqlParameter[] {
                        new SqlParameter("@TotalFine", totalFineIssued),
                        new SqlParameter("@ReaderID", txtReaderID.Text)
                    });
                }

                // 4. TỰ ĐỘNG CẬP NHẬT LOẠI ĐỘC GIẢ (Cơ chế chuyển đổi W -> G -> B)
                UpdateReaderViolationStatus(txtReaderID.Text.Trim());

                string message = chkIsLost.Checked ? "Đã ghi nhận mất sách." : "Đã nhận trả sách thành công!";
                if (totalFineIssued > 0) message += $"\nTổng tiền phạt phát sinh: {totalFineIssued:N0} VNĐ (Đã cộng vào nợ độc giả).";
                
                MessageBox.Show(message, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xử lý trả sách: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateReaderViolationStatus(string readerID)
        {
            try
            {
                // Chỉ đếm vi phạm trong vòng 12 tháng gần nhất
                DateTime oneYearAgo = DateTime.Now.AddYears(-1);
                
                string queryCount = @"
                    SELECT COUNT(*) 
                    FROM ChiTietMuon CT 
                    JOIN PhieuMuon PM ON CT.IDPhieuMuon = PM.IDPhieuMuon 
                    WHERE PM.IDNguoiMuon = @ID 
                      AND CT.NgayTra IS NOT NULL
                      AND CT.NgayTra >= @OneYearAgo
                      AND (CT.NgayTra > CT.HanTra OR CT.TinhTrangTra = N'Mất' OR CT.TinhTrangTra = N'Hỏng')";
                
                int violationCount = Convert.ToInt32(DatabaseHelper.ExecuteScalar(queryCount, new SqlParameter[] { 
                    new SqlParameter("@ID", readerID),
                    new SqlParameter("@OneYearAgo", oneYearAgo)
                }));

                string currentQuery = "SELECT LoaiDocGia FROM TheDocGia WHERE IDDocGia = @ID";
                string currentType = DatabaseHelper.ExecuteScalar(currentQuery, new SqlParameter[] { new SqlParameter("@ID", readerID) })?.ToString() ?? "Whitelist";

                string newType = "Whitelist";
                if (violationCount >= 3)
                {
                    newType = "Blacklist";
                }
                else if (violationCount >= 1)
                {
                    newType = "Graylist";
                }

                // Chỉ cập nhật nếu có thay đổi để tránh trigger không cần thiết
                if (newType != currentType)
                {
                    string queryUpdate = "UPDATE TheDocGia SET LoaiDocGia = @NewType WHERE IDDocGia = @ID";
                    DatabaseHelper.ExecuteNonQuery(queryUpdate, new SqlParameter[] {
                        new SqlParameter("@NewType", newType),
                        new SqlParameter("@ID", readerID)
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi cập nhật hạng độc giả: " + ex.Message);
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
