namespace QuanLyThuVien
{
    partial class FrmBookEntry
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
            this.lblTenSach = new System.Windows.Forms.Label();
            this.lblTheLoai = new System.Windows.Forms.Label();
            this.lblTacGia = new System.Windows.Forms.Label();
            this.lblNamXuatBan = new System.Windows.Forms.Label();
            this.lblNhaXuatBan = new System.Windows.Forms.Label();
            this.lblGiaBan = new System.Windows.Forms.Label();
            this.lblNgayNhap = new System.Windows.Forms.Label();
            this.lblGiaThue = new System.Windows.Forms.Label();
            this.txtTenSach = new System.Windows.Forms.TextBox();
            this.txtTacGia = new System.Windows.Forms.TextBox();
            this.txtNhaXuatBan = new System.Windows.Forms.TextBox();
            this.dtpNgayNhap = new System.Windows.Forms.DateTimePicker();
            this.txtNamXuatBan = new System.Windows.Forms.TextBox();
            this.txtGiaBan = new System.Windows.Forms.TextBox();
            this.txtGiaThue = new System.Windows.Forms.TextBox();
            this.cboTheLoai = new System.Windows.Forms.ComboBox();
            this.btnLuu = new System.Windows.Forms.Button();
            this.btnDong = new System.Windows.Forms.Button();
            this.dgvBooks = new System.Windows.Forms.DataGridView();
            this.panelLeft = new System.Windows.Forms.Panel();
            this.txtSoLuong = new System.Windows.Forms.NumericUpDown();
            this.lblSoLuong = new System.Windows.Forms.Label();
            this.lblTotalBooksValue = new System.Windows.Forms.Label();
            this.lblTotalBooks = new System.Windows.Forms.Label();
            this.btnBack = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBooks)).BeginInit();
            this.panelLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSoLuong)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTenSach
            // 
            this.lblTenSach.AutoSize = true;
            this.lblTenSach.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTenSach.Location = new System.Drawing.Point(16, 11);
            this.lblTenSach.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTenSach.Name = "lblTenSach";
            this.lblTenSach.Size = new System.Drawing.Size(90, 24);
            this.lblTenSach.TabIndex = 0;
            this.lblTenSach.Text = "Tên sách";
            // 
            // lblTheLoai
            // 
            this.lblTheLoai.AutoSize = true;
            this.lblTheLoai.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTheLoai.Location = new System.Drawing.Point(16, 96);
            this.lblTheLoai.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTheLoai.Name = "lblTheLoai";
            this.lblTheLoai.Size = new System.Drawing.Size(92, 24);
            this.lblTheLoai.TabIndex = 1;
            this.lblTheLoai.Text = "Đầu sách";
            // 
            // lblTacGia
            // 
            this.lblTacGia.AutoSize = true;
            this.lblTacGia.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTacGia.Location = new System.Drawing.Point(16, 49);
            this.lblTacGia.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTacGia.Name = "lblTacGia";
            this.lblTacGia.Size = new System.Drawing.Size(75, 24);
            this.lblTacGia.TabIndex = 2;
            this.lblTacGia.Text = "Tác giả";
            // 
            // lblNamXuatBan
            // 
            this.lblNamXuatBan.AutoSize = true;
            this.lblNamXuatBan.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNamXuatBan.Location = new System.Drawing.Point(16, 177);
            this.lblNamXuatBan.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNamXuatBan.Name = "lblNamXuatBan";
            this.lblNamXuatBan.Size = new System.Drawing.Size(135, 24);
            this.lblNamXuatBan.TabIndex = 3;
            this.lblNamXuatBan.Text = "Năm xuất bản";
            // 
            // lblNhaXuatBan
            // 
            this.lblNhaXuatBan.AutoSize = true;
            this.lblNhaXuatBan.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNhaXuatBan.Location = new System.Drawing.Point(16, 138);
            this.lblNhaXuatBan.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNhaXuatBan.Name = "lblNhaXuatBan";
            this.lblNhaXuatBan.Size = new System.Drawing.Size(129, 24);
            this.lblNhaXuatBan.TabIndex = 4;
            this.lblNhaXuatBan.Text = "Nhà xuất bản";
            // 
            // lblGiaBan
            // 
            this.lblGiaBan.AutoSize = true;
            this.lblGiaBan.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGiaBan.Location = new System.Drawing.Point(16, 252);
            this.lblGiaBan.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblGiaBan.Name = "lblGiaBan";
            this.lblGiaBan.Size = new System.Drawing.Size(78, 24);
            this.lblGiaBan.TabIndex = 5;
            this.lblGiaBan.Text = "Giá bán";
            // 
            // lblNgayNhap
            // 
            this.lblNgayNhap.AutoSize = true;
            this.lblNgayNhap.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNgayNhap.Location = new System.Drawing.Point(16, 213);
            this.lblNgayNhap.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNgayNhap.Name = "lblNgayNhap";
            this.lblNgayNhap.Size = new System.Drawing.Size(105, 24);
            this.lblNgayNhap.TabIndex = 6;
            this.lblNgayNhap.Text = "Ngày nhập";
            // 
            // lblGiaThue
            // 
            this.lblGiaThue.AutoSize = true;
            this.lblGiaThue.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGiaThue.Location = new System.Drawing.Point(16, 289);
            this.lblGiaThue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblGiaThue.Name = "lblGiaThue";
            this.lblGiaThue.Size = new System.Drawing.Size(85, 24);
            this.lblGiaThue.TabIndex = 7;
            this.lblGiaThue.Text = "Giá thuê";
            // 
            // txtTenSach
            // 
            this.txtTenSach.Location = new System.Drawing.Point(208, 10);
            this.txtTenSach.Margin = new System.Windows.Forms.Padding(4);
            this.txtTenSach.Name = "txtTenSach";
            this.txtTenSach.Size = new System.Drawing.Size(265, 22);
            this.txtTenSach.TabIndex = 8;
            // 
            // txtTacGia
            // 
            this.txtTacGia.Location = new System.Drawing.Point(208, 48);
            this.txtTacGia.Margin = new System.Windows.Forms.Padding(4);
            this.txtTacGia.Name = "txtTacGia";
            this.txtTacGia.Size = new System.Drawing.Size(265, 22);
            this.txtTacGia.TabIndex = 9;
            // 
            // txtNhaXuatBan
            // 
            this.txtNhaXuatBan.Location = new System.Drawing.Point(208, 137);
            this.txtNhaXuatBan.Margin = new System.Windows.Forms.Padding(4);
            this.txtNhaXuatBan.Name = "txtNhaXuatBan";
            this.txtNhaXuatBan.Size = new System.Drawing.Size(265, 22);
            this.txtNhaXuatBan.TabIndex = 10;
            // 
            // dtpNgayNhap
            // 
            this.dtpNgayNhap.CustomFormat = "dd/MM/yyyy";
            this.dtpNgayNhap.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpNgayNhap.Location = new System.Drawing.Point(208, 213);
            this.dtpNgayNhap.Margin = new System.Windows.Forms.Padding(4);
            this.dtpNgayNhap.Name = "dtpNgayNhap";
            this.dtpNgayNhap.Size = new System.Drawing.Size(265, 22);
            this.dtpNgayNhap.TabIndex = 11;
            // 
            // txtNamXuatBan
            // 
            this.txtNamXuatBan.Location = new System.Drawing.Point(208, 176);
            this.txtNamXuatBan.Margin = new System.Windows.Forms.Padding(4);
            this.txtNamXuatBan.Name = "txtNamXuatBan";
            this.txtNamXuatBan.Size = new System.Drawing.Size(265, 22);
            this.txtNamXuatBan.TabIndex = 12;
            // 
            // txtGiaBan
            // 
            this.txtGiaBan.Location = new System.Drawing.Point(208, 252);
            this.txtGiaBan.Margin = new System.Windows.Forms.Padding(4);
            this.txtGiaBan.Name = "txtGiaBan";
            this.txtGiaBan.Size = new System.Drawing.Size(265, 22);
            this.txtGiaBan.TabIndex = 13;
            // 
            // txtGiaThue
            // 
            this.txtGiaThue.Location = new System.Drawing.Point(208, 291);
            this.txtGiaThue.Margin = new System.Windows.Forms.Padding(4);
            this.txtGiaThue.Name = "txtGiaThue";
            this.txtGiaThue.Size = new System.Drawing.Size(265, 22);
            this.txtGiaThue.TabIndex = 14;
            // 
            // cboTheLoai
            // 
            this.cboTheLoai.FormattingEnabled = true;
            this.cboTheLoai.Location = new System.Drawing.Point(208, 96);
            this.cboTheLoai.Margin = new System.Windows.Forms.Padding(4);
            this.cboTheLoai.Name = "cboTheLoai";
            this.cboTheLoai.Size = new System.Drawing.Size(265, 24);
            this.cboTheLoai.TabIndex = 15;
            // 
            // btnLuu
            // 
            this.btnLuu.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLuu.Location = new System.Drawing.Point(83, 348);
            this.btnLuu.Margin = new System.Windows.Forms.Padding(4);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(133, 37);
            this.btnLuu.TabIndex = 16;
            this.btnLuu.Text = "Lưu";
            this.btnLuu.UseVisualStyleBackColor = true;
            // 
            // btnDong
            // 
            this.btnDong.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDong.Location = new System.Drawing.Point(265, 348);
            this.btnDong.Margin = new System.Windows.Forms.Padding(4);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(133, 37);
            this.btnDong.TabIndex = 17;
            this.btnDong.Text = "Đóng";
            this.btnDong.UseVisualStyleBackColor = true;
            // 
            // dgvBooks
            // 
            this.dgvBooks.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.dgvBooks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBooks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvBooks.Location = new System.Drawing.Point(500, 0);
            this.dgvBooks.Name = "dgvBooks";
            this.dgvBooks.RowHeadersWidth = 51;
            this.dgvBooks.RowTemplate.Height = 24;
            this.dgvBooks.Size = new System.Drawing.Size(750, 670);
            this.dgvBooks.TabIndex = 18;
            // 
            // panelLeft
            // 
            this.panelLeft.Controls.Add(this.txtSoLuong);
            this.panelLeft.Controls.Add(this.lblSoLuong);
            this.panelLeft.Controls.Add(this.lblTotalBooksValue);
            this.panelLeft.Controls.Add(this.lblTotalBooks);
            this.panelLeft.Controls.Add(this.btnBack);
            this.panelLeft.Controls.Add(this.btnDong);
            this.panelLeft.Controls.Add(this.btnLuu);
            this.panelLeft.Controls.Add(this.cboTheLoai);
            this.panelLeft.Controls.Add(this.txtGiaThue);
            this.panelLeft.Controls.Add(this.txtGiaBan);
            this.panelLeft.Controls.Add(this.txtNamXuatBan);
            this.panelLeft.Controls.Add(this.dtpNgayNhap);
            this.panelLeft.Controls.Add(this.txtNhaXuatBan);
            this.panelLeft.Controls.Add(this.txtTacGia);
            this.panelLeft.Controls.Add(this.txtTenSach);
            this.panelLeft.Controls.Add(this.lblGiaThue);
            this.panelLeft.Controls.Add(this.lblNgayNhap);
            this.panelLeft.Controls.Add(this.lblGiaBan);
            this.panelLeft.Controls.Add(this.lblNhaXuatBan);
            this.panelLeft.Controls.Add(this.lblNamXuatBan);
            this.panelLeft.Controls.Add(this.lblTacGia);
            this.panelLeft.Controls.Add(this.lblTheLoai);
            this.panelLeft.Controls.Add(this.lblTenSach);
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLeft.Location = new System.Drawing.Point(0, 0);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(500, 670);
            this.panelLeft.TabIndex = 19;
            // 
            // txtSoLuong
            // 
            this.txtSoLuong.Location = new System.Drawing.Point(208, 325);
            this.txtSoLuong.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtSoLuong.Name = "txtSoLuong";
            this.txtSoLuong.Size = new System.Drawing.Size(100, 22);
            this.txtSoLuong.TabIndex = 23;
            this.txtSoLuong.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblSoLuong
            // 
            this.lblSoLuong.AutoSize = true;
            this.lblSoLuong.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSoLuong.Location = new System.Drawing.Point(16, 325);
            this.lblSoLuong.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSoLuong.Name = "lblSoLuong";
            this.lblSoLuong.Size = new System.Drawing.Size(88, 24);
            this.lblSoLuong.TabIndex = 22;
            this.lblSoLuong.Text = "Số lượng";
            // 
            // lblTotalBooksValue
            // 
            this.lblTotalBooksValue.AutoSize = true;
            this.lblTotalBooksValue.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalBooksValue.ForeColor = System.Drawing.Color.Red;
            this.lblTotalBooksValue.Location = new System.Drawing.Point(180, 410);
            this.lblTotalBooksValue.Name = "lblTotalBooksValue";
            this.lblTotalBooksValue.Size = new System.Drawing.Size(23, 24);
            this.lblTotalBooksValue.TabIndex = 21;
            this.lblTotalBooksValue.Text = "0";
            // 
            // lblTotalBooks
            // 
            this.lblTotalBooks.AutoSize = true;
            this.lblTotalBooks.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalBooks.Location = new System.Drawing.Point(16, 410);
            this.lblTotalBooks.Name = "lblTotalBooks";
            this.lblTotalBooks.Size = new System.Drawing.Size(147, 24);
            this.lblTotalBooks.TabIndex = 20;
            this.lblTotalBooks.Text = "Tổng số sách:";
            // 
            // btnBack
            // 
            this.btnBack.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.btnBack.Location = new System.Drawing.Point(16, 450);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(212, 35);
            this.btnBack.TabIndex = 24;
            this.btnBack.Text = "← Quay lại danh sách";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Visible = false;
            // 
            // FrmBookEntry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1250, 670);
            this.Controls.Add(this.dgvBooks);
            this.Controls.Add(this.panelLeft);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmBookEntry";
            this.Text = "Tiếp Nhận Sách Mới";
            ((System.ComponentModel.ISupportInitialize)(this.dgvBooks)).EndInit();
            this.panelLeft.ResumeLayout(false);
            this.panelLeft.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSoLuong)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblTenSach;
        private System.Windows.Forms.Label lblTheLoai;
        private System.Windows.Forms.Label lblTacGia;
        private System.Windows.Forms.Label lblNamXuatBan;
        private System.Windows.Forms.Label lblNhaXuatBan;
        private System.Windows.Forms.Label lblGiaBan;
        private System.Windows.Forms.Label lblNgayNhap;
        private System.Windows.Forms.Label lblGiaThue;
        private System.Windows.Forms.TextBox txtTenSach;
        private System.Windows.Forms.TextBox txtTacGia;
        private System.Windows.Forms.TextBox txtNhaXuatBan;
        private System.Windows.Forms.DateTimePicker dtpNgayNhap;
        private System.Windows.Forms.TextBox txtNamXuatBan;
        private System.Windows.Forms.TextBox txtGiaBan;
        private System.Windows.Forms.TextBox txtGiaThue;
        private System.Windows.Forms.ComboBox cboTheLoai;
        private System.Windows.Forms.Button btnLuu;
        private System.Windows.Forms.Button btnDong;
        private System.Windows.Forms.DataGridView dgvBooks;
        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.Label lblTotalBooks;
        private System.Windows.Forms.Label lblTotalBooksValue;
        private System.Windows.Forms.Label lblSoLuong;
        private System.Windows.Forms.NumericUpDown txtSoLuong;
        private System.Windows.Forms.Button btnBack;
    }
}