using LiveCharts.Wpf;
using LiveCharts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BLL;
using DAL.DataControl;
using Models.ResumeInfo;
using Models;

namespace ChartRender
{
    // DisplayEdubg.xaml 的交互逻辑
    public partial class DisplayEdubg : UserControl
    {
        Dictionary<string, int> Edu{
            get
            {
                List<ResumeInfo> list = new List<ResumeInfo>();
                List<Object> Files = (new ResumeFileControl()).Select(new Dictionary<string, List<string>>());
                foreach (var file in Files)
                {
                    list.Add((new ResumeInfoControl())[((ResumeFile)file).Filename]);
                }
                return ArmyClass.CountEduBG(list);
            }

            set { 

            }
}
       
          
            public DisplayEdubg()
        {
            InitializeComponent();
            Display();
        }
        // 显示教育背景的柱状图和饼状图
        private void Display()
        {
            var eduBackgrounds = Edu;
           

            // 设置柱状图数据
            var barValues = new ChartValues<int>(eduBackgrounds.Values);
            BarChart.Series = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "教育背景分布",
                    Values = barValues
                }
            };
            BarChart.AxisX[0].Labels = eduBackgrounds.Keys.ToArray();

            // 设置饼状图数据
            PieChart.Series = new SeriesCollection();
            foreach (var edu in eduBackgrounds)
            {
                PieChart.Series.Add(new PieSeries
                {
                    Title = edu.Key,
                    Values = new ChartValues<double> { edu.Value },
                    DataLabels = true
                });
            }
            // 更新最大值和最小值
            UpdateMinMax(eduBackgrounds, "教育背景分布");

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
