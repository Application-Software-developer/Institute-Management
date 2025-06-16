// AttendanceClientForm.cs
using System;
using System.Windows.Forms;

namespace AcademyManager
{
    public partial class AttendanceClientForm : Form
    {
        private TextBox inputBox;
        private Button sendButton;
        private AttendanceForm attendanceForm;

        public AttendanceClientForm(AttendanceForm form)
        {
            this.attendanceForm = form;

            this.Text = "출석 입력 클라이언트";
            this.Width = 300;
            this.Height = 150;
            this.StartPosition = FormStartPosition.CenterScreen;

            InitializeLayout();
        }

        private void InitializeLayout()
        {
            Label label = new Label();
            label.Text = "학생 이름 입력:";
            label.Top = 20;
            label.Left = 20;
            label.Width = 100;
            this.Controls.Add(label);

            inputBox = new TextBox();
            inputBox.Top = 20;
            inputBox.Left = 130;
            inputBox.Width = 120;
            this.Controls.Add(inputBox);

            sendButton = new Button();
            sendButton.Text = "출석 전송";
            sendButton.Top = 60;
            sendButton.Left = 130;
            sendButton.Width = 120;
            sendButton.Click += SendButton_Click;
            this.Controls.Add(sendButton);
        }

        private void SendButton_Click(object sender, EventArgs e)
        {
            string studentName = inputBox.Text.Trim();
            if (!string.IsNullOrEmpty(studentName))
            {
                ((AttendanceForm)attendanceForm).MarkAttendance(studentName);
            }
        }
    }
}
