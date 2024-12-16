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

        public void ShowFiles(List<ResumeFile> Files)
        {
            foreach (var item in Files)
            {
                TabItem tabItem = new TabItem();
                tabItem.HorizontalAlignment = HorizontalAlignment.Stretch;
                tabItem.VerticalAlignment = VerticalAlignment.Stretch;
                tabItem.Header = item;
                SingleFileRender singleFileRender = new SingleFileRender();
                singleFileRender.VerticalAlignment = VerticalAlignment.Stretch;
                singleFileRender.HorizontalAlignment = HorizontalAlignment.Stretch;
                singleFileRender.OpenFile(item);
                tabItem.Content = singleFileRender;
                tagControl.Items.Add(tabItem);
            }
        }


        public void ShowFiles(List<string> FilePath)
        {
            foreach (string item in FilePath)
            {
                TabItem tabItem = new TabItem();
                tabItem.HorizontalAlignment = HorizontalAlignment.Stretch;
                tabItem.VerticalAlignment = VerticalAlignment.Stretch;
                tabItem.Header = item;
                SingleFileRender singleFileRender = new SingleFileRender();
                singleFileRender.VerticalAlignment = VerticalAlignment.Stretch;
                singleFileRender.HorizontalAlignment = HorizontalAlignment.Stretch;
                singleFileRender.OpenFile(item);
                tabItem.Content = singleFileRender;
                tagControl.Items.Add(tabItem);
            }
        }

    }
}
