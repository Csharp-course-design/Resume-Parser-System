using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office;
using MSWord = Microsoft.Office.Interop.Word;
using Microsoft.Office.Interop.Word;
using Spire.PdfViewer.Forms;
using System.IO;
using Rectangle = System.Drawing.Rectangle;
using System.Diagnostics;
using Application = System.Windows.Forms.Application;
using ChartRender;


namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        List<string> FileList = new List<string>();
        public Form1()
        {
            InitializeComponent();

            UserControl1 userControl1 = new UserControl1();

            TabPage tabPage = new TabPage();

            tabPage.Controls.Add(userControl1);

            tabControl1.TabPages.Add(tabPage);
            panel1.Show();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //OpenFileDialog dlg = new OpenFileDialog();
            //dlg.Filter = "word文件|*.docx";
            //object fileName = 0;
            //if (dlg.ShowDialog() == DialogResult.OK)
            //{
            //    fileName = dlg.FileName;
            //}
            //string str = (string)fileName;
            //OpenWord(str);


        }
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

                    TabPage tabPage = new TabPage(Path.GetFileName(file));
                    tabPage.Controls.Add(viewer);
                    tabControl1.TabPages.Add(tabPage);
                }
                else if (extension == ".doc" || extension == ".docx")
                {
                    RichTextBox richTextBox = new RichTextBox();
                    richTextBox.Dock = DockStyle.Fill;
                    OpenWord(file, richTextBox);

                    TabPage tabPage = new TabPage(Path.GetFileName(file));
                    tabPage.Controls.Add(richTextBox);
                    tabControl1.TabPages.Add(tabPage);
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
            panel1.Show();
        }

        public void OpenFile(string[] files)
        {
            foreach (string file in files)
            {
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

                        TabPage tabPage = new TabPage(Path.GetFileName(file));
                        tabPage.Controls.Add(viewer);
                        tabControl1.TabPages.Add(tabPage);
                    }
                    else if (extension == ".doc" || extension == ".docx")
                    {
                        RichTextBox richTextBox = new RichTextBox();
                        richTextBox.Dock = DockStyle.Fill;
                        OpenWord(file, richTextBox);

                        TabPage tabPage = new TabPage(Path.GetFileName(file));
                        tabPage.Controls.Add(richTextBox);
                        tabControl1.TabPages.Add(tabPage);
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
            }

            panel1.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {


        }

        private void 导入pdfToolStripMenuItem_Click(object sender, EventArgs e)
        {

            OpenFileDialog dlg = new OpenFileDialog();

            dlg.Filter = "Word 和 PDF 文件|*.doc;*.docx;*.pdf|所有文件|*.*";
            object fileName = 0;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                fileName = dlg.FileName;
            }
            if(fileName != null)
            {
                string str = (string)fileName;
                OpenFile(str);

            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }




        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            OpenFile(files);
        }

        // 双击关闭标签页事件
        private void TabControl_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            TabControl tabControl = sender as TabControl;

            for (int i = 0; i < tabControl.TabCount; i++)
            {
                Rectangle tabRect = tabControl.GetTabRect(i);

                // 检查鼠标是否双击在标签页范围内
                if (tabRect.Contains(e.Location))
                {
                    tabControl.TabPages.RemoveAt(i); // 移除双击的 TabPage
                    break;
                }
            }
        }


        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }
    }
}
