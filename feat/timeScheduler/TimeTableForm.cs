using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace timeScheduler
{
    public partial class TimeTableForm : Form
    {
        private const int StartHour = 10;
        private const int EndHour = 22;
        private const int Days = 7;

        private bool isMouseDown = false;
        private List<Label> selectedCells = new List<Label>();
        private List<TeacherSchedule> schedules = new List<TeacherSchedule>();
        private string selectedTeacher = null;
        public TimeTableForm()
        {
            InitializeComponent();
            InitializeTimeGrid();
            this.MouseUp += TimeTableForm_MouseUp;
        }

        private void InitializeTimeGrid()
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
                    cell.Location = new Point(150 + day * 80, 50 + (hour - StartHour) * 30);

                    cell.MouseDown += Cell_MouseDown;
                    cell.MouseEnter += Cell_MouseEnter;
                    cell.MouseUp += Cell_MouseUp;

                    panelGrid.Controls.Add(cell);
                }
            }
        }

        private void Cell_MouseDown(object sender, MouseEventArgs e)
        {
            isMouseDown = true;
            ToggleCell(sender as Label);
        }

        private void Cell_MouseEnter(object sender, EventArgs e)
        {
            if (isMouseDown)
                ToggleCell(sender as Label);
        }

        private void Cell_MouseUp(object sender, MouseEventArgs e) => isMouseDown = false;

        private void TimeTableForm_MouseUp(object sender, MouseEventArgs e) => isMouseDown = false;

        private void ToggleCell(Label cell)
        {
            if (cell == null || string.IsNullOrEmpty(selectedTeacher)) return;

            var info = (CellInfo)cell.Tag;
            if (selectedCells.Contains(cell))
            {
                cell.BackColor = Color.White;
                cell.Text = "";
                selectedCells.Remove(cell);
            }
            else
            {
                cell.BackColor = Color.LightBlue;
                selectedCells.Add(cell);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string teacherName = txtTeacherName.Text.Trim();
            if (string.IsNullOrEmpty(teacherName))
            {
                MessageBox.Show("선생님 이름을 입력하세요.");
                return;
            }

            Color teacherColor = GetRandomColor();

            var newSchedule = new TeacherSchedule
            {
                Name = teacherName,
                Color = teacherColor,
                TimeSlots = new HashSet<(int, int)>()
            };

            foreach (Label cell in selectedCells)
            {
                var info = (CellInfo)cell.Tag;
                newSchedule.TimeSlots.Add((info.Day, info.Hour));
            }

            schedules.RemoveAll(s => s.Name == teacherName);
            schedules.Add(newSchedule);

            if (!lstTeachers.Items.Contains(teacherName))
                lstTeachers.Items.Add(teacherName);

            selectedTeacher = teacherName;
            RefreshTimeGridUI();
            selectedCells.Clear();
            txtTeacherName.Clear();
        }
        private void btnFinish_Click(object sender, EventArgs e)
        {
            SaveAllSchedulesToCsv();
            MessageBox.Show("전체 시간표가 저장되었습니다.");
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "CSV Files|*.csv";
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                LoadSchedulesFromCsv(openFile.FileName);
                RefreshTimeGridUI();
                MessageBox.Show("시간표가 불러와졌습니다.");
            }
        }

        private void lstTeachers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstTeachers.SelectedItem != null)
            {
                selectedTeacher = lstTeachers.SelectedItem.ToString();
                txtTeacherName.Text = selectedTeacher;
            }
            else
            {
                selectedTeacher = null; // 전체 보기 모드
                txtTeacherName.Text = "";
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
                        if (string.IsNullOrEmpty(selectedTeacher)) // 전체 보기 모드
                        {
                            // 여러 선생님 겹치는 경우 → 모두 표시
                            cell.BackColor = matched.First().Color; // 첫 사람 기준 색상 (시각용)
                            cell.Text = string.Join(", ", matched.Select(m => m.Name));
                        }
                        else // 특정 선생님 보기
                        {
                            var target = matched.FirstOrDefault(m => m.Name == selectedTeacher);
                            if (target != null)
                            {
                                cell.BackColor = target.Color;
                                cell.Text = target.Name;
                            }
                            else
                            {
                                cell.BackColor = Color.White;
                                cell.Text = "";
                            }
                        }
                    }
                    else
                    {
                        cell.BackColor = Color.White;
                        cell.Text = "";
                    }
                }
            }
        }

        private void SaveAllSchedulesToCsv()
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "CSV Files|*.csv";
            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var schedule in schedules)
                {
                    foreach (var slot in schedule.TimeSlots)
                    {
                        sb.AppendLine($"{schedule.Name},{schedule.Color.ToArgb()},{slot.Item1},{slot.Item2}");
                    }
                }
                File.WriteAllText(saveFile.FileName, sb.ToString());
            }
        }

        private void LoadSchedulesFromCsv(string path)
        {
            schedules.Clear();
            var lines = File.ReadAllLines(path);
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
                    if (!lstTeachers.Items.Contains(name))
                        lstTeachers.Items.Add(name);
                }
                schedule.TimeSlots.Add((day, hour));
            }
        }

        private Color GetRandomColor()
        {
            Random rand = new Random();
            return Color.FromArgb(rand.Next(100, 256), rand.Next(100, 256), rand.Next(100, 256));
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

        private void btnAllView_Click(object sender, EventArgs e)
        {
            selectedTeacher = null;
            lstTeachers.ClearSelected();
            txtTeacherName.Clear();
            RefreshTimeGridUI();
        }

        private void txtTeacherName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // Enter 입력 시 '삑' 소리 방지
                btnSave.PerformClick();    // 저장 버튼 누른 것처럼 동작
            }
        }

        private void btnOut_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
