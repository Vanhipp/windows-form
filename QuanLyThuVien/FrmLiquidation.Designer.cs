namespace QuanLyThuVien
{
    partial class FrmLiquidation
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
            this.grpThanhLy = new System.Windows.Forms.GroupBox();
            this.nudTienPhat = new System.Windows.Forms.NumericUpDown();
            this.cboLyDo = new System.Windows.Forms.ComboBox();
            this.lblLyDo = new System.Windows.Forms.Label();
            this.lblTienPhat = new System.Windows.Forms.Label();
            this.btnDong = new System.Windows.Forms.Button();
            this.btnXacNhan = new System.Windows.Forms.Button();
            this.btnTimKiem = new System.Windows.Forms.Button();
            this.txtMaSach = new System.Windows.Forms.TextBox();
            this.lblMaSach = new System.Windows.Forms.Label();
            this.dgvBooks = new System.Windows.Forms.DataGridView();
            this.btnDatimthay = new System.Windows.Forms.Button();
            this.grpThanhLy.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTienPhat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBooks)).BeginInit();
            this.SuspendLayout();
            // 
            // grpThanhLy
            // 
            this.grpThanhLy.Controls.Add(this.nudTienPhat);
            this.grpThanhLy.Controls.Add(this.cboLyDo);
            this.grpThanhLy.Controls.Add(this.lblLyDo);
            this.grpThanhLy.Controls.Add(this.lblTienPhat);
            this.grpThanhLy.Controls.Add(this.btnDong);
            this.grpThanhLy.Controls.Add(this.btnXacNhan);
            this.grpThanhLy.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpThanhLy.Location = new System.Drawing.Point(19, 145);
            this.grpThanhLy.Margin = new System.Windows.Forms.Padding(4);
            this.grpThanhLy.Name = "grpThanhLy";
            this.grpThanhLy.Padding = new System.Windows.Forms.Padding(4);
            this.grpThanhLy.Size = new System.Drawing.Size(287, 304);
            this.grpThanhLy.TabIndex = 7;
            this.grpThanhLy.TabStop = false;
            this.grpThanhLy.Text = "Thanh lý";
            // 
            // nudTienPhat
            // 
            this.nudTienPhat.Location = new System.Drawing.Point(13, 129);
            this.nudTienPhat.Margin = new System.Windows.Forms.Padding(4);
            this.nudTienPhat.Name = "nudTienPhat";
            this.nudTienPhat.Size = new System.Drawing.Size(261, 32);
            this.nudTienPhat.TabIndex = 9;
            // 
            // cboLyDo
            // 
            this.cboLyDo.FormattingEnabled = true;
            this.cboLyDo.Location = new System.Drawing.Point(13, 65);
            this.cboLyDo.Margin = new System.Windows.Forms.Padding(4);
            this.cboLyDo.Name = "cboLyDo";
            this.cboLyDo.Size = new System.Drawing.Size(260, 32);
            this.cboLyDo.TabIndex = 4;
            // 
            // lblLyDo
            // 
            this.lblLyDo.AutoSize = true;
            this.lblLyDo.Location = new System.Drawing.Point(8, 38);
            this.lblLyDo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLyDo.Name = "lblLyDo";
            this.lblLyDo.Size = new System.Drawing.Size(58, 24);
            this.lblLyDo.TabIndex = 8;
            this.lblLyDo.Text = "Lý do";
            // 
            // lblTienPhat
            // 
            this.lblTienPhat.AutoSize = true;
            this.lblTienPhat.Location = new System.Drawing.Point(8, 102);
            this.lblTienPhat.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTienPhat.Name = "lblTienPhat";
            this.lblTienPhat.Size = new System.Drawing.Size(95, 24);
            this.lblTienPhat.TabIndex = 7;
            this.lblTienPhat.Text = "Tiền phạt";
            // 
            // btnDong
            // 
            this.btnDong.Location = new System.Drawing.Point(8, 218);
            this.btnDong.Margin = new System.Windows.Forms.Padding(4);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(267, 33);
            this.btnDong.TabIndex = 6;
            this.btnDong.Text = "Đóng";
            this.btnDong.UseVisualStyleBackColor = true;
            this.btnDong.Click += new System.EventHandler(this.Dong_Click);
            // 
            // btnXacNhan
            // 
            this.btnXacNhan.Location = new System.Drawing.Point(8, 174);
            this.btnXacNhan.Margin = new System.Windows.Forms.Padding(4);
            this.btnXacNhan.Name = "btnXacNhan";
            this.btnXacNhan.Size = new System.Drawing.Size(267, 33);
            this.btnXacNhan.TabIndex = 5;
            this.btnXacNhan.Text = "Xác nhận";
            this.btnXacNhan.UseVisualStyleBackColor = true;
            this.btnXacNhan.Click += new System.EventHandler(this.XacNhan_Click);
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTimKiem.Location = new System.Drawing.Point(19, 79);
            this.btnTimKiem.Margin = new System.Windows.Forms.Padding(4);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(287, 33);
            this.btnTimKiem.TabIndex = 6;
            this.btnTimKiem.Text = "Tìm kiếm";
            this.btnTimKiem.UseVisualStyleBackColor = true;
            this.btnTimKiem.Click += new System.EventHandler(this.Timkiem_Click);
            // 
            // txtMaSach
            // 
            this.txtMaSach.Location = new System.Drawing.Point(19, 47);
            this.txtMaSach.Margin = new System.Windows.Forms.Padding(4);
            this.txtMaSach.Name = "txtMaSach";
            this.txtMaSach.Size = new System.Drawing.Size(285, 22);
            this.txtMaSach.TabIndex = 5;
            // 
            // lblMaSach
            // 
            this.lblMaSach.AutoSize = true;
            this.lblMaSach.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaSach.Location = new System.Drawing.Point(13, 15);
            this.lblMaSach.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMaSach.Name = "lblMaSach";
            this.lblMaSach.Size = new System.Drawing.Size(82, 24);
            this.lblMaSach.TabIndex = 9;
            this.lblMaSach.Text = "Mã sách";
            // 
            // dgvBooks
            // 
            this.dgvBooks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBooks.Location = new System.Drawing.Point(325, 15);
            this.dgvBooks.Margin = new System.Windows.Forms.Padding(4);
            this.dgvBooks.Name = "dgvBooks";
            this.dgvBooks.RowHeadersWidth = 51;
            this.dgvBooks.Size = new System.Drawing.Size(773, 538);
            this.dgvBooks.TabIndex = 8;
            // 
            // btnDatimthay
            // 
            this.btnDatimthay.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnDatimthay.Location = new System.Drawing.Point(27, 404);
            this.btnDatimthay.Margin = new System.Windows.Forms.Padding(4);
            this.btnDatimthay.Name = "btnDatimthay";
            this.btnDatimthay.Size = new System.Drawing.Size(267, 33);
            this.btnDatimthay.TabIndex = 6;
            this.btnDatimthay.Text = "Đã tìm thấy";
            this.btnDatimthay.UseVisualStyleBackColor = true;
            // 
            // FrmLiquidation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1112, 567);
            this.Controls.Add(this.btnTimKiem);
            this.Controls.Add(this.txtMaSach);
            this.Controls.Add(this.lblMaSach);
            this.Controls.Add(this.dgvBooks);
            this.Controls.Add(this.btnDatimthay);
            this.Controls.Add(this.grpThanhLy);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmLiquidation";
            this.Text = "Thanh Lý";
            this.grpThanhLy.ResumeLayout(false);
            this.grpThanhLy.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTienPhat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBooks)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpThanhLy;
        private System.Windows.Forms.NumericUpDown nudTienPhat;
        private System.Windows.Forms.ComboBox cboLyDo;
        private System.Windows.Forms.Label lblLyDo;
        private System.Windows.Forms.Label lblTienPhat;
        private System.Windows.Forms.Button btnDong;
        private System.Windows.Forms.Button btnXacNhan;
        private System.Windows.Forms.Button btnTimKiem;
        private System.Windows.Forms.TextBox txtMaSach;
        private System.Windows.Forms.Label lblMaSach;
        private System.Windows.Forms.DataGridView dgvBooks;
        private System.Windows.Forms.Button btnDatimthay;
    }
}