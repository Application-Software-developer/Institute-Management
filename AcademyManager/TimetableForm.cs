using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using CommonUI;

namespace AcademyManager
{
    public partial class TimetableForm : Form
    {
        private TextBox txtTeacherName;
        private Button btnStart;
        private Button btnSave;
        private Button btnShowAll;

        private Panel panelGrid;
        private Label[,] gridLabels;
        private const int startHour = 10;
        private const int endHour = 22;
        private const int days = 7;
        private const int cellSize = 40;

        private string[] dayNames = { "월", "화", "수", "목", "금", "토", "일" };
        private bool isMouseDown = false;
        private bool isSelecting = true;

        private Dictionary<string, List<Tuple<int, int>>> teacherSchedules;
        private string currentTeacher;
        private ToolTip tooltip;

        //UI
        private Panel panelSidebar;
        private Panel panelHeader;
        private Panel panelMain;
        private Label labelUserName, labelRole;
        private Button btnDashboard, btnNoticeBoard, btnStudent, btnTeacher, btnTimeTable, btnSales, btnExit;
        private PictureBox pictureUser;

        public TimetableForm()
        {
            this.Text = "시간표 작성 - When2meet 스타일";
            this.Width = 1100;
            this.Height = 700;
            this.StartPosition = FormStartPosition.CenterScreen;

            teacherSchedules = new Dictionary<string, List<Tuple<int, int>>>();
            currentTeacher = "";
            tooltip = new ToolTip();

            InitializeGrid();
            InitializeControls();
            InitializeCommonLayout();
        }

        private void InitializeControls()
        {
            int controlLeft = panelGrid.Left - 360;
            int controlTopOffset = 150;

            Label lbl = new Label
            {
                Text = "선생님 이름:",
                Left = controlLeft,
                Top = 40 + controlTopOffset,
                Width = 100
            };
            this.Controls.Add(lbl);

            txtTeacherName = new TextBox
            {
                Left = controlLeft + 100,
                Top = 38 + controlTopOffset,
                Width = 160
            };
            this.Controls.Add(txtTeacherName);

            btnStart = new Button
            {
                Text = "시간 선택 시작",
                Left = controlLeft,
                Top = 80 + controlTopOffset,
                Width = 260,
                Height = 30
            };
            btnStart.Click += BtnStart_Click;
            this.Controls.Add(btnStart);

            btnSave = new Button
            {
                Text = "저장",
                Left = controlLeft,
                Top = 120 + controlTopOffset,
                Width = 260,
                Height = 30
            };
            btnSave.Click += BtnSave_Click;
            this.Controls.Add(btnSave);

            btnShowAll = new Button
            {
                Text = "모든 선생님 시간 보기",
                Left = controlLeft,
                Top = 160 + controlTopOffset,
                Width = 260,
                Height = 30
            };
            btnShowAll.Click += BtnShowAll_Click;
            this.Controls.Add(btnShowAll);
        }

        private void InitializeGrid()
        {
            int gridWidth = days * cellSize + 50;
            int gridHeight = (endHour - startHour) * cellSize + 30;

            panelGrid = new Panel
            {
                Width = gridWidth,
                Height = gridHeight,
                Top = 70,
                Left = this.ClientSize.Width - gridWidth - 20,
                BorderStyle = BorderStyle.FixedSingle
            };
            this.Controls.Add(panelGrid);

            gridLabels = new Label[endHour - startHour, days];

            for (int i = 0; i < days; i++)
            {
                Label lbl = new Label
                {
                    Text = dayNames[i],
                    Size = new Size(cellSize, 30),
                    Location = new Point(50 + i * cellSize, 0),
                    TextAlign = ContentAlignment.MiddleCenter,
                    BackColor = Color.LightGray
                };
                panelGrid.Controls.Add(lbl);
            }

            for (int i = 0; i < endHour - startHour; i++)
            {
                Label lbl = new Label
                {
                    Text = $"{(startHour + i):00}:00",
                    Size = new Size(50, cellSize),
                    Location = new Point(0, 30 + i * cellSize),
                    TextAlign = ContentAlignment.MiddleCenter,
                    BackColor = Color.LightGray
                };
                panelGrid.Controls.Add(lbl);
            }

            for (int row = 0; row < endHour - startHour; row++)
            {
                for (int col = 0; col < days; col++)
                {
                    Label cell = new Label
                    {
                        Size = new Size(cellSize, cellSize),
                        Location = new Point(50 + col * cellSize, 30 + row * cellSize),
                        BorderStyle = BorderStyle.FixedSingle,
                        BackColor = Color.White,
                        Tag = Tuple.Create(row, col)
                    };

                    tooltip.SetToolTip(cell, "");
                    cell.MouseDown += Cell_MouseDown;
                    cell.MouseEnter += Cell_MouseEnter;

                    panelGrid.Controls.Add(cell);
                    gridLabels[row, col] = cell;
                }
            }

            this.MouseUp += (s, e) => isMouseDown = false;
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            string name = txtTeacherName.Text.Trim();
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("선생님 이름을 입력하세요.");
                return;
            }

