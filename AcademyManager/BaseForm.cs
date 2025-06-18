using System;
using System.Drawing;
using System.Windows.Forms;

namespace CommonUI
{
    public　partial class BaseForm : Form
    {
        protected Panel panelMain;
        protected Panel panelSidebar;
        protected Panel panelHeader;
        protected Label labelUserName;
        protected Label labelRole;
        protected PictureBox pictureUser;
        protected Button btnDashboard;
        protected Button btnNoticeBoard;
        protected Button btnTimeTable;
        protected Button btnStudent;
        protected Button btnTeacher;
        protected Button btnSales;
        protected Button btnExit;
        protected Label labelTitle;

        public BaseForm()
        {
            InitializeBaseLayout();
        }

        private void InitializeBaseLayout()
        {
            this.Text = "BaseForm";
            this.Size = new Size(950, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.White;

            // 상단 바
            panelHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 52,
                BackColor = SystemColors.MenuHighlight
            };

            labelTitle = new Label
            {
                Text = "공통 헤더",
                Font = new Font("Noto Sans KR", 14.25F, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(62, 12),
                AutoSize = true
            };

            btnExit = new Button
            {
                FlatStyle = FlatStyle.Flat,
                FlatAppearance = { BorderSize = 0 },
                Text = "X",
                ForeColor = Color.White,
                Font = new Font("Arial", 10F, FontStyle.Bold),
                Size = new Size(30, 30),
                Location = new Point(910, 10)
            };
            btnExit.Click += (s, e) => this.Close();

            panelHeader.Controls.Add(labelTitle);
            panelHeader.Controls.Add(btnExit);
            this.Controls.Add(panelHeader);

            // 사이드바
            panelSidebar = new Panel
            {
                Dock = DockStyle.Left,
                Width = 186,
                BackColor = SystemColors.ControlLight
            };

            // 사용자 정보 영역
            Panel panelUser = new Panel
            {
                Dock = DockStyle.Top,
                Height = 144
            };
            pictureUser = new PictureBox
            {
                BackColor = Color.LightGray,
                SizeMode = PictureBoxSizeMode.Zoom,
                Size = new Size(63, 60),
                Location = new Point(63, 19)
            };
            labelUserName = new Label
            {
                Text = "User Name",
                Font = new Font("Arial Rounded MT Bold", 11.25F),
                ForeColor = SystemColors.MenuHighlight,
                Location = new Point(51, 86)
            };
            labelRole = new Label
            {
                Text = "(직급)",
                Font = new Font("Arial Rounded MT Bold", 8.25F),
                Location = new Point(49, 103)
            };
            panelUser.Controls.Add(pictureUser);
            panelUser.Controls.Add(labelUserName);
            panelUser.Controls.Add(labelRole);
            panelSidebar.Controls.Add(panelUser);

            // 버튼들 추가 (순서대로 아래에 쌓임)
            btnDashboard = CreateSidebarButton("대시보드");
            btnNoticeBoard = CreateSidebarButton("공지사항");
            btnStudent = CreateSidebarButton("학생 관리");
            btnTimeTable = CreateSidebarButton("시간표");
            btnTeacher = CreateSidebarButton("교사 관리");
            btnSales = CreateSidebarButton("매출 현황");

            panelSidebar.Controls.AddRange(new Control[]
            {
                btnSales, btnTeacher, btnTimeTable, btnStudent, btnNoticeBoard, btnDashboard
            });

            this.Controls.Add(panelSidebar);

            // 콘텐츠 영역
            panelMain = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White
            };
            this.Controls.Add(panelMain);
        }

        private Button CreateSidebarButton(string text)
        {
            return new Button
            {
                Dock = DockStyle.Top,
                FlatStyle = FlatStyle.Flat,
                FlatAppearance = { BorderSize = 0 },
                Font = new Font("Noto Sans KR", 11.25F, FontStyle.Bold),
                ForeColor = SystemColors.Highlight,
                Text = "   " + text,
                Height = 42,
                TextAlign = ContentAlignment.MiddleLeft
            };
        }
    }
}
