using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace FormNoticeBoardAndCalendar
{
    public class FormNoticeBoard : Form
    {
        private Label lblTitle;
        private ListView lvNotices;
        private Button btnCreate;
        private Button btnEdit;
        private Button btnDetail;
        private Button btnOpenCalendar;

        public FormNoticeBoard()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "공지사항 게시판";
            this.ClientSize = new Size(850, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(255, 245, 230);
            this.Font = new Font("Segoe UI", 10);

            lblTitle = new Label();
            lblTitle.Text = "📌 공지사항 게시판";
            lblTitle.Font = new Font("Segoe UI", 22, FontStyle.Bold);
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(20, 20);
            this.Controls.Add(lblTitle);

            lvNotices = new ListView();
            lvNotices.Location = new Point(20, 70);
            lvNotices.Size = new Size(800, 400);
            lvNotices.View = View.Details;
            lvNotices.FullRowSelect = true;
            lvNotices.GridLines = true;
            lvNotices.BorderStyle = BorderStyle.FixedSingle;
            lvNotices.Columns.Add("제목", 460);
            lvNotices.Columns.Add("작성자", 160);
            lvNotices.Columns.Add("작성일", 160);
            lvNotices.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            lvNotices.BackColor = Color.FromArgb(240, 240, 240);
            lvNotices.ForeColor = Color.Black;
            this.Controls.Add(lvNotices);

            Size btnSize = new Size(160, 40);
            int btnTop = 490;

            btnCreate = new Button();
            btnCreate.Text = "📝 공지사항 작성";
            btnCreate.Size = btnSize;
            btnCreate.Location = new Point(95, btnTop);
            StyleButton(btnCreate);
            btnCreate.Click += BtnCreate_Click;
            this.Controls.Add(btnCreate);

            btnEdit = new Button();
            btnEdit.Text = "✏️ 공지사항 수정";
            btnEdit.Size = btnSize;
            btnEdit.Location = new Point(345, btnTop);
            StyleButton(btnEdit);
            btnEdit.Click += BtnEdit_Click;
            this.Controls.Add(btnEdit);

            btnDetail = new Button();
            btnDetail.Text = "🔍 상세 보기";
            btnDetail.Size = btnSize;
            btnDetail.Location = new Point(595, btnTop);
            StyleButton(btnDetail);
            btnDetail.Click += BtnDetail_Click;
            this.Controls.Add(btnDetail);

            btnOpenCalendar = new Button();
            btnOpenCalendar.Text = "📅 캘린더 보기";
            btnOpenCalendar.Size = new Size(160, 40);
            btnOpenCalendar.Location = new Point(345, 550);
            StyleButton(btnOpenCalendar);
            btnOpenCalendar.Click += BtnOpenCalendar_Click;
            this.Controls.Add(btnOpenCalendar);
        }

        private void StyleButton(Button btn)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.BackColor = Color.FromArgb(240, 240, 240);
            btn.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btn.FlatAppearance.BorderColor = Color.Gray;
            btn.FlatAppearance.BorderSize = 1;
        }

        private void BtnCreate_Click(object sender, EventArgs e)
        {
            FormCreateNotice createForm = new FormCreateNotice((title, author, content) =>
            {
                string date = DateTime.Now.ToString("yyyy-MM-dd");
                ListViewItem item = new ListViewItem(title);
                item.SubItems.Add(author);
                item.SubItems.Add(date);
                item.Tag = content;
                lvNotices.Items.Add(item);
            });

            createForm.ShowDialog();
        }

        private void BtnEdit_Click(object sender, EventArgs e)
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
            string currentContent = selectedItem.Tag.ToString();

            FormEditNotice editForm = new FormEditNotice(
        currentTitle, currentAuthor, currentContent,
        (newTitle, newAuthor, newContent) =>
        {
            // 제목 앞에 연필 아이콘 없으면 붙임
            if (!selectedItem.Text.StartsWith("✏️ "))
            {
                selectedItem.Text = "✏️ " + newTitle;
            }
            else
            {
                selectedItem.Text = newTitle;
            }
            selectedItem.SubItems[1].Text = newAuthor;
            selectedItem.SubItems[2].Text = currentDate;
            selectedItem.Tag = newContent;
            lvNotices.Invalidate();
        },
        (titleToDelete) =>
        {
            // 삭제 콜백: 선택된 항목 지우기
            if (selectedItem.Text == titleToDelete || selectedItem.Text == "✏️ " + titleToDelete)
            {
                lvNotices.Items.Remove(selectedItem);
            }
        });

            editForm.ShowDialog();
        }

        private void BtnDetail_Click(object sender, EventArgs e)
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

        private void BtnOpenCalendar_Click(object sender, EventArgs e)
        {
            List<NoticeSimple> noticeList = new List<NoticeSimple>();
            foreach (ListViewItem item in lvNotices.Items)
            {
                string title = item.Text;
                string author = item.SubItems[1].Text;
                DateTime date = DateTime.Parse(item.SubItems[2].Text);
                string content = item.Tag.ToString();

                noticeList.Add(new NoticeSimple(date, title, author, content));
            }

            FormCalendar calendarForm = new FormCalendar();
            calendarForm.SetNotices(noticeList);
            calendarForm.ShowDialog();
        }
    }
}
