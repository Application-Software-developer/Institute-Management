using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using InstituteManagement.Models;

namespace InstituteManagement
{
    public partial class SheetForm : Form
    {
        private TextBox txtItem, txtAmount;
        private DateTimePicker dtpDate;
        private ComboBox cbType;
        private Button btnAdd, btnUpdate, btnDelete, btnDownload,btnLoad;
        private DataGridView dgvSheet;
        private Label lblTotalSummary;
        private List<IncomeExpenseItem> itemList = new List<IncomeExpenseItem>();

        public SheetForm()
        {
            InitializeStandaloneLayout();
            InitializeControls();
        }

        private void InitializeStandaloneLayout()
        {
            this.Text = "재무 관리 - 장부";
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.ClientSize = new Size(1000, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;

            Panel topPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 50,
                BackColor = Color.DodgerBlue
            };

            Label titleLabel = new Label
            {
                Text = "재무 관리 - 장부",
                Font = new Font("Noto Sans KR", 14.25F, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(20, 12)
            };

            Button closeButton = new Button
            {
                Text = "X",
                Font = new Font("Arial", 10F, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Size = new Size(30, 30),
                Location = new Point(this.ClientSize.Width - 40, 10),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                ForeColor = Color.White,
                BackColor = Color.DodgerBlue,
                FlatAppearance = { BorderSize = 0 }
            };
            closeButton.Click += (s, e) => this.Close();

            topPanel.Controls.Add(titleLabel);
            topPanel.Controls.Add(closeButton);
            this.Controls.Add(topPanel);
        }

        private void InitializeControls()
        {
            Size fieldSize = new Size(140, 30);
            Size buttonSize = new Size(70, 30);

            txtItem = new TextBox { Size = fieldSize, Location = new Point(40, 70) };
            txtAmount = new TextBox { Size = fieldSize, Location = new Point(200, 70) };
            dtpDate = new DateTimePicker
            {
                Format = DateTimePickerFormat.Short,
                Size = fieldSize,
                Location = new Point(360, 70)
            };
            cbType = new ComboBox
            {
                Size = fieldSize,
                Location = new Point(520, 70),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cbType.Items.AddRange(new string[] { "수입", "지출" });
            cbType.SelectedIndex = 0;

            btnAdd = new Button { Text = "추가", Size = buttonSize, Location = new Point(680, 70) };
            btnUpdate = new Button { Text = "수정", Size = buttonSize, Location = new Point(760, 70) };
            btnDelete = new Button { Text = "삭제", Size = buttonSize, Location = new Point(840, 70) };
            btnDownload = new Button
            {
                Text = "다운로드",
                Size = new Size(100, 36),
                Location = new Point(this.ClientSize.Width - 160, this.ClientSize.Height - 50),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Right,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                BackColor = Color.LightSeaGreen,
                FlatStyle = FlatStyle.Flat
            };

            btnAdd.Click += btnAdd_Click;
            btnUpdate.Click += btnUpdate_Click;
            btnDelete.Click += btnDelete_Click;
            btnDownload.Click += btnDownload_Click;

            dgvSheet = new DataGridView
            {
                Location = new Point(40, 130),
                Size = new Size(900, 480),
                AllowUserToAddRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                DefaultCellStyle = { Font = new Font("Segoe UI", 11F) },
                ColumnHeadersDefaultCellStyle = { Font = new Font("Segoe UI", 11F, FontStyle.Bold) },
                RowTemplate = { Height = 28 }
            };

            dgvSheet.Columns.Add("Date", "날짜");
            dgvSheet.Columns.Add("Name", "항목명");
            dgvSheet.Columns.Add("Amount", "금액");
            dgvSheet.Columns.Add("Type", "구분");

            dgvSheet.Columns[0].FillWeight = 30;
            dgvSheet.Columns[1].FillWeight = 20;
            dgvSheet.Columns[2].FillWeight = 30;
            dgvSheet.Columns[3].FillWeight = 20;

            dgvSheet.SelectionChanged += dgvSheet_SelectionChanged;

            SetPlaceholder(txtItem, "항목명");
            SetPlaceholder(txtAmount, "금액");

            this.Controls.AddRange(new Control[]
            {
                txtItem, txtAmount, dtpDate, cbType,
                btnAdd, btnUpdate, btnDelete,
                dgvSheet, btnDownload
            });

            lblTotalSummary = new Label
            {
                Text = "총 수입: 0 | 총 지출: 0",
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                Size = new Size(900, 30),
                Location = new Point(40, 620),
                ForeColor = Color.DarkSlateGray,
                TextAlign = ContentAlignment.MiddleCenter
            };
            this.Controls.Add(lblTotalSummary);

            btnLoad = new Button
            {
                Text = "불러오기",
                Size = new Size(100, 36),
                Location = new Point(this.ClientSize.Width - 280, this.ClientSize.Height - 50),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Right,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                BackColor = Color.Goldenrod,
                FlatStyle = FlatStyle.Flat
            };
            btnLoad.Click += btnLoad_Click;

            this.Controls.Add(btnLoad);

        }
        private void btnLoad_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "CSV 파일 (*.csv)|*.csv";
                ofd.Title = "CSV 파일 불러오기";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var lines = File.ReadAllLines(ofd.FileName);
                        if (lines.Length < 2)
                        {
                            MessageBox.Show("CSV 파일에 데이터가 없습니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        itemList.Clear(); // 기존 데이터 초기화

                        foreach (var line in lines.Skip(1))
                        {
                            var parts = line.Split(',');
                            if (parts.Length < 4) continue;

                            string dateText = parts[0].Trim();
                            string name = parts[1].Trim();
                            string amountText = parts[2].Trim();
                            string type = parts[3].Trim();

                            if (!DateTime.TryParse(dateText, out DateTime date)) continue;
                            if (!decimal.TryParse(amountText, out decimal amount)) continue;
                            if (type != "수입" && type != "지출") continue;

                            itemList.Add(new IncomeExpenseItem(name, amount, date, type));
                        }

                        RefreshGrid();
                        ClearInput();

                        MessageBox.Show("CSV 파일이 성공적으로 불러와졌습니다.", "성공", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"불러오기 중 오류 발생: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            if (itemList.Count == 0)
            {
                MessageBox.Show("저장할 데이터가 없습니다.");
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "CSV 파일 (*.csv)|*.csv";
                sfd.FileName = "장부_데이터.csv";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (StreamWriter sw = new StreamWriter(sfd.FileName, false, System.Text.Encoding.UTF8))
                        {
                            sw.WriteLine("날짜,항목명,금액,구분");
                            foreach (var item in itemList)
                            {
                                sw.WriteLine($"{item.Date:yyyy-MM-dd},{item.Name},{item.Amount},{item.Type}");
                            }
                        }

                        MessageBox.Show("CSV 파일로 저장되었습니다.", "성공", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"저장 중 오류 발생: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void SetPlaceholder(TextBox textbox, string placeholder)
        {
            textbox.ForeColor = Color.Gray;
            textbox.Text = placeholder;

            textbox.GotFocus += (s, e) =>
            {
                if (textbox.Text == placeholder)
                {
                    textbox.Text = "";
                    textbox.ForeColor = Color.Black;
                }
            };

            textbox.LostFocus += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(textbox.Text))
                {
                    textbox.Text = placeholder;
                    textbox.ForeColor = Color.Gray;
                }
            };
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string name = txtItem.Text.Trim();
                string amountText = txtAmount.Text.Trim();
                DateTime date = dtpDate.Value;
                string type = cbType.SelectedItem.ToString();

                if (string.IsNullOrEmpty(name) || name == "항목명")
                    throw new ArgumentException("항목명을 입력하세요.");

                if (!decimal.TryParse(amountText, out decimal amount) || amount <= 0 || amountText == "금액")
                    throw new ArgumentException("금액은 숫자이며 0보다 커야 합니다.");

                var item = new IncomeExpenseItem(name, amount, date, type);
                itemList.Add(item);
                RefreshGrid();
                ClearInput();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvSheet.SelectedRows.Count == 0)
            {
                MessageBox.Show("수정할 항목을 선택하세요.");
                return;
            }

            int index = dgvSheet.SelectedRows[0].Index;

            try
            {
                string name = txtItem.Text.Trim();
                string amountText = txtAmount.Text.Trim();
                DateTime date = dtpDate.Value;
                string type = cbType.SelectedItem.ToString();

                if (string.IsNullOrEmpty(name) || name == "항목명")
                    throw new ArgumentException("항목명을 입력하세요.");

                if (!decimal.TryParse(amountText, out decimal amount) || amount <= 0 || amountText == "금액")
                    throw new ArgumentException("금액은 숫자이며 0보다 커야 합니다.");

                itemList[index].Name = name;
                itemList[index].Amount = amount;
                itemList[index].Date = date;
                itemList[index].Type = type;

                RefreshGrid();
                ClearInput();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvSheet.SelectedRows.Count == 0)
            {
                MessageBox.Show("삭제할 항목을 선택하세요.");
                return;
            }

            int index = dgvSheet.SelectedRows[0].Index;
            itemList.RemoveAt(index);
            RefreshGrid();
            ClearInput();
        }

        private void dgvSheet_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvSheet.SelectedRows.Count > 0)
            {
                int index = dgvSheet.SelectedRows[0].Index;
                var item = itemList[index];

                txtItem.Text = item.Name;
                txtItem.ForeColor = Color.Black;

                txtAmount.Text = item.Amount.ToString();
                txtAmount.ForeColor = Color.Black;

                dtpDate.Value = item.Date;
                cbType.SelectedItem = item.Type;
            }
        }

        private void RefreshGrid()
        {
            dgvSheet.Rows.Clear();
            foreach (var item in itemList)
            {
                dgvSheet.Rows.Add(item.Date.ToShortDateString(), item.Name, item.Amount.ToString("N0"), item.Type);
            }

            decimal totalIncome = 0;
            decimal totalExpense = 0;

            foreach (var item in itemList)
            {
                if (item.Type == "수입")
                    totalIncome += item.Amount;
                else if (item.Type == "지출")
                    totalExpense += item.Amount;
            }

            decimal net = totalIncome - totalExpense;
            string netDisplay = net >= 0 ? $"+{net:N0}" : $"-{Math.Abs(net):N0}";

            lblTotalSummary.Text = $"총 수입: {totalIncome:N0}  |  총 지출: {totalExpense:N0}  |  손익: {netDisplay}";

        }

        private void ClearInput()
        {
            txtItem.Text = "항목명";
            txtItem.ForeColor = Color.Gray;

            txtAmount.Text = "금액";
            txtAmount.ForeColor = Color.Gray;

            dtpDate.Value = DateTime.Today;
            cbType.SelectedIndex = 0;
        }
    }
}
