using QuanLyThuVien.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyThuVien
{
    public partial class FrmStaff : Form
    {
        public FrmStaff()
        {
            InitializeComponent();
        }

        private void FrmStaff_Load(object sender, EventArgs e)
        {
            cboBangCap.SelectedIndex = 0;
            cboChucVu.SelectedIndex = 0;
            cboBoPhan.SelectedIndex = 0;
            LayHS_NhanVien();
        }

        private void LayHS_NhanVien()
        {
            try
            {
                string sql = "SELECT * FROM HoSoNhanVien";
                dataGridView1.DataSource = DatabaseHelper.ExecuteQuery(sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách nhân viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Thembtn_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra dữ liệu trống
                if (string.IsNullOrWhiteSpace(cboHoTen.Text) ||
                    string.IsNullOrWhiteSpace(cboDiaChi.Text) ||
                    string.IsNullOrWhiteSpace(cboSDT.Text) ||
                    string.IsNullOrWhiteSpace(cboMatKhau.Text))
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Kiểm tra định dạng ngày tháng
                if (!DateTime.TryParse(date_NS.Text, out DateTime ngaySinh))
                {
                    MessageBox.Show("Ngày sinh không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Kiểm tra BoPhan hợp lệ
                string selectedBoPhan = cboBoPhan.SelectedItem?.ToString() ?? "";
                string[] validBoPhan = { "Quản Trị", "Ban Giám Đốc", "Thủ Quỹ", "Thủ Kho", "Thủ Thư" };
                if (!validBoPhan.Contains(selectedBoPhan))
                {
                    MessageBox.Show("Bộ phận không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Kiểm tra ChucVu hợp lệ
                string selectedChucVu = cboChucVu.SelectedItem?.ToString() ?? "";
                string[] validChucVu = { "Giám Đốc", "Phó Giám Đốc", "Trưởng Phòng", "Phó Phòng", "Nhân Viên" };
                if (!validChucVu.Contains(selectedChucVu))
                {
                    MessageBox.Show("Chức vụ không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string identifierBoPhan = "";
                switch (selectedBoPhan)
                {
                    case "Quản Trị":
                        identifierBoPhan = "QT";
                        break;
                    case "Ban Giám Đốc":
                        identifierBoPhan = "BGD";
                        break;
                    case "Thủ Quỹ":
                        identifierBoPhan = "TQ";
                        break;
                    case "Thủ Kho":
                        identifierBoPhan = "TK";
                        break;
                    case "Thủ Thư":
                        identifierBoPhan = "TT";
                        break;
                }

                string staffID = identifierBoPhan + DateTime.Now.ToString("yyyyMMddHHmmss");

                string sql = "INSERT INTO HoSoNhanVien (IDNhanVien, HoTen, NgaySinh, DiaChi, DienThoai, BangCap, BoPhan, ChucVu, MatKhau) " +
                             "VALUES (@ID, @HoTen, @NgaySinh, @DiaChi, @DienThoai, @BangCap, @BoPhan, @ChucVu, @MatKhau)";

                SqlParameter[] parameters = {
                    new SqlParameter("@ID", staffID),
                    new SqlParameter("@HoTen", cboHoTen.Text.Trim()),
                    new SqlParameter("@NgaySinh", ngaySinh),
                    new SqlParameter("@DiaChi", cboDiaChi.Text.Trim()),
                    new SqlParameter("@DienThoai", cboSDT.Text.Trim()),
                    new SqlParameter("@BangCap", cboBangCap.SelectedItem?.ToString() ?? ""),
                    new SqlParameter("@BoPhan", selectedBoPhan),
                    new SqlParameter("@ChucVu", selectedChucVu),
                    new SqlParameter("@MatKhau", cboMatKhau.Text.Trim())
                };

                DatabaseHelper.ExecuteNonQuery(sql, parameters);
                MessageBox.Show("Thêm nhân viên thành công! ID: " + staffID, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LayHS_NhanVien();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm nhân viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnxoa_Click_1(object sender, EventArgs e)
        {
            try
            {
                // Ensure a row is selected
                if (dataGridView1.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn một nhân viên để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Get the ID of the selected row
                var selectedRow = dataGridView1.SelectedRows[0];
                string selectedID = selectedRow.Cells["IDNhanVien"].Value?.ToString();

                if (string.IsNullOrEmpty(selectedID))
                {
                    MessageBox.Show("Không thể xác định mã nhân viên!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Confirm deletion
                var confirmResult = MessageBox.Show($"Bạn có chắc chắn muốn xóa nhân viên với mã {selectedID}?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirmResult != DialogResult.Yes)
                {
                    return;
                }

                // Delete the record from the database
                string sql = "DELETE FROM HoSoNhanVien WHERE IDNhanVien = @ID";
                SqlParameter[] parameters = {
                    new SqlParameter("@ID", selectedID)
                };

                DatabaseHelper.ExecuteNonQuery(sql, parameters);

                // Remove the row from the DataGridView
                dataGridView1.Rows.Remove(selectedRow);

                MessageBox.Show("Xóa nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa nhân viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
