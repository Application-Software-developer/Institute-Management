using System.Windows.Forms;

namespace InstituteManagement.UserControls
{
    public class DashboardControl : UserControl
    {
        public DashboardControl()
        {
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Dock = DockStyle.Fill;
            this.Controls.Add(new Label
            {
                Text = "대시보드 (임시)",
                Dock = DockStyle.Fill,
                Font = new System.Drawing.Font("Arial", 20),
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            });
        }
    }
}
