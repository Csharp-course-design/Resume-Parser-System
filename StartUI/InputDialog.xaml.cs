using System.Windows;

namespace YourNamespace
{
    public partial class InputDialog : Window
    {
        // 用于存储用户输入的内容
        public string UserInput { get; private set; }

        // 构造函数，接受一个提示文本作为参数
        public InputDialog(string prompt)
        {
            InitializeComponent();
            PromptTextBlock.Text = prompt;  // 设置提示文本

            // 设置窗口位置为屏幕中央
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
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
    }
}
