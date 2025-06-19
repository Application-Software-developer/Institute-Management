using System;
using System.Drawing;
using System.Windows.Forms;

namespace FormNoticeBoardAndCalendar
{
    public class FormCreateNotice : Form
    {
        private Label lblTitle;
        private Label lblAuthor;
        private Label lblContent;
        private Label lblSchedule;
        private TextBox txtTitle;
        private TextBox txtAuthor;
        private TextBox txtContent;
        private DateTimePicker datePicker;
        private Button btnSubmit;
        private Button btnClose;

        private readonly Action<string, string, string,DateTime> onSubmit;

        public FormCreateNotice(Action<string, string, string, DateTime> onSubmitCallback)
        {
            this.onSubmit = onSubmitCallback;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "공지사항 작성";
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
                Width = 350,
                Height = 30
            };

            lblAuthor = new Label()
            {
                Text = "작성자:",
                Location = new Point(30, 75),
                AutoSize = true
            };
            txtAuthor = new TextBox()
            {
                Location = new Point(100, 70),
                Width = 200,
                Height=30
            };

            lblContent = new Label()
            {
                Text = "내용:",
                Location = new Point(30, 120),
                AutoSize = true
            };
            txtContent = new TextBox()
            {
                Location = new Point(100, 115),
                Width = 350,
                Height = 200,
                Multiline = true,
                ScrollBars = ScrollBars.Vertical
            };
            lblSchedule = new Label()
            {
                Text = "일정 날짜:",
                Location = new Point(30, 330),
                AutoSize = true
            };
            datePicker = new DateTimePicker()
            {
                Location = new Point(120, 325),
                Width = 200,
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "yyyy-MM-dd"
            };

            btnSubmit = new Button()
            {
                Text = "공지사항 올리기",
                Location = new Point(100, 370),
                Width = 150,
                Height = 40,
                BackColor = Color.LightGreen,
                FlatStyle = FlatStyle.Flat
            };
            btnSubmit.Click += BtnSubmit_Click;

            btnClose = new Button()
            {
                Text = "닫기",
                Location = new Point(270, 370),
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
            this.Controls.Add(lblSchedule);
            this.Controls.Add(datePicker);
            this.Controls.Add(btnSubmit);
            this.Controls.Add(btnClose);
        }

        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            string title = txtTitle.Text.Trim();
            string author = txtAuthor.Text.Trim();
            string content = txtContent.Text.Trim();
            DateTime scheduleDate = datePicker.Value.Date;

            if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(author))
            {
                MessageBox.Show("제목과 작성자를 입력해주세요.", "입력 오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 콜백을 통해 메인폼으로 작성된 내용을 전달
            onSubmit?.Invoke(title, author, content,scheduleDate);
            this.Close();
        }
    }
}
