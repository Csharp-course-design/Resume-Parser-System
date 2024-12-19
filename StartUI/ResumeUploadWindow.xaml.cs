using CsharpAPI;
using System;
using System.IO;
using System.Windows;

namespace StartUI
{
    public partial class ResumeUploadWindow : Window
    {
        private const string DestinationFolder = @"E:\GitHubDeskTop_\Resume-Parser-System\Info";

        public ResumeUploadWindow()
        {
            InitializeComponent();

            // 设置窗口位置为屏幕中央
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        // 点击按钮选择文件
        private void SelectFileButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog();
            if (dialog.ShowDialog() == true)
            {
                string selectedFile = dialog.FileName;
                SaveFileToDestination(selectedFile);
            }
        }

        // 拖动文件进入窗口时的处理
        private void Window_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effects = DragDropEffects.Copy; // 显示为可复制的图标
            }
            else
            {
                e.Effects = DragDropEffects.None; // 不允许拖放
            }
        }

        // 拖放文件释放时的处理
        private void Window_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                foreach (string file in files)
                {
                    SaveFileToDestination(file);
                }
            }
        }

        // 保存文件到指定路径
        private void SaveFileToDestination(string filePath)
        {
            try
            {
                if (!Directory.Exists(DestinationFolder))
                {
                    Directory.CreateDirectory(DestinationFolder); // 确保目标文件夹存在
                }

                string fileName = Path.GetFileName(filePath);
                string destinationPath = Path.Combine(DestinationFolder, fileName);

                if (File.Exists(destinationPath)) // 移除已存在文件的只读属性，确保可以覆盖
                {
                    FileInfo fileInfo = new FileInfo(destinationPath);
                    if (fileInfo.IsReadOnly)
                    {
                        fileInfo.IsReadOnly = false;
                    }
                }

                File.Copy(filePath, destinationPath, true); // 覆盖已存在的文件

                LinkToAPI api = new LinkToAPI();
                api.ResumeFile(Path.Combine(destinationPath));
                File.WriteAllText(Path.ChangeExtension(destinationPath, ".json"), api.GetJson());

                MessageBox.Show($"文件已成功上传到：{destinationPath}", "成功");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"上传文件时出错：{ex.Message}", "错误");
            }
        }
    }
}
