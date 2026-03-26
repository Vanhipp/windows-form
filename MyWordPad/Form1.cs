using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace MyWordPad
{
    public partial class Form1 : Form
    {
        [StructLayout(LayoutKind.Sequential)]
        private struct RECT { public int Left; public int Top; public int Right; public int Bottom; }
        [StructLayout(LayoutKind.Sequential)]
        private struct CHARRANGE { public int cpMin; public int cpMax; }
        [StructLayout(LayoutKind.Sequential)]
        private struct FORMATRANGE { public IntPtr hdc; public IntPtr hdcTarget; public RECT rc; public RECT rcPage; public CHARRANGE chrg; }
        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);
        private const int WM_USER = 0x0400;
        private const int EM_FORMATRANGE = WM_USER + 57;
        private int m_nFirstCharOnPage = 0; // Biến này để đếm ký tự trang in


        public Form1()
        {
            InitializeComponent();
        }

        string fileName = string.Empty;
        bool isChange = false;
        // tracks the current print position (character index) when printing RTF with formatting
        private int _printCharIndex = 0;
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

        private void normalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontStyle style = richTextBox1.SelectionFont.Style;
            style |= FontStyle.Regular; //thêm regular
            richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont, style);
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
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
            if (richTextBox1.SelectionBullet)
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

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("WordPad - Text Editor\n\n" +
        "A simple yet powerful rich text editor built with Windows Forms.\n\n" +
        "Features:\n" +
        "• Rich text formatting (Bold, Italic, Underline, Strikeout)\n" +
        "• Font and color customization\n" +
        "• Paragraph alignment and indentation\n" +
        "• Bullets and numbering\n" +
        "• Find and Replace\n" +
        "• File operations (New, Open, Save, Print)\n\n",
        "About WordPad",
        MessageBoxButtons.OK,
        MessageBoxIcon.Information);
        }

        private void groupInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("WordPad - Group 3 - 23BITV02 - NIIE");
        }

        private void printStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintDocument printDocument = new PrintDocument();
            PrintDialog printDialog = new PrintDialog();

            // Reset lại trang in về đầu tiên trước khi bắt đầu
            m_nFirstCharOnPage = 0;

            // Gán sự kiện in
            printDocument.PrintPage += new PrintPageEventHandler(pd_PrintPage);

            printDialog.Document = printDocument;

            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDocument.Print();
            }
        }
        Stack<string> undoStack = new Stack<string>();
        Stack<string> redoStack = new Stack<string>();
        bool isOperating = false;
        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (undoStack.Count > 1)
            {
                isOperating = true;
                redoStack.Push(undoStack.Pop());
                richTextBox1.Rtf = undoStack.Peek();
                isOperating = false;
            }
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (redoStack.Count > 0)
            {
                isOperating = true;
                string content = redoStack.Pop();
                undoStack.Push(content);
                richTextBox1.Rtf = content;
                isOperating = false;
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (!isOperating)
            {
                undoStack.Push(richTextBox1.Rtf);
                redoStack.Clear();
            }
        }

        private void pd_PrintPage(object sender, PrintPageEventArgs e)
        {
            // 1. Tính toán vùng in (Twips: 1 inch = 1440 units)
            RECT rectLayoutArea;
            rectLayoutArea.Top = (int)(e.MarginBounds.Top * 14.4);
            rectLayoutArea.Bottom = (int)(e.MarginBounds.Bottom * 14.4);
            rectLayoutArea.Left = (int)(e.MarginBounds.Left * 14.4);
            rectLayoutArea.Right = (int)(e.MarginBounds.Right * 14.4);

            RECT rectPage;
            rectPage.Top = (int)(e.PageBounds.Top * 14.4);
            rectPage.Bottom = (int)(e.PageBounds.Bottom * 14.4);
            rectPage.Left = (int)(e.PageBounds.Left * 14.4);
            rectPage.Right = (int)(e.PageBounds.Right * 14.4);

            IntPtr hdc = e.Graphics.GetHdc();

            FORMATRANGE fmtRange;
            fmtRange.chrg.cpMax = -1; // In hết văn bản
            fmtRange.chrg.cpMin = m_nFirstCharOnPage; // Bắt đầu từ trang trước để lại
            fmtRange.hdc = hdc;
            fmtRange.hdcTarget = hdc;
            fmtRange.rc = rectLayoutArea;
            fmtRange.rcPage = rectPage;

            // 2. Ra lệnh cho RichTextBox tự vẽ chính nó lên trang in
            IntPtr structPtr = Marshal.AllocCoTaskMem(Marshal.SizeOf(fmtRange));
            Marshal.StructureToPtr(fmtRange, structPtr, false);
            IntPtr nextCharIndex = SendMessage(richTextBox1.Handle, EM_FORMATRANGE, (IntPtr)1, structPtr);
            Marshal.FreeCoTaskMem(structPtr);

            e.Graphics.ReleaseHdc(hdc);

            // 3. Kiểm tra xem có cần in thêm trang tiếp theo không
            m_nFirstCharOnPage = (int)nextCharIndex;

            if (m_nFirstCharOnPage < richTextBox1.TextLength && m_nFirstCharOnPage != -1)
                e.HasMorePages = true;
            else
            {
                e.HasMorePages = false;
                m_nFirstCharOnPage = 0; // Xong việc thì reset
            }
        }

        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FindReplaceForm f = new FindReplaceForm(richTextBox1, false);
            f.Show();
        }

        private void findAndReplaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FindReplaceForm f = new FindReplaceForm(richTextBox1, true);
            f.Show();
        }
    }
}
