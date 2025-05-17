using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using InstituteManagement.Models;


namespace InstituteManagement
{
    // 금액 표시용 라벨들 - 디자이너에서 생성 + 라벨 초기화해주세요
    private Label labelIncome;
    private Label labelExpense;
    private Label labelBalance;
    //디자이너에서 Chart 컨트롤 추가해주세요
    //private System.Windows.Forms.DataVisualization.Charting.Chart chartSummary;

    public partial class AdminControl : UserControl
    {
        private List<IncomeExpenseItem> itemList = new List<IncomeExpenseItem>();

        public AdminControl()
        {
            InitializeComponent();
            InitListView();
            InitChart(); // 차트 초기화

        }

        private void InitListView()
        {
            listViewItems.View = View.Details;
            listViewItems.Columns.Add("항목명", 120);
            listViewItems.Columns.Add("금액", 80);
            listViewItems.Columns.Add("날짜", 100);
            listViewItems.Columns.Add("유형", 60);
        }

        public void AddItem(string name, decimal amount, DateTime date, string type)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name))
                    throw new ArgumentException("항목명은 비어 있을 수 없습니다.");

                if (amount <= 0)
                    throw new ArgumentException("금액은 0보다 커야 합니다.");

                if (type != "수입" && type != "지출")
                    throw new ArgumentException("유형은 '수입' 또는 '지출'만 가능합니다.");

                var item = new IncomeExpenseItem(name, amount, date, type);
                itemList.Add(item);

                var listItem = new ListViewItem(new[] {
            item.Name,
            item.Amount.ToString("N0"),
            item.Date.ToShortDateString(),
            item.Type
        });

                listViewItems.Items.Add(listItem);
                CalculateSummary();  // 추가 후 합계 갱신
                UpdateChart(); // 추가 후 차트 갱신

            }
            catch (Exception ex)
            {
                MessageBox.Show($"항목 추가 중 오류 발생:\n{ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void DeleteSelectedItem()
        {
            try
            {
                if (listViewItems.SelectedItems.Count == 0)
                {
                    MessageBox.Show("삭제할 항목을 선택해주세요.", "안내", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var selectedIndex = listViewItems.SelectedIndices[0];
                listViewItems.Items.RemoveAt(selectedIndex);
                itemList.RemoveAt(selectedIndex);
                CalculateSummary();  // 삭제 후 합계 갱신
                UpdateChart(); // 삭제 후 차트 갱신

            }
            catch (Exception ex)
            {
                MessageBox.Show($"항목 삭제 중 오류 발생:\n{ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //내부 데이터(itemList)와 화면에 표시되는 리스트뷰(listViewItems)의 내용을 동기화해서 최신 상태로 갱신
        public void RefreshItemList()
        {
            listViewItems.Items.Clear();

            foreach (var item in itemList)
            {
                var listItem = new ListViewItem(new[] {
                    item.Name,
                    item.Amount.ToString("N0"),
                    item.Date.ToShortDateString(),
                    item.Type
                });

                listViewItems.Items.Add(listItem);
            }
        }

        // 선택된 항목을 반환함. 수정 폼에 값을 넣을 때 사용
        // <returns>선택된 IncomeExpenseItem 혹은 null</returns>
        public IncomeExpenseItem? GetSelectedItem()
        {
            if (listViewItems.SelectedIndices.Count == 0)
                return null;

            int index = listViewItems.SelectedIndices[0];
            return itemList[index];
        }

        /// 선택된 항목을 수정하고 UI와 리스트 동기화
        public void UpdateSelectedItem(string name, decimal amount, DateTime date, string type)
        {
            try
            {
                if (listViewItems.SelectedIndices.Count == 0)
                {
                    MessageBox.Show("수정할 항목을 선택해주세요.", "안내", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (string.IsNullOrWhiteSpace(name))
                    throw new ArgumentException("항목명은 비어 있을 수 없습니다.");

                if (amount <= 0)
                    throw new ArgumentException("금액은 0보다 커야 합니다.");

                if (type != "수입" && type != "지출")
                    throw new ArgumentException("유형은 '수입' 또는 '지출'만 가능합니다.");

                int index = listViewItems.SelectedIndices[0];

                itemList[index].Name = name;
                itemList[index].Amount = amount;
                itemList[index].Date = date;
                itemList[index].Type = type;

                var listItem = listViewItems.Items[index];
                listItem.SubItems[0].Text = name;
                listItem.SubItems[1].Text = amount.ToString("N0");
                listItem.SubItems[2].Text = date.ToShortDateString();
                listItem.SubItems[3].Text = type;

                CalculateSummary();  // 수정 후 합계 갱신
                UpdateChart(); // 수정 후 차트 갱신

            }
            catch (Exception ex)
            {
                MessageBox.Show($"항목 수정 중 오류 발생:\n{ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CalculateSummary()
        {
            try
            {
                decimal totalIncome = 0m;
                decimal totalExpense = 0m;

                foreach (var item in itemList)
                {
                    if (item.Type == "수입")
                        totalIncome += item.Amount;
                    else if (item.Type == "지출")
                        totalExpense += item.Amount;
                }

                decimal balance = totalIncome - totalExpense;

                // UI에 보기 좋게 숫자 포맷팅
                labelIncome.Text = $"전체 수입: {totalIncome:N0} 원";
                labelExpense.Text = $"전체 지출: {totalExpense:N0} 원";
                labelBalance.Text = $"잔액: {balance:N0} 원";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"요약 계산 중 오류 발생: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //========================================== 차트 설정 ===========================================
        private void InitChart()
        {
            chartSummary.Series.Clear();
            chartSummary.ChartAreas.Clear();
            chartSummary.Legends.Clear();

            // 차트 영역
            var chartArea = new ChartArea("MainArea");
            chartSummary.ChartAreas.Add(chartArea);

            // 시리즈
            var series = new Series("수입지출비율");
            series.ChartType = SeriesChartType.Doughnut;
            series.Points.Clear();

            chartSummary.Series.Add(series);

            // 범례
            var legend = new Legend();
            chartSummary.Legends.Add(legend);

            // 색상
            series.Points.AddXY("수입", 0);
            series.Points.AddXY("지출", 0);
            series.Points[0].Color = Color.LightGreen;
            series.Points[1].Color = Color.LightCoral;
        }
        private void UpdateChart()
        {
            decimal totalIncome = itemList.Where(x => x.Type == "수입").Sum(x => x.Amount);
            decimal totalExpense = itemList.Where(x => x.Type == "지출").Sum(x => x.Amount);

            var series = chartSummary.Series["수입지출비율"];
            series.Points.Clear();

            series.Points.AddXY("수입", totalIncome);
            series.Points.AddXY("지출", totalExpense);

            series.Points[0].Color = Color.LightGreen;
            series.Points[1].Color = Color.LightCoral;
        }
    }
}