using System;
using System.Drawing;
using System.Windows.Forms;

namespace MyWordPad
{
    public partial class FindReplaceForm : Form
    {
        private RichTextBox _rtb;   // RichTextBox từ Form chính
        private int _lastIndex = 0; // lưu vị trí tìm lần trước
        private bool _isReplaceMode; // xác định đang ở chế độ Find hay Replace

        public FindReplaceForm(RichTextBox rtb, bool isReplaceMode)
        {
            InitializeComponent();

            _rtb = rtb; // gán RichTextBox
            _isReplaceMode = isReplaceMode; // nhận chế độ từ Form1

            // ===== Ẩn/hiện phần Replace =====
            txtReplace.Visible = _isReplaceMode;
            btnReplace.Visible = _isReplaceMode;
            btnReplaceAll.Visible = _isReplaceMode;
            labelReplace.Visible = _isReplaceMode;

            // đổi tiêu đề form
            this.Text = _isReplaceMode ? "Find and Replace" : "Find";
        }

        // ================= FIND NEXT =================
        private void btnFindNext_Click(object sender, EventArgs e)
        {
            string keyword = txtFind.Text;

            // nếu chưa nhập từ khóa thì thoát
            if (string.IsNullOrEmpty(keyword)) return;

            // ===== CASE SENSITIVE =====
            // nếu tick chkCase → phân biệt hoa/thường
            // nếu không → không phân biệt
            RichTextBoxFinds option = chkCase.Checked
                ? RichTextBoxFinds.MatchCase
                : RichTextBoxFinds.None;

            // ===== XÓA highlight cũ =====
            _rtb.SelectAll();
            _rtb.SelectionBackColor = _rtb.BackColor;
            _rtb.DeselectAll();

            // ===== TÌM TỪ vị trí hiện tại =====
            int index = _rtb.Find(keyword, _lastIndex, option);

            // ===== nếu không tìm thấy → quay lại từ đầu =====
            if (index < 0)
            {
                _lastIndex = 0;
                index = _rtb.Find(keyword, _lastIndex, option);
            }

            // ===== nếu tìm thấy =====
            if (index >= 0)
            {
                // chọn đoạn text
                _rtb.Select(index, keyword.Length);

                // highlight màu vàng
                _rtb.SelectionBackColor = Color.Yellow;

                _rtb.Focus(); // đưa con trỏ về RichTextBox
                _rtb.ScrollToCaret(); // cuộn tới vị trí tìm thấy

                // cập nhật vị trí để tìm tiếp
                _lastIndex = index + keyword.Length;
            }
            else
            {
                MessageBox.Show("Không tìm thấy!");
            }
        }

        // ================= FIND PREVIOUS =================
        private void btnPrev_Click(object sender, EventArgs e)
        {
            string keyword = txtFind.Text;
            if (string.IsNullOrEmpty(keyword)) return;

            // option case sensitive
            RichTextBoxFinds option = chkCase.Checked
                ? RichTextBoxFinds.MatchCase
                : RichTextBoxFinds.None;

            // ===== tìm ngược (Reverse) =====
            int index = _rtb.Find(keyword, 0, _lastIndex,
                option | RichTextBoxFinds.Reverse);

            // ===== nếu không thấy → quay về cuối =====
            if (index < 0)
            {
                index = _rtb.Find(keyword, 0, _rtb.TextLength,
                    option | RichTextBoxFinds.Reverse);
            }

            if (index >= 0)
            {
                // xóa highlight cũ
                _rtb.SelectAll();
                _rtb.SelectionBackColor = _rtb.BackColor;
                _rtb.DeselectAll();

                // highlight vị trí mới
                _rtb.Select(index, keyword.Length);
                _rtb.SelectionBackColor = Color.Yellow;

                _rtb.Focus();
                _rtb.ScrollToCaret();

                // cập nhật vị trí
                _lastIndex = index;
            }
            else
            {
                MessageBox.Show("Không tìm thấy!");
            }
        }

        // ================= REPLACE ONE =================
        private void btnReplace_Click(object sender, EventArgs e)
        {
            // nếu có phân biệt hoa/thường
            if (chkCase.Checked)
            {
                if (_rtb.SelectedText == txtFind.Text)
                {
                    _rtb.SelectedText = txtReplace.Text;
                }
            }
            else // không phân biệt
            {
                if (_rtb.SelectedText.Equals(txtFind.Text, StringComparison.OrdinalIgnoreCase))
                {
                    _rtb.SelectedText = txtReplace.Text;
                }
            }

            // sau khi replace → tìm tiếp
            btnFindNext.PerformClick();
        }

        // ================= REPLACE ALL =================
        private void btnReplaceAll_Click(object sender, EventArgs e)
        {
            string find = txtFind.Text;
            string replace = txtReplace.Text;

            if (string.IsNullOrEmpty(find)) return;

            // nếu phân biệt hoa/thường
            if (chkCase.Checked)
            {
                _rtb.Text = _rtb.Text.Replace(find, replace);
            }
            else // không phân biệt
            {
                _rtb.Text = System.Text.RegularExpressions.Regex.Replace(
                    _rtb.Text,
                    find,
                    replace,
                    System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            }
        }

        // ===== reset vị trí khi đổi từ khóa =====
        private void txtFind_TextChanged(object sender, EventArgs e)
        {
            _lastIndex = 0;
        }

        // ===== thoát form + xóa highlight =====
        private void btnThoat_Click(object sender, EventArgs e)
        {
            _rtb.SelectAll();
            _rtb.SelectionBackColor = _rtb.BackColor;
            _rtb.DeselectAll();

            Close();
        }
    }
}