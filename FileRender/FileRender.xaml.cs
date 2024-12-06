using Aspose.Words;
using Models;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using Function.Factory;
using System.Security.Permissions;



namespace FileRender
{
    /// <summary>
    /// </summary>
    public partial class FileRender : UserControl
    {
        public FileRender()
        {
            InitializeComponent();
        }

        ResumeFile fileObj;

        Dictionary<string, ResumeFile> keyValuePairs = new Dictionary<string, ResumeFile>();

        public ResumeFile FileObj
        {
            get
            {
                return fileObj;
            }
            set
            {
                fileObj = value;
            }
        }

        public List<string> Files
        {
            set
            {
                ResumeFiles.Clear();
                foreach(string item in value)
                {
                    ResumeFile resumeFile = (ResumeFile)ResumeFIleFactory.Get(item);
                    ResumeFiles.Add(resumeFile);
                    keyValuePairs.Add(resumeFile.Filename, resumeFile);
                }
            }
        }

        List<ResumeFile> resumeFiles = new List<ResumeFile>();

        public List<ResumeFile> ResumeFiles
        {
            set
            {
                resumeFiles = value;
            }
            get
            {
                return resumeFiles;
            }
        }

        public void ShowFiles()
        {
            foreach(ResumeFile item in ResumeFiles)
            {
                TabItem tabItem = new TabItem();
                tabItem.Header = item.Filename;
                tabItem.MouseDown += TagOpen;
                SingleFileRender singleFileRender = new SingleFileRender();
                singleFileRender.FileObj = item;
                singleFileRender.OpenFile(item);
                tabItem.Content = singleFileRender;
                tagControl.Items.Add(tabItem);
            }
        }

        public void TagOpen(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            FileObj = keyValuePairs[((TabItem)sender).Header.ToString()];
        }
    }
}
