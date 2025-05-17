using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InstituteManagement.Models
{
    // 수입/지출 항목 정보를 저장하는 데이터 클래스
    public class IncomeExpenseItem
    {
        public string Name { get; set; }       // 항목명
        public decimal Amount { get; set; }    // 금액
        public DateTime Date { get; set; }     // 날짜
        public string Type { get; set; }       // 유형: "수입" 또는 "지출"

        public IncomeExpenseItem(string name, decimal amount, DateTime date, string type)
        {
            Name = name;
            Amount = amount;
            Date = date;
            Type = type;
        }
    }
}
