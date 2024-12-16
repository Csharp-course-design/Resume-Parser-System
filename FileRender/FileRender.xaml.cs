using Aspose.Words;
using Models;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using Function.Factory;
using System.Security.Permissions;



namespace FileRender
{
    /// <summary>
    /// </summary>
    public partial class FileRender : UserControl
    {
        public FileRender()
        {
            InitializeComponent();
        }

        public void ClearAll()
        {
            tagControl.Items.Clear();
        }

        public void ShowFiles(List<ResumeFile> Files)
        {
            // 假设tagControl已经在XAML中定义
            foreach (var item in Files)
            {
                // 创建 TabItem
                TabItem tabItem = new TabItem
                {
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    VerticalAlignment = VerticalAlignment.Stretch,
                    Header = item // 设置 TabItem 的标题为文件路径
                };

                // 创建自定义控件来显示文件内容
                SingleFileRender singleFileRender = new SingleFileRender
                {
                    VerticalAlignment = VerticalAlignment.Stretch,
                    HorizontalAlignment = HorizontalAlignment.Stretch
                };

                try
                {
                    // 假设 OpenFile 方法用于加载文件内容
                    singleFileRender.OpenFile(item);
                }
                catch (Exception ex)
                {
                    // 处理文件加载错误
                    MessageBox.Show($"文件加载失败: {ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // 将控件设置为 TabItem 的内容
                tabItem.Content = singleFileRender;

                // 将 TabItem 添加到 TabControl 中
                tagControl.Items.Add(tabItem);
            }
        }
        public void ShowFiles(List<string> files)
        {

            // 循环处理每个筛选后的文件
            foreach (var file in files)
            {
                // 创建 TabItem
                TabItem tabItem = new TabItem
                {
                    Header = Path.GetFileName(file)  // 设置 TabItem 的标题为文件名
                };

                // 创建一个用于展示文件内容的控件（例如 TextBlock 或自定义控件）
                SingleFileRender singleFileRender = new SingleFileRender
                {
                    VerticalAlignment = VerticalAlignment.Stretch,
                    HorizontalAlignment = HorizontalAlignment.Stretch
                };

                try
                {
                    // 假设 SingleFileRender 有一个 OpenFile 方法用于打开并显示文件内容
                    singleFileRender.OpenFile(file);
                }
                catch (Exception ex)
                {
                    // 处理文件打开异常
                    MessageBox.Show($"无法打开文件 {Path.GetFileName(file)}: {ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                // 将控件添加到 TabItem 内容中
                tabItem.Content = singleFileRender;

                // 将 TabItem 添加到 TabControl 中
                tagControl.Items.Add(tabItem);
            }
        }


    }
}
