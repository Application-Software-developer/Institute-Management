using System;
using System.Drawing;
using System.Windows.Forms;

namespace FormNoticeBoard
{
    public class FormEditNotice : Form
    {
        private Label lblTitle;
        private Label lblAuthor;
        private Label lblContent;
        private TextBox txtTitle;
        private TextBox txtAuthor;
        private TextBox txtContent;
        private Button btnSubmit;
        private Button btnClose;

        private readonly Action<string, string, string> onSubmit;

        public FormEditNotice(string currentTitle, string currentAuthor, string currentContent, Action<string, string, string> onSubmitCallback)
        {
            this.onSubmit = onSubmitCallback;
            InitializeComponent();

            // 기존 값으로 텍스트 박스 채우기
            txtTitle.Text = currentTitle;
            txtAuthor.Text = currentAuthor;
            txtContent.Text = currentContent;
        }

        private void InitializeComponent()
        {
            this.Text = "공지사항 수정";
            this.ClientSize = new Size(500, 500);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.White;
            this.Font = new Font("Segoe UI", 10);

            lblTitle = new Label()
            {
                Text = "제목:",
                Location = new Point(30, 30),
                AutoSize = true
            };
            txtTitle = new TextBox()
            {
                Location = new Point(100, 25),
                Width = 350
            };

            lblAuthor = new Label()
            {
                Text = "작성자:",
                Location = new Point(30, 80),
                AutoSize = true
            };
            txtAuthor = new TextBox()
            {
                Location = new Point(100, 75),
                Width = 200
            };

            lblContent = new Label()
            {
                Text = "내용:",
                Location = new Point(30, 130),
                AutoSize = true
            };
            txtContent = new TextBox()
            {
                Location = new Point(100, 125),
                Width = 350,
                Height = 200,
                Multiline = true,
                ScrollBars = ScrollBars.Vertical
            };

            btnSubmit = new Button()
            {
                Text = "수정 완료",
                Location = new Point(100, 350),
                Width = 150,
                Height = 40,
                BackColor = Color.LightGreen,
                FlatStyle = FlatStyle.Flat
            };
            btnSubmit.Click += BtnSubmit_Click;

            btnClose = new Button()
            {
                Text = "닫기",
                Location = new Point(270, 350),
                Width = 100,
                Height = 40,
                BackColor = Color.LightGray,
                FlatStyle = FlatStyle.Flat
            };
            btnClose.Click += (s, e) => this.Close();

            this.Controls.Add(lblTitle);
            this.Controls.Add(txtTitle);
            this.Controls.Add(lblAuthor);
            this.Controls.Add(txtAuthor);
            this.Controls.Add(lblContent);
            this.Controls.Add(txtContent);
            this.Controls.Add(btnSubmit);
            this.Controls.Add(btnClose);
        }

        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            string title = txtTitle.Text.Trim();
            string author = txtAuthor.Text.Trim();
            string content = txtContent.Text.Trim();

            if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(author))
            {
                MessageBox.Show("제목과 작성자를 입력해주세요.", "입력 오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            onSubmit?.Invoke(title, author, content);
            this.Close();
        }
    }
}
