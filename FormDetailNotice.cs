using System;
using System.Drawing;
using System.Windows.Forms;

namespace FormNoticeBoard
{
    public class FormDetailNotice : Form
    {
        private Label lblTitle;
        private Label lblAuthor;
        private Label lblDate;
        private Label lblContent;

        public FormDetailNotice(string title, string author, string date, string content)
        {
            InitializeComponent();

            // 폼에 전달된 내용으로 라벨을 설정
            lblTitle.Text = "제목: " + title;
            lblAuthor.Text = "작성자: " + author;
            lblDate.Text = "작성일: " + date;
            lblContent.Text = "내용: \n" + content;
        }

        private void InitializeComponent()
        {
            this.Text = "공지사항 상세보기";
            this.ClientSize = new Size(600, 500);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.White;
            this.Font = new Font("Segoe UI", 10);

            // 제목 Label
            lblTitle = new Label()
            {
                Location = new Point(30, 30),
                Size = new Size(540, 30),
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                AutoSize = true
            };

            // 작성자 Label
            lblAuthor = new Label()
            {
                Location = new Point(30, 80),
                Size = new Size(540, 30),
                Font = new Font("Segoe UI", 10),
                AutoSize = true
            };

            // 작성일 Label
            lblDate = new Label()
            {
                Location = new Point(30, 130),
                Size = new Size(540, 30),
                Font = new Font("Segoe UI", 10),
                AutoSize = true
            };

            // 내용 Label
            lblContent = new Label()
            {
                Location = new Point(30, 180),
                Size = new Size(540, 200),
                Font = new Font("Segoe UI", 10),
                AutoSize = true,
                MaximumSize = new Size(540, 300),
                Height = 200,
                TextAlign = ContentAlignment.TopLeft
            };

            this.Controls.Add(lblTitle);
            this.Controls.Add(lblAuthor);
            this.Controls.Add(lblDate);
            this.Controls.Add(lblContent);
        }
    }
}
