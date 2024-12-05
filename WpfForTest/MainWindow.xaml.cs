using System;
using System.Windows;
using Microsoft.Office.Interop.Word;
using Application = Microsoft.Office.Interop.Word.Application;
using Window = System.Windows.Window;

namespace WpfWordViewer
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Word 文件路径
            string filePath = @"C:\path\to\your\document.docx";

            // 打开并显示 Word 文件内容
            DisplayWordContent(filePath);
        }

        private void DisplayWordContent(string filePath)
        {
            Application wordApp = new Application();
            Document wordDoc = null;

            try
            {
                wordApp.Visible = false;  // 不显示 Word 应用
                wordDoc = wordApp.Documents.Open(filePath);

                // 将 Word 文件的内容转换为 HTML 格式
                string tempHtmlFile = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "tempWord.html");
                wordDoc.SaveAs2(tempHtmlFile, WdSaveFormat.wdFormatHTML);

                // 使用 WebBrowser 控件加载 HTML 内容
                wordWebViewer.Navigate(tempHtmlFile);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening Word file: {ex.Message}");
            }
            finally
            {
                wordDoc?.Close();
                wordApp.Quit();
            }
        }
    }
}
