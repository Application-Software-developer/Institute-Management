using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FormNoticeBoard;

namespace mainForm
{
    public partial class Form1 : Form
    {
        private DashboardControl dashboardControl;
        private NoticeBoardControl noticeBoardControl;
        public Form1()
        {
            InitializeComponent();
            dashboardControl = new DashboardControl();
            noticeBoardControl = new NoticeBoardControl();

            LoadControlToMain(dashboardControl);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadControlToMain(new DashboardControl());
        }


        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        public void LoadControlToMain(UserControl control)
        {
            panelMain.Controls.Clear();
            panelMain.Controls.Add(control);
            control.Dock = DockStyle.Fill;
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            dashboardControl.RefreshNotices(); // 최신 데이터로 갱신
            LoadControlToMain(dashboardControl);
        }

        private void btnNoticeBoard_Click(object sender, EventArgs e)
        {
            noticeBoardControl.LoadAllNotices(); // 최신 데이터로 갱신
            LoadControlToMain(noticeBoardControl);
        }
        private void panelMain_Paint(object sender, PaintEventArgs e)
        {
            // 필요 없으면 여긴 비워도 됨
        }
        private void btnNoticeboard_Click(object sender, EventArgs e)
        {
            LoadControlToMain(noticeBoardControl); // 이거 아예 연결 안 되어 있었으면 이렇게 쓰면 됨
        }

    }
}
