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
    /// DisplaySkill.xaml 的交互逻辑
    /// </summary>
    public partial class DisplaySkill : UserControl
    {
        Dictionary<string, int> Ski
        {
            get
            {
                List<ResumeInfo> list = new List<ResumeInfo>();
                List<Object> Files = (new ResumeFileControl()).Select(new Dictionary<string, List<string>>());
                foreach (var file in Files)
                {
                    list.Add((new ResumeInfoControl())[((ResumeFile)file).Filename]);
                }
                return ArmyClass.CountSkill(list);
            }
            set
            {

            }
        }
        public DisplaySkill()
        {
            InitializeComponent();
            Display();
        }
        // 显示技能分布的柱状图和饼状图
        private void Display()
        {
            var skills = Ski;

            // 设置柱状图数据
            var barValues = new ChartValues<int>(skills.Values);
            BarChart.Series = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "技能分布",
                    Values = barValues
                }
            };
            BarChart.AxisX[0].Labels = skills.Keys.ToArray();

            // 设置饼状图数据
            PieChart.Series = new SeriesCollection();
            foreach (var skill in skills)
            {
                PieChart.Series.Add(new PieSeries
                {
                    Title = skill.Key,
                    Values = new ChartValues<double> { skill.Value },
                    DataLabels = true
                });
            }

            // 更新最大值和最小值
            UpdateMinMax(skills, "技能分布");
        }
        // 更新最大值和最小值
        private void UpdateMinMax(Dictionary<string, int> data, string category)
        {
            // 获取最大值和最小值
            var maxItem = data.OrderByDescending(x => x.Value).First();
            var minItem = data.OrderBy(x => x.Value).First();

            // 更新界面上的文本
            MaxValueText.Text = $"{category}中：{maxItem.Key} 占比最大 ({maxItem.Value}%)";
            MinValueText.Text = $"，{minItem.Key} 占比最小 ({minItem.Value}%)";
        }
    }
}
