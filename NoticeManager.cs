using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mainForm
{
    public static class NoticeManager
    {
        public static List<Notice> Notices { get; private set; } = new List<Notice>
    {
        new Notice { Title = "5월 시간표 공지", Author = "관리자", Date = "2025-05-01", Content = "시간표 내용..." },
        new Notice { Title = "중간고사 대비 특강 안내", Author = "학원장", Date = "2025-04-25", Content = "특강 내용..." },
        new Notice { Title = "5월 휴무일 안내", Author = "행정팀", Date = "2025-04-20", Content = "휴무일 내용..." }
    };

        public static void AddNotice(Notice notice)
        {
            Notices.Insert(0, notice);
        }
    }
}
