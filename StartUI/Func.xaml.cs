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
 
using Microsoft.VisualBasic;
using CsharpAPI;
using Function.TransFactory;
using Google.Protobuf.WellKnownTypes;
using System.IO;

namespace StartUI
{
    /// <summary>
    /// Func.xaml 的交互逻辑
    /// </summary>
    public partial class Func : Window
    {
        public Func()
        {
            InitializeComponent();
        }

        private void Button_Click_Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_ChangeSize(object sender, RoutedEventArgs e)
        {
            if(this.WindowState == WindowState.Maximized)
            {
                Max.Visibility = Visibility.Visible;
                Normal.Visibility = Visibility.Collapsed;
                this.WindowState = WindowState.Normal;
            }

            else
            {
                Max.Visibility = Visibility.Collapsed;
                Normal.Visibility = Visibility.Visible;
                this.WindowState = WindowState.Maximized;
            }
        }

        private void Button_Click_Min(object sender, RoutedEventArgs e)
        {
            this.WindowState= WindowState.Minimized;
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void MenuItem_Click_Resume_Analysis(object sender, RoutedEventArgs e)
        {
            InputArea.Visibility = Visibility.Visible;
            InputAreaScrollViewer.Visibility = Visibility.Visible;
            closeInputAreaButton.Visibility = Visibility.Visible;
            closeInputAreaIcon.Visibility = Visibility.Visible;
            

            // 创建文件选择对话框
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                Title = "选择一个文件",
                Filter = "支持的文件 (*.pdf;*.docx;*.doc;*.txt)|*.pdf;*.docx;*.doc;*.txt|所有文件 (*.*)|*.*",
                Multiselect = false // 限制只选择一个文件
            };

            // 显示对话框并判断用户是否选择了文件
            if (openFileDialog.ShowDialog() == true)
            {
                // 获取文件的绝对路径
                string filePath = openFileDialog.FileName;

                // 显示文件路径（也可以替换为其他逻辑）
                // MessageBox.Show($"您选择的文件路径是：{filePath}", "文件路径", MessageBoxButton.OK, MessageBoxImage.Information);

                // 创建 LinkToAPI 实例
                LinkToAPI api = new LinkToAPI();

                // 假设 API_Json 会包含返回的 JSON 数据
                if (api.ResumeFile(filePath).ToString() == String.Empty)
                {
                    throw new Exception("Null ");
                }
                InputArea.Text = api.ResumeFile(filePath).ToString();
            }
            else
            {
                // 用户取消选择
                MessageBox.Show("未选择任何文件。", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void closeInputAreaButton_Click(object sender, RoutedEventArgs e)
        {
            InputArea.Visibility = Visibility.Collapsed;
            InputAreaScrollViewer.Visibility = Visibility.Collapsed;
            closeInputAreaButton.Visibility = Visibility.Collapsed; 
            closeInputAreaIcon.Visibility = Visibility.Collapsed;


            InputArea.Text = String.Empty;
        }
         
        private void MenuItem_Click_JSON(object sender, RoutedEventArgs e)
        {
            InputArea.Visibility = Visibility.Visible;
            InputAreaScrollViewer.Visibility = Visibility.Visible;
            closeInputAreaButton.Visibility = Visibility.Visible;
            closeInputAreaIcon.Visibility = Visibility.Visible;


            // 创建文件选择对话框
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                Title = "选择一个文件",
                Filter = "支持的文件 (*.pdf;*.docx;*.doc;*.txt)|*.pdf;*.docx;*.doc;*.txt|所有文件 (*.*)|*.*",
                Multiselect = false // 限制只选择一个文件
            };

            // 显示对话框并判断用户是否选择了文件
            if (openFileDialog.ShowDialog() == true)
            {
                // 获取文件的绝对路径
                string filePath = openFileDialog.FileName;

                // 显示文件路径（也可以替换为其他逻辑）
                // MessageBox.Show($"您选择的文件路径是：{filePath}", "文件路径", MessageBoxButton.OK, MessageBoxImage.Information);

                // 创建 LinkToAPI 实例
                LinkToAPI api = new LinkToAPI();

                // 假设 API_Json 会包含返回的 JSON 数据
                // InputArea.Text = api.ResumeFile(filePath).ToString();
                var TestObject = api.ResumeFile(filePath);
                var res = (new JsonFactory()).Content(TestObject);
                InputArea.Text = res;
            }
            else
            {
                // 用户取消选择
                MessageBox.Show("未选择任何文件。", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void MenuItem_Click_CSV(object sender, RoutedEventArgs e)
        {
            InputArea.Visibility = Visibility.Visible;
            InputAreaScrollViewer.Visibility = Visibility.Visible;
            closeInputAreaButton.Visibility = Visibility.Visible;
            closeInputAreaIcon.Visibility = Visibility.Visible;


            // 创建文件选择对话框
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                Title = "选择一个文件",
                Filter = "支持的文件 (*.pdf;*.docx;*.doc;*.txt)|*.pdf;*.docx;*.doc;*.txt|所有文件 (*.*)|*.*",
                Multiselect = false // 限制只选择一个文件
            };

            // 显示对话框并判断用户是否选择了文件
            if (openFileDialog.ShowDialog() == true)
            {
                // 获取文件的绝对路径
                string filePath = openFileDialog.FileName;

                // 显示文件路径（也可以替换为其他逻辑）
                // MessageBox.Show($"您选择的文件路径是：{filePath}", "文件路径", MessageBoxButton.OK, MessageBoxImage.Information);

                // 创建 LinkToAPI 实例
                LinkToAPI api = new LinkToAPI();

                // 假设 API_Json 会包含返回的 JSON 数据
                // InputArea.Text = api.ResumeFile(filePath).ToString();
                var TestObject = api.ResumeFile(filePath);
                var res = (new CSVFactory()).Content(TestObject);
                InputArea.Text = res;
            }
            else
            {
                // 用户取消选择
                MessageBox.Show("未选择任何文件。", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void MenuItem_Click_XML(object sender, RoutedEventArgs e)
        {
            InputArea.Visibility = Visibility.Visible;
            InputAreaScrollViewer.Visibility = Visibility.Visible;
            closeInputAreaButton.Visibility = Visibility.Visible;
            closeInputAreaIcon.Visibility = Visibility.Visible;


            // 创建文件选择对话框
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                Title = "选择一个文件",
                Filter = "支持的文件 (*.pdf;*.docx;*.doc;*.txt)|*.pdf;*.docx;*.doc;*.txt|所有文件 (*.*)|*.*",
                Multiselect = false // 限制只选择一个文件
            };

            // 显示对话框并判断用户是否选择了文件
            if (openFileDialog.ShowDialog() == true)
            {
                // 获取文件的绝对路径
                string filePath = openFileDialog.FileName;

                // 显示文件路径（也可以替换为其他逻辑）
                // MessageBox.Show($"您选择的文件路径是：{filePath}", "文件路径", MessageBoxButton.OK, MessageBoxImage.Information);

                // 创建 LinkToAPI 实例
                LinkToAPI api = new LinkToAPI();

                // 假设 API_Json 会包含返回的 JSON 数据
                // InputArea.Text = api.ResumeFile(filePath).ToString();
                var TestObject = api.ResumeFile(filePath);
                var res = (new XMLFactory()).Content(TestObject);
                InputArea.Text = res;
            }
            else
            {
                // 用户取消选择
                MessageBox.Show("未选择任何文件。", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


        private void DragEnterFunc(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effects = DragDropEffects.Copy;
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }
        }

        private void DragDropFunc(object sender, DragEventArgs e)
        {

            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            DocDisplay.ShowFiles(new List<string>(files));
            DocDisplay.Visibility = Visibility.Visible;
            DocDisplayScroll.Visibility = Visibility.Visible;
            DocDisplayIcon.Visibility = Visibility.Visible;
            DocDisplayButton.Visibility = Visibility.Visible;
        }

        private void Button_Click_AddResumeClose(object sender, RoutedEventArgs e)
        {
            DocDisplay.Visibility = Visibility.Collapsed;
            DocDisplayScroll.Visibility = Visibility.Collapsed;
            DocDisplayIcon.Visibility = Visibility.Collapsed;
            DocDisplayButton.Visibility = Visibility.Collapsed;
        }

        private void OpenResumeUploadWindow(object sender, RoutedEventArgs e)
        {
            // 打开新增简历窗口
            ResumeUploadWindow resumeUploadWindow = new ResumeUploadWindow();
            resumeUploadWindow.ShowDialog(); // 以对话框方式打开，阻塞当前窗口
        }

        private void SearchByDate_Click(object sender, RoutedEventArgs e)
        {
            string directoryPath = @"E:\GitHubDeskTop_\Resume-Parser-System\Info";

            // 假设用户选择了开始日期和结束日期
            DateTime startDate = new DateTime(2023, 1, 1);  // 示例起始日期
            DateTime endDate = new DateTime(2023, 12, 31);  // 示例结束日期

            // 获取指定目录下的所有文件
            string[] files = Directory.GetFiles(directoryPath);

            // 按日期筛选文件
            var filteredFiles = files.Where(file =>
            {
                DateTime creationDate = File.GetCreationTime(file);  // 获取文件创建日期
                return creationDate >= startDate && creationDate <= endDate;
            }).ToList();

            // 显示筛选的文件（可以将文件名或文件内容展示在界面上）
            DisplayFiles(filteredFiles);
        }

        private void SearchByName_Click(object sender, RoutedEventArgs e)
        {
            string directoryPath = @"E:\GitHubDeskTop_\Resume-Parser-System\Info";

            // 假设用户输入了一个查询关键字（可以通过文本框获取）
            string searchKeyword = "John";  // 示例关键字

            // 获取指定目录下的所有文件
            string[] files = Directory.GetFiles(directoryPath);

            // 按文件名筛选
            var filteredFiles = files.Where(file =>
            {
                string fileName = Path.GetFileName(file);  // 获取文件名
                return fileName.Contains(searchKeyword, StringComparison.OrdinalIgnoreCase);  // 检查文件名是否包含关键字
            }).ToList();

            // 显示筛选的文件（可以将文件名或文件内容展示在界面上）
            DisplayFiles(filteredFiles);
        }

        private void DisplayFiles(List<string> files)
        {
            FilesListBox.Items.Clear();  // 清空当前项
            foreach (var file in files)
            {
                FilesListBox.Items.Add(Path.GetFileName(file));  // 只显示文件名
            }
        }

    }
}

