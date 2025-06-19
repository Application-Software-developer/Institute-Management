using System.Windows.Forms;

namespace InstituteManagement.UserControls
{
    public class StudentListControl : UserControl
    {
        public StudentListControl()
        {
            this.Dock = DockStyle.Fill;
            this.Controls.Add(new Label
            {
                Text = "학생 관리 (임시)",
                Dock = DockStyle.Fill,
                Font = new System.Drawing.Font("Arial", 20),
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            });
        }
    }
}
