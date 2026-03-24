using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyWordPad
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string fileName = string.Empty;
        bool isChange = false;
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // Sử dụng switch (keyData) thay vì while (true)
            switch (keyData)
            {
                case Keys.Control | Keys.A:
                    richTextBox1.SelectAll();
                    return true;

                case Keys.Control | Keys.C:
                    richTextBox1.Copy();
                    return true;

                case Keys.Control | Keys.V:
                    richTextBox1.Paste();
                    return true;

                case Keys.Control | Keys.X:
                    richTextBox1.Cut();
                    return true;

                // Phóng to
                case Keys.Control | Keys.Oemplus:
                case Keys.Control | Keys.Add:
                    if (richTextBox1.ZoomFactor < 64.0f)
                        richTextBox1.ZoomFactor += 0.2f;
                    return true;

                // Thu nhỏ
                case Keys.Control | Keys.OemMinus:
                case Keys.Control | Keys.Subtract:
                    if (richTextBox1.ZoomFactor > 0.1f)
                        richTextBox1.ZoomFactor -= 0.2f;
                    return true;

                // Reset về 100% (Ctrl + Số 0)
                case Keys.Control | Keys.D0:
                    richTextBox1.ZoomFactor = 1.0f;
                    return true;

                case Keys.Control | Keys.E:
                    richTextBox1.SelectionAlignment = HorizontalAlignment.Center;
                    return true;

                case Keys.Control | Keys.L:
                    richTextBox1.SelectionAlignment = HorizontalAlignment.Left;
                    return true;

                case Keys.Control | Keys.R:
                    richTextBox1.SelectionAlignment = HorizontalAlignment.Right;
                    return true;

                case Keys.Control | Keys.S:
                    XuLySave(this, EventArgs.Empty);
                    return true;

                case Keys.Control | Keys.O:
                    XuLyOpen(this, EventArgs.Empty);
                    return true;

                case Keys.Control | Keys.B:
                    boldToolStripMenuItem_Click(this, EventArgs.Empty);
                    return true;

                case Keys.Control | Keys.I:
                    italicToolStripMenuItem_Click(this, EventArgs.Empty);
                    return true;

                case Keys.Control | Keys.J:
                    underlineToolStripMenuItem_Click(this, EventArgs.Empty);
                    return true;

                case Keys.Control | Keys.K:
                    insertImageToolStripMenuItem_Click(this, EventArgs.Empty);
                    return true;

                case Keys.Control | Keys.M:
                    AddBulletsToolStripMenuItem_Click(this, EventArgs.Empty);
                    return true;

                case Keys.Tab:
                    if (richTextBox1.SelectionBullet)
                    {
                        richTextBox1.SelectionIndent += 20;
                        return true;
                    }
                    break;
            }

            // Nếu không khớp phím nào ở trên, trả về xử lý mặc định của hệ thống
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void XuLySave(object sender, EventArgs e)
        {
            if (fileName == string.Empty)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Text files (*.txt)|*.txt|Rich Text Format|*.rtf|All files (*.*)|*.*";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    fileName = saveFileDialog.FileName;
                    saveFile(fileName);
                }
            }
            else
            {
                saveFile(fileName);
            }
        }

        private void saveFile(string fileName)
        {
            var extension = Path.GetExtension(fileName).ToLower();
            if (extension == ".rtf")
            {
                richTextBox1.SaveFile(fileName, RichTextBoxStreamType.RichText);
            }
            else
            {
                File.WriteAllText(fileName, richTextBox1.Text);
            }
        }

        private void XuLyOpen(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Text files (*.txt)|*.txt|Rich Text Format|*.rtf|All files (*.*)|*.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                var extension = Path.GetExtension(ofd.FileName).ToLower();
                if (extension == ".rtf")
                {
                    richTextBox1.LoadFile(ofd.FileName);
                }
                else
                {
                    richTextBox1.Text = File.ReadAllText(ofd.FileName);
                }
            }
        }

        private void MenuSelectFont_Click(object sender, EventArgs e)
        {
            var fontDialog = new FontDialog();
            fontDialog.ShowColor = true;
            fontDialog.ShowApply = true;
            fontDialog.Apply += new EventHandler(XuLyApplyFont);
            if (fontDialog.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.SelectionFont = fontDialog.Font;
                richTextBox1.SelectionColor = fontDialog.Color;
            }
        }

        private void XuLyApplyFont(object sender, EventArgs e)
        {
            var fontDialog = sender as FontDialog;
            richTextBox1.SelectionFont = fontDialog.Font;
            richTextBox1.SelectionColor = fontDialog.Color;
        }

        private void fontColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.SelectionColor = colorDialog.Color;
            }
        }

        private void boldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //lấy style của font hiện tại
            FontStyle style = richTextBox1.SelectionFont.Style;
            if (richTextBox1.SelectionFont.Bold)
            {
                style &= ~FontStyle.Bold; //bỏ bold
            }
            else
            {
                style |= FontStyle.Bold; //thêm bold
            }
            richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont, style);
        }
        private void italicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontStyle style = richTextBox1.SelectionFont.Style;
            if (richTextBox1.SelectionFont.Italic)
            {
                style &= ~FontStyle.Italic; //bỏ italic
            }
            else
            {
                style |= FontStyle.Italic; //thêm italic
            }
            richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont, style);
        }
        private void underlineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontStyle style = richTextBox1.SelectionFont.Style;
            if (richTextBox1.SelectionFont.Underline)
            {
                style &= ~FontStyle.Underline; //bỏ underline
            }
            else
            {
                style |= FontStyle.Underline; //thêm underline
            }
            richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont, style);
        }
        private void insertImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.Filter = "Image files (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                var image = Image.FromFile(ofd.FileName);
                Clipboard.SetImage(image);
                richTextBox1.Paste();
                Clipboard.Clear();//tùy chọn
            }
        }
        private void addBulletsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.BackColor = colorDialog.Color;
            }
        }
        private void AddBulletsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Mở rộng lựa chọn thành các dòng đầy đủ (paragraphs) để áp dụng dấu đầu dòng cho mỗi đoạn
            int selStart = richTextBox1.SelectionStart;
            int selLength = richTextBox1.SelectionLength;

            int startLine = richTextBox1.GetLineFromCharIndex(selStart);
            int endLine = richTextBox1.GetLineFromCharIndex(selStart + Math.Max(0, selLength - 1));

            int startChar = richTextBox1.GetFirstCharIndexFromLine(startLine);
            int endChar;
            if (endLine < richTextBox1.Lines.Length - 1)
            {
                endChar = richTextBox1.GetFirstCharIndexFromLine(endLine + 1);
            }
            else
            {
                endChar = richTextBox1.TextLength;
            }

            // áp dụng lựa chọn mở rộng
            richTextBox1.SelectionStart = startChar;
            richTextBox1.SelectionLength = Math.Max(0, endChar - startChar);

            // chuyển đổi dấu đầu dòng cho các đoạn văn đã chọn
            if (!richTextBox1.SelectionBullet)
            {
                richTextBox1.SelectionBullet = true;
                richTextBox1.SelectionIndent = 30;
                richTextBox1.SelectionHangingIndent = -20;
            }
            else
            {
                richTextBox1.SelectionBullet = false;
                richTextBox1.SelectionIndent = 0;
                richTextBox1.SelectionHangingIndent = 0;
            }

            // di chuyển dấu mũ đến đầu vùng bị ảnh hưởng và xóa vùng chọn
            richTextBox1.SelectionStart = startChar;
            richTextBox1.SelectionLength = 0;
            richTextBox1.Focus();
        }

        // Đặt lại thụt lề thành 0
        private void noneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionIndent = 0;
            richTextBox1.SelectionHangingIndent = 0;
        }

        // Đặt thụt lề thành 5px
        private void pts5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionIndent = 5;
            richTextBox1.SelectionHangingIndent = 0;
        }

        // Đặt thụt lề thành 10px
        private void pts10ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionIndent = 10;
            richTextBox1.SelectionHangingIndent = 0;
        }

        // Đặt thụt lề thành 15px
        private void pts15ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionIndent = 15;
            richTextBox1.SelectionHangingIndent = 0;
        }

        // Đặt thụt lề thành 20px
        private void pts20ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionIndent = 20;
            richTextBox1.SelectionHangingIndent = 0;
        }

        // Căn chỉnh văn bản sang trái
        private void leftToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionAlignment = HorizontalAlignment.Left;
        }

        // Căn chỉnh văn bản vào giữa
        private void centerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionAlignment = HorizontalAlignment.Center;
        }

        // Căn chỉnh văn bản sang phải
        private void rightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionAlignment = HorizontalAlignment.Right;
        }
    }
}
