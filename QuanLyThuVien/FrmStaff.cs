using QuanLyThuVien.Managers;
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
        string chuoiketnoi = @"Data Source=VanHipp;Initial Catalog=QUANLYTHUVIEN;Integrated Security=True";
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

            var conn = new SqlConnection(chuoiketnoi);
            conn.Open();
            var dataTable = new DataTable();
            var dataAdapter = new SqlDataAdapter("SELECT * FROM HoSoNhanVien", conn);
            dataAdapter.Fill(dataTable);
            conn.Close();

            dataGridView1.DataSource = dataTable;
        }

        private void Thembtn_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra dữ liệu trống
                if (string.IsNullOrWhiteSpace(cboidnhanvien.Text) ||
                    string.IsNullOrWhiteSpace(cboHoTen.Text) ||
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

                var sql = "INSERT INTO HoSoNhanVien (IDNhanVien, HoTen, NgaySinh, DiaChi, DienThoai, BangCap, BoPhan, ChucVu, MatKhau) " +
                    "VALUES (@IDNhanVien, @HoTen, @NgaySinh, @DiaChi, @DienThoai, @BangCap, @BoPhan, @ChucVu, @MatKhau)";

                //Tránh SQL Injection 
                var parameters = new[]
                {
                    new SqlParameter("@IDNhanVien", cboidnhanvien.Text.Trim()),
                    new SqlParameter("@HoTen", cboHoTen.Text.Trim()),
                    new SqlParameter("@NgaySinh", ngaySinh),
                    new SqlParameter("@DiaChi", cboDiaChi.Text.Trim()),
                    new SqlParameter("@DienThoai", cboSDT.Text.Trim()),
                    new SqlParameter("@BangCap", cboBangCap.SelectedItem?.ToString() ?? ""),
                    new SqlParameter("@BoPhan", cboBoPhan.SelectedItem?.ToString() ?? ""),
                    new SqlParameter("@ChucVu", cboChucVu.SelectedItem?.ToString() ?? ""),
                    new SqlParameter("@MatKhau", cboMatKhau.Text.Trim())
                };

                var result = ExecuteSqlWithParameters(sql, parameters);

                if (result)
                {
                    MessageBox.Show("Thêm nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FrmStaff_Load(sender, e);
                }
                else
                {
                    MessageBox.Show("Thêm nhân viên thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ExecuteSqlWithParameters(string sql, SqlParameter[] parameters)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(chuoiketnoi))
                {
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddRange(parameters);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
                return false;
            }
        }

        private void btnxoa_Click_1(object sender, EventArgs e)
        {
            var malop = cboidnhanvien.Text.Trim();
            if (malop.Length > 0)
            {
                var result = MessageBox.Show("Bạn có chắc chắn muốn xóa lớp không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    var sql = $"DELETE FROM HoSoNhanVien WHERE IDNhanVien = N'{cboidnhanvien.Text}'";
                    var xoatat = DataProvider.TruyVan_XuLy(sql);
                    FrmStaff_Load(sender, e);
                }
            }

        }
    }
}
