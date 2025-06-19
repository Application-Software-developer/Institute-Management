using System.Windows.Forms;

namespace InstituteManagement.UserControls
{
    public class TimetableControl : UserControl
    {
        public TimetableControl()
        {
            this.Dock = DockStyle.Fill;
            this.Controls.Add(new Label
            {
                Text = "시간표 (임시)",
                Dock = DockStyle.Fill,
                Font = new System.Drawing.Font("Arial", 20),
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            });
        }
    }
}
