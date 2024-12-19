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
using static System.Runtime.InteropServices.JavaScript.JSType;
using YourNamespace;
using JiebaNet.Segmenter.Common;
using MySqlX.XDevAPI.Common;
using Models.ResumeInfo;

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

                // 显示文件路径（也可以替换为其他逻辑）
                // MessageBox.Show($"您选择的文件路径是：{filePath}", "文件路径", MessageBoxButton.OK, MessageBoxImage.Information);


                // 获取文件的绝对路径
                string filePath = openFileDialog.FileName;

                //// 定义保存目录，文件名，保存文件路径
                //string saveDirectory = "E:\\GitHubDeskTop_\\Resume-Parser-System\\Info";
                //string fileName = System.IO.Path.GetFileNameWithoutExtension(filePath) + "_content.txt";
                //string savePath = System.IO.Path.Combine(saveDirectory, fileName);

                //// 如果文件存在，直接读取内容
                //if (System.IO.File.Exists(savePath))
                //{
                //    string existingContent = System.IO.File.ReadAllText(savePath);
                //    InputArea.Text = existingContent;
                //}

                // 创建 LinkToAPI 实例
                LinkToAPI api = new LinkToAPI();

                try
                {
                    // 调用 ResumeFile 方法解析文件内容
                    var API_RETURN = api.ResumeFile(filePath);
                    string result = API_RETURN?.ToString();

                    if (string.IsNullOrEmpty(result))
                    {
                        throw new Exception("解析结果为空。");
                    }

                    // 显示解析结果到 InputArea
                    InputArea.Text = result;

                    // 定义保存目录，文件名，保存文件路径
                    string saveDirectory = "E:\\GitHubDeskTop_\\Resume-Parser-System\\Info";
                    string fileName = System.IO.Path.GetFileNameWithoutExtension(filePath) + "_content.txt";
                    string savePath = System.IO.Path.Combine(saveDirectory, fileName);

                    // 创建保存目录（如果不存在）
                    if (!System.IO.Directory.Exists(saveDirectory))
                    {
                        System.IO.Directory.CreateDirectory(saveDirectory);
                    }

                    // 将解析结果写入文件
                    System.IO.File.WriteAllText(savePath, result);

                    //MessageBox.Show("格式转换完成");


                    var resJson = (new JsonFactory()).Content(API_RETURN);
                    //fileName = System.IO.Path.GetFileNameWithoutExtension(filePath) + "_json.txt";
                    fileName = System.IO.Path.GetFileNameWithoutExtension(filePath) + ".json";
                    savePath = System.IO.Path.Combine(saveDirectory, fileName);
                    System.IO.File.WriteAllText(savePath, resJson);


                    var resCSV = (new CSVFactory()).Content(API_RETURN);
                    //fileName = System.IO.Path.GetFileNameWithoutExtension(filePath) + "_csv.txt";
                    fileName = System.IO.Path.GetFileNameWithoutExtension(filePath) + ".csv";
                    savePath = System.IO.Path.Combine(saveDirectory, fileName);
                    System.IO.File.WriteAllText(savePath, resCSV);


                    var resXML = (new XMLFactory()).Content(API_RETURN);
                    //fileName = System.IO.Path.GetFileNameWithoutExtension(filePath) + "_xml.txt";
                    fileName = System.IO.Path.GetFileNameWithoutExtension(filePath) + ".xml";
                    savePath = System.IO.Path.Combine(saveDirectory, fileName);
                    System.IO.File.WriteAllText(savePath, resXML);


                    // 提示保存成功
                    //MessageBox.Show($"解析结果已保存到: {savePath}", "保存成功", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    // 捕获异常并提示
                    MessageBox.Show($"发生错误: {ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                //// 解析返回结果
                //if (api.ResumeFile(filePath).ToString() == string.Empty)
                //{
                //    throw new Exception("Null ");
                //}
                //InputArea.Text = api.ResumeFile(filePath).ToString();

                //// 定义保存目录
                //string saveDirectory = "E:\\GitHubDeskTop_\\Resume-Parser-System\\Info";

                //// 创建保存目录（如果不存在）
                //if (!System.IO.Directory.Exists(saveDirectory))
                //{
                //    System.IO.Directory.CreateDirectory(saveDirectory);
                //}

                //// 定义保存文件路径
                //string fileName = System.IO.Path.GetFileNameWithoutExtension(filePath) + "_content.txt";
                //string savePath = System.IO.Path.Combine(saveDirectory, fileName);

                //// 将解析结果写入文件
                //System.IO.File.WriteAllText(savePath, result);
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


            InputArea.Text = string.Empty;
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


                // 定义保存目录，文件名，保存文件路径 ：为了检测是否已经存在解析结果
                string saveDirectory = "E:\\GitHubDeskTop_\\Resume-Parser-System\\Info";
                string fileName = System.IO.Path.GetFileNameWithoutExtension(filePath) + "_json.txt";
                string savePath = System.IO.Path.Combine(saveDirectory, fileName);

                // 如果文件存在，直接读取内容
                if (System.IO.File.Exists(savePath))
                {
                    string existingContent = System.IO.File.ReadAllText(savePath);
                    InputArea.Text = existingContent;
                    return;
                }

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



                // 定义保存目录，文件名，保存文件路径 ：为了检测是否已经存在解析结果
                string saveDirectory = "E:\\GitHubDeskTop_\\Resume-Parser-System\\Info";
                string fileName = System.IO.Path.GetFileNameWithoutExtension(filePath) + "_csv.txt";
                string savePath = System.IO.Path.Combine(saveDirectory, fileName);

                // 如果文件存在，直接读取内容
                if (System.IO.File.Exists(savePath))
                {
                    string existingContent = System.IO.File.ReadAllText(savePath);
                    InputArea.Text = existingContent;

                    return;
                }

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

                // 定义保存目录，文件名，保存文件路径 ：为了检测是否已经存在解析结果
                string saveDirectory = "E:\\GitHubDeskTop_\\Resume-Parser-System\\Info";
                string fileName = System.IO.Path.GetFileNameWithoutExtension(filePath) + "_xml.txt";
                string savePath = System.IO.Path.Combine(saveDirectory, fileName);

                // 如果文件存在，直接读取内容
                if (System.IO.File.Exists(savePath))
                {
                    string existingContent = System.IO.File.ReadAllText(savePath);
                    InputArea.Text = existingContent;
                    return;
                }

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
            DocDisplay.ClearAll();
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
            // 弹出输入框让用户输入日期
            string userInput = ShowInputDialog("请输入导入日期（格式：yyyy-MM-dd）",
                DateTime.Now.ToString("yyyy-MM-dd"));

            if (!string.IsNullOrEmpty(userInput))
            {
                // 日期格式检查
                DateTime parsedDate;
                bool isValidDate = DateTime.TryParseExact(userInput, "yyyy-MM-dd",
                                                          System.Globalization.CultureInfo.InvariantCulture,
                                                          System.Globalization.DateTimeStyles.None,
                                                          out parsedDate);

                if (isValidDate)
                {
                    // 如果日期格式正确，根据用户输入的日期进行查询
                    var files = GetFilesByDate(parsedDate.ToString("yyyy-MM-dd"));

                    // 显示查询结果
                    DisplayFiles(files);
                }
                else
                {
                    // 如果日期格式无效，提示用户重新输入
                    MessageBox.Show("输入的日期格式无效，请确保格式为 yyyy-MM-dd", "日期格式错误", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("输入的日期为空", "日期格式错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SearchByName_Click(object sender, RoutedEventArgs e)
        {
            // 弹出输入框让用户输入文件名称
            string userInput = ShowInputDialog("请输入文件名称");

            if (!string.IsNullOrEmpty(userInput))
            {
                // 根据用户输入的名称进行查询
                var files = GetFilesByName(userInput);

                // 显示查询结果
                DisplayFiles(files);
            }
        }

        // 弹出输入框让用户输入
        private string ShowInputDialog(string prompt, string defaultContent = "")
        {
            // 创建 InputDialog 对话框实例并传入提示信息
            var inputDialog = new InputDialog(prompt, defaultContent);
            bool? result = inputDialog.ShowDialog();  // 显示对话框并等待用户操作

            if (result == true)  // 如果用户点击了 OK
            {
                //MessageBox.Show(inputDialog.UserInput);
                return inputDialog.UserInput;  // 返回用户输入的信息
            }

            return string.Empty;  // 如果用户点击了 Cancel 或关闭了对话框
        }

        // 根据日期获取文件
        private List<string> GetFilesByDate(string date)
        {
            List<string> matchingFiles = new List<string>();

            // 在指定目录中搜索所有文件，假设路径是 "E:\GitHubDeskTop_\Resume-Parser-System\Info"
            string directoryPath = @"E:\GitHubDeskTop_\Resume-Parser-System\Info";
            var files = Directory.GetFiles(directoryPath);

            foreach (var file in files)
            {
                var fileInfo = new FileInfo(file);

                // 假设文件的创建日期是导入日期
                if (fileInfo.CreationTime.ToString("yyyy-MM-dd") == date)
                {
                    matchingFiles.Add(file);
                }
            }

            return matchingFiles;
        }

        // 根据文件名获取文件
        private List<string> GetFilesByName(string fileName)
        {
            List<string> matchingFiles = new List<string>();

            // 在指定目录中搜索所有文件，假设路径是 "E:\GitHubDeskTop_\Resume-Parser-System\Info"
            string directoryPath = @"E:\GitHubDeskTop_\Resume-Parser-System\Info";
            var files = Directory.GetFiles(directoryPath);

            foreach (var file in files)
            {
                if (file.Contains(fileName)) // 根据文件名进行模糊查询
                {
                    matchingFiles.Add(file);
                }
            }

            return matchingFiles;
        }

        // 显示查询结果
        private void DisplayFiles(List<string> files)
        {
            FilesListBox.Items.Clear();  // 清空当前项

            List<string> nlis = new List<string>();

            string directoryPath = @"E:\GitHubDeskTop_\Resume-Parser-System\Info";
            foreach (var file in files)
            {
                string filePath = directoryPath + file;
                //MessageBox.Show(file);
                if(file.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase) || file.EndsWith(".txt", StringComparison.OrdinalIgnoreCase) || file.EndsWith(".doc", StringComparison.OrdinalIgnoreCase)
                    || file.EndsWith(".docx", StringComparison.OrdinalIgnoreCase))
                {
                    nlis.Add(file);
                }

            }

            if(nlis.Count == 0)
            {
                MessageBox.Show("查询不到对应结果");
                return;
            }

            DocDisplay.ShowFiles(nlis);
            DocDisplay.Visibility = Visibility.Visible;
            DocDisplayScroll.Visibility = Visibility.Visible;
            DocDisplayIcon.Visibility = Visibility.Visible;
            DocDisplayButton.Visibility = Visibility.Visible;
        }

        private void TestClick(object sender, RoutedEventArgs e)
        {
            List<string> Test = new List<string> ();
            Test.Add("C:\\Users\\95432\\Desktop\\闫振斌.pdf");
            DocDisplay.ShowFiles(Test);
            DocDisplay.Visibility = Visibility.Visible;
            DocDisplayScroll.Visibility = Visibility.Visible;
            DocDisplayIcon.Visibility = Visibility.Visible;
            DocDisplayButton.Visibility = Visibility.Visible;
        }

        private void Button_Click_Format_Conversion(object sender, RoutedEventArgs e)
        {
            // 创建文件选择对话框
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                Title = "选择一个文件",
                Filter = "支持的文件 (*.json;*.csv;*.xml)|*.json;*.csv;*.xml|所有文件 (*.*)|*.*",
                Multiselect = false // 限制只选择一个文件
            };

            // 显示对话框并判断用户是否选择了文件
            if (openFileDialog.ShowDialog() == true)
            {
                // 获取文件的绝对路径
                string filePath = openFileDialog.FileName;
                string fileExtension = System.IO.Path.GetExtension(filePath).ToLower();

                // 检查文件类型是否合法
                if (fileExtension != ".json" && fileExtension != ".csv" && fileExtension != ".xml")
                {
                    MessageBox.Show("请选择有效的文件类型（.json, .csv, .xml, .txt）", "无效文件类型", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // 提供目标格式选择
                MessageBoxResult result = MessageBox.Show(
                    "请选择目标格式:\nYes -> 转换为 JSON\nNo -> 转换为 CSV\nCancel -> 转换为 XML",
                    "目标格式选择",
                    MessageBoxButton.YesNoCancel,
                    MessageBoxImage.Question
                );

                string targetExtension = string.Empty;
                string convertedContent = string.Empty;

                switch (result)
                {
                    case MessageBoxResult.Yes:
                        targetExtension = ".json";
                        break;
                    case MessageBoxResult.No:
                        targetExtension = ".csv";
                        break;
                    case MessageBoxResult.Cancel:
                        targetExtension = ".xml";
                        break;
                    default:
                        MessageBox.Show("未选择目标格式。", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                }

                // 定义目标文件路径
                string targetFileName = System.IO.Path.GetFileNameWithoutExtension(filePath) + targetExtension;
                string targetFilePath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(filePath), targetFileName);

                // 防止重复解析
                if (File.Exists(targetFilePath))
                {
                    MessageBox.Show($"文件已成功转换为 {targetExtension} 格式，并保存到: {targetFilePath}", "转换成功", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                //// 定义保存目录，文件名，保存文件路径 ：为了检测是否已经存在解析结果
                //string saveDirectory = "E:\\GitHubDeskTop_\\Resume-Parser-System\\Info";
                //string fileName = System.IO.Path.GetFileNameWithoutExtension(filePath) + "_xml.txt";
                //string savePath = System.IO.Path.Combine(saveDirectory, fileName);

                //// 如果文件存在，直接读取内容
                //if (System.IO.File.Exists(savePath))
                //{
                //    string existingContent = System.IO.File.ReadAllText(savePath);
                //    InputArea.Text = existingContent;
                //    return;
                //}



                try
                {
                    // 根据文件类型进行内容转换
                    string fileContent = System.IO.File.ReadAllText(filePath);

                    if (fileExtension == ".json")
                    {
                        if (targetExtension == ".csv")
                        {
                            convertedContent = JsonToCsv(fileContent);
                        }
                        else if (targetExtension == ".xml")
                        {
                            convertedContent = JsonToXml(fileContent);
                        }
                        else if(targetExtension == ".json")
                        {
                            MessageBox.Show("JSON文件已存在");
                            return;
;                        }
                    }
                    else if (fileExtension == ".csv")
                    {
                        if (targetExtension == ".json")
                        {
                            convertedContent = CsvToJson(fileContent);
                        }
                        else if (targetExtension == ".xml")
                        {
                            convertedContent = CsvToXml(fileContent);
                        }
                        else if(targetExtension == ".csv")
                        {
                            MessageBox.Show("CSV文件已存在");
                            return;
                        }
                    }
                    else if (fileExtension == ".xml")
                    {
                        if (targetExtension == ".json")
                        {
                            convertedContent = XmlToJson(fileContent);
                        }
                        else if (targetExtension == ".csv")
                        {
                            convertedContent = XmlToCsv(fileContent);
                        }
                        else if(targetExtension == ".xml")
                        {
                            MessageBox.Show("XML文件已存在");
                            return;
                        }
                    }

                    // 将转换结果写入目标文件
                    System.IO.File.WriteAllText(targetFilePath, convertedContent);

                    MessageBox.Show($"文件已成功转换为 {targetExtension} 格式，并保存到: {targetFilePath}", "转换成功", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"转换失败: {ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                // 用户取消选择
                MessageBox.Show("未选择任何文件。", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // 以下是示例转换方法的占位实现，可根据实际需求替换
        private string JsonToCsv(string jsonContent)
        {
            // 实现 JSON 转 CSV 的逻辑
            var obj = (new JsonFactory()).Model(jsonContent);
            return (new CSVFactory()).Content(obj);
        }

        private string JsonToXml(string jsonContent)
        {
            // 实现 JSON 转 XML 的逻辑
            var obj = (new JsonFactory()).Model(jsonContent);
            return (new XMLFactory()).Content(obj);
        }

        private string CsvToJson(string csvContent)
        {
            var obj = (new CSVFactory()).Model(csvContent);
            // 实现 CSV 转 JSON 的逻辑
            return (new JsonFactory()).Content(obj);
        }

        private string CsvToXml(string csvContent)
        {
            // 实现 CSV 转 XML 的逻辑
            var obj = (new CSVFactory()).Model(csvContent);
            return (new XMLFactory()).Content(obj);
        }

        private string XmlToJson(string xmlContent)
        {
            // 实现 XML 转 JSON 的逻辑
            var obj = (new XMLFactory().Model(xmlContent));
            return (new JsonFactory().Content(obj));
        }

        private string XmlToCsv(string xmlContent)
        {
            // 实现 XML 转 CSV 的逻辑
            var obj = (new XMLFactory().Model(xmlContent));
            return (new CSVFactory()).Content(obj);
        }

        private void Button_Click_Analysis(object sender, RoutedEventArgs e)
        {
            new ChartRender.MainWindow().Show();
        }
    }
}

