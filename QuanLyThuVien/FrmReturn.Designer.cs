namespace QuanLyThuVien
{
    partial class FrmReturn
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
            this.btnSearchBorrowing = new System.Windows.Forms.Button();
            this.txtReaderID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnConfirmReturn = new System.Windows.Forms.Button();
            this.dgvBorrowingList = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtTotalFine = new System.Windows.Forms.TextBox();
            this.txtLateDays = new System.Windows.Forms.TextBox();
            this.txtPricePerMonth = new System.Windows.Forms.TextBox();
            this.dtpReturnDate = new System.Windows.Forms.DateTimePicker();
            this.dtpBorrowDate = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.chkIsLost = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBorrowingList)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnSearchBorrowing);
            this.groupBox1.Controls.Add(this.txtReaderID);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(8, 13);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(332, 183);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Chi tiết thông tin";
            // 
            // btnSearchBorrowing
            // 
            this.btnSearchBorrowing.Location = new System.Drawing.Point(80, 111);
            this.btnSearchBorrowing.Margin = new System.Windows.Forms.Padding(4);
            this.btnSearchBorrowing.Name = "btnSearchBorrowing";
            this.btnSearchBorrowing.Size = new System.Drawing.Size(160, 42);
            this.btnSearchBorrowing.TabIndex = 4;
            this.btnSearchBorrowing.Text = "Tìm kiếm";
            this.btnSearchBorrowing.UseVisualStyleBackColor = true;
            // 
            // txtReaderID
            // 
            this.txtReaderID.Location = new System.Drawing.Point(13, 63);
            this.txtReaderID.Margin = new System.Windows.Forms.Padding(4);
            this.txtReaderID.Name = "txtReaderID";
            this.txtReaderID.Size = new System.Drawing.Size(305, 32);
            this.txtReaderID.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 36);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Mã đọc giả";
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(8, 555);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(133, 37);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Hủy";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnConfirmReturn
            // 
            this.btnConfirmReturn.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfirmReturn.Location = new System.Drawing.Point(192, 555);
            this.btnConfirmReturn.Margin = new System.Windows.Forms.Padding(4);
            this.btnConfirmReturn.Name = "btnConfirmReturn";
            this.btnConfirmReturn.Size = new System.Drawing.Size(133, 37);
            this.btnConfirmReturn.TabIndex = 6;
            this.btnConfirmReturn.Text = "Xác nhận";
            this.btnConfirmReturn.UseVisualStyleBackColor = true;
            // 
            // dgvBorrowingList
            // 
            this.dgvBorrowingList.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.dgvBorrowingList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBorrowingList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvBorrowingList.Location = new System.Drawing.Point(0, 0);
            this.dgvBorrowingList.Margin = new System.Windows.Forms.Padding(4);
            this.dgvBorrowingList.Name = "dgvBorrowingList";
            this.dgvBorrowingList.RowHeadersWidth = 51;
            this.dgvBorrowingList.Size = new System.Drawing.Size(1112, 625);
            this.dgvBorrowingList.TabIndex = 5;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtTotalFine);
            this.groupBox2.Controls.Add(this.txtLateDays);
            this.groupBox2.Controls.Add(this.txtPricePerMonth);
            this.groupBox2.Controls.Add(this.dtpReturnDate);
            this.groupBox2.Controls.Add(this.dtpBorrowDate);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.chkIsLost);
            this.groupBox2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(8, 203);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(332, 314);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Thanh toán";
            // 
            // txtTotalFine
            // 
            this.txtTotalFine.Location = new System.Drawing.Point(185, 241);
            this.txtTotalFine.Margin = new System.Windows.Forms.Padding(4);
            this.txtTotalFine.Name = "txtTotalFine";
            this.txtTotalFine.Size = new System.Drawing.Size(132, 32);
            this.txtTotalFine.TabIndex = 9;
            // 
            // txtLateDays
            // 
            this.txtLateDays.Location = new System.Drawing.Point(185, 201);
            this.txtLateDays.Margin = new System.Windows.Forms.Padding(4);
            this.txtLateDays.Name = "txtLateDays";
            this.txtLateDays.Size = new System.Drawing.Size(132, 32);
            this.txtLateDays.TabIndex = 8;
            // 
            // txtPricePerMonth
            // 
            this.txtPricePerMonth.Location = new System.Drawing.Point(185, 160);
            this.txtPricePerMonth.Margin = new System.Windows.Forms.Padding(4);
            this.txtPricePerMonth.Name = "txtPricePerMonth";
            this.txtPricePerMonth.Size = new System.Drawing.Size(132, 32);
            this.txtPricePerMonth.TabIndex = 7;
            // 
            // dtpReturnDate
            // 
            this.dtpReturnDate.CalendarFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpReturnDate.Location = new System.Drawing.Point(15, 120);
            this.dtpReturnDate.Margin = new System.Windows.Forms.Padding(4);
            this.dtpReturnDate.Name = "dtpReturnDate";
            this.dtpReturnDate.Size = new System.Drawing.Size(305, 32);
            this.dtpReturnDate.TabIndex = 6;
            // 
            // dtpBorrowDate
            // 
            this.dtpBorrowDate.CalendarFont = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpBorrowDate.Location = new System.Drawing.Point(13, 55);
            this.dtpBorrowDate.Margin = new System.Windows.Forms.Padding(4);
            this.dtpBorrowDate.Name = "dtpBorrowDate";
            this.dtpBorrowDate.Size = new System.Drawing.Size(305, 32);
            this.dtpBorrowDate.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 251);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 24);
            this.label6.TabIndex = 4;
            this.label6.Text = "Tổng";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 210);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(112, 24);
            this.label5.TabIndex = 3;
            this.label5.Text = "Số ngày trễ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 170);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(142, 24);
            this.label4.TabIndex = 2;
            this.label4.Text = "Giá theo tháng";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 92);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 24);
            this.label3.TabIndex = 1;
            this.label3.Text = "Ngày trả";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 28);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 24);
            this.label2.TabIndex = 0;
            this.label2.Text = "Ngày mượn";
            // 
            // chkIsLost
            // 
            this.chkIsLost.AutoSize = true;
            this.chkIsLost.Location = new System.Drawing.Point(12, 281);
            this.chkIsLost.Name = "chkIsLost";
            this.chkIsLost.Size = new System.Drawing.Size(111, 28);
            this.chkIsLost.TabIndex = 10;
            this.chkIsLost.Text = "Mất sách";
            this.chkIsLost.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.btnConfirmReturn);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(347, 625);
            this.panel1.TabIndex = 9;
            // 
            // FrmReturn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1112, 625);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dgvBorrowingList);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmReturn";
            this.Text = "Phiếu Trả Sách";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBorrowingList)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSearchBorrowing;
        private System.Windows.Forms.TextBox txtReaderID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnConfirmReturn;
        private System.Windows.Forms.DataGridView dgvBorrowingList;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTotalFine;
        private System.Windows.Forms.TextBox txtLateDays;
        private System.Windows.Forms.TextBox txtPricePerMonth;
        private System.Windows.Forms.DateTimePicker dtpReturnDate;
        private System.Windows.Forms.DateTimePicker dtpBorrowDate;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox chkIsLost;
        private System.Windows.Forms.Panel panel1;
    }
}