using CsharpAPI;
using OxyPlot;
using OxyPlot.Series;
using System.IO;
using System.Windows;

namespace ChartRender;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private string directoryPath = "E:\\GitHubDeskTop_\\Resume-Parser-System\\Info";

    private Dictionary<string, int> degreeMap;
    public MainWindow()
    {
        InitializeComponent();
        initFileData();

        var plotModel = new PlotModel
        {
            Title = "学历分布"
        };

        var pieSeries = new PieSeries
        {
            StrokeThickness = 2, // 扇区边框宽度
            Slices = degreeMap.Select((kv, index) =>
                new PieSlice(kv.Key, kv.Value) // 生成一个 PieSlice
                {
                    Fill = GetColorForDegree(kv.Key), // 根据学历选择填充颜色
                    IsExploded = false, // 是否突出显示
                }).ToList()
        };
        plotModel.Series.Add(pieSeries);
        plotView.Model = plotModel;
    }

    private static OxyColor GetColorForDegree(string degree)
    {
        return degree switch
        {
            "专科" => OxyColor.FromArgb(255, 255, 225, 104), // 浅黄到蓝绿色渐变
            "本科" => OxyColor.FromArgb(255, 212, 236, 89),
            "硕士" => OxyColor.FromArgb(255, 156, 220, 130),
            "博士" => OxyColor.FromArgb(255, 50, 211, 235),
            _ => OxyColors.Gray // 默认颜色
        };
    }

    private void initFileData()
    {
        degreeMap = new Dictionary<string, int>();
        var files = Directory.GetFiles(directoryPath);
        LinkToAPI api = new LinkToAPI();
        foreach (var file in files)
        {
            if (!file.EndsWith(".pdf")) continue;
            var resumeInfo = api.ResumeFile(Path.Combine(directoryPath, file));
            if (!degreeMap.TryAdd(resumeInfo.EduBG.Degree, 1))
            {
                degreeMap[resumeInfo.EduBG.Degree]++;
            }
        }
    }
}
