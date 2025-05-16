using System;
using System.Drawing;
using System.Windows.Forms;

namespace calendar

{
    public partial class FormCalendar : Form
    {
        private Label lblYearMonth;
        private Button btnPrevMonth;
        private Button btnNextMonth;
        private Panel panelCalendarGrid;

        private readonly string[] weekDays = { "Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat" };
        private const int CellWidth = 50;
        private const int CellHeight = 40;

        private int displayYear = 2025;
        private int displayMonth = 5;

        // 날짜 셀 버튼 배열
        private Button[] dayCells = new Button[42];

        public FormCalendar()
        {
            InitializeComponent();
            InitializeCalendarStructure();
            ShowCalendar(displayYear, displayMonth);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            this.ClientSize = new Size(400, 400);
            this.Name = "FormCalendar";
            this.Text = "Calendar";

            this.ResumeLayout(false);
        }

        private void InitializeCalendarStructure()
        {
            // 년/월 라벨
            lblYearMonth = new Label()
            {
                Text = $"{displayYear} / {displayMonth:D2}",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Top,
                Height = 30
            };
            this.Controls.Add(lblYearMonth);

            // 이전 달 버튼
            btnPrevMonth = new Button()
            {
                Text = "<",
                Size = new Size(40, 30),
                Location = new Point(10, 30)
            };
            btnPrevMonth.Click += BtnPrevMonth_Click;
            this.Controls.Add(btnPrevMonth);

            // 다음 달 버튼
            btnNextMonth = new Button()
            {
                Text = ">",
                Size = new Size(40, 30),
                Location = new Point(this.ClientSize.Width - 50, 30)
            };
            btnNextMonth.Click += BtnNextMonth_Click;
            this.Controls.Add(btnNextMonth);

            // 달력 그리드 패널 (요일 + 날짜 셀 포함)
            panelCalendarGrid = new Panel()
            {
                Location = new Point(10, 70),
                Size = new Size(CellWidth * 7, CellHeight * 7),
                BorderStyle = BorderStyle.FixedSingle
            };
            this.Controls.Add(panelCalendarGrid);

            // 요일 헤더 그리기 (일~토)
            for (int i = 0; i < 7; i++)
            {
                Label lblDay = new Label()
                {
                    Text = weekDays[i],
                    Size = new Size(CellWidth, CellHeight),
                    Location = new Point(i * CellWidth, 0),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font("Segoe UI", 9, FontStyle.Bold),
                    BackColor = Color.LightGray
                };
                panelCalendarGrid.Controls.Add(lblDay);
            }

            // 날짜 셀 버튼 생성 및 저장
            for (int i = 0; i < 42; i++)
            {
                Button dayCell = new Button()
                {
                    Size = new Size(CellWidth, CellHeight),
                    Location = new Point((i % 7) * CellWidth, (i / 7 + 1) * CellHeight),
                    Text = "",
                    Enabled = false
                };
                dayCells[i] = dayCell;
                panelCalendarGrid.Controls.Add(dayCell);
            }
        }

        // 달력 보여주기
        private void ShowCalendar(int year, int month)
        {
            lblYearMonth.Text = $"{year} / {month:D2}";

            DateTime firstDay = new DateTime(year, month, 1);
            int daysInMonth = DateTime.DaysInMonth(year, month);

            // 0: 일요일 ... 6: 토요일
            int startDayOfWeek = (int)firstDay.DayOfWeek;

            // 날짜 셀 초기화
            for (int i = 0; i < 42; i++)
            {
                dayCells[i].Text = "";
                dayCells[i].Enabled = false;
                dayCells[i].BackColor = SystemColors.Control;
            }

            // 날짜 배치
            for (int day = 1; day <= daysInMonth; day++)
            {
                int cellIndex = startDayOfWeek + day - 1;
                dayCells[cellIndex].Text = day.ToString();
                dayCells[cellIndex].Enabled = true;
                dayCells[cellIndex].BackColor = Color.White;
            }
        }

        private void BtnPrevMonth_Click(object sender, EventArgs e)
        {
            // 월 감소 (1월 -> 12월 전환)
            displayMonth--;
            if (displayMonth < 1)
            {
                displayMonth = 12;
                displayYear--;
            }
            ShowCalendar(displayYear, displayMonth);
        }

        private void BtnNextMonth_Click(object sender, EventArgs e)
        {
            // 월 증가 (12월 -> 1월 전환)
            displayMonth++;
            if (displayMonth > 12)
            {
                displayMonth = 1;
                displayYear++;
            }
            ShowCalendar(displayYear, displayMonth);
        }
    }
}
