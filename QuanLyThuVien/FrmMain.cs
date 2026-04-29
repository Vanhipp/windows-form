using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static QuanLyThuVien.FrmLogin;

namespace QuanLyThuVien
{
    public partial class FrmMain : Form
    {

        Form currentForm = null;

        public FrmMain()
        {
            InitializeComponent();
        }

        void OpenForm(Form f)
        {
            if (currentForm != null)
                currentForm.Close();

            currentForm = f;


            f.TopLevel = false;
            f.FormBorderStyle = FormBorderStyle.None;
            

            PanelContent.Controls.Clear();
            PanelContent.Controls.Add(f);

            if (f is FrmLogin)
            {
                f.Dock = DockStyle.None;
                f.Left = (PanelContent.Width - f.Width) / 2;
                f.Top = (PanelContent.Height - f.Height) / 2;
            }
            else
            {
                f.Dock = DockStyle.Fill;
            }

            f.Show();
        }

        public void UpdateUserUI()
        {
            DangNhap.Text = CurrentUser.HoTen;
            DangXuat.Visible = true;
            DangNhap.Enabled = false;

            if (CurrentUser.BoPhan == "Quản Trị")
            {
                HSNhanVien.Enabled = true;
                TheDocGia.Enabled = true;
                PhieuMuonSach.Enabled = true;
                PhieuTraSach.Enabled = true;
                TiepNhanSachMoi.Enabled = true;
                TraCuuSach.Enabled = true;
                ThuTienPhat.Enabled = true;
                ThanhLy.Enabled = true;
                BaoCaoThongKe.Enabled = true;
                CaiDat.Enabled = true;
            }
            else if (CurrentUser.BoPhan == "Ban Giám Đốc")
            {
                HSNhanVien.Enabled = true;
                TheDocGia.Enabled = false;
                PhieuMuonSach.Enabled = false;
                PhieuTraSach.Enabled = false;
                TiepNhanSachMoi.Enabled = false;
                TraCuuSach.Enabled = true;
                ThuTienPhat.Enabled = false;
                ThanhLy.Enabled = false;
                BaoCaoThongKe.Enabled = true;
                CaiDat.Enabled = true;
            }
            else if (CurrentUser.BoPhan == "Thủ Thư")
            {
                HSNhanVien.Enabled = false;
                TheDocGia.Enabled = true;
                PhieuMuonSach.Enabled = true;
                PhieuTraSach.Enabled = true;
                TiepNhanSachMoi.Enabled = false;
                TraCuuSach.Enabled = true;
                ThuTienPhat.Enabled = false;
                ThanhLy.Enabled = false;
                BaoCaoThongKe.Enabled = false;
                CaiDat.Enabled = false;

            }
            else if (CurrentUser.BoPhan == "Thủ Kho")
            {
                HSNhanVien.Enabled = false;
                TheDocGia.Enabled = false;
                PhieuMuonSach.Enabled = false;
                PhieuTraSach.Enabled = false;
                TiepNhanSachMoi.Enabled = true;
                TraCuuSach.Enabled = true;
                ThuTienPhat.Enabled = false;
                ThanhLy.Enabled = true;
                BaoCaoThongKe.Enabled = false;
                CaiDat.Enabled = false;

            }
            else if (CurrentUser.BoPhan == "Thủ Quỹ")
            {
                HSNhanVien.Enabled = false;
                TheDocGia.Enabled = false;
                PhieuMuonSach.Enabled = false;
                PhieuTraSach.Enabled = false;
                TiepNhanSachMoi.Enabled = false;
                TraCuuSach.Enabled = true;
                ThuTienPhat.Enabled = true;
                ThanhLy.Enabled = false;
                BaoCaoThongKe.Enabled = false;
                CaiDat.Enabled = false;
            }
        }

        private void DangNhapToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (currentForm is FrmLogin)
                return;

            OpenForm(new FrmLogin(this));
        }

        private void HSNhanVien_Click(object sender, EventArgs e)
        {
            OpenForm(new FrmStaff());
        }

        private void TheDocGia_Click(object sender, EventArgs e)
        {

            if (CurrentUser.BoPhan == "Quản Trị" || CurrentUser.BoPhan == "Thủ Thư")
            {
                OpenForm(new FrmReaderCard());
            }
        }

