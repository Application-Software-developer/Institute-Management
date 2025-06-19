using System.Windows.Forms;

namespace InstituteManagement.UserControls
{
    public class NoticeControl : UserControl
    {
        public NoticeControl()
        {
            this.Dock = DockStyle.Fill;
            this.Controls.Add(new Label
            {
                Text = "공지사항 (임시)",
                Dock = DockStyle.Fill,
                Font = new System.Drawing.Font("Arial", 20),
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            });
        }
    }
}
