using System;
using System.Drawing;
using System.Windows.Forms;

namespace FormNoticeBoardAndCalendar
{
    public class FormDetailNotice : Form
    {
        private Label lblTitle;
        private Label lblMeta;
        private TextBox txtContent;
        private Panel panelHeader;
        private Panel panelContent;

        public FormDetailNotice(string title, string author, string date, string content)
        {
            InitializeComponent();

            // 설정된 데이터 적용
            lblTitle.Text = title;
            lblMeta.Text = $"작성자: {author}   작성일: {date}";
            txtContent.Text = content;
        }

        private void InitializeComponent()
        {
            this.Text = "📌 공지사항 상세보기";
            this.ClientSize = new Size(650, 550);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.WhiteSmoke;
            this.Font = new Font("Segoe UI", 10);

            // 상단 패널
            panelHeader = new Panel()
            {
                Dock = DockStyle.Top,
                Height = 120,
                BackColor = Color.FromArgb(240, 248, 255),
                Padding = new Padding(20)
            };

            lblTitle = new Label()
            {
                Font = new Font("Segoe UI Semibold", 16, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 30, 60),
                Dock = DockStyle.Top,
                Height = 40,
                TextAlign = ContentAlignment.MiddleLeft
            };

            lblMeta = new Label()
            {
                Font = new Font("Segoe UI", 10, FontStyle.Italic),
                ForeColor = Color.Gray,
                Dock = DockStyle.Bottom,
                Height = 30,
                TextAlign = ContentAlignment.MiddleLeft
            };

            panelHeader.Controls.Add(lblTitle);
            panelHeader.Controls.Add(lblMeta);

            // 내용 영역 패널
            panelContent = new Panel()
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20)
            };

            txtContent = new TextBox()
            {
                Multiline = true,
                ReadOnly = true,
                ScrollBars = ScrollBars.Vertical,
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 11),
                BackColor = Color.White,
                ForeColor = Color.Black,
                BorderStyle = BorderStyle.FixedSingle
            };

            panelContent.Controls.Add(txtContent);

            // 전체 폼에 추가
            this.Controls.Add(panelContent);
            this.Controls.Add(panelHeader);
        }
    }
}
