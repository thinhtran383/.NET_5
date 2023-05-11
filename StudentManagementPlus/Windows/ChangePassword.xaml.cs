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
using System.Windows.Shapes;

namespace StudentManagementPlus.Windows {
    /// <summary>
    /// Interaction logic for ChangePassword.xaml
    /// </summary>
    public partial class ChangePassword:Window {
        private string username;
        public ChangePassword(string username) {
            InitializeComponent();
            this.username = username;
        }

        private void Button_Click(object sender,RoutedEventArgs e) {
            string sql = "update adminaccount  set password = @password where username = @username";
            // thuc hien viec doi mat khau
            if(tbNewPassword.Password == tbConfirmPassword.Password) {
                if(tbNewPassword.Password.Length >= 6) {
                    if(tbNewPassword.Password != tbOldPassword.Password) {
                        if(tbOldPassword.Password == getPassword()) {
                            SqlConnection connection = new SqlConnection(ConnectionString.connectionString);
                            SqlCommand command = new SqlCommand(sql,connection);
                            command.Parameters.AddWithValue("@password",tbNewPassword.Password);
                            command.Parameters.AddWithValue("@username",username);
                            connection.Open();
                            int result = command.ExecuteNonQuery();
                            if(result > 0) {
                                MessageBox.Show("Đổi mật khẩu thành công","Thông báo",MessageBoxButton.OK,MessageBoxImage.Information);
                                this.Close();
                            } else {
                                MessageBox.Show("Đổi mật khẩu thất bại","Thông báo",MessageBoxButton.OK,MessageBoxImage.Error);
                            }
                        } else {
                            MessageBox.Show("Mật khẩu cũ không đúng","Thông báo",MessageBoxButton.OK,MessageBoxImage.Error);
                        }
                    } else {
                        MessageBox.Show("Mật khẩu mới không được trùng với mật khẩu cũ","Thông báo",MessageBoxButton.OK,MessageBoxImage.Error);
                    }
                } else {
                    MessageBox.Show("Mật khẩu phải có ít nhất 6 ký tự","Thông báo",MessageBoxButton.OK,MessageBoxImage.Error);
                }
            } else {
                MessageBox.Show("Mật khẩu mới không trùng khớp","Thông báo",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }

        private string getPassword() {
            string password = "";
            string sql = "select password from adminaccount where username = @username";
            SqlConnection connection = new SqlConnection(ConnectionString.connectionString);
            SqlCommand command = new SqlCommand(sql,connection);
            command.Parameters.AddWithValue("@username",username);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            if(reader.Read()) {
                password = reader.GetString(0);
            }
            reader.Close();
            connection.Close();
            return password;
        }

    }
}
