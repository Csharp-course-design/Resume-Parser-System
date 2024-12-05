using Models;
using System.Windows;
using System.Windows.Controls;
using MOIW = Microsoft.Office.Interop.Word;

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

        Microsoft.Office.Interop.Word.Application wordOpener = new Microsoft.Office.Interop.Word.Application();

        public Microsoft.Office.Interop.Word.Application WordOpener
        {
            set
            {
                wordOpener = value;
            }
            get
            {
                return wordOpener;
            }
        }


        public string Filepath
        {
            set
            {
                OpenFile(value);
            }
        }

        public ResumeFile File
        {
            set
            {
                OpenFile(value);
            }
        }

        private void OpenWord(string word, RichTextBox richTextBox)
        {
            MOIW.Document doc = null;      //需要记录打开的word

            object missing = System.Reflection.Missing.Value;
            object File = word;
            object readOnly = false;//不是只读
            object isVisible = true;

            try
            {
                doc = wordOpener.Documents.Open(ref File, ref missing, ref readOnly,
                 ref missing, ref missing, ref missing, ref missing, ref missing,
                 ref missing, ref missing, ref missing, ref isVisible, ref missing,
                 ref missing, ref missing, ref missing);

                doc.ActiveWindow.Selection.WholeStory();//全选word文档中的数据
                doc.ActiveWindow.Selection.Copy();//复制数据到剪切板
                richTextBox.Paste();//richTextBox粘贴数据
                                    //richTextBox1.Text = doc.Content.Text;//显示无格式数据
            }
            finally
            {
                if (doc != null)
                {
                    doc.Close(ref missing, ref missing, ref missing);
                    doc = null;
                }

                if (wordOpener != null)
                {
                    wordOpener.Quit(ref missing, ref missing, ref missing);
                    wordOpener = null;
                }
            }
        }

        private void OpenFile(ResumeFile value)
        {
            throw new NotImplementedException();
        }
        private void OpenFile(string value)
        {
            RichTextBox richTextBox = new RichTextBox();
            richTextBox.HorizontalAlignment = HorizontalAlignment.Stretch;
            richTextBox.VerticalAlignment = VerticalAlignment.Stretch;
            OpenWord(value, richTextBox);
            this.AddVisualChild(richTextBox);
        }
    }
}
