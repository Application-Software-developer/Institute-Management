// AttendanceForm.cs
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace AcademyManager
{
    public partial class AttendanceForm : Form
    {
        private Dictionary<string, Tuple<string, Label>> studentMap = new Dictionary<string, Tuple<string, Label>>();
        private Panel panelStudents;

        //UI
        private Panel panelSidebar;
        private Panel panelHeader;
        private Panel panelMain;
        private Label labelUserName, labelRole;
        private Button btnDashboard, btnNoticeBoard, btnStudent, btnTeacher, btnTimeTable, btnSales, btnExit;
        private PictureBox pictureUser;
        public AttendanceForm()
        {
            this.Text = "출결 확인";
            this.Width = 600;
            this.Height = 550;
            this.StartPosition = FormStartPosition.CenterScreen;

            InitializeLayout();
            LoadStudentsFromCSV("students.csv");

            InitializeCommonLayout();
        }

        private void InitializeLayout()
        {
            panelStudents = new Panel();
            panelStudents.Left = 186 + 200; // Adjusted for sidebar width and margin
            panelStudents.Top = 70 + 20;
            panelStudents.Width = 540;
            panelStudents.Height = 480;
            panelStudents.AutoScroll = true;
            panelStudents.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(panelStudents);
        }

        private void LoadStudentsFromCSV(string path)
        {
            string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path);
            if (!File.Exists(fullPath))
            {
                MessageBox.Show("학생 정보 파일이 존재하지 않습니다.");
                return;
            }

            StreamReader reader = new StreamReader(fullPath);
            string header = reader.ReadLine(); // skip header

            int y = 10;
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                string[] parts = line.Split(',');
                if (parts.Length != 2) continue;

                string name = parts[0];
                string grade = parts[1];

                Label lbl = new Label();
                lbl.Text = name + " - " + grade + " - 출석 전";
                lbl.Left = 10;
                lbl.Top = y;
                lbl.Width = 500;
                lbl.Height = 30;
                lbl.BackColor = Color.LightGray;
                lbl.BorderStyle = BorderStyle.FixedSingle;
                lbl.TextAlign = ContentAlignment.MiddleLeft;

                panelStudents.Controls.Add(lbl);
                studentMap[name] = Tuple.Create(grade, lbl);
                y += 35;
            }

            reader.Close();
        }

        public void MarkAttendance(string studentName)
        {
            if (studentMap.ContainsKey(studentName))
            {
                string grade = studentMap[studentName].Item1;
                Label label = studentMap[studentName].Item2;
                label.Text = studentName + " - " + grade + " - 출석 완료";
                label.BackColor = Color.LightGreen;
            }
            else
            {
                MessageBox.Show(studentName + " 학생을 찾을 수 없습니다.");
            }
        }

        private void InitializeCommonLayout()
        {
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.ClientSize = new Size(1100, 700);

            // 상단 바
            panelHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 52,
                BackColor = SystemColors.MenuHighlight
            };

            Label labelTitle = new Label
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
                Location = new Point(this.ClientSize.Width - 40, 10),
                Anchor = AnchorStyles.Top | AnchorStyles.Right
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

            // 사용자 정보 패널
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
                Location = new Point(49, 113)
            };

            panelUser.Controls.Add(pictureUser);
            panelUser.Controls.Add(labelUserName);
            panelUser.Controls.Add(labelRole);
            //panelSidebar.Controls.Add(panelUser);

            // 버튼 생성 및 추가
            btnDashboard = CreateSidebarButton("   대시보드");
            btnNoticeBoard = CreateSidebarButton("   공지사항");
            btnStudent = CreateSidebarButton("   학생 관리");
            btnTimeTable = CreateSidebarButton("   시간표");
            btnTeacher = CreateSidebarButton("   교사 관리");
            btnSales = CreateSidebarButton("   매출 현황");

            panelSidebar.Controls.AddRange(new Control[]
            {
        btnSales, btnTeacher, btnTimeTable, btnStudent, btnNoticeBoard, btnDashboard
            });

            this.Controls.Add(panelSidebar);
            panelSidebar.Controls.Add(panelUser);

            // 메인 콘텐츠 패널
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