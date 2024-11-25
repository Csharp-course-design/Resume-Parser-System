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


namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "word文件|*.docx";
            object fileName = 0;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                fileName = dlg.FileName;
            }
            string str = (string)fileName;
            OpenWord(str);


        }
        public void OpenWord(string fileName)
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
                richTextBox1.Paste();//richTextBox粘贴数据
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

        private void button2_Click(object sender, EventArgs e)
        {

            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "pdf文件|*.pdf";
            object fileName = 0;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                fileName = dlg.FileName;
            }
            string str = (string)fileName;
            pdfViewer1.LoadFromFile(str);

        }
    }
}
