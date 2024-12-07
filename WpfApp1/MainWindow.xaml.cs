using System.Windows;
namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            a.ShowFiles(new List<string>(){"C:\\Users\\王翔\\Desktop\\ms.docx" }) ;
        }
    }
}