using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace FormNoticeBoardAndCalendar
{
    public class FormNoticeBoard : Form
    {
        private Panel panelSidebar, panelTopbar, panelMain;
        private Label lblTitle;
        private ListView lvNotices;
        private Button btnCreate, btnEdit, btnDetail, btnOpenCalendar, btnExit;
        private PictureBox pictureUser;
        private Label labelUserName, labelRole;

        public FormNoticeBoard()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "공지사항 게시판";
            this.ClientSize = new Size(1100, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Font = new Font("Noto Sans KR", 10, FontStyle.Regular);
            this.BackColor = Color.WhiteSmoke;

            // ===== 좌측 사이드바 =====
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

            void StyleSidebarButton(Button btn, string iconFileName)
            {
                btn.Dock = DockStyle.Top;
                btn.Height = 50;
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
                btn.ForeColor = SystemColors.Highlight;
                btn.Font = new Font("Noto Sans KR", 11.25F, FontStyle.Bold);
                btn.TextAlign = ContentAlignment.MiddleLeft;
                btn.Padding = new Padding(20, 0, 0, 0);
                btn.Cursor = Cursors.Hand;
                btn.BackColor = SystemColors.ControlLight;

                string iconPath = Path.Combine(Application.StartupPath, iconFileName);
                if (File.Exists(iconPath))
                {
                    btn.Image = Image.FromFile(iconPath);
                    btn.ImageAlign = ContentAlignment.MiddleLeft;
                    btn.TextImageRelation = TextImageRelation.ImageBeforeText;
                }

            }

            // 사이드바 버튼 생성
            Button btnDashboard = new Button { Text = " 대시보드" };
            Button btnNoticeBoard = new Button { Text = " 공지사항" };
            Button btnStudent = new Button { Text = " 학생관리" };
            Button btnSchedule = new Button { Text = " 일정" };
            Button btnTimeTable = new Button { Text = " 시간표" };
            Button btnTeacher = new Button { Text = " 교사 관리" };
            Button btnSales = new Button { Text = " 매출 현황" };

          

            StyleSidebarButton(btnDashboard, "homeIcon.png");
            StyleSidebarButton(btnNoticeBoard, "notificationIcon.png");
            StyleSidebarButton(btnStudent, "studentIcon.png");
            StyleSidebarButton(btnSchedule, "scheduleIcon.png");
            StyleSidebarButton(btnTimeTable, "timetableIcon.png");
            StyleSidebarButton(btnTeacher, "teacherIcon.png");
            StyleSidebarButton(btnSales, "financeIcon.png");

            panelSidebar.Controls.AddRange(new Control[] {
                btnSales, btnTeacher, btnTimeTable,
                btnStudent, btnNoticeBoard, btnDashboard, panelUser
            });

            btnDashboard.Click += (s, e) => MessageBox.Show("대시보드로 이동");
            btnNoticeBoard.Click += (s, e) => MessageBox.Show("현재 페이지입니다.");
            btnStudent.Click += (s, e) => MessageBox.Show("학생관리 페이지로 이동");
            btnTimeTable.Click += (s, e) => MessageBox.Show("시간표 페이지로 이동");

            // ===== 상단 바 =====
            panelTopbar = new Panel
            {
                Dock = DockStyle.Top,
                Height = 50,
                BackColor = SystemColors.MenuHighlight
            };

            lblTitle = new Label
            {
                Text = "공지사항 게시판",
                ForeColor = Color.White,
                Font = new Font("Noto Sans KR", 14, FontStyle.Bold),
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleLeft,
                Dock = DockStyle.Left,
                Width = 300,
                Padding = new Padding(20, 0, 0, 0)
            };
            panelTopbar.Controls.Add(lblTitle);

            btnExit = new Button
            {
                Text = "X",
                ForeColor = Color.White,
                Font = new Font("Noto Sans KR", 12, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                FlatAppearance = { BorderSize = 0 },
                BackColor = Color.FromArgb(220, 53, 69),
                Size = new Size(50, 30),
                Location = new Point(this.ClientSize.Width - 60, 10),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Cursor = Cursors.Hand
            };
            


            // ===== 메인 컨텐츠 =====
            panelMain = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20)
            };

            lvNotices = new ListView
            {
                View = View.Details,
                FullRowSelect = true,
                GridLines = true,
                HeaderStyle = ColumnHeaderStyle.Nonclickable,
                Dock = DockStyle.Top,
                Height = 500,
                Font = new Font("Noto Sans KR", 11, FontStyle.Bold),
                BackColor = Color.White,
                ForeColor = Color.Black
            };

            lvNotices.Columns.Add("제목", 480, HorizontalAlignment.Center);
            lvNotices.Columns.Add("작성자", 150, HorizontalAlignment.Center);
            lvNotices.Columns.Add("작성일", 150, HorizontalAlignment.Center);
            panelMain.Controls.Add(lvNotices);

            FlowLayoutPanel buttonPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 45,
                Padding = new Padding(0, 10, 0, 0),
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false
            };

            void StyleMainButton(Button btn)
            {
                btn.Size = new Size(160, 35);
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderColor = Color.Gray;
                btn.FlatAppearance.BorderSize = 1;
                btn.BackColor = Color.White;
                btn.ForeColor = Color.Black;
                btn.Font = new Font("Noto Sans KR", 11, FontStyle.Bold);
                btn.Cursor = Cursors.Hand;
                btn.Margin = new Padding(10, 0, 0, 0);
            }

            btnCreate = new Button() { Text = "공지사항 올리기" };
            btnEdit = new Button() { Text = "공지사항 수정" };
            btnDetail = new Button() { Text = "공지사항 상세 보기" };
            btnOpenCalendar = new Button() { Text = "캘린더 보기" };

            StyleMainButton(btnCreate);
            StyleMainButton(btnEdit);
            StyleMainButton(btnDetail);
            StyleMainButton(btnOpenCalendar);

            btnCreate.Click += BtnCreate_Click;
            btnEdit.Click += BtnEdit_Click;
            btnDetail.Click += BtnDetail_Click;
            btnOpenCalendar.Click += BtnOpenCalendar_Click;

            buttonPanel.Controls.AddRange(new Control[] {
                btnCreate, btnEdit, btnDetail, btnOpenCalendar
            });

            panelMain.Controls.Add(buttonPanel);
            panelMain.Controls.SetChildIndex(buttonPanel, 0);

            // === 패널 추가 순서 ===
            
            this.Controls.Add(panelMain);
            this.Controls.Add(panelSidebar);
            this.Controls.Add(panelTopbar);
            panelTopbar.Controls.Add(btnExit);
            string imgPath = Path.Combine(Application.StartupPath, "XIcon.png");
            if (File.Exists(imgPath))
            {
                btnExit.Image = Image.FromFile(imgPath);
                btnExit.ImageAlign = ContentAlignment.MiddleCenter;
                btnExit.Text = "";
            }
            else
            {
                btnExit.Text = "X";
            }

            btnExit.Click += (s, e) => this.Close();

            // 사용자 정보
            pictureUser = new PictureBox
            {
                BackColor = Color.LightGray,
                SizeMode = PictureBoxSizeMode.Zoom,
                Size = new Size(63, 60),
                Location = new Point(63, 19)
            };
            string imagePath = Path.Combine(Application.StartupPath, "userIcon.png");
            if (File.Exists(imagePath))
            {
                pictureUser.Image = Image.FromFile(imagePath);
            }
            else
            {
                MessageBox.Show("프로필 이미지를 찾을 수 없습니다.");
            }

            labelUserName = new Label
            {
                Text = "관리자",
                Font = new Font("Arial Rounded MT Bold", 11.25F),
                ForeColor = SystemColors.MenuHighlight,
                Location = new Point(51, 86)
            };

            labelRole = new Label
            {
                Text = "관리자",
                Font = new Font("Arial Rounded MT Bold", 8.25F),
                Location = new Point(49, 113)
            };

            panelUser.Controls.Add(pictureUser);
            panelUser.Controls.Add(labelUserName);
            panelUser.Controls.Add(labelRole);
        }

        // === 버튼 이벤트 ===
        private void BtnCreate_Click(object sender, EventArgs e)
        {
            FormCreateNotice createForm = new FormCreateNotice((title, author, content,scheduleDate) =>
            {
                string writeDate = DateTime.Now.ToString("yyyy-MM-dd");
                ListViewItem item = new ListViewItem(title);
                item.SubItems.Add(author);
                item.SubItems.Add(writeDate);
                item.Tag = new NoticeSimple(scheduleDate, title, author, content,writeDate);
                lvNotices.Items.Add(item);
            });
            createForm.ShowDialog();
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (lvNotices.SelectedItems.Count == 0)
            {
                MessageBox.Show("수정할 공지사항을 선택하세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var selectedItem = lvNotices.SelectedItems[0];
            var notice = selectedItem.Tag as NoticeSimple;
            string currentTitle = selectedItem.Text;
            string currentAuthor = selectedItem.SubItems[1].Text;
            string currentDate = selectedItem.SubItems[2].Text;
            string currentContent = notice.Content;
            DateTime currentScheduleDate = notice.ScheduleDate;

            FormEditNotice editForm = new FormEditNotice(
                currentTitle, currentAuthor, currentContent,currentScheduleDate,
                (newTitle, newAuthor, newContent,newScheduleDate) =>
                {
                    selectedItem.Text = newTitle.StartsWith("✏️") ? newTitle : "✏️ " + newTitle;
                    selectedItem.SubItems[1].Text = newAuthor;
                    selectedItem.SubItems[2].Text = currentDate;
                    
                    selectedItem.Tag = newContent;

                    notice.Title = newTitle;
                    notice.Author = newAuthor;
                    notice.Content = newContent;
                    notice.ScheduleDate = newScheduleDate;
                    selectedItem.Tag = notice;
                },
                (titleToDelete) =>
                {
                    if (selectedItem.Text.Contains(titleToDelete))
                        lvNotices.Items.Remove(selectedItem);
                });

            editForm.ShowDialog();
        }

        private void BtnDetail_Click(object sender, EventArgs e)
        {
            if (lvNotices.SelectedItems.Count == 0)
            {
                MessageBox.Show("상세보기를 할 공지사항을 선택하세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var selectedItem = lvNotices.SelectedItems[0];
            var notice = selectedItem.Tag as NoticeSimple;
            FormDetailNotice detailForm = new FormDetailNotice(
                 notice.Title,
                 notice.Author,
                selectedItem.SubItems[2].Text,
                notice.Content

            );
            detailForm.ShowDialog();
        }
        
        private void BtnOpenCalendar_Click(object sender, EventArgs e)
        {
            List<NoticeSimple> noticeList = new List<NoticeSimple>();
            foreach (ListViewItem item in lvNotices.Items)
            {
                var notice = item.Tag as NoticeSimple;
                if (notice != null)
                {
                    noticeList.Add(notice);
                }
            }

            FormCalendar calendarForm = new FormCalendar();
            calendarForm.SetNotices(noticeList);
            calendarForm.ShowDialog();
        }
    }
}