        private void PhieuMuonSach_Click(object sender, EventArgs e)
        {
            if (CurrentUser.BoPhan == "Quản Trị" || CurrentUser.BoPhan == "Thủ Thư")
            {
                OpenForm(new FrmBorrow());
            }
        }

        private void PhieuTraSach_Click(object sender, EventArgs e)
        {
            if (CurrentUser.BoPhan == "Quản Trị" || CurrentUser.BoPhan == "Thủ Thư")
            {
                OpenForm(new FrmReturn());
            }
        }

        private void TiepNhanSachMoi_Click(object sender, EventArgs e)
        {
            if (CurrentUser.BoPhan == "Quản Trị" || CurrentUser.BoPhan == "Thủ Kho")
            {
                OpenForm(new FrmBookEntry());
            }
        }

        private void TraCuuSach_Click(object sender, EventArgs e)
        {
            if (CurrentUser.BoPhan == "Quản Trị" || CurrentUser.BoPhan == "Thủ Kho" || CurrentUser.BoPhan == "Ban Giám Đốc" || CurrentUser.BoPhan == "Thủ Thư" || CurrentUser.BoPhan == "Thủ Quỹ")
            {
                OpenForm(new FrmSearchBook());
            }
        }

        private void ThuTienPhat_Click(object sender, EventArgs e)
        {
            if (CurrentUser.BoPhan == "Quản Trị" || CurrentUser.BoPhan == "Thủ Quỹ")
            {
                OpenForm(new FrmFineCollection());
            }
        }

        private void ThanhLy_Click(object sender, EventArgs e)
        {
            if (CurrentUser.BoPhan == "Quản Trị" || CurrentUser.BoPhan == "Thủ Kho")
            {
                OpenForm(new FrmLiquidation());
            }

        }

        private void BaoCaoThongKe_Click(object sender, EventArgs e)
        {
            if (CurrentUser.BoPhan == "Quản Trị" || CurrentUser.BoPhan == "Ban Giám Đốc")
            {
                OpenForm(new FrmReports());
            }

        }

        private void CaiDat_Click(object sender, EventArgs e)
        {
            if (CurrentUser.BoPhan == "Quản Trị" || CurrentUser.BoPhan == "Ban Giám Đốc")
            {
                OpenForm(new FrmSettings());
            }
        }


        private void TroGiupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Quản Lý Thư Viện:\n\n\n" +
                "1. Nguyễn Đăng Khoa - 2311554033\n\n" +
                "2. Nguyễn Lê Khánh Hoàng - 2311552947\n\n" +
                "3. Đỗ Văn Hiệp - 2311553289\n\n" +
                "4. Nguyễn Lê Văn Dũng - 2311555475\n\n" +
                "5. Nguyễn Hữu Giàu - 2311553450", "Thông tin nhóm 3", MessageBoxButtons.OK);
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            // Show login form automatically on startup (as MDI child)
            OpenForm(new FrmLogin(this));

            DangXuat.Visible = false;

            HSNhanVien.Enabled = false;
            TheDocGia.Enabled = false;
            PhieuMuonSach.Enabled = false;
            PhieuTraSach.Enabled = false;
            TiepNhanSachMoi.Enabled = false;
            TraCuuSach.Enabled = false;
            ThuTienPhat.Enabled = false;
            ThanhLy.Enabled = false;
            BaoCaoThongKe.Enabled = false;
            CaiDat.Enabled = false;
        }

        private void DangXuat_Click(object sender, EventArgs e)
        {
            // Clear current user info
            CurrentUser.ID = string.Empty;
            CurrentUser.HoTen = string.Empty;
            CurrentUser.BoPhan = string.Empty;
            CurrentUser.ChucVu = string.Empty;


            OpenForm(new FrmLogin(this));
            // Update UI to logged-out state
            DangXuat.Visible = false;
            DangNhap.Text = "Đăng nhập";
            DangNhap.Enabled = true;

            HSNhanVien.Enabled = false;
            TheDocGia.Enabled = false;
            PhieuMuonSach.Enabled = false;
            PhieuTraSach.Enabled = false;
            TiepNhanSachMoi.Enabled = false;
            TraCuuSach.Enabled = false;
            ThuTienPhat.Enabled = false;
            ThanhLy.Enabled = false;
            BaoCaoThongKe.Enabled = false;
            CaiDat.Enabled = false;
        }

    }
}
