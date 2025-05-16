using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace timeScheduler
{
    public partial class FinalTimeTableForm : Form
    {
        private const int StartHour = 10;
        private const int EndHour = 22;
        private const int Days = 7;
        private List<TeacherSchedule> schedules = new List<TeacherSchedule>();
        private string scheduleFilePath = "schedule.csv";
        public FinalTimeTableForm()
        {
            InitializeComponent();
            InitializeGrid();
            LoadAndDisplaySchedule();
        }
        private void InitializeGrid()
        {
            for (int hour = StartHour; hour <= EndHour; hour++)
            {
                for (int day = 0; day < Days; day++)
                {
                    Label cell = new Label();
                    cell.BorderStyle = BorderStyle.FixedSingle;
                    cell.BackColor = Color.White;
                    cell.TextAlign = ContentAlignment.MiddleCenter;
                    cell.Tag = new CellInfo { Day = day, Hour = hour };
                    cell.Size = new Size(80, 30);
                    cell.Location = new Point(100 + day * 80, 50 + (hour - StartHour) * 30);

                    panelGrid.Controls.Add(cell);
                }
            }
        }

        private void LoadAndDisplaySchedule()
        {
            if (!File.Exists(scheduleFilePath)) return;

            schedules.Clear();
            var lines = File.ReadAllLines(scheduleFilePath);
            foreach (var line in lines)
            {
                var parts = line.Split(',');
                if (parts.Length != 4) continue;

                string name = parts[0];
                Color color = Color.FromArgb(int.Parse(parts[1]));
                int day = int.Parse(parts[2]);
                int hour = int.Parse(parts[3]);

                var schedule = schedules.FirstOrDefault(s => s.Name == name);
                if (schedule == null)
                {
                    schedule = new TeacherSchedule
                    {
                        Name = name,
                        Color = color,
                        TimeSlots = new HashSet<(int, int)>()
                    };
                    schedules.Add(schedule);
                }
                schedule.TimeSlots.Add((day, hour));
            }

            RefreshTimeGridUI();
        }

        private void RefreshTimeGridUI()
        {
            foreach (Control ctrl in panelGrid.Controls)
            {
                if (ctrl is Label cell)
                {
                    var info = (CellInfo)cell.Tag;
                    var matched = schedules.Where(s => s.TimeSlots.Contains((info.Day, info.Hour))).ToList();

                    if (matched.Count > 0)
                    {
                        cell.BackColor = matched.First().Color;
                        cell.Text = string.Join(", ", matched.Select(m => m.Name));
                    }
                    else
                    {
                        cell.BackColor = Color.White;
                        cell.Text = "";
                    }
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            TimeTableForm editForm = new TimeTableForm();
            editForm.FormClosed += (s, args) => LoadAndDisplaySchedule();
            editForm.Show();
        }

        private class CellInfo
        {
            public int Day { get; set; }
            public int Hour { get; set; }
        }

        private class TeacherSchedule
        {
            public string Name { get; set; }
            public Color Color { get; set; }
            public HashSet<(int, int)> TimeSlots { get; set; }
        }
    }
}
