using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace StartUI
{
    /// <summary>
    /// SubSuccess.xaml 的交互逻辑
    /// </summary>
    public partial class SubSuccess : Window
    {
        private DispatcherTimer timer;
        private Window? window;      // 可空类型，防止空引用
        private double margin = 20;
        public SubSuccess()
        {
            InitializeComponent();
            this.ShowInTaskbar = false;   // 启动时不显示任务栏图标

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(5);
            timer.Tick += new EventHandler(Timer_Tick);
            timer.Start();
        }

        public void SubShow(Window parentWindow)
        {
            //获取SignUp的屏幕位置
            var parentPosition = parentWindow.PointToScreen(new Point(0, 0));
            //设置弹窗的初始位置
            this.Top = parentWindow.Top + this.Height / 2;
            this.Left = parentWindow.Left + (parentWindow.Width - this.Width) / 2;

            //遮盖窗口
            Border border = new Border()
            {
                CornerRadius = new CornerRadius(10),  // 设置四个角的圆角弧度
                Background = new SolidColorBrush(Color.FromArgb(128, 0, 0, 0)), // 半透明黑色背景
                BorderBrush = Brushes.Transparent,    // 不显示边框
                BorderThickness = new Thickness(0)    //边框厚度设为0
            };

            window = new Window()
            {
                Content = border,
                Height = parentWindow.Height - margin * 2,
                Width = parentWindow.Width - margin * 2,
                Top = parentWindow.Top + margin,
                Left = parentWindow.Left + margin,
                WindowStyle = WindowStyle.None,
                Owner = parentWindow,              // 设置父窗口为拥有者
                Background = Brushes.Transparent,  //设置窗口背景颜色为透明
                AllowsTransparency = true,         // 允许透明效果
                ShowInTaskbar = false,             // 不显示在任务栏
                IsHitTestVisible = false,          // 禁用与遮罩窗口的交互
                IsEnabled = false                  // 禁用与鼠标的交互       
            };
            window.Show();

            this.Topmost = true;  // 保证该窗口在最上层
            this.Show();          
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (timer.IsEnabled)
            {
                timer.Stop();
                window?.Close();
                this.Close();
            }
        }
    }
}
