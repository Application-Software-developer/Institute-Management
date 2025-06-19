using System.Windows.Forms;

namespace InstituteManagement.UserControls
{
    public class CalendarControl : UserControl
    {
        public CalendarControl()
        {
            this.Dock = DockStyle.Fill;
            this.Controls.Add(new Label
            {
                Text = "캘린더 (임시)",
                Dock = DockStyle.Fill,
                Font = new System.Drawing.Font("Arial", 20),
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            });
        }
    }
}
