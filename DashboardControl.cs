using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace mainForm
{
    public partial class DashboardControl : UserControl
    {

        public DashboardControl()
        {
            InitializeComponent();
            InitDashboard();

        }
    

        private void InitDashboard()
        {
            lblDate.Text = DateTime.Now.ToString("yyyy-MM-dd (dddd)");
            lblTime.Text = DateTime.Now.ToString("HH시 mm분 ss초");
            timerClock.Start();

            LoadRecentNotices();
        }

        private void LvNoticeSummary_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            using (SolidBrush backBrush = new SolidBrush(Color.LightGray))
            using (Pen borderPen = new Pen(Color.Gray))
            using (Font headerFont = new Font("Noto Sans KR", 10.5F, FontStyle.Bold))
            {
                e.Graphics.FillRectangle(backBrush, e.Bounds);
                e.Graphics.DrawRectangle(borderPen, e.Bounds);
                TextRenderer.DrawText(e.Graphics, e.Header.Text, headerFont, e.Bounds, Color.Black, TextFormatFlags.Left | TextFormatFlags.VerticalCenter);
            }
        }

        private void LvNoticeSummary_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            e.DrawBackground(); // 선택 효과도 줄 거면 e.DrawFocusRectangle(); 추가
        }

        private void LvNoticeSummary_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            using (Font rowFont = new Font("Noto Sans KR", 10.5F, FontStyle.Bold))
            {
                e.Graphics.DrawString(e.SubItem.Text, rowFont, Brushes.Black, e.Bounds);
            }
        }


        private void timerClock_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("HH시 mm분 ss초");
        }

        private void btnViewAllNotices_Click(object sender, EventArgs e)
        {
            var noticeBoard = new NoticeBoardControl();
            if (this.Parent is Panel panel && panel.Parent is Form1 form)
            {
                form.LoadControlToMain(noticeBoard);
            }
        }

        private void LoadRecentNotices()
        {
            lvNoticeSummary.Items.Clear();
            lvNoticeSummary.Columns.Clear();  // 기존 컬럼 제거하고

            // 컬럼 다시 추가
            lvNoticeSummary.Columns.Add("제목", 400);
            lvNoticeSummary.Columns.Add("작성자", 120);
            lvNoticeSummary.Columns.Add("작성일", 120);

            foreach (var notice in NoticeManager.Notices.Take(3))
            {
                var item = new ListViewItem(notice.Title);
                item.SubItems.Add(notice.Author);
                item.SubItems.Add(notice.Date);
                lvNoticeSummary.Items.Add(item);
            }
        }
        public void RefreshNotices()
        {
            lvNoticeSummary.Items.Clear();

            lvNoticeSummary.Columns.Add("제목", 400);
            lvNoticeSummary.Columns.Add("작성자", 120);
            lvNoticeSummary.Columns.Add("작성일", 120);

            foreach (var notice in NoticeManager.Notices.Take(3))
            {
                var item = new ListViewItem(notice.Title);
                item.SubItems.Add(notice.Author);
                item.SubItems.Add(notice.Date);
                lvNoticeSummary.Items.Add(item);
            }
        }

    }
}
