using System.Windows;
using System.Windows.Input;

namespace YourNamespace
{
    public partial class InputDialog : Window
    {
        // 用于存储用户输入的内容
        public string UserInput { get; private set; }

        // 构造函数，接受一个提示文本作为参数
        public InputDialog(string prompt, string defaultContent = "")
        {
            InitializeComponent();
            InputTextBox.Text = defaultContent;
            PromptTextBlock.Text = prompt;  // 设置提示文本

            // 设置窗口位置为屏幕中央
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        // 按键事件
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) // 回车即 OK
            {
                OkButton_Click(sender, e);
            }
        }

        // OK 按钮点击事件
        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            UserInput = InputTextBox.Text;  // 获取用户输入
            DialogResult = true;  // 设置对话框结果为 OK
            Close();  // 关闭窗口
        }

        // Cancel 按钮点击事件
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;  // 设置对话框结果为 Cancel
            Close();  // 关闭窗口
        }

        // 加载事件
        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            // 自动转移焦点至输入框并全选内容
            InputTextBox.Focus();
            InputTextBox.SelectAll();
        }
    }
}
