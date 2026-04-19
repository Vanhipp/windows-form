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
    public partial class FrmReaderCard : Form
    {
        public FrmReaderCard()
        {
            InitializeComponent();
            LoadReaderTypes();
            
            this.btnAdd.Click += BtnAdd_Click;
            this.btnCancel.Click += BtnCancel_Click;
        }

        private void LoadReaderTypes()
        {
            cboReaderType.DataSource = Enum.GetValues(typeof(ReaderType));
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                MessageBox.Show("Vui lòng nhập họ tên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string readerID = "DG" + DateTime.Now.ToString("yyyyMMddHHmmss").Substring(8);
            
            string query = "INSERT INTO TheDocGia (IDDocGia, HoTen, NgaySinh, DiaChi, Email, NgayLap, LoaiDocGia, TienNo) " +
                           "VALUES (@ID, @Name, @DOB, @Address, @Email, @RegDate, @Type, @Debt)";
            
            SqlParameter[] parameters = {
                new SqlParameter("@ID", readerID),
                new SqlParameter("@Name", txtFullName.Text),
                new SqlParameter("@DOB", dtpBirthDate.Value),
                new SqlParameter("@Address", txtAddress.Text),
                new SqlParameter("@Email", txtEmail.Text),
                new SqlParameter("@RegDate", DateTime.Now),
                new SqlParameter("@Type", cboReaderType.SelectedItem.ToString()),
                new SqlParameter("@Debt", 0)
            };

            try
            {
                DatabaseHelper.ExecuteNonQuery(query, parameters);
                MessageBox.Show($"Lập thẻ độc giả thành công! Mã độc giả: {readerID}", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
