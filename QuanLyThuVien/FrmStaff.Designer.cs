namespace QuanLyThuVien
{
    partial class FrmStaff
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cboMatKhau = new System.Windows.Forms.TextBox();
            this.cboChucVu = new System.Windows.Forms.ComboBox();
            this.cboBoPhan = new System.Windows.Forms.ComboBox();
            this.cboHoTen = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cboBangCap = new System.Windows.Forms.ComboBox();
            this.cboSDT = new System.Windows.Forms.TextBox();
            this.cboDiaChi = new System.Windows.Forms.TextBox();
            this.btnxoa = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.Thembtn = new System.Windows.Forms.Button();
            this.date_NS = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.cboidnhanvien = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.cboMatKhau);
            this.groupBox1.Controls.Add(this.cboChucVu);
            this.groupBox1.Controls.Add(this.cboBoPhan);
            this.groupBox1.Controls.Add(this.cboHoTen);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cboBangCap);
            this.groupBox1.Controls.Add(this.cboSDT);
            this.groupBox1.Controls.Add(this.cboDiaChi);
            this.groupBox1.Controls.Add(this.btnxoa);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.Thembtn);
            this.groupBox1.Controls.Add(this.date_NS);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cboidnhanvien);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(453, 554);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Chi tiết nhân viên";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(8, 367);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(92, 24);
            this.label9.TabIndex = 20;
            this.label9.Text = "Mật khẩu";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 287);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(83, 24);
            this.label8.TabIndex = 19;
            this.label8.Text = "Bộ phận";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 247);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(92, 24);
            this.label7.TabIndex = 18;
            this.label7.Text = "Bằng cấp";
            // 
            // cboMatKhau
            // 
            this.cboMatKhau.Location = new System.Drawing.Point(137, 364);
            this.cboMatKhau.Margin = new System.Windows.Forms.Padding(4);
            this.cboMatKhau.Name = "cboMatKhau";
            this.cboMatKhau.Size = new System.Drawing.Size(308, 32);
            this.cboMatKhau.TabIndex = 17;
            // 
            // cboChucVu
            // 
            this.cboChucVu.FormattingEnabled = true;
            this.cboChucVu.Items.AddRange(new object[] {
            "Giám Đốc",
            "Phó Giám Đốc",
            "Trưởng Phòng",
            "Phó Phòng",
            "Nhân Viên"});
            this.cboChucVu.Location = new System.Drawing.Point(138, 324);
            this.cboChucVu.Margin = new System.Windows.Forms.Padding(4);
            this.cboChucVu.Name = "cboChucVu";
            this.cboChucVu.Size = new System.Drawing.Size(308, 32);
            this.cboChucVu.TabIndex = 16;
            // 
            // cboBoPhan
            // 
            this.cboBoPhan.FormattingEnabled = true;
            this.cboBoPhan.Items.AddRange(new object[] {
            "Quản Trị",
            "Ban Giám Đốc",
            "Thủ Quỹ",
            "Thủ Kho",
            "Thủ Thư"});
            this.cboBoPhan.Location = new System.Drawing.Point(138, 284);
            this.cboBoPhan.Margin = new System.Windows.Forms.Padding(4);
            this.cboBoPhan.Name = "cboBoPhan";
            this.cboBoPhan.Size = new System.Drawing.Size(308, 32);
            this.cboBoPhan.TabIndex = 15;
            // 
            // cboHoTen
            // 
            this.cboHoTen.Location = new System.Drawing.Point(138, 84);
            this.cboHoTen.Margin = new System.Windows.Forms.Padding(4);
            this.cboHoTen.Name = "cboHoTen";
            this.cboHoTen.Size = new System.Drawing.Size(308, 32);
            this.cboHoTen.TabIndex = 14;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 24);
            this.label3.TabIndex = 13;
            this.label3.Text = "Họ tên";
            // 
            // cboBangCap
            // 
            this.cboBangCap.FormattingEnabled = true;
            this.cboBangCap.Items.AddRange(new object[] {
            "Tiến Sĩ",
            "Thạc Sĩ ",
            "Đại Học",
            "Cao Đẳng",
            "Trung Cấp ",
            "Tú Tài"});
            this.cboBangCap.Location = new System.Drawing.Point(138, 244);
            this.cboBangCap.Margin = new System.Windows.Forms.Padding(4);
            this.cboBangCap.Name = "cboBangCap";
            this.cboBangCap.Size = new System.Drawing.Size(308, 32);
            this.cboBangCap.TabIndex = 12;
            // 
            // cboSDT
            // 
            this.cboSDT.Location = new System.Drawing.Point(137, 204);
            this.cboSDT.Margin = new System.Windows.Forms.Padding(4);
            this.cboSDT.Name = "cboSDT";
            this.cboSDT.Size = new System.Drawing.Size(308, 32);
            this.cboSDT.TabIndex = 11;
            // 
            // cboDiaChi
            // 
            this.cboDiaChi.Location = new System.Drawing.Point(138, 164);
            this.cboDiaChi.Margin = new System.Windows.Forms.Padding(4);
            this.cboDiaChi.Name = "cboDiaChi";
            this.cboDiaChi.Size = new System.Drawing.Size(308, 32);
            this.cboDiaChi.TabIndex = 10;
            // 
            // btnxoa
            // 
            this.btnxoa.Location = new System.Drawing.Point(176, 425);
            this.btnxoa.Margin = new System.Windows.Forms.Padding(4);
            this.btnxoa.Name = "btnxoa";
            this.btnxoa.Size = new System.Drawing.Size(160, 42);
            this.btnxoa.TabIndex = 9;
            this.btnxoa.Text = "Xóa";
            this.btnxoa.UseVisualStyleBackColor = true;
            this.btnxoa.Click += new System.EventHandler(this.btnxoa_Click_1);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 327);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(81, 24);
            this.label6.TabIndex = 8;
            this.label6.Text = "Chức vụ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 167);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 24);
            this.label5.TabIndex = 7;
            this.label5.Text = "Địa chỉ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 207);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 24);
            this.label4.TabIndex = 6;
            this.label4.Text = "SDT";
            // 
            // Thembtn
            // 
            this.Thembtn.Location = new System.Drawing.Point(8, 425);
            this.Thembtn.Margin = new System.Windows.Forms.Padding(4);
            this.Thembtn.Name = "Thembtn";
            this.Thembtn.Size = new System.Drawing.Size(160, 42);
            this.Thembtn.TabIndex = 4;
            this.Thembtn.Text = "Lưu thay đổi";
            this.Thembtn.UseVisualStyleBackColor = true;
            this.Thembtn.Click += new System.EventHandler(this.Thembtn_Click);
            // 
            // date_NS
            // 
            this.date_NS.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.date_NS.Location = new System.Drawing.Point(137, 124);
            this.date_NS.Margin = new System.Windows.Forms.Padding(4);
            this.date_NS.Name = "date_NS";
            this.date_NS.Size = new System.Drawing.Size(308, 28);
            this.date_NS.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 130);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 24);
            this.label2.TabIndex = 2;
            this.label2.Text = "Ngày sinh";
            // 
            // cboidnhanvien
            // 
            this.cboidnhanvien.Location = new System.Drawing.Point(138, 44);
            this.cboidnhanvien.Margin = new System.Windows.Forms.Padding(4);
            this.cboidnhanvien.Name = "cboidnhanvien";
            this.cboidnhanvien.Size = new System.Drawing.Size(308, 32);
            this.cboidnhanvien.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 47);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(129, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Mã nhân viên";
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(453, 0);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.Size = new System.Drawing.Size(614, 554);
            this.dataGridView1.TabIndex = 5;
            // 
            // FrmStaff
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 554);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmStaff";
            this.Text = "Hồ Sơ Nhân Viên";
            this.Load += new System.EventHandler(this.FrmStaff_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button Thembtn;
        private System.Windows.Forms.DateTimePicker date_NS;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox cboidnhanvien;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox cboDiaChi;
        private System.Windows.Forms.Button btnxoa;
        private System.Windows.Forms.TextBox cboSDT;
        private System.Windows.Forms.ComboBox cboBangCap;
        private System.Windows.Forms.TextBox cboMatKhau;
        private System.Windows.Forms.ComboBox cboChucVu;
        private System.Windows.Forms.ComboBox cboBoPhan;
        private System.Windows.Forms.TextBox cboHoTen;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
    }
}