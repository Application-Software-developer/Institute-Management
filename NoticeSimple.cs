using System;

namespace FormNoticeBoardAndCalendar
{
    public class NoticeSimple
    {
        public DateTime ScheduleDate { get; set; }  // 일정 날짜
        public string Title { get; set; }
        public string Author { get; set; }
        public string Content { get; set; }
        public string WriteDate { get; set; }       // 작성일

        public NoticeSimple(DateTime scheduleDate, string title, string author, string content, string writeDate)
        {
            ScheduleDate = scheduleDate;
            Title = title;
            Author = author;
            Content = content;
            WriteDate = writeDate;
        }
    }

}
