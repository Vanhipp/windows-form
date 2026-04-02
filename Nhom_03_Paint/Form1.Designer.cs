namespace Nhom_03_Paint
{
    partial class Form1
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.shapeSelect = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.colorBorder = new System.Windows.Forms.Button();
            this.sizeBorder = new System.Windows.Forms.NumericUpDown();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.colorFill = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sizeBorder)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(205, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(583, 426);
            this.panel1.TabIndex = 0;
            // 
            // shapeSelect
            // 
            this.shapeSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.shapeSelect.FormattingEnabled = true;
            this.shapeSelect.Location = new System.Drawing.Point(12, 68);
            this.shapeSelect.Name = "shapeSelect";
            this.shapeSelect.Size = new System.Drawing.Size(173, 24);
            this.shapeSelect.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Type";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.sizeBorder);
            this.groupBox1.Controls.Add(this.colorBorder);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(15, 108);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(170, 119);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Border";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "Color";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 16);
            this.label3.TabIndex = 1;
            this.label3.Text = "Size";
            // 
            // colorBorder
            // 
            this.colorBorder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.colorBorder.Location = new System.Drawing.Point(65, 30);
            this.colorBorder.Name = "colorBorder";
            this.colorBorder.Size = new System.Drawing.Size(85, 33);
            this.colorBorder.TabIndex = 3;
            this.colorBorder.UseVisualStyleBackColor = true;
            // 
            // sizeBorder
            // 
            this.sizeBorder.Location = new System.Drawing.Point(64, 73);
            this.sizeBorder.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.sizeBorder.Name = "sizeBorder";
            this.sizeBorder.Size = new System.Drawing.Size(86, 22);
            this.sizeBorder.TabIndex = 4;
            this.sizeBorder.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.colorFill);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(15, 250);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(170, 75);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Fill";
            // 
            // colorFill
            // 
            this.colorFill.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.colorFill.Location = new System.Drawing.Point(65, 21);
            this.colorFill.Name = "colorFill";
            this.colorFill.Size = new System.Drawing.Size(85, 33);
            this.colorFill.TabIndex = 6;
            this.colorFill.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 16);
            this.label4.TabIndex = 5;
            this.label4.Text = "Color";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.shapeSelect);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "MyPaint";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sizeBorder)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox shapeSelect;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown sizeBorder;
        private System.Windows.Forms.Button colorBorder;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button colorFill;
        private System.Windows.Forms.Label label4;
    }
}

