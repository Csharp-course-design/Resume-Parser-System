using CsharpAPI;
using JiebaNet.Segmenter;
using Models.ResumeInfo;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace ChartRender;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class ChartWindow : Window
{
    private string directoryPath = "E:\\GitHubDeskTop_\\Resume-Parser-System\\Info";

    private Dictionary<string, int> degreeMap, skillMap;
    private Dictionary<int, int> ageMap;
    public ChartWindow()
    {
        InitializeComponent();
        initFileData();
        initDegreePieChart();
        initSkillBarChart();
        initAgePieChart();

        if ((degreeMap!.Count | skillMap!.Count | ageMap!.Count) == 0)
        {
            MessageBox.Show("暂无已导入的简历", "", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }
        Show();
    }

    private static OxyColor getColor(string degree)
    {
        return degree switch
        {
            // "专科" => OxyColor.FromArgb(255, 255, 225, 104), // 浅黄到蓝绿色渐变
            // "本科" => OxyColor.FromArgb(255, 212, 236, 89),
            // "硕士" => OxyColor.FromArgb(255, 156, 220, 130),
            // "博士" => OxyColor.FromArgb(255, 50, 211, 235),
            "专科" => OxyColor.FromArgb(255, 6, 140, 168), // 配色参考 www.materialpalette.com
            "本科" => OxyColor.FromArgb(255, 8, 135, 192),
            "硕士" => OxyColor.FromArgb(255, 16, 107, 179),
            "博士" => OxyColor.FromArgb(255, 66, 78, 156),
            _ => OxyColors.Gray // 默认颜色
        };
    }

    private static OxyColor getColor(int age)
    {
        return age switch
        {
            <= 18 => OxyColor.FromArgb(255, 149, 113, 6), // 配色参考 www.materialpalette.com
            <= 20 => OxyColor.FromArgb(255, 126, 115, 6),
            <= 22 => OxyColor.FromArgb(255, 116, 122, 26),
            <= 24 => OxyColor.FromArgb(255, 114, 146, 53),
            _ => OxyColor.FromArgb(255, 64, 139, 67)
        };
    }

    private static string getAgeCategoryName(int age)
    {
        return age switch
        {
            <= 18 => "<=18",
            <= 20 => "19~20",
            <= 22 => "21~22",
            <= 24 => "23~24",
            _ => ">=25"
        };
    }

    private void initDegreePieChart()
    {
        var plotModel = new PlotModel
        {
            Title = "学历分布",
            TextColor = OxyColors.White,
            TitleFontSize = 30.0
        };

        var pieSeries = new PieSeries
        {
            FontSize = 20.0,
            StrokeThickness = 2, // 扇区边框宽度
            Slices = degreeMap.Select((kv, index) =>
                new PieSlice(kv.Key, kv.Value) // 生成一个 PieSlice
                {
                    Fill = getColor(kv.Key), // 根据学历选择填充颜色
                    IsExploded = false, // 是否突出显示
                }).ToList()
        };
        plotModel.Series.Add(pieSeries);
        DegreePieChartPlotView.Model = plotModel;
    }

    private void initAgePieChart()
    {
        var plotModel = new PlotModel
        {
            Title = "年龄分布",
            TextColor = OxyColors.White,
            TitleFontSize = 30.0
        };

        var pieSeries = new PieSeries
        {
            FontSize = 20.0,
            StrokeThickness = 2, // 扇区边框宽度
            Slices = ageMap.Select((kv, index) =>
                new PieSlice(getAgeCategoryName(kv.Key), kv.Value) // 生成一个 PieSlice
                {
                    Fill = getColor(kv.Key), // 根据学历选择填充颜色
                    IsExploded = false, // 是否突出显示
                }).ToList()
        };
        plotModel.Series.Add(pieSeries);
        AgePieChartPlotView.Model = plotModel;
    }

    private void initSkillBarChart()
    {
        var barItems = new List<BarItem>();
        var items = new List<KeyValuePair<string, int>>();
        var skillNames = new List<string>();

        foreach (var s in skillMap)
        {
            items.Add(s);
        }
        items = items.OrderByDescending(item => item.Value).Take(20).ToList();

        foreach (var (skill, num) in items)
        {
            barItems.Add(new BarItem(num));
            skillNames.Add(skill);
        }

        // 创建柱状图数据
        var barSeries = new BarSeries
        {
            ItemsSource = barItems,
            LabelFormatString = "{0}"
        };

        // 创建 PlotModel
        var plotModel = new PlotModel
        {
            TitleFontSize = 30,
            Title = "技能统计 (top20)",
            TextColor = OxyColors.White,
            PlotAreaBorderColor = OxyColors.Transparent
        };

        var categoryAxis = new CategoryAxis
        {
            FontSize = 20,
            Position = AxisPosition.Left
        };
        categoryAxis.Labels.AddRange(skillNames);
        var valueAxis = new LinearAxis
        {
            FontSize = 20,
            Position = AxisPosition.Bottom,
        };

        plotModel.Axes.Add(valueAxis);
        plotModel.Axes.Add(categoryAxis);
        plotModel.Series.Add(barSeries);
        SkillBarChartPlotView.Model = plotModel;
    }


    private void initFileData() // 读取 json 文件，获取用于构建图标的学历，年龄，技能信息
    {
        degreeMap = new Dictionary<string, int>();
        skillMap = new Dictionary<string, int>();
        ageMap = new Dictionary<int, int>();
        var files = Directory.GetFiles(directoryPath);
        foreach (var file in files)
        {
            if (!file.EndsWith(".json_chart")) continue;
            try
            {
                var resumeInfo = (ResumeInfo)Function.Factory.ResumeInfoFactory.Get(File.ReadAllText(file));
                if (!degreeMap.TryAdd(resumeInfo.EduBG.Degree, 1))
                {
                    degreeMap[resumeInfo.EduBG.Degree]++;
                }
                foreach (var skill in resumeInfo.Skills)
                {
                    if (!skillMap.TryAdd(skill, 1))
                    {
                        skillMap[skill]++;
                    }
                }
                if (!ageMap.TryAdd(resumeInfo.BaseInfo.Age, 1))
                {
                    ageMap[resumeInfo.BaseInfo.Age]++;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

    private void MinimizeButton_Click(object sender, MouseButtonEventArgs e)
    {
        this.WindowState = WindowState.Minimized;
    }

    private void CloseButton_Click(object sender, MouseButtonEventArgs e)
    {
        this.Close();
    }
}
