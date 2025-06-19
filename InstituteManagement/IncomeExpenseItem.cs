using System;

namespace InstituteManagement.Models
{
    public class IncomeExpenseItem
    {
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; }

        public IncomeExpenseItem(string name, decimal amount, DateTime date, string type)
        {
            Name = name;
            Amount = amount;
            Date = date;
            Type = type;
        }
    }
}
