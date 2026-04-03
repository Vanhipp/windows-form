using System;
using System.Drawing;
using System.Windows.Forms;

namespace Nhom_03_Paint
{
    public partial class TextInputDialog : Form
    {
        public string InputText { get; private set; }
        public Font SelectedFont { get; private set; }
        public Color SelectedColor { get; private set; }

        private TextBox textBox;
        private Button fontButton;
        private Button colorButton;
        private Button okButton;
        private Button cancelButton;
        private Label textLabel;
        private Label fontLabel;
        private Label colorLabel;

        public TextInputDialog()
        {
            InitializeComponent();
            SelectedFont = new Font("Arial", 12);
            SelectedColor = Color.Black;
        }

        private void InitializeComponent()
        {
            this.textBox = new TextBox();
            this.fontButton = new Button();
            this.colorButton = new Button();
            this.okButton = new Button();
            this.cancelButton = new Button();
            this.textLabel = new Label();
            this.fontLabel = new Label();
            this.colorLabel = new Label();

            // textLabel
            this.textLabel.AutoSize = true;
            this.textLabel.Location = new Point(12, 15);
            this.textLabel.Text = "Text:";

            // textBox
            this.textBox.Location = new Point(12, 35);
            this.textBox.Size = new Size(260, 20);
            this.textBox.Text = "";

            // fontLabel
            this.fontLabel.AutoSize = true;
            this.fontLabel.Location = new Point(12, 70);
            this.fontLabel.Text = "Font:";

            // fontButton
            this.fontButton.Location = new Point(12, 90);
            this.fontButton.Size = new Size(75, 23);
            this.fontButton.Text = "Select Font";
            this.fontButton.Click += new EventHandler(this.fontButton_Click);

            // colorLabel
            this.colorLabel.AutoSize = true;
            this.colorLabel.Location = new Point(100, 70);
            this.colorLabel.Text = "Color:";

            // colorButton
            this.colorButton.Location = new Point(100, 90);
            this.colorButton.Size = new Size(75, 23);
            this.colorButton.Text = "Select Color";
            this.colorButton.Click += new EventHandler(this.colorButton_Click);

            // okButton
            this.okButton.Location = new Point(120, 130);
            this.okButton.Size = new Size(75, 23);
            this.okButton.Text = "OK";
            this.okButton.DialogResult = DialogResult.OK;

            // cancelButton
            this.cancelButton.Location = new Point(200, 130);
            this.cancelButton.Size = new Size(75, 23);
            this.cancelButton.Text = "Cancel";
            this.cancelButton.DialogResult = DialogResult.Cancel;

            // Form
            this.ClientSize = new Size(284, 165);
            this.Controls.Add(this.textBox);
            this.Controls.Add(this.fontButton);
            this.Controls.Add(this.colorButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.textLabel);
            this.Controls.Add(this.fontLabel);
            this.Controls.Add(this.colorLabel);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Text Input";

            this.AcceptButton = this.okButton;
            this.CancelButton = this.cancelButton;
        }

        private void fontButton_Click(object sender, EventArgs e)
        {
            FontDialog fontDialog = new FontDialog();
            fontDialog.Font = SelectedFont;
            if (fontDialog.ShowDialog() == DialogResult.OK)
            {
                SelectedFont = fontDialog.Font;
            }
        }

        private void colorButton_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            colorDialog.Color = SelectedColor;
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                SelectedColor = colorDialog.Color;
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.OK)
            {
                InputText = textBox.Text;
            }
            base.OnFormClosing(e);
        }
    }
}