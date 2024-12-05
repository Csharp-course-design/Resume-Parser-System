using System.Windows;
using System.Windows.Threading;

namespace StartUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer timerProgressbar;
        public MainWindow()
        {
            InitializeComponent();
            this.ShowInTaskbar = false;   // 启动时不显示任务栏图标

            timerProgressbar = new DispatcherTimer();
            timerProgressbar.Interval = TimeSpan.FromSeconds(0.01); // 时间间隔为0.01s
            timerProgressbar.Tick += new EventHandler(timer_Tick);  // 绑定timer_Tick事件
            timerProgressbar.Start();
        }

        //关闭按钮
        private void buttonClick(Object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //进度条更新函数
        private void timer_Tick(Object sender, EventArgs e)
        {
            panelProgressbar.Width += 5;

            if (panelProgressbar.Width >= 600)
            {
                timerProgressbar.Stop();

                LoginIn loginIn = new LoginIn();                
                loginIn.Show();
                this.Close();
            }
        }

    }
}