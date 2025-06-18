using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace FormNoticeBoardAndCalendar
{
    public class FormCalendar : Form
    {
        private TableLayoutPanel calendarTable;
        private Dictionary<DateTime, FlowLayoutPanel> datePanels = new Dictionary<DateTime, FlowLayoutPanel>();
        private DateTime currentMonth = DateTime.Today;

        public FormCalendar()
        {
            InitializeComponent();
            CreateCalendar(currentMonth);
        }

        private void InitializeComponent()
        {
            this.Text = "📅 캘린더";
            this.ClientSize = new Size(1000, 800);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(245,240,255);
            this.Font = new Font("Noto Sans KR", 10, FontStyle.Regular);

            calendarTable = new TableLayoutPanel
            {
                RowCount = 6,
                ColumnCount = 7,
                Dock = DockStyle.Fill,
                CellBorderStyle = TableLayoutPanelCellBorderStyle.Single,
                Padding = new Padding(5),
                BackColor = Color.White
            };

            for (int i = 0; i < 7; i++)
                calendarTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / 7));
            for (int i = 0; i < 6; i++)
                calendarTable.RowStyles.Add(new RowStyle(SizeType.Percent, 100f / 6));

            this.Controls.Add(calendarTable);
        }

        private void CreateCalendar(DateTime month)
        {
            calendarTable.Controls.Clear();
            datePanels.Clear();

            DateTime firstDay = new DateTime(month.Year, month.Month, 1);
            int startDayOfWeek = (int)firstDay.DayOfWeek;
            int daysInMonth = DateTime.DaysInMonth(month.Year, month.Month);

            // 필요한 행 계산
            int totalCells = startDayOfWeek + daysInMonth;
            int requiredRows = (int)Math.Ceiling(totalCells / 7.0);

            calendarTable.RowCount = requiredRows;
            calendarTable.RowStyles.Clear();
            for (int i = 0; i < requiredRows; i++)
            {
                calendarTable.RowStyles.Add(new RowStyle(SizeType.Percent, 100f / requiredRows));
            }

            int dayCounter = 1;
            for (int row = 0; row < requiredRows; row++)
            {
                for (int col = 0; col < 7; col++)
                {
                    Panel dayPanel = new Panel();
                    dayPanel.Dock = DockStyle.Fill;
                    dayPanel.BorderStyle = BorderStyle.FixedSingle;
                    dayPanel.BackColor = Color.WhiteSmoke;

                    if (row == 0 && col < startDayOfWeek)
                    {
                        calendarTable.Controls.Add(dayPanel, col, row);
                        continue;
                    }

                    if (dayCounter > daysInMonth)
                    {
                        calendarTable.Controls.Add(dayPanel, col, row);
                        continue;
                    }

                    DateTime currentDate = new DateTime(month.Year, month.Month, dayCounter);

                    Label dayLabel = new Label();
                    dayLabel.Text = dayCounter.ToString();
                    dayLabel.Dock = DockStyle.Top;
                    dayLabel.TextAlign = ContentAlignment.TopRight;
                    dayLabel.Padding = new Padding(0, 0, 5, 0);
                    dayLabel.Font = new Font("Segoe UI Semibold", 12);
                    dayLabel.ForeColor = Color.FromArgb(60, 60, 60);

                    FlowLayoutPanel noticePanel = new FlowLayoutPanel();
                    noticePanel.Dock = DockStyle.Fill;
                    noticePanel.FlowDirection = FlowDirection.TopDown;
                    noticePanel.WrapContents = false;
                    noticePanel.AutoScroll = true;
                    noticePanel.Padding = new Padding(3);

                    dayPanel.Controls.Add(noticePanel);
                    dayPanel.Controls.Add(dayLabel);
                    calendarTable.Controls.Add(dayPanel, col, row);
                    datePanels[currentDate] = noticePanel;

                    dayCounter++;
                }
            }
        }


        public void SetNotices(List<NoticeSimple> notices)
        {
            foreach (var flowPanel in datePanels.Values)
            {
                flowPanel.Controls.Clear();
            }

            foreach (var notice in notices)
            {
                SetNoticeButton(notice.ScheduleDate.Date, notice);
            }
        }

        private void SetNoticeButton(DateTime date, NoticeSimple notice)
        {
            if (!datePanels.ContainsKey(date))
                return;

            var flowPanel = datePanels[date];

            Button btn = new Button
            {
                Text = notice.Title.Length > 12 ? notice.Title.Substring(0, 12) + "..." : notice.Title,
                AutoSize = false,
                Width = flowPanel.ClientSize.Width - 10,
                Height = 35,
                BackColor = Color.FromArgb(91, 155, 213),  // DodgerBlue 계열, 시원한 파랑색
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Margin = new Padding(3, 2, 3, 2),
                Cursor = Cursors.Hand,
                TextAlign = ContentAlignment.MiddleCenter,
                Padding = new Padding(6, 0, 6, 0)
            };
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(41, 128, 185);
            btn.FlatAppearance.MouseDownBackColor = Color.FromArgb(31, 97, 141);
            btn.Padding = new Padding(5, 0, 5, 0);

            btn.Click += (s, e) =>
            {
                FormDetailNotice detailForm = new FormDetailNotice(
                    notice.Title,
                    notice.Author,
                    notice.WriteDate,
                    notice.Content
                );
                detailForm.ShowDialog();
            };

            flowPanel.Controls.Add(btn);
        }
    }
}
