using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MySql.Data.MySqlClient;

namespace StartUI
{
    /// <summary>
    /// LoginIn.xaml 的交互逻辑
    /// </summary>
    public partial class LoginIn : Window
    {
        public LoginIn()
        {
            InitializeComponent();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_SignUp(object sender, RoutedEventArgs e)
        {
            SignUp signUp = new SignUp();            
            signUp.Show();
            this.Close();
        }

        public class DatabaseHelper
        {
            private string connectionString = "Server=localhost;Database=CSharp;User ID=root;Password=031029;";

            // 从数据库中读取并验证用户名、邮箱和密码
            public bool ValidateUser(string username, string email, string password)
            {
                try
                {
                    using (var connection = new MySqlConnection(connectionString))
                    {
                        connection.Open();  //打开数据库
                        string query = @"SELECT COUNT(*) FROM userMessage 
                                    WHERE userName = @username 
                                      AND userEmail = @userEmail 
                                      AND userPassword = @password";

                        using (var cmd = new MySqlCommand(query, connection))
                        {
                            cmd.Parameters.AddWithValue("@username", username);
                            cmd.Parameters.AddWithValue("@userEmail", email);
                            cmd.Parameters.AddWithValue("@password", password);

                            var result = Convert.ToInt32(cmd.ExecuteScalar());
                            return result > 0;
                        }
                    }
                }
                catch (Exception e)  //捕捉数据库链接错误信息
                {
                    throw new Exception("Database connection failed: " + e.Message);
                }
            }
        }

        private void Button_Click_Login(object sender, RoutedEventArgs e)
        {
            //从输入框获取字符串
            string username = UsernameTextBox.Text;
            string email = EmailTextBox.Text;
            string password = PasswordBox.Password;

            var dbHelper = new DatabaseHelper();
            bool isValidUser = dbHelper.ValidateUser(username, email, password);

            if (isValidUser)
            {
                MessageBox.Show("登录成功！");
            }
            else
            {
                ErrorMessageTextBlock.Text = "Incorrect Username, Email or Password";
                ErrorMessageTextBlock.Visibility = Visibility.Visible; // 显示错误消息
            }
        }

        private void UsernameTextBox_TextChanged(object sender, TextChangedEventArgs e)  //当信息被修改时，错误信息再次成为收缩状态
        {
            ErrorMessageTextBlock.Visibility = Visibility.Collapsed;
        }

        private void EmailTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ErrorMessageTextBlock.Visibility = Visibility.Collapsed;
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            ErrorMessageTextBlock.Visibility = Visibility.Collapsed;
        }
    }
}
