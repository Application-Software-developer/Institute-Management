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
            this.ClientSize = new Size(850, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;
            this.Font = new Font("Segoe UI", 10);

            calendarTable = new TableLayoutPanel();
            calendarTable.RowCount = 6;
            calendarTable.ColumnCount = 7;
            calendarTable.Dock = DockStyle.Fill;
            calendarTable.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;

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

            int dayCounter = 1;
            for (int row = 0; row < 6; row++)
            {
                for (int col = 0; col < 7; col++)
                {
                    Panel dayPanel = new Panel();
                    dayPanel.Dock = DockStyle.Fill;
                    dayPanel.BorderStyle = BorderStyle.FixedSingle;

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

                    // 공지사항 버튼들을 담을 패널
                    FlowLayoutPanel noticePanel = new FlowLayoutPanel();
                    noticePanel.Dock = DockStyle.Fill;
                    noticePanel.FlowDirection = FlowDirection.TopDown;
                    noticePanel.WrapContents = false;
                    noticePanel.AutoScroll = true;

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
            // 기존 버튼 제거
            foreach (var flowPanel in datePanels.Values)
            {
                flowPanel.Controls.Clear();
            }

            // 새 공지사항 버튼 추가
            foreach (var notice in notices)
            {
                SetNoticeButton(notice.Date.Date, notice);
            }
        }

        private void SetNoticeButton(DateTime date, NoticeSimple notice)
        {
            if (!datePanels.ContainsKey(date))
                return;

            var flowPanel = datePanels[date];

            Button btn = new Button();
            btn.Text = notice.Title.Length > 12 ? notice.Title.Substring(0, 12) + "..." : notice.Title;
            btn.Width = flowPanel.Width - 5;
            btn.Height = 25;
            btn.BackColor = Color.LightSkyBlue;
            btn.ForeColor = Color.Black;
            btn.Font = new Font("Segoe UI", 8);
            btn.Margin = new Padding(1);

            btn.Click += (s, e) =>
            {
                FormDetailNotice detailForm = new FormDetailNotice(
                    notice.Title,
                    notice.Author,
                    date.ToString("yyyy-MM-dd"),
                    notice.Content
                );
                detailForm.ShowDialog();
            };

            flowPanel.Controls.Add(btn);
        }
    }
}
