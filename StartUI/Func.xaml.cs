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

namespace StartUI
{
    /// <summary>
    /// Func.xaml 的交互逻辑
    /// </summary>
    public partial class Func : Window
    {
        public Func()
        {
            InitializeComponent();
        }

        private void Button_Click_Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_ChangeSize(object sender, RoutedEventArgs e)
        {
            if(this.WindowState == WindowState.Maximized)
            {
                Max.Visibility = Visibility.Visible;
                Normal.Visibility = Visibility.Collapsed;
                this.WindowState = WindowState.Normal;
            }

            else
            {
                Max.Visibility = Visibility.Collapsed;
                Normal.Visibility = Visibility.Visible;
                this.WindowState = WindowState.Maximized;
            }
        }

        private void Button_Click_Min(object sender, RoutedEventArgs e)
        {
            this.WindowState= WindowState.Minimized;
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
    }
}
