/*
디렉토리 구조

InstituteManager/
├── MainForm.cs          
├── UserControls/
│   ├── DashboardControl.cs
│   ├── AttendanceControl.cs
│   ├── TimetableControl.cs
│   ├── StudentListControl.cs
│   ├── AdminControl.cs
│   ├── NoticeControl.cs
│   └── CalendarControl.cs
*/

/* 
mainform
 - 사이드바
 - UserControls 부모클래스
 - 나머지 폼, 자식 클래스(기능 폼)
*/
using System;
using System.Windows.Forms;

namespace InsituteManager
{
    public partial class MainForm : Form
    {
        private Panel sidebar;
        private Panel mainPanel;

        public MainForm()
        {
            InitializeComponent();
            InitializeUI();
        }

        private void InitializeUI()
        {
            this.Text = "학원 통합 관리 시스템";
            this.Size = new System.Drawing.Size(1200, 800);
            this.StartPosition = FormStartPosition.CenterScreen;

            sidebar = new Panel
            {
                Dock = DockStyle.Left,
                Width = 200,
                BackColor = System.Drawing.Color.LightSlateGray
            };
            this.Controls.Add(sidebar);

            mainPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = System.Drawing.Color.White
            };
            this.Controls.Add(mainPanel);

            string[] menuItems = {
                "대시보드", "출석 관리", "시간표 보기", "학생 명단 관리",
                "관리자 기능", "공지사항", "캘린더 일정", "로그아웃"
            };

            for (int i = 0; i < menuItems.Length; i++)
            {
                Button btn = new Button
                {
                    Text = menuItems[i],
                    Width = 180,
                    Height = 50,
                    Top = 20 + i * 60,
                    Left = 10,
                    BackColor = System.Drawing.Color.WhiteSmoke,
                    FlatStyle = FlatStyle.Flat
                };
                btn.Click += SidebarButton_Click;
                sidebar.Controls.Add(btn);
            }

            LoadControl(new UserControls.DashboardControl());
        }

        private void SidebarButton_Click(object sender, EventArgs e)
        {
            if (sender is Button btn)
            {
                UserControl control = null;

                switch (btn.Text)
                {
                    case "대시보드":
                        control = new UserControls.DashboardControl();
                        break;
                    case "출석 관리":
                        control = new UserControls.AttendanceControl();
                        break;
                    case "시간표 보기":
                        control = new UserControls.TimetableControl();
                        break;
                    case "학생 명단 관리":
                        control = new UserControls.StudentListControl();
                        break;
                    case "관리자 기능":
                        control = new UserControls.AdminControl();
                        break;
                    case "공지사항":
                        control = new UserControls.NoticeControl();
                        break;
                    case "캘린더 일정":
                        control = new UserControls.CalendarControl();
                        break;
                    case "로그아웃":
                        Application.Exit();
                        return;
                }

                if (control != null)
                    LoadControl(control);
            }
        }

        private void LoadControl(UserControl control)
        {
            mainPanel.Controls.Clear();
            control.Dock = DockStyle.Fill;
            mainPanel.Controls.Add(control);
        }
    }
}
