using Aspose.Words;
using Function;
using Microsoft.Web.WebView2.Wpf;
using Models;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace FileRender
{
    /// <summary>
    /// SingleFileRender.xaml 的交互逻辑
    /// </summary>
    public partial class SingleFileRender : UserControl
    {

        /// <summary>
        /// 统一读取
        /// </summary>
        //XpsDocument readerDoc;

        /// <summary>
        /// 转化临时显示文件
        /// </summary>
        public string tempPdfPreAddress = Environment.CurrentDirectory + "\\tempPdfPre\\";


        public SingleFileRender()
        {
            InitializeComponent();
        }

        public string Filepath
        {
            set
            {
                OpenFile(value);
            }
        }

        public ResumeFile FileObj
        {
            set
            {
                OpenFile(value);
            }
        }


        public void OpenWord(string fileName, RichTextBox richTextBox)
        {
            try
            {
                // 加载 Word 文档
                Document doc = new Document(fileName);

                // 将文档保存为 RTF 格式
                using (MemoryStream rtfStream = new MemoryStream())
                {
                    doc.Save(rtfStream, SaveFormat.Rtf);
                    rtfStream.Position = 0;

                    // 将 RTF 数据加载到 RichTextBox
                    richTextBox.Selection.Load(rtfStream, DataFormats.Rtf);
                }
            }
            catch (IOException ioEx)
            {
                MessageBox.Show($"无法打开文件: {ioEx.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"发生错误: {ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void OpenFile(ResumeFile value)
        {
            string projectPath = AppDomain.CurrentDomain.BaseDirectory;
            string filePath = System.IO.Path.Combine(projectPath, value.Filename);

            try
            {
                // 将 Base64 字符串转换为文件
                Base64Helper.Base64StringToFile(value.Base64Data, filePath);

                // 打开文件
                OpenFile(filePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"文件加载失败: {ex.Message}");
            }
        }

        public void OpenFile(string value)
        {
            if (value.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
            {
                var webView = new WebView2();
                webView.Source = new Uri(value);
                FileGrid.Children.Add(webView);
            }
            else if (value.EndsWith(".docx", StringComparison.OrdinalIgnoreCase) ||
                     value.EndsWith(".doc", StringComparison.OrdinalIgnoreCase))
            {
                try
                {
                    RichTextBox richTextBox = new RichTextBox();
                    OpenWord(value, richTextBox);

                    // 设置富文本控件显示样式
                    richTextBox.HorizontalAlignment = HorizontalAlignment.Stretch;
                    richTextBox.VerticalAlignment = VerticalAlignment.Stretch;
                    richTextBox.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;

                    // 将 RichTextBox 添加到 Grid 中
                    FileGrid.Children.Add(richTextBox);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"加载失败: {ex.Message}");
                }
            }
        }
    }
}
