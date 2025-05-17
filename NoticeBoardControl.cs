using FormNoticeBoard;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace mainForm
{
    public partial class NoticeBoardControl : UserControl
    {

        public NoticeBoardControl()
        {
            InitializeComponent();
            SetupListViewColumns();
            LoadAllNotices();
        }


        public void LoadAllNotices()
        {
            lvNotices.Items.Clear();
            foreach (var notice in NoticeManager.Notices)
            {
                var item = new ListViewItem(notice.Title);
                item.SubItems.Add(notice.Author);
                item.SubItems.Add(notice.Date);
                item.Tag = notice.Content;
                lvNotices.Items.Add(item);
            }
        }
        private void SetupListViewColumns()
        {
            lvNotices.Columns.Clear();
            lvNotices.Columns.Add("제목", 300);
            lvNotices.Columns.Add("작성자", 120);
            lvNotices.Columns.Add("작성일", 120);
        }
        private void btnCreate_Click_1(object sender, EventArgs e)
        {
            FormCreateNotice createForm = new FormCreateNotice((title, author, content) =>
            {
                string date = DateTime.Now.ToString("yyyy-MM-dd");

                // 1. NoticeManager에 넣기
                var newNotice = new Notice
                {
                    Title = title,
                    Author = author,
                    Date = date,
                    Content = content
                };
                NoticeManager.AddNotice(newNotice);

                // 2. 리스트뷰 갱신
                LoadAllNotices(); // 🔥 전체 다시 불러오면 정렬도 맞고 태그도 완벽히 유지됨
            });

            createForm.ShowDialog();
        }


        private void btnEdit_Click_1(object sender, EventArgs e)
        {

            if (lvNotices.SelectedItems.Count == 0)
            {
                MessageBox.Show("수정할 공지사항을 선택하세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var selectedItem = lvNotices.SelectedItems[0];

            string currentTitle = selectedItem.Text;
            string currentAuthor = selectedItem.SubItems[1].Text;
            string currentDate = selectedItem.SubItems[2].Text;
            string currentContent = selectedItem.Tag.ToString(); // 내용 가져오기

            FormEditNotice editForm = new FormEditNotice(currentTitle, currentAuthor, currentContent, (newTitle, newAuthor, newContent) =>
            {
                selectedItem.Text = newTitle + " (수정됨)";
                selectedItem.SubItems[1].Text = newAuthor;
                selectedItem.SubItems[2].Text = currentDate;
                selectedItem.Tag = newContent;
                lvNotices.Invalidate();
            });

            editForm.ShowDialog();
        }

        private void btnDetail_Click_1(object sender, EventArgs e)
        {
            if (lvNotices.SelectedItems.Count == 0)
            {
                MessageBox.Show("상세보기를 할 공지사항을 선택하세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var selectedItem = lvNotices.SelectedItems[0];

            string title = selectedItem.Text;
            string author = selectedItem.SubItems[1].Text;
            string date = selectedItem.SubItems[2].Text;
            string content = selectedItem.Tag.ToString();

            FormDetailNotice detailForm = new FormDetailNotice(title, author, date, content);
            detailForm.ShowDialog();
        }

    }

}
