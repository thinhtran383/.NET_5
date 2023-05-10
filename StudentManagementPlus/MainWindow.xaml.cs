using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using StudentManagementPlus.Models;

namespace StudentManagementPlus {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow:Window {
        
        
        private static List<Account> accounts = new List<Account>();

        public MainWindow() {
            InitializeComponent();
        }

        private void btnSignin_Click(object sender,RoutedEventArgs e) {

            try {
                string sql = "Select * from adminaccount";
                SqlConnection connection = new SqlConnection(ConnectionString.connectionString);
                SqlCommand command = new SqlCommand(sql,connection);
                connection.Open();
                var data = command.ExecuteReader();
                while (data.Read()) {
                    string username = data.GetString(data.GetOrdinal("username"));
                    string password = data.GetString(data.GetOrdinal("password"));
                    Account account = new Account(username,password);
                    accounts.Add(account);
                }
            } catch(SqlException E) {
                MessageBox.Show("Lỗi khi truy vấn cớ sở dữ liệu ");
                throw;
            }

            string usernameInput = tbUsername.Text;
            string passwordInput = tbPassword.Password;

            if(tbPassword.Password == "" || tbUsername.Text == "") {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin");
                return;
            }

            foreach(var account in accounts) {
                if(account.Username == usernameInput && account.Password == passwordInput) {
                    MessageBox.Show("Đăng nhập thành công");
                    Panel panel = new Panel();
                    this.Close();
                    panel.Show();
                    return;
                }
                else {
                    MessageBox.Show("Thông tin tài khoản hoặc mật khẩu không chính xác");
                }
            }
        }
    }
}
