using Spire.PdfViewer.Forms;
using System;
using System.IO;
using System.Windows.Forms;
using MSWord = Microsoft.Office.Interop.Word;
//using Models;




namespace FileRender
{
    public partial class UserControl1 : UserControl
    {
        public UserControl1(string Path)
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

        //public ResumeFile File
        //{
        //    set
        //    {
        //        OpenFile(value);
        //    }
        //}

        public void OpenWord(string fileName, RichTextBox richTextBox)
        {
            MSWord.Application app = new MSWord.Application();//可以打开word
            MSWord.Document doc = null;      //需要记录打开的word



            object missing = System.Reflection.Missing.Value;
            object File = fileName;
            object readOnly = false;//不是只读
            object isVisible = true;

            object unknow = Type.Missing;

            try
            {
                doc = app.Documents.Open(ref File, ref missing, ref readOnly,
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

                if (app != null)
                {
                    app.Quit(ref missing, ref missing, ref missing);
                    app = null;
                }
            }
        }
        public void OpenFile(string file)
        {
            //获取文件后缀名，并转成小写
            string extension = Path.GetExtension(file).ToLower();
            try
            {
                string destinationPath = Path.Combine(Application.StartupPath, Path.GetFileName(file)); // 保存到程序启动目录
                File.Copy(file, destinationPath, true);
                if (extension == ".pdf")
                {
                    PdfViewer viewer = new PdfViewer();
                    viewer.LoadFromFile(file);
                    viewer.Dock = DockStyle.Fill;

                }
                else if (extension == ".doc" || extension == ".docx")
                {
                    RichTextBox richTextBox = new RichTextBox();
                    richTextBox.Dock = DockStyle.Fill;
                    OpenWord(file, richTextBox);
                }
                else
                {
                    MessageBox.Show($"暂不支持此文件类型：{extension}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载文件失败：{file}\n错误信息：{ex.Message}");
            }
            //panel1.Show();
        }


        //public void OpenFile(ResumeFil file)
        //{

        //}
    }
}
