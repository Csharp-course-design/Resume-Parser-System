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
using Microsoft.VisualBasic;
using CsharpAPI;
using Function.TransFactory;
using Google.Protobuf.WellKnownTypes;

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

        private void MenuItem_Click_Resume_Analysis(object sender, RoutedEventArgs e)
        {
            InputArea.Visibility = Visibility.Visible;
            InputAreaScrollViewer.Visibility = Visibility.Visible;
            closeInputAreaButton.Visibility = Visibility.Visible;
            closeInputAreaIcon.Visibility = Visibility.Visible;
            

            // 创建文件选择对话框
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                Title = "选择一个文件",
                Filter = "支持的文件 (*.pdf;*.docx;*.doc;*.txt)|*.pdf;*.docx;*.doc;*.txt|所有文件 (*.*)|*.*",
                Multiselect = false // 限制只选择一个文件
            };

            // 显示对话框并判断用户是否选择了文件
            if (openFileDialog.ShowDialog() == true)
            {
                // 获取文件的绝对路径
                string filePath = openFileDialog.FileName;

                // 显示文件路径（也可以替换为其他逻辑）
                // MessageBox.Show($"您选择的文件路径是：{filePath}", "文件路径", MessageBoxButton.OK, MessageBoxImage.Information);

                // 创建 LinkToAPI 实例
                LinkToAPI api = new LinkToAPI();

                // 假设 API_Json 会包含返回的 JSON 数据
                InputArea.Text = api.ResumeFile(filePath).ToString();
            }
            else
            {
                // 用户取消选择
                MessageBox.Show("未选择任何文件。", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void closeInputAreaButton_Click(object sender, RoutedEventArgs e)
        {
            InputArea.Visibility = Visibility.Collapsed;
            InputAreaScrollViewer.Visibility = Visibility.Collapsed;
            closeInputAreaButton.Visibility = Visibility.Collapsed; 
            closeInputAreaIcon.Visibility = Visibility.Collapsed;


            InputArea.Text = String.Empty;
        }
         
        private void MenuItem_Click_JSON(object sender, RoutedEventArgs e)
        {
            InputArea.Visibility = Visibility.Visible;
            InputAreaScrollViewer.Visibility = Visibility.Visible;
            closeInputAreaButton.Visibility = Visibility.Visible;
            closeInputAreaIcon.Visibility = Visibility.Visible;


            // 创建文件选择对话框
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                Title = "选择一个文件",
                Filter = "支持的文件 (*.pdf;*.docx;*.doc;*.txt)|*.pdf;*.docx;*.doc;*.txt|所有文件 (*.*)|*.*",
                Multiselect = false // 限制只选择一个文件
            };

            // 显示对话框并判断用户是否选择了文件
            if (openFileDialog.ShowDialog() == true)
            {
                // 获取文件的绝对路径
                string filePath = openFileDialog.FileName;

                // 显示文件路径（也可以替换为其他逻辑）
                // MessageBox.Show($"您选择的文件路径是：{filePath}", "文件路径", MessageBoxButton.OK, MessageBoxImage.Information);

                // 创建 LinkToAPI 实例
                LinkToAPI api = new LinkToAPI();

                // 假设 API_Json 会包含返回的 JSON 数据
                // InputArea.Text = api.ResumeFile(filePath).ToString();
                var TestObject = api.ResumeFile(filePath);
                var res = (new JsonFactory()).Content(TestObject);
                InputArea.Text = res;
            }
            else
            {
                // 用户取消选择
                MessageBox.Show("未选择任何文件。", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void MenuItem_Click_CSV(object sender, RoutedEventArgs e)
        {
            InputArea.Visibility = Visibility.Visible;
            InputAreaScrollViewer.Visibility = Visibility.Visible;
            closeInputAreaButton.Visibility = Visibility.Visible;
            closeInputAreaIcon.Visibility = Visibility.Visible;


            // 创建文件选择对话框
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                Title = "选择一个文件",
                Filter = "支持的文件 (*.pdf;*.docx;*.doc;*.txt)|*.pdf;*.docx;*.doc;*.txt|所有文件 (*.*)|*.*",
                Multiselect = false // 限制只选择一个文件
            };

            // 显示对话框并判断用户是否选择了文件
            if (openFileDialog.ShowDialog() == true)
            {
                // 获取文件的绝对路径
                string filePath = openFileDialog.FileName;

                // 显示文件路径（也可以替换为其他逻辑）
                // MessageBox.Show($"您选择的文件路径是：{filePath}", "文件路径", MessageBoxButton.OK, MessageBoxImage.Information);

                // 创建 LinkToAPI 实例
                LinkToAPI api = new LinkToAPI();

                // 假设 API_Json 会包含返回的 JSON 数据
                // InputArea.Text = api.ResumeFile(filePath).ToString();
                var TestObject = api.ResumeFile(filePath);
                var res = (new CSVFactory()).Content(TestObject);
                InputArea.Text = res;
            }
            else
            {
                // 用户取消选择
                MessageBox.Show("未选择任何文件。", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void MenuItem_Click_XML(object sender, RoutedEventArgs e)
        {
            InputArea.Visibility = Visibility.Visible;
            InputAreaScrollViewer.Visibility = Visibility.Visible;
            closeInputAreaButton.Visibility = Visibility.Visible;
            closeInputAreaIcon.Visibility = Visibility.Visible;


            // 创建文件选择对话框
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                Title = "选择一个文件",
                Filter = "支持的文件 (*.pdf;*.docx;*.doc;*.txt)|*.pdf;*.docx;*.doc;*.txt|所有文件 (*.*)|*.*",
                Multiselect = false // 限制只选择一个文件
            };

            // 显示对话框并判断用户是否选择了文件
            if (openFileDialog.ShowDialog() == true)
            {
                // 获取文件的绝对路径
                string filePath = openFileDialog.FileName;

                // 显示文件路径（也可以替换为其他逻辑）
                // MessageBox.Show($"您选择的文件路径是：{filePath}", "文件路径", MessageBoxButton.OK, MessageBoxImage.Information);

                // 创建 LinkToAPI 实例
                LinkToAPI api = new LinkToAPI();

                // 假设 API_Json 会包含返回的 JSON 数据
                // InputArea.Text = api.ResumeFile(filePath).ToString();
                var TestObject = api.ResumeFile(filePath);
                var res = (new XMLFactory()).Content(TestObject);
                InputArea.Text = res;
            }
            else
            {
                // 用户取消选择
                MessageBox.Show("未选择任何文件。", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        
    }
}

