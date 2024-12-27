using BLL;
using DAL.DataControl;
using LiveCharts;
using LiveCharts.Wpf;
using Models;
using Models.ResumeInfo;
using System.Windows.Controls;

namespace ChartRender
{
    /// <summary>
    /// DisplayAge.xaml 的交互逻辑
    /// </summary>
    public partial class DisplayAge : UserControl
    {
        Dictionary<string, int> Age
        {
            get
            {
                List<ResumeInfo> list = new List<ResumeInfo>();
                List<Object> Files = (new ResumeFileControl()).Select(new Dictionary<string, List<string>>());
                foreach (ResumeFile file in Files)
                {

                    list.Add((new ResumeInfoControl())[(file).Filename]);
                }
                return ArmyClass.CountAge(list);
            }
            set
            {

            }
        }
        public DisplayAge()
        {
            InitializeComponent();
            Display();
        }


        private void Display()
        {


            var ageGroups = Age;

            // 设置柱状图数据
            var barValues = new ChartValues<int>(ageGroups.Values);
            BarChart.Series = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "人数分布",
                    Values = barValues
                }
            };
            BarChart.AxisX[0].Labels = ageGroups.Keys.ToArray();

            // 设置饼状图数据
            PieChart.Series = new SeriesCollection();
            foreach (var age in ageGroups)
            {
                PieChart.Series.Add(new PieSeries
                {
                    Title = age.Key,
                    Values = new ChartValues<int> { age.Value },
                    DataLabels = true
                });
            }

            // 更新最大值和最小值
            UpdateMinMax(ageGroups, "年龄分布");
        }


        // 更新最大值和最小值
        private void UpdateMinMax(Dictionary<string, int> data, string category)
        {
            if(data.Count > 0)
            {
                // 获取最大值和最小值
                var maxItem = data.OrderByDescending(x => x.Value).First();
                var minItem = data.OrderBy(x => x.Value).First();

                // 更新界面上的文本
                MaxValueText.Text = $"{category}中：{maxItem.Key} 最多，共有 ({maxItem.Value}个)";
                MinValueText.Text = $"，{minItem.Key} 最小，共有 ({minItem.Value}个)";

            }
        }
    }
}
