using System;
using System.Drawing;
using System.Windows.Forms;
using InstituteManagement;

namespace InstituteManagement
{
    public partial class MainForm : Form
    {
        private Panel panelSidebar;
        private Panel panelHeader;
        private Panel panelMain;
        private Label labelUserName, labelRole;
        private Button btnDashboard, btnNoticeBoard, btnStudent, btnTeacher, btnTimeTable, btnSales, btnExit, btnSheet;
        private PictureBox pictureUser;

        public MainForm()
        {
            if (!Program.IsAuthenticated)
            {
                MessageBox.Show("로그인이 필요합니다.", "접근 거부", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Environment.Exit(0);
            }

            InitializeComponent();
            InitializeCommonLayout(); // 공통 레이아웃 적용
            HookSidebarEvents();      // 버튼 이벤트 연결
            LoadControl(new UserControls.DashboardControl());
        }

        private void HookSidebarEvents()
        {
            btnDashboard.Click += (s, e) => LoadControl(new UserControls.DashboardControl());
            btnStudent.Click += (s, e) => LoadControl(new UserControls.StudentListControl());
            btnTimeTable.Click += (s, e) => LoadControl(new UserControls.TimetableControl());
            btnTeacher.Click += (s, e) => LoadControl(new UserControls.AdminControl());
            btnNoticeBoard.Click += (s, e) => LoadControl(new UserControls.NoticeControl());
            btnSales.Click += (s, e) => new PaymentChartForm().Show();
            btnSheet.Click += (s, e) => new SheetForm().Show();
            btnExit.Click += (s, e) => Application.Exit();
        }


        private void LoadControl(UserControl control)
        {
            panelMain.Controls.Clear();
            control.Dock = DockStyle.Fill;
            panelMain.Controls.Add(control);
        }

        private void InitializeCommonLayout()
        {
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.ClientSize = new Size(1100, 700);

            // 헤더
            panelHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 52,
                BackColor = SystemColors.MenuHighlight
            };

            Label labelTitle = new Label
            {
                Text = "학원 통합 관리 시스템",
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
                Location = new Point(this.ClientSize.Width - 40, 10),
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };

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
                Text = "관리자",
                Font = new Font("Arial Rounded MT Bold", 11.25F),
                ForeColor = SystemColors.MenuHighlight,
                Location = new Point(51, 86)
            };

            labelRole = new Label
            {
                Text = "Master",
                Font = new Font("Arial Rounded MT Bold", 8.25F),
                Location = new Point(49, 113)
            };

            panelUser.Controls.Add(pictureUser);
            panelUser.Controls.Add(labelUserName);
            panelUser.Controls.Add(labelRole);
            panelSidebar.Controls.Add(panelUser);

            // 버튼 생성
            btnDashboard = CreateSidebarButton("   대시보드");
            btnNoticeBoard = CreateSidebarButton("   공지사항");
            btnStudent = CreateSidebarButton("   학생 관리");
            btnTimeTable = CreateSidebarButton("   시간표");
            btnTeacher = CreateSidebarButton("   교사 관리");
            btnSales = CreateSidebarButton("   매출 현황");
            btnSheet = CreateSidebarButton("   장부 관리");

            panelSidebar.Controls.AddRange(new Control[]
            {
                btnSheet, btnSales, btnTeacher, btnTimeTable,
                btnStudent, btnNoticeBoard, btnDashboard
            });

            this.Controls.Add(panelSidebar);

            // 메인 패널
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
                Text = text,
                Height = 42,
                TextAlign = ContentAlignment.MiddleLeft
            };
        }
    }
}
