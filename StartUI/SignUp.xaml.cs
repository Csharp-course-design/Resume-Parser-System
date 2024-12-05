using System.Windows;
using System.Windows.Input;
using MySql.Data.MySqlClient;

namespace StartUI
{
    /// <summary>
    /// SignUp.xaml 的交互逻辑
    /// </summary>
    public partial class SignUp : Window
    {
        private string connectionString = "Server=localhost;Database=CSharp;User ID=root;Password=031029;";

        public SignUp()
        {
            InitializeComponent();
        }
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LoginIn loginIn = new LoginIn();            
            loginIn.Show();
            this.Close();
        }

        private void Button_Click_Cancel(object sender, RoutedEventArgs e)
        {
            LoginIn loginIn = new LoginIn();
            loginIn.Show();
            this.Close();
        }

        private void Button_Click_Submit(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string email = EmailTextBox.Text;
            string password = PasswordBox.Password;
            string confirmPassword = ConfirmPasswordBox.Password;

            bool isValid = true;

            // 清除之前的错误信息
            UsernameRequire.Visibility = Visibility.Visible;
            PasswordRequire.Visibility = Visibility.Visible;
            UsernameErrorTextBlock.Visibility = Visibility.Collapsed;
            EmailErrorTextBlock.Visibility = Visibility.Collapsed;
            PasswordErrorTextBlock.Visibility = Visibility.Collapsed;
            ConfirmPasswordErrorTextBlock.Visibility = Visibility.Collapsed;

            if (string.IsNullOrEmpty(username))  //检测用户名是否为空
            {
                UsernameRequire.Visibility = Visibility.Collapsed;
                UsernameErrorTextBlock.Text = "Username is required";
                UsernameErrorTextBlock.Visibility = Visibility.Visible;
                isValid = false;
            }
            //else if (IsUsernameExists(username)) //检测用户名是否已经存在
            //{
            //    UsernameRequire.Visibility = Visibility.Collapsed;
            //    UsernameErrorTextBlock.Text = "Username already exists";
            //    UsernameErrorTextBlock.Visibility = Visibility.Visible;
            //    isValid = false;
            //}

            if (string.IsNullOrEmpty(email))     //检测邮箱是否为空
            {
                EmailErrorTextBlock.Text = "Email is required";
                EmailErrorTextBlock.Visibility = Visibility.Visible;
                isValid = false;
            }
            //else if (IsEmailExists(email))       //检测邮箱是否正确或存在
            //{
            //    EmailErrorTextBlock.Text = "Email already exists or invalid";
            //    EmailErrorTextBlock.Visibility = Visibility.Visible;
            //    isValid = false;
            //}

            if (string.IsNullOrEmpty(password) || password.Length < 1 || password.Length > 30) //检测密码是否符合输入
            {
                PasswordRequire.Visibility = Visibility.Collapsed;
                PasswordErrorTextBlock.Text = "Password is required between 1 and 30 characters";
                PasswordErrorTextBlock.Visibility = Visibility.Visible;
                isValid = false;
            }
           
            if (password != confirmPassword)      // 验证密码和确认密码一致
            {
                ConfirmPasswordErrorTextBlock.Visibility = Visibility.Visible;
                isValid = false;
            }

            // 所有验证通过，则插入数据到数据库
            if (isValid)
            {
                //InsertUserToDatabase(username, email, password);
                MessageBox.Show("Registration successful!");
            }
        }

        private bool IsUsernameExists(string username)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM usermessage WHERE userName = @Username";
                var command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", username);
                int count = Convert.ToInt32(command.ExecuteScalar());
                return count > 0;
            }
        }

        private bool IsEmailExists(string email)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM usermessage WHERE userEmail = @Email";
                var command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Email", email);
                int count = Convert.ToInt32(command.ExecuteScalar());
                return count > 0;
            }
        }

        private void InsertUserToDatabase(string username, string email, string password)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO usermessage (userName, userEmail, userPassword) VALUES (@Username, @Email, @Password)";
                var command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Password", password);
                command.ExecuteNonQuery();
            }
        }
    }
}
