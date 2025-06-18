// PaymentChartForm.cs
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace AcademyManager
{
    public partial class PaymentChartForm : Form
    {
        private Chart paymentPieChart;
        private Chart incomeBarChart;

        //UI
        private Panel panelSidebar;
        private Panel panelHeader;
        private Panel panelMain;
        private Label labelUserName, labelRole;
        private Button btnDashboard, btnNoticeBoard, btnStudent, btnTeacher, btnTimeTable, btnSales, btnExit;
        private PictureBox pictureUser;

        public PaymentChartForm()
        {
            this.Text = "결제 상태 및 월 수입 현황";
            this.ClientSize = new Size(1000, 500);
            this.StartPosition = FormStartPosition.CenterScreen;

            InitializePaymentChart();
            InitializeIncomeChart();

            LoadPaymentDataFromCSV("students1.csv");
            LoadIncomeData();
            InitializeCommonLayout();
        }

        private void InitializePaymentChart()
        {
            paymentPieChart = new Chart
            {
                Size = new Size(400, 400),
                Location = new Point(256, 150)
            };

            ChartArea chartArea = new ChartArea("PieArea");
            paymentPieChart.ChartAreas.Add(chartArea);

            Series series = new Series("PaymentStatus")
            {
                ChartType = SeriesChartType.Pie
            };
            paymentPieChart.Series.Add(series);

            paymentPieChart.Legends.Add(new Legend("Legend"));

            this.Controls.Add(paymentPieChart);
        }

        private void LoadPaymentDataFromCSV(string path)
        {
            if (!File.Exists(path))
            {
                MessageBox.Show("students1.csv 파일이 없습니다.");
                return;
            }

            var lines = File.ReadAllLines(path).Skip(1);
            int paid = 0, unpaid = 0, pending = 0;

            foreach (var line in lines)
            {
                var parts = line.Split(',');
                if (parts.Length < 3) continue;
                string status = parts[2].Trim().ToLower();
                switch (status)
                {
                    case "paid": paid++; break;
                    case "unpaid": unpaid++; break;
                    case "pending": pending++; break;
                }
            }

            var series = paymentPieChart.Series["PaymentStatus"];
            series.Points.Clear();

            series.Points.AddXY("결제", paid);
            series.Points.AddXY("미결제", unpaid);
            series.Points.AddXY("보류", pending);

            series.Points[0].Color = Color.Blue;
            series.Points[1].Color = Color.Red;
            series.Points[2].Color = Color.Orange;
        }

        private void InitializeIncomeChart()
        {
            incomeBarChart = new Chart
            {
                Size = new Size(450, 400),
                Location = new Point(670, 150)
            };

            ChartArea area = new ChartArea("IncomeArea");
            area.AxisX.Title = "월";
            area.AxisY.Title = "수입 (만원)";
            incomeBarChart.ChartAreas.Add(area);

            Series series = new Series("월간 수입")
            {
                ChartType = SeriesChartType.Column
            };
            incomeBarChart.Series.Add(series);
            incomeBarChart.Legends.Add(new Legend("Legend"));

            this.Controls.Add(incomeBarChart);
        }

        private void LoadIncomeData()
        {
            var series = incomeBarChart.Series["월간 수입"];
            series.Points.Clear();

            string[] months = { "1월", "2월", "3월", "4월", "5월", "6월" };
            int[] income = { 180, 200, 160, 220, 210, 195 };

            for (int i = 0; i < months.Length; i++)
            {
                series.Points.AddXY(months[i], income[i]);
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
