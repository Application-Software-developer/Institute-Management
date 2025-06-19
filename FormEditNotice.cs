using System;
using System.Drawing;
using System.Windows.Forms;

namespace FormNoticeBoardAndCalendar
{
    public class FormEditNotice : Form
    {
        private TextBox txtTitle;
        private TextBox txtAuthor;
        private TextBox txtContent;
        private Button btnSubmit;
        private Button btnDelete;
        private DateTimePicker dtpScheduleDate;



        private readonly Action<string, string, string,DateTime> onSubmit;
        private readonly Action<string> onDelete;

        public FormEditNotice(string currentTitle, string currentAuthor, string currentContent,DateTime currentScheduledate,
                              Action<string, string, string,DateTime> onSubmitCallback,
                              Action<string> onDeleteCallback)
        {
            onSubmit = onSubmitCallback;
            onDelete = onDeleteCallback;

            InitializeComponent();

            txtTitle.Text = currentTitle;
            txtAuthor.Text = currentAuthor;
            txtContent.Text = currentContent;
            dtpScheduleDate.Value = currentScheduledate;
        }

        private void InitializeComponent()
        {
            this.Text = "공지사항 수정";
            this.ClientSize = new Size(500, 500);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.White;
            this.Font = new Font("Segoe UI", 10);

            Label lblTitle = new Label()
            {
                Text = "제목",
                Location = new Point(30, 30),
                AutoSize = true
            };
            txtTitle = new TextBox()
            {
                Location = new Point(100, 27),
                Width = 350
            };

            Label lblAuthor = new Label()
            {
                Text = "작성자",
                Location = new Point(30, 80),
                AutoSize = true
            };
            txtAuthor = new TextBox()
            {
                Location = new Point(100, 77),
                Width = 350
            };

            Label lblContent = new Label()
            {
                Text = "내용",
                Location = new Point(30, 130),
                AutoSize = true
            };
            txtContent = new TextBox()
            {
                Location = new Point(100, 127),
                Width = 350,
                Height = 200,
                Multiline = true,
                ScrollBars = ScrollBars.Vertical
            };
            Label lblScheduleDate = new Label()
            {
                Text = "일정 날짜",
                Location = new Point(30, 350),
                AutoSize = true
            };
            dtpScheduleDate = new DateTimePicker()
            {
                Location = new Point(100, 347),
                Width = 350,
                Format = DateTimePickerFormat.Short
            };

            btnSubmit = new Button()
            {
                Text = "수정 완료",
                Location = new Point(100, 410),
                Width = 150,
                Height = 40,
                BackColor = Color.SteelBlue,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnSubmit.Click += BtnSubmit_Click;

            btnDelete = new Button()
            {
                Text = "삭제",
                Location = new Point(300, 410),
                Width = 150,
                Height = 40,
                BackColor = Color.IndianRed,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnDelete.Click += BtnDelete_Click;

            this.Controls.Add(lblTitle);
            this.Controls.Add(txtTitle);
            this.Controls.Add(lblAuthor);
            this.Controls.Add(txtAuthor);
            this.Controls.Add(lblContent);
            this.Controls.Add(txtContent);
            this.Controls.Add(lblScheduleDate);
            this.Controls.Add(dtpScheduleDate);
            this.Controls.Add(btnSubmit);
            this.Controls.Add(btnDelete);
        }

        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            string newTitle = txtTitle.Text.Trim();
            string newAuthor = txtAuthor.Text.Trim();
            string newContent = txtContent.Text.Trim();

            if (string.IsNullOrWhiteSpace(newTitle) || string.IsNullOrWhiteSpace(newAuthor))
            {
                MessageBox.Show("제목과 작성자는 반드시 입력해야 합니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            onSubmit?.Invoke(newTitle, newAuthor, newContent,dtpScheduleDate.Value.Date);
            this.Close();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("정말로 이 공지사항을 삭제하시겠습니까?", "삭제 확인",
                                         MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                onDelete?.Invoke(txtTitle.Text.Trim());
                this.Close();
            }
        }
    }
}
