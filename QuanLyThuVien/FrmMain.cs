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
using FontAwesome.Sharp;

namespace QuanLyThuVien
{
    public partial class FrmMain : Form
    {

        Form currentForm = null;

        bool isCollapsed = false;
        Timer sidebarTimer = new Timer();

        int sidebarMaxWidth = 236;
        int sidebarMinWidth = 70;

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
                EnableButton(HSNhanVien);
                EnableButton(TheDocGia);
                EnableButton(PhieuMuonSach);
                EnableButton(PhieuTraSach);
                EnableButton(TiepNhanSachMoi);
                EnableButton(TraCuuSach);
                EnableButton(ThuTienPhat);
                EnableButton(ThanhLy);
                EnableButton(BaoCaoThongKe);

            }
            else if (CurrentUser.BoPhan == "Ban Giám Đốc")
            {
                EnableButton(HSNhanVien);
                DisableButton(TheDocGia);
                DisableButton(PhieuMuonSach);
                DisableButton(PhieuTraSach);
                DisableButton(TiepNhanSachMoi);
                EnableButton(TraCuuSach);
                DisableButton(ThuTienPhat);
                DisableButton(ThanhLy);
                EnableButton(BaoCaoThongKe);
            }
            else if (CurrentUser.BoPhan == "Thủ Thư")
            {
                DisableButton(HSNhanVien);
                EnableButton(TheDocGia);
                EnableButton(PhieuMuonSach);
                EnableButton(PhieuTraSach);
                DisableButton(TiepNhanSachMoi);
                EnableButton(TraCuuSach);
                DisableButton(ThuTienPhat);
                DisableButton(ThanhLy);
                DisableButton(BaoCaoThongKe);
            }
            else if (CurrentUser.BoPhan == "Thủ Kho")
            {
                DisableButton(HSNhanVien);
                DisableButton(TheDocGia);
                DisableButton(PhieuMuonSach);
                DisableButton(PhieuTraSach);
                EnableButton(TiepNhanSachMoi);
                EnableButton(TraCuuSach);
                DisableButton(ThuTienPhat);
                EnableButton(ThanhLy);
                DisableButton(BaoCaoThongKe);
            }
            else if (CurrentUser.BoPhan == "Thủ Quỹ")
            {
                DisableButton(HSNhanVien);
                DisableButton(TheDocGia);
                DisableButton(PhieuMuonSach);
                DisableButton(PhieuTraSach);
                DisableButton(TiepNhanSachMoi);
                EnableButton(TraCuuSach);
                EnableButton(ThuTienPhat);
                DisableButton(ThanhLy);
                DisableButton(BaoCaoThongKe);
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

        private void TroGiupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Quản Lý Thư Viện:\n\n\n" +
                "1. Nguyễn Đăng Khoa - 2311554033\n\n" +
                "2. Nguyễn Lê Khánh Hoàng - 2311552947\n\n" +
                "3. Đỗ Văn Hiệp - 2311553289\n\n" +
                "4. Nguyễn Lê Văn Dũng - 2311555475\n\n" +
                "5. Nguyễn Hữu Giàu - 2311553450", "Thông tin nhóm 3", MessageBoxButtons.OK);
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            sidebarTimer.Start();
        }

        private void ShowText()
        {
            HSNhanVien.Text = "Hồ sơ nhân viên";
            TheDocGia.Text = "Thẻ độc giả";
            PhieuMuonSach.Text = "Phiếu mượn sách";
            PhieuTraSach.Text = "Phiếu trả sách";
            TiepNhanSachMoi.Text = "Tiếp nhận sách mới";
            TraCuuSach.Text = "Tra cứu sách";
            ThuTienPhat.Text = "Thu tiền phạt";
            ThanhLy.Text = "Thanh lý sách";
            BaoCaoThongKe.Text = "Báo cáo thống kê";

            // thêm các nút khác

            foreach (var ctrl in panel1.Controls)
            {
                if (ctrl is FontAwesome.Sharp.IconButton btn && btn.Name != "btnMenu")
                {

                    btn.Width = 229;  // đủ icon + text
                    btn.Height = 50;

                    btn.TextAlign = ContentAlignment.MiddleCenter;
                    btn.TextImageRelation = TextImageRelation.ImageBeforeText;


                    btn.Padding = new Padding(10, 0, 0, 0);
                }
            }
        }

        private void HideText()
        {
            foreach (var ctrl in panel1.Controls)
            {
                if (ctrl is FontAwesome.Sharp.IconButton btn && btn.Name != "btnMenu")
                {
                    btn.Text = "";
                    btn.Width = 60;   // chỉ icon
                    btn.Height = 50;
                    btn.ImageAlign = ContentAlignment.MiddleCenter;
                    btn.Padding = new Padding(0);
                }
            }
        }

        private void SidebarTimer_Tick(object sender, EventArgs e)
        {
            if (isCollapsed)
            {

                splitter1.Width += 10;
                panel1.Width += 10;

                if (panel1.Width >= sidebarMaxWidth)
                {
                    sidebarTimer.Stop();
                    isCollapsed = false;
                    ShowText(); // hiện lại chữ
                }
            }
            else
            {
                splitter1.Width -= 10;
                panel1.Width -= 10;

                if (panel1.Width <= sidebarMinWidth)
                {
                    sidebarTimer.Stop();
                    isCollapsed = true;
                    HideText(); // ẩn chữ
                }
            }
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            InitHover();

            sidebarTimer.Interval = 10;
            sidebarTimer.Tick += SidebarTimer_Tick;

            OpenForm(new FrmLogin(this));

            DangXuat.Visible = false;
 

            DisableButton(HSNhanVien);
            DisableButton(TheDocGia);
            DisableButton(PhieuMuonSach);
            DisableButton(PhieuTraSach);
            DisableButton(TiepNhanSachMoi);
            DisableButton(TraCuuSach);
            DisableButton(ThuTienPhat);
            DisableButton(ThanhLy);
            DisableButton(BaoCaoThongKe);
        }

        private void DangXuat_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Bạn đã có chắc chắn muốn đăng xuất?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
            {
                return;
            }

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

            DisableButton(HSNhanVien);
            DisableButton(TheDocGia);
            DisableButton(PhieuMuonSach);
            DisableButton(PhieuTraSach);
            DisableButton(TiepNhanSachMoi);
            DisableButton(TraCuuSach);
            DisableButton(ThuTienPhat);
            DisableButton(ThanhLy);
            DisableButton(BaoCaoThongKe);
        }

        private void DisableButton(IconButton btn)
        {
            btn.Enabled = false;

            btn.BackColor = Color.White;
            btn.ForeColor = Color.Gray;
            btn.IconColor = Color.Gray;

            btn.Font = new Font(btn.Font, FontStyle.Regular);
        }

        private void EnableButton(IconButton btn)
        {
            btn.Enabled = true;

            btn.BackColor = Color.White;
            btn.ForeColor = Color.Black;
            btn.IconColor = Color.Black;
            btn.Font = new Font(btn.Font, FontStyle.Bold);
        }

        private void Hover_Enter(object sender, EventArgs e)
        {
            var btn = sender as Button;

            if (!btn.Enabled || btn.Name == "btnMenu") return;

            btn.BackColor = Color.LightBlue;

            btn.Width += 6;
            btn.Height += 6;
            btn.Left -= 3;
            btn.Top -= 3;
        }

        private void Hover_Leave(object sender, EventArgs e)
        {
            var btn = sender as Button;
            if (btn.Name == "btnMenu") return;

            btn.BackColor = Color.White;
            btn.Width -= 6;
            btn.Height -= 6;
            btn.Left += 3;
            btn.Top += 3;
        }

        private void InitHover()
        {
            foreach (Control ctrl in panel1.Controls)
            {
                if (ctrl is Button btn && btn.Name != "btnMenu")
                {
                    // Lưu vị trí + size gốc
                    btn.Tag = new Rectangle(btn.Location, btn.Size);

                    btn.MouseEnter += Hover_Enter;
                    btn.MouseLeave += Hover_Leave;
                }
            }
        }

    }
}
