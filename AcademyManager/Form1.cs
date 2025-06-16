using System;
using System.Windows.Forms;

namespace AcademyManager
{
    public partial class Form1 : Form
    {
        private Button btnTimetable;
        private Button btnAttendance;
        private Button btnCounseling;

        public Form1()
        {
            this.Text = "학원 관리 프로그램 - 메인";
            this.Width = 450;
            this.Height = 300;
            this.StartPosition = FormStartPosition.CenterScreen;

            // 1. When2meet 스타일 시간표
            btnTimetable = new Button
            {
                Text = "1. 시간표 작성 (When2meet)",
                Width = 300,
                Height = 40,
                Top = 30,
                Left = (this.ClientSize.Width - 300) / 2
            };
            btnTimetable.Click += (s, e) =>
            {
                // TODO: 시간표 폼으로 연결
                TimetableForm timetableForm = new TimetableForm();
                timetableForm.ShowDialog(this);

            };
            this.Controls.Add(btnTimetable);

            // 2. 출결 확인 (통신 포함)
            btnAttendance = new Button
            {
                Text = "2. 학생 출결 확인 (통신 기능 포함)",
                Width = 300,
                Height = 40,
                Top = 90,
                Left = (this.ClientSize.Width - 300) / 2
            };
            btnAttendance.Click += (s, e) =>
            {
                var serverForm = new AttendanceForm();
                serverForm.Show();

                var clientForm = new AttendanceClientForm(serverForm);
                clientForm.Show();
            };
            this.Controls.Add(btnAttendance);

            // 3. 상담 배정 및 드래그 이동
            btnCounseling = new Button
            {
                Text = "3. 상담 시간 배정 및 드래그 이동",
                Width = 300,
                Height = 40,
                Top = 150,
                Left = (this.ClientSize.Width - 300) / 2
            };
            btnCounseling.Click += (s, e) =>
            {
                // TODO: 상담 기능 폼으로 연결
                PaymentChartForm counselingForm = new PaymentChartForm();
                counselingForm.ShowDialog(this);

            };
            this.Controls.Add(btnCounseling);
        }
    }
}
