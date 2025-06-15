using System;

namespace FormNoticeBoardAndCalendar
{
    public partial class NoticeSimple
    {
        public DateTime Date { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Content { get; set; }

        public NoticeSimple(DateTime date, string title, string author, string content)
        {
            Date = date;
            Title = title;
            Author = author;
            Content = content;
        }
    }
}
