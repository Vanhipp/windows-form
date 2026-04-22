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
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        public static class CurrentUser
        {
            public static string ID { get; set; }
            public static string HoTen { get; set; }
            public static string BoPhan { get; set; }
            public static string ChucVu { get; set; }
        }

        private void Login(object sender, EventArgs e)
        {
            try
            {
                string idNhanVien = taikhoan.Text.Trim();
                string matKhau = matkhau.Text.Trim();

                if (string.IsNullOrWhiteSpace(idNhanVien) || string.IsNullOrWhiteSpace(matKhau))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ tài khoản và mật khẩu!");
                    return;
                }

                string sql = @"SELECT IDNhanVien, HoTen, BoPhan, ChucVu 
                       FROM HoSoNhanVien 
                       WHERE IDNhanVien = @IDNhanVien AND MatKhau = @MatKhau";

                SqlParameter[] paras =
                {
            new SqlParameter("@IDNhanVien", idNhanVien),
            new SqlParameter("@MatKhau", matKhau)
        };

                var taikhoa = DataProvider.SelectData(sql, CommandType.Text, paras);

                if (taikhoa != null && taikhoa.Rows.Count > 0)
                {
                    var row = taikhoa.Rows[0];
                    
                    CurrentUser.ID = row["IDNhanVien"].ToString();
                    CurrentUser.HoTen = row["HoTen"].ToString();
                    CurrentUser.BoPhan = row["BoPhan"].ToString();
                    CurrentUser.ChucVu = row["ChucVu"].ToString();

                    // 🔥 gọi về FrmMain
                    FrmMain main = (FrmMain)this.MdiParent;
                    main.UpdateUserUI();

                    MessageBox.Show($"Xin chào! {CurrentUser.HoTen}\n" + $"Bộ phận: {CurrentUser.BoPhan}\n" + $"Chức vụ: {CurrentUser.ChucVu}", "Thông tin người dùng", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.Close();
                }
                else
                {
                    MessageBox.Show("Sai tài khoản hoặc mật khẩu!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        public static class Permission
        {
            public static bool IsGiamDoc()
                => CurrentUser.BoPhan == "Ban Giám Đốc";
            public static bool IsPhoGiamDoc()
               => CurrentUser.ChucVu == "Thủ Quỹ";

            public static bool IsTruongPhong()
                => CurrentUser.ChucVu == "Thủ Kho";
            public static bool IsPhoPhong()
                => CurrentUser.ChucVu == "Thủ Thư";

        }
    }
}