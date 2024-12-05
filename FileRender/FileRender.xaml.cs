using Models;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Xps.Packaging;
using Aspose.Words;
using System.IO;
using System.Windows.Documents;




namespace FileRender
{
    /// <summary>
    /// FileRender.xaml 的交互逻辑
    /// </summary>
    public partial class FileRender : UserControl
    {
        public FileRender()
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

                // 将文档保存为 XAML 流
                using (MemoryStream xamlStream = new MemoryStream())
                {
                    doc.Save(xamlStream, SaveFormat.XamlFlow);
                    xamlStream.Position = 0;

                    // 将 XAML 流加载到 FlowDocument
                    FlowDocument flowDocument = System.Windows.Markup.XamlReader.Load(xamlStream) as FlowDocument;

                    // 设置到 RichTextBox
                    richTextBox.Document = flowDocument;
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

        private void OpenFile(ResumeFile value)
        {
            throw new NotImplementedException();
        }
        private void OpenFile(string value)
        {
            RichTextBox richTextBox = new RichTextBox();
            OpenWord(value, richTextBox);

            // 设置富文本控件显示样式
            richTextBox.HorizontalAlignment = HorizontalAlignment.Stretch;
            richTextBox.VerticalAlignment = VerticalAlignment.Stretch;
            richTextBox.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;

            // 将 RichTextBox 添加到 Grid 中
            grid.Children.Add(richTextBox);

        }

    }
}