            currentTeacher = name;
            if (!teacherSchedules.ContainsKey(name))
                teacherSchedules[name] = new List<Tuple<int, int>>();

            LoadTeacherSchedule(name);
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentTeacher))
            {
                MessageBox.Show("선생님 이름을 먼저 입력하세요.");
                return;
            }

            MessageBox.Show($"{currentTeacher} 선생님의 시간표가 저장되었습니다.");
        }

        private void BtnShowAll_Click(object sender, EventArgs e)
        {
            int[,] overlapCount = new int[endHour - startHour, days];

            foreach (var schedule in teacherSchedules.Values)
            {
                foreach (var pair in schedule)
                {
                    int r = pair.Item1;
                    int c = pair.Item2;
                    overlapCount[r, c]++;
                }
            }

            for (int row = 0; row < endHour - startHour; row++)
            {
                for (int col = 0; col < days; col++)
                {
                    int count = overlapCount[row, col];
                    if (count == 0)
                    {
                        gridLabels[row, col].BackColor = Color.White;
                        tooltip.SetToolTip(gridLabels[row, col], "");
                    }
                    else if (count == 1)
                    {
                        gridLabels[row, col].BackColor = Color.LightGreen;
                        tooltip.SetToolTip(gridLabels[row, col], "1명 가능");
                    }
                    else
                    {
                        gridLabels[row, col].BackColor = Color.ForestGreen;
                        tooltip.SetToolTip(gridLabels[row, col], $"{count}명 가능");
                    }
                }
            }
        }

        private void Cell_MouseDown(object sender, MouseEventArgs e)
        {
            if (string.IsNullOrEmpty(currentTeacher)) return;

            isMouseDown = true;
            Label cell = (Label)sender;
            var tag = (Tuple<int, int>)cell.Tag;
            int row = tag.Item1;
            int col = tag.Item2;
            isSelecting = cell.BackColor == Color.White;

            ToggleCell(cell, isSelecting, row, col);
        }

        private void Cell_MouseEnter(object sender, EventArgs e)
        {
            if (!isMouseDown || string.IsNullOrEmpty(currentTeacher)) return;

            Label cell = (Label)sender;
            var tag = (Tuple<int, int>)cell.Tag;
            int row = tag.Item1;
            int col = tag.Item2;

            ToggleCell(cell, isSelecting, row, col);
        }

        private void ToggleCell(Label cell, bool select, int row, int col)
        {
            Tuple<int, int> key = Tuple.Create(row, col);
            if (select)
            {
                cell.BackColor = Color.LightGreen;
                if (!teacherSchedules[currentTeacher].Contains(key))
                    teacherSchedules[currentTeacher].Add(key);
            }
            else
            {
                cell.BackColor = Color.White;
                teacherSchedules[currentTeacher].RemoveAll(p => p.Item1 == row && p.Item2 == col);
            }
        }

        private void LoadTeacherSchedule(string name)
        {
            for (int row = 0; row < endHour - startHour; row++)
            {
                for (int col = 0; col < days; col++)
                {
                    gridLabels[row, col].BackColor = Color.White;
                }
            }

            foreach (var pair in teacherSchedules[name])
            {
                int row = pair.Item1;
                int col = pair.Item2;
                gridLabels[row, col].BackColor = Color.LightGreen;
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
                Text = "Master",//직급
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