using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using prototype.Models;


namespace InstituteManagement
{
    public partial class SheetForm : Form
    {
        private List<IncomeExpenseItem> itemList = new List<IncomeExpenseItem>();

        public SheetForm()
        {
            InitializeComponent();
            InitComboBox();
            InitDataGridView();
        }
        private void InitComboBox()
        {
            cbType.Items.Clear();
            cbType.Items.Add("수입");
            cbType.Items.Add("지출");
            cbType.SelectedIndex = 0;
        }

        private void InitDataGridView()
        {
            dgvSheet.Columns.Add("Date", "날짜");
            dgvSheet.Columns.Add("Name", "항목");
            dgvSheet.Columns.Add("Amount", "금액");
            dgvSheet.Columns.Add("Type", "구분");
            dgvSheet.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSheet.ReadOnly = true;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string name = txtItem.Text.Trim();
                decimal amount = decimal.Parse(txtAmount.Text.Trim());
                DateTime date = dtpDate.Value;
                string type = cbType.SelectedItem.ToString();

                if (string.IsNullOrEmpty(name))
                    throw new ArgumentException("항목을 입력해주세요.");

                if (amount <= 0)
                    throw new ArgumentException("금액은 0보다 커야 합니다.");

                var item = new IncomeExpenseItem(name, amount, date, type);
                itemList.Add(item);
                RefreshDataGridView();
                ClearInputs();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"항목 추가 중 오류 발생:\n{ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvSheet.SelectedRows.Count == 0)
                {
                    MessageBox.Show("수정할 항목을 선택해주세요.", "안내", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                int index = dgvSheet.SelectedRows[0].Index;

                string name = txtItem.Text.Trim();
                decimal amount = decimal.Parse(txtAmount.Text.Trim());
                DateTime date = dtpDate.Value;
                string type = cbType.SelectedItem.ToString();

                itemList[index].Name = name;
                itemList[index].Amount = amount;
                itemList[index].Date = date;
                itemList[index].Type = type;

                RefreshDataGridView();
                ClearInputs();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"항목 수정 중 오류 발생:\n{ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvSheet.SelectedRows.Count == 0)
                {
                    MessageBox.Show("삭제할 항목을 선택해주세요.", "안내", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                int index = dgvSheet.SelectedRows[0].Index;
                itemList.RemoveAt(index);
                RefreshDataGridView();
                ClearInputs();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"항목 삭제 중 오류 발생:\n{ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void RefreshDataGridView()
        {
            dgvSheet.Rows.Clear();
            foreach (var item in itemList)
            {
                dgvSheet.Rows.Add(item.Date.ToShortDateString(), item.Name, item.Amount.ToString("N0"), item.Type);
            }
        }

        private void dgvSheet_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvSheet.SelectedRows.Count > 0)
            {
                int index = dgvSheet.SelectedRows[0].Index;
                var selectedItem = itemList[index];

                dtpDate.Value = selectedItem.Date;
                txtItem.Text = selectedItem.Name;
                txtAmount.Text = selectedItem.Amount.ToString();
                cbType.SelectedItem = selectedItem.Type;
            }
        }
        private void ClearInputs()
        {
            txtItem.Clear();
            txtAmount.Clear();
            cbType.SelectedIndex = 0;
            dtpDate.Value = DateTime.Today;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
