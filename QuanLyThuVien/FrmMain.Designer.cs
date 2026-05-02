namespace QuanLyThuVien
{
    partial class FrmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.DangNhap = new System.Windows.Forms.ToolStripMenuItem();
            this.DangXuat = new System.Windows.Forms.ToolStripMenuItem();
            this.TroGiup = new System.Windows.Forms.ToolStripMenuItem();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.PanelContent = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.BaoCaoThongKe = new FontAwesome.Sharp.IconButton();
            this.ThanhLy = new FontAwesome.Sharp.IconButton();
            this.ThuTienPhat = new FontAwesome.Sharp.IconButton();
            this.TraCuuSach = new FontAwesome.Sharp.IconButton();
            this.TiepNhanSachMoi = new FontAwesome.Sharp.IconButton();
            this.PhieuTraSach = new FontAwesome.Sharp.IconButton();
            this.PhieuMuonSach = new FontAwesome.Sharp.IconButton();
            this.TheDocGia = new FontAwesome.Sharp.IconButton();
            this.HSNhanVien = new FontAwesome.Sharp.IconButton();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(55)))), ((int)(((byte)(49)))));
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DangNhap,
            this.DangXuat,
            this.TroGiup});
            this.menuStrip1.Location = new System.Drawing.Point(236, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1449, 28);
            this.menuStrip1.TabIndex = 10;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // DangNhap
            // 
            this.DangNhap.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DangNhap.ForeColor = System.Drawing.SystemColors.Control;
            this.DangNhap.Name = "DangNhap";
            this.DangNhap.Size = new System.Drawing.Size(99, 24);
            this.DangNhap.Text = "Đăng nhập";
            this.DangNhap.Click += new System.EventHandler(this.DangNhapToolStripMenuItem_Click);
            // 
            // DangXuat
            // 
            this.DangXuat.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DangXuat.ForeColor = System.Drawing.SystemColors.Control;
            this.DangXuat.Name = "DangXuat";
            this.DangXuat.Size = new System.Drawing.Size(95, 24);
            this.DangXuat.Text = "Đăng xuất";
            this.DangXuat.Click += new System.EventHandler(this.DangXuat_Click);
            // 
            // TroGiup
            // 
            this.TroGiup.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TroGiup.ForeColor = System.Drawing.SystemColors.Control;
            this.TroGiup.Name = "TroGiup";
            this.TroGiup.Size = new System.Drawing.Size(81, 24);
            this.TroGiup.Text = "Trợ giúp";
            this.TroGiup.Click += new System.EventHandler(this.TroGiupToolStripMenuItem_Click);
            // 
            // splitter1
            // 
            this.splitter1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.splitter1.Location = new System.Drawing.Point(0, 0);
            this.splitter1.Margin = new System.Windows.Forms.Padding(4);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(236, 838);
            this.splitter1.TabIndex = 12;
            this.splitter1.TabStop = false;
            // 
            // PanelContent
            // 
            this.PanelContent.BackColor = System.Drawing.Color.White;
            this.PanelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelContent.Location = new System.Drawing.Point(236, 28);
            this.PanelContent.Name = "PanelContent";
            this.PanelContent.Size = new System.Drawing.Size(1449, 810);
            this.PanelContent.TabIndex = 13;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.panel1.Controls.Add(this.BaoCaoThongKe);
            this.panel1.Controls.Add(this.ThanhLy);
            this.panel1.Controls.Add(this.ThuTienPhat);
            this.panel1.Controls.Add(this.TraCuuSach);
            this.panel1.Controls.Add(this.TiepNhanSachMoi);
            this.panel1.Controls.Add(this.PhieuTraSach);
            this.panel1.Controls.Add(this.PhieuMuonSach);
            this.panel1.Controls.Add(this.TheDocGia);
            this.panel1.Controls.Add(this.HSNhanVien);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(236, 838);
            this.panel1.TabIndex = 14;
            // 
            // BaoCaoThongKe
            // 
            this.BaoCaoThongKe.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.BaoCaoThongKe.FlatAppearance.BorderSize = 2;
            this.BaoCaoThongKe.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BaoCaoThongKe.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BaoCaoThongKe.IconChar = FontAwesome.Sharp.IconChar.ChartColumn;
            this.BaoCaoThongKe.IconColor = System.Drawing.Color.Black;
            this.BaoCaoThongKe.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.BaoCaoThongKe.IconSize = 30;
            this.BaoCaoThongKe.Location = new System.Drawing.Point(3, 707);
            this.BaoCaoThongKe.Name = "BaoCaoThongKe";
            this.BaoCaoThongKe.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.BaoCaoThongKe.Size = new System.Drawing.Size(229, 50);
            this.BaoCaoThongKe.TabIndex = 17;
            this.BaoCaoThongKe.Text = "Báo cáo - Thống kê";
            this.BaoCaoThongKe.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BaoCaoThongKe.UseVisualStyleBackColor = true;
            this.BaoCaoThongKe.Click += new System.EventHandler(this.BaoCaoThongKe_Click);
            // 
            // ThanhLy
            // 
            this.ThanhLy.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.ThanhLy.FlatAppearance.BorderSize = 2;
            this.ThanhLy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ThanhLy.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ThanhLy.IconChar = FontAwesome.Sharp.IconChar.Sellcast;
            this.ThanhLy.IconColor = System.Drawing.Color.Black;
            this.ThanhLy.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.ThanhLy.IconSize = 30;
            this.ThanhLy.Location = new System.Drawing.Point(3, 628);
            this.ThanhLy.Name = "ThanhLy";
            this.ThanhLy.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.ThanhLy.Size = new System.Drawing.Size(229, 50);
            this.ThanhLy.TabIndex = 16;
            this.ThanhLy.Text = "Thanh lý";
            this.ThanhLy.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ThanhLy.UseVisualStyleBackColor = true;
            this.ThanhLy.Click += new System.EventHandler(this.ThanhLy_Click);
            // 
            // ThuTienPhat
            // 
            this.ThuTienPhat.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.ThuTienPhat.FlatAppearance.BorderSize = 2;
            this.ThuTienPhat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ThuTienPhat.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ThuTienPhat.IconChar = FontAwesome.Sharp.IconChar.MoneyBill1Wave;
            this.ThuTienPhat.IconColor = System.Drawing.Color.Black;
            this.ThuTienPhat.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.ThuTienPhat.IconSize = 30;
            this.ThuTienPhat.Location = new System.Drawing.Point(3, 547);
            this.ThuTienPhat.Name = "ThuTienPhat";
            this.ThuTienPhat.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.ThuTienPhat.Size = new System.Drawing.Size(229, 50);
            this.ThuTienPhat.TabIndex = 15;
            this.ThuTienPhat.Text = "Thu tiền phạt";
            this.ThuTienPhat.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ThuTienPhat.UseVisualStyleBackColor = true;
            this.ThuTienPhat.Click += new System.EventHandler(this.ThuTienPhat_Click);
            // 
            // TraCuuSach
            // 
            this.TraCuuSach.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.TraCuuSach.FlatAppearance.BorderSize = 2;
            this.TraCuuSach.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TraCuuSach.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TraCuuSach.IconChar = FontAwesome.Sharp.IconChar.Searchengin;
            this.TraCuuSach.IconColor = System.Drawing.Color.Black;
            this.TraCuuSach.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.TraCuuSach.IconSize = 30;
            this.TraCuuSach.Location = new System.Drawing.Point(3, 466);
            this.TraCuuSach.Name = "TraCuuSach";
            this.TraCuuSach.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.TraCuuSach.Size = new System.Drawing.Size(229, 50);
            this.TraCuuSach.TabIndex = 14;
            this.TraCuuSach.Text = "Tra cứu sách";
            this.TraCuuSach.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.TraCuuSach.UseVisualStyleBackColor = true;
            this.TraCuuSach.Click += new System.EventHandler(this.TraCuuSach_Click);
            // 
            // TiepNhanSachMoi
            // 
            this.TiepNhanSachMoi.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.TiepNhanSachMoi.FlatAppearance.BorderSize = 2;
            this.TiepNhanSachMoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TiepNhanSachMoi.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TiepNhanSachMoi.IconChar = FontAwesome.Sharp.IconChar.Info;
            this.TiepNhanSachMoi.IconColor = System.Drawing.Color.Black;
            this.TiepNhanSachMoi.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.TiepNhanSachMoi.IconSize = 30;
            this.TiepNhanSachMoi.Location = new System.Drawing.Point(3, 389);
            this.TiepNhanSachMoi.Name = "TiepNhanSachMoi";
            this.TiepNhanSachMoi.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.TiepNhanSachMoi.Size = new System.Drawing.Size(229, 50);
            this.TiepNhanSachMoi.TabIndex = 13;
            this.TiepNhanSachMoi.Text = "Tiếp nhận sách mới";
            this.TiepNhanSachMoi.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.TiepNhanSachMoi.UseVisualStyleBackColor = true;
            this.TiepNhanSachMoi.Click += new System.EventHandler(this.TiepNhanSachMoi_Click);
            // 
            // PhieuTraSach
            // 
            this.PhieuTraSach.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.PhieuTraSach.FlatAppearance.BorderSize = 2;
            this.PhieuTraSach.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PhieuTraSach.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PhieuTraSach.IconChar = FontAwesome.Sharp.IconChar.Repeat;
            this.PhieuTraSach.IconColor = System.Drawing.Color.Black;
            this.PhieuTraSach.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.PhieuTraSach.IconSize = 30;
            this.PhieuTraSach.Location = new System.Drawing.Point(3, 311);
            this.PhieuTraSach.Name = "PhieuTraSach";
            this.PhieuTraSach.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.PhieuTraSach.Size = new System.Drawing.Size(229, 50);
            this.PhieuTraSach.TabIndex = 12;
            this.PhieuTraSach.Text = "Phiếu Trả sách";
            this.PhieuTraSach.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.PhieuTraSach.UseVisualStyleBackColor = true;
            this.PhieuTraSach.Click += new System.EventHandler(this.PhieuTraSach_Click);
            // 
            // PhieuMuonSach
            // 
            this.PhieuMuonSach.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.PhieuMuonSach.FlatAppearance.BorderSize = 2;
            this.PhieuMuonSach.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PhieuMuonSach.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PhieuMuonSach.IconChar = FontAwesome.Sharp.IconChar.BookOpen;
            this.PhieuMuonSach.IconColor = System.Drawing.Color.Black;
            this.PhieuMuonSach.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.PhieuMuonSach.IconSize = 30;
            this.PhieuMuonSach.Location = new System.Drawing.Point(3, 233);
            this.PhieuMuonSach.Name = "PhieuMuonSach";
            this.PhieuMuonSach.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.PhieuMuonSach.Size = new System.Drawing.Size(229, 50);
            this.PhieuMuonSach.TabIndex = 11;
            this.PhieuMuonSach.Text = "Phiếu mượn sách";
            this.PhieuMuonSach.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.PhieuMuonSach.UseVisualStyleBackColor = true;
            this.PhieuMuonSach.Click += new System.EventHandler(this.PhieuMuonSach_Click);
            // 
            // TheDocGia
            // 
            this.TheDocGia.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.TheDocGia.FlatAppearance.BorderSize = 2;
            this.TheDocGia.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TheDocGia.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TheDocGia.IconChar = FontAwesome.Sharp.IconChar.IdCard;
            this.TheDocGia.IconColor = System.Drawing.Color.Black;
            this.TheDocGia.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.TheDocGia.IconSize = 30;
            this.TheDocGia.Location = new System.Drawing.Point(3, 151);
            this.TheDocGia.Name = "TheDocGia";
            this.TheDocGia.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.TheDocGia.Size = new System.Drawing.Size(229, 50);
            this.TheDocGia.TabIndex = 10;
            this.TheDocGia.Text = "Thẻ độc giả";
            this.TheDocGia.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.TheDocGia.UseVisualStyleBackColor = true;
            this.TheDocGia.Click += new System.EventHandler(this.TheDocGia_Click);
            // 
            // HSNhanVien
            // 
            this.HSNhanVien.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.HSNhanVien.FlatAppearance.BorderSize = 2;
            this.HSNhanVien.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.HSNhanVien.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HSNhanVien.IconChar = FontAwesome.Sharp.IconChar.User;
            this.HSNhanVien.IconColor = System.Drawing.Color.Black;
            this.HSNhanVien.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.HSNhanVien.IconSize = 20;
            this.HSNhanVien.Location = new System.Drawing.Point(3, 73);
            this.HSNhanVien.Name = "HSNhanVien";
            this.HSNhanVien.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.HSNhanVien.Size = new System.Drawing.Size(229, 50);
            this.HSNhanVien.TabIndex = 9;
            this.HSNhanVien.Text = "Hồ sơ nhân viên";
            this.HSNhanVien.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.HSNhanVien.UseVisualStyleBackColor = true;
            this.HSNhanVien.Click += new System.EventHandler(this.HSNhanVien_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1685, 838);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.PanelContent);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.splitter1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmMain";
            this.Text = "Quản Lý Thư Viện";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem DangNhap;
        private System.Windows.Forms.ToolStripMenuItem TroGiup;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.ToolStripMenuItem DangXuat;
        private System.Windows.Forms.Panel PanelContent;
        private System.Windows.Forms.Panel panel1;
        private FontAwesome.Sharp.IconButton TheDocGia;
        private FontAwesome.Sharp.IconButton HSNhanVien;
        private FontAwesome.Sharp.IconButton TiepNhanSachMoi;
        private FontAwesome.Sharp.IconButton PhieuTraSach;
        private FontAwesome.Sharp.IconButton PhieuMuonSach;
        private FontAwesome.Sharp.IconButton ThanhLy;
        private FontAwesome.Sharp.IconButton ThuTienPhat;
        private FontAwesome.Sharp.IconButton TraCuuSach;
        private FontAwesome.Sharp.IconButton BaoCaoThongKe;
    }
}

