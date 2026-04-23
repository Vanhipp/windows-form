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
        public FrmMain()
        {
            InitializeComponent();
        }

        public void UpdateUserUI()
        {
            DangNhap.Text = CurrentUser.HoTen;
            DangXuat.Visible = true;
            DangNhap.Enabled = false;
            if(CurrentUser.BoPhan == "Ban Giám Đốc")
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
            else if(CurrentUser.BoPhan == "Thủ Thư")
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
            else if(CurrentUser.BoPhan == "Thủ Kho")
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
            else if(CurrentUser.BoPhan == "Thủ Quỹ")
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
            foreach (Form frm in this.MdiChildren)
            {
                if(frm is FrmLogin)
                {
                    frm.Activate();
                    return;
                }
            }
            FrmLogin frmLogin = new FrmLogin();
            frmLogin.MdiParent = this;
            frmLogin.Show();
        }

        private void HSNhanVien_Click(object sender, EventArgs e)
        {
            if (CurrentUser.BoPhan == "Ban Giám Đốc") //Oke
            {
                foreach (Form frm in this.MdiChildren)
                {
                    if (frm is FrmStaff)
                    {
                        frm.Activate();
                        return;
                    }
                }
                FrmStaff frmStaff = new FrmStaff();
                frmStaff.MdiParent = this;
                frmStaff.Show();
            }
        }

        private void TheDocGia_Click(object sender, EventArgs e)
        {

            if (CurrentUser.BoPhan == "Thủ Thư") //Oke
            {
                foreach (Form frm in this.MdiChildren)
                {
                    if (frm is FrmReaderCard)
                    {
                        frm.Activate();
                        return;
                    }
                }
                FrmReaderCard frmReaderCard = new FrmReaderCard();
                frmReaderCard.MdiParent = this;
                frmReaderCard.Show();
            }
        }

        private void PhieuMuonSach_Click(object sender, EventArgs e)
        {
            if (CurrentUser.BoPhan == "Thủ Thư") //Oke
            {
                foreach (Form frm in this.MdiChildren)
                {
                    if (frm is FrmBorrow)
                    {
                        frm.Activate();
                        return;
                    }
                }
                FrmBorrow frmBorrow = new FrmBorrow();
                frmBorrow.MdiParent = this;
                frmBorrow.Show();
            }
        }

        private void PhieuTraSach_Click(object sender, EventArgs e)
        {
            if (CurrentUser.BoPhan == "Thủ Thư") //Oke
            {
                foreach (Form frm in this.MdiChildren)
                {
                    if (frm is FrmReturn)
                    {
                        frm.Activate();
                        return;
                    }
                }
                FrmReturn frmReturn = new FrmReturn();
                frmReturn.MdiParent = this;
                frmReturn.Show();
            }
        }

        private void TiepNhanSachMoi_Click(object sender, EventArgs e)
        {
            if (CurrentUser.BoPhan == "Thủ Kho") //Oke
            {
                foreach (Form frm in this.MdiChildren)
                {
                    if (frm is FrmBookEntry)
                    {
                        frm.Activate();
                        return;
                    }
                }
                FrmBookEntry frmBookEntry = new FrmBookEntry();
                frmBookEntry.MdiParent = this;
                frmBookEntry.Show();
            }
        }

        private void TraCuuSach_Click(object sender, EventArgs e)
        {
            if (CurrentUser.BoPhan == "Thủ Kho" || CurrentUser.BoPhan == "Ban Giám Đốc" || CurrentUser.BoPhan == "Thủ Thư" || CurrentUser.BoPhan == "Thủ Quỹ") //Oke
            {
                foreach (Form frm in this.MdiChildren)
                {
                    if (frm is FrmSearchBook)
                    {
                        frm.Activate();
                        return;
                    }
                }
                FrmSearchBook frmSearchBook = new FrmSearchBook();
                frmSearchBook.MdiParent = this;
                frmSearchBook.Show();
            }
        }

        private void ThuTienPhat_Click(object sender, EventArgs e)
        {
            if (CurrentUser.BoPhan == "Thủ Quỹ")//Oke
            {
                foreach (Form frm in this.MdiChildren)
                {
                    if (frm is FrmFineCollection)
                    {
                        frm.Activate();
                        return;
                    }
                }
                FrmFineCollection frmFineCollection = new FrmFineCollection();
                frmFineCollection.MdiParent = this;
                frmFineCollection.Show();
            }
        }

        private void ThanhLy_Click(object sender, EventArgs e)
        {
            if (CurrentUser.BoPhan == "Thủ Kho") //Oke
            {
                foreach (Form frm in this.MdiChildren)
                {
                    if (frm is FrmLiquidation)
                    {
                        frm.Activate();
                        return;
                    }
                }
                FrmLiquidation frmLiquidation = new FrmLiquidation();
                frmLiquidation.MdiParent = this;
                frmLiquidation.Show();
            }

        }

        private void BaoCaoThongKe_Click(object sender, EventArgs e)//Oke   
        {
            if (CurrentUser.BoPhan == "Ban Giám Đốc")
            {
                foreach (Form frm in this.MdiChildren)
                {
                    if (frm is FrmReports)
                    {
                        frm.Activate();
                        return;
                    }
                }
                FrmReports frmReports = new FrmReports();
                frmReports.MdiParent = this;
                frmReports.Show();
            }

        }

        private void CaiDat_Click(object sender, EventArgs e)
        {
            if (CurrentUser.BoPhan == "Ban Giám Đốc") //Oke
            {   
                foreach (Form frm in this.MdiChildren)
                {
                    if (frm is FrmSettings)
                    {
                        frm.Activate();
                        return;
                    }
                }
                FrmSettings frmSettings = new FrmSettings();
                frmSettings.MdiParent = this;
                frmSettings.Show(); 
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
            foreach (Form frm in this.MdiChildren)
            {
                if (frm is FrmLogin)
                {
                    frm.Activate();
                    return;
                }
            }

            FrmLogin frmLogin = new FrmLogin();
            frmLogin.MdiParent = this;
            frmLogin.Show();

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

            // Close all open child forms except the login form
            foreach (Form frm in this.MdiChildren)
            {
                if (!(frm is FrmLogin))
                    frm.Close();
            }

            foreach (Form frm in this.MdiChildren)
            {
                if (frm is FrmLogin)
                {
                    frm.Activate();
                    return;
                }
            }

            FrmLogin frmLogin = new FrmLogin();
            frmLogin.MdiParent = this;
            frmLogin.Show();

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
