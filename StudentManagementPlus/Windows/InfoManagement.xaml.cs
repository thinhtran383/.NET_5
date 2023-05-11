using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using StudentManagementPlus.Models;

namespace StudentManagementPlus.Windows {
    /// <summary>
    /// Interaction logic for InfoManagement.xaml
    /// </summary>
    public partial class InfoManagement:Window {
        ObservableCollection<Student> studentList = new ObservableCollection<Student>();

        public InfoManagement() {
            InitializeComponent();
            loadStudent();
            loadCbGender();
        }

        private void loadCbGender() {
            cbGender.Items.Add("Male");
            cbGender.Items.Add("Female");
        }

        private void clear() {
            tbName.Text = "";
            tbId.Text = "";
            cbGender.Text = "";
            tbAddress.Text = "";
            tbEmail.Text = "";
            tbPhone.Text = "";
            pickDate.SelectedDate = null;
            dgStudent.SelectedItem = null;
        }

        private void loadStudent() {
            try {
                string sql = "Select * from students";
                SqlConnection connection = new SqlConnection(ConnectionString.connectionString);
                SqlCommand command = new SqlCommand(sql,connection);
                connection.Open();
                var data = command.ExecuteReader();
                while(data.Read()) {
                    string studentId = data.GetString(data.GetOrdinal("studentId"));
                    string studentName = data.GetString(data.GetOrdinal("studentName"));
                    string studentGender = data.GetString(data.GetOrdinal("studentGender"));
                    string studenAddess = data.GetString(data.GetOrdinal("studentAddress"));
                    string studentEmail = data.GetString(data.GetOrdinal("studentEmail"));
                    string studentPhone = data.GetString(data.GetOrdinal("studentPhone"));
                    DateTime studentBirthday = data.GetDateTime(data.GetOrdinal("studentBirthday"));

                    Student student = new Student(studentId,studentName,studentGender,studenAddess,studentEmail,studentPhone,studentBirthday);
                    studentList.Add(student);
                }
            } catch(SqlException E) {
                MessageBox.Show("Lỗi khi truy vấn cớ sở dữ liệu ");
                throw;
            }
        }

        private void dgStudent_Loaded(object sender,RoutedEventArgs e) {
            dgStudent.ItemsSource = studentList;
        }

        private void dgStudent_SelectionChanged(object sender,SelectionChangedEventArgs e) {
            if(dgStudent.SelectedIndex != -1) {
                btnDelete.IsEnabled = true;
                btnUpdate.IsEnabled = true;
            }
            
            Student selectedStudent = dgStudent.SelectedItem as Student;
            if(selectedStudent != null) {
                tbId.Text = selectedStudent.StudentId;
                tbName.Text = selectedStudent.StudentName;
                cbGender.Text = selectedStudent.StudentGender;
                tbAddress.Text = selectedStudent.StudentAddress;
                tbEmail.Text = selectedStudent.StudentEmail;
                tbPhone.Text = selectedStudent.StudentPhone;
                pickDate.SelectedDate = selectedStudent.StudentBirthday;
            }
        }

        private void tbSearch_TextChanged(object sender,TextChangedEventArgs e) {
            string search = tbSearch.Text.ToLower();
            var result = studentList.Where(s => s.StudentName.ToLower().Contains(search));
            dgStudent.ItemsSource = result;
        }

       

        private void btnAdd_Click_1(object sender,RoutedEventArgs e) {
            // kiem tra du lieu nhap vao neu rong thi khong lam gi
            if(tbId.Text == "" || tbName.Text == "" || cbGender.Text == "" || tbAddress.Text == "" || tbEmail.Text == "" || tbPhone.Text == "" || pickDate.SelectedDate == null) {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin");
                return;
            }

            // kiem tra id da ton tai chua
            foreach(Student student in studentList) {
                if(student.StudentId == tbId.Text) {
                    MessageBox.Show("Id đã tồn tại");
                    return;
                }
            }

            string sql = "Insert into students values(@studentId,@studentName,@studentGender,@studentAddress,@studentEmail,@studentPhone,@studentBirthday)";
            SqlConnection connection = new SqlConnection(ConnectionString.connectionString);
            SqlCommand command = new SqlCommand(sql,connection);
            command.Parameters.AddWithValue("@studentId",tbId.Text);
            command.Parameters.AddWithValue("@studentName",tbName.Text);
            command.Parameters.AddWithValue("@studentGender",cbGender.Text);
            command.Parameters.AddWithValue("@studentAddress",tbAddress.Text);
            command.Parameters.AddWithValue("@studentEmail",tbEmail.Text);
            command.Parameters.AddWithValue("@studentPhone",tbPhone.Text);
            command.Parameters.AddWithValue("@studentBirthday",pickDate.SelectedDate);
            connection.Open();
            int result = command.ExecuteNonQuery();
            if(result > 0) {
                MessageBox.Show("Thêm thành công");
                Student student = new Student(tbId.Text,tbName.Text,cbGender.Text,tbAddress.Text,tbEmail.Text,tbPhone.Text,pickDate.SelectedDate.Value);
                studentList.Add(student);
                dgStudent.Items.Refresh();
                clear();
            } else {
                MessageBox.Show("Thêm thất bại");
            }

       
        }

        private void btnDelete_Click(object sender,RoutedEventArgs e) {
          

            string sql = "Delete from students where studentId = @studentId";
            SqlConnection connection = new SqlConnection(ConnectionString.connectionString);
            SqlCommand command = new SqlCommand(sql,connection);
            command.Parameters.AddWithValue("@studentId",tbId.Text);
            connection.Open();
            int result = command.ExecuteNonQuery();
            if(result > 0) {
                MessageBox.Show("Xóa thành công");
                Student student = dgStudent.SelectedItem as Student;
                studentList.Remove(student);
                clear();
            } else {
                MessageBox.Show("Xóa thất bại");    
            }
        }

        private void btnUpdate_Click(object sender,RoutedEventArgs e) {
            Student selectedStudent = dgStudent.SelectedItem as Student;
            string oldId = selectedStudent.StudentId;
            string sql = "Update students set studentId = @studentIdNew ,studentName = @studentName, studentGender = @studentGender, studentAddress = @studentAddress, studentEmail = @studentEmail, studentPhone = @studentPhone, studentBirthday = @studentBirthday where studentId = @studentId";
            SqlConnection connection = new SqlConnection(ConnectionString.connectionString);
            SqlCommand command = new SqlCommand(sql,connection);
            command.Parameters.AddWithValue("@studentIdNew", tbId.Text);
            command.Parameters.AddWithValue("@studentId",oldId);
            command.Parameters.AddWithValue("@studentName",tbName.Text);
            command.Parameters.AddWithValue("@studentGender",cbGender.Text);
            command.Parameters.AddWithValue("@studentAddress",tbAddress.Text);
            command.Parameters.AddWithValue("@studentEmail",tbEmail.Text);
            command.Parameters.AddWithValue("@studentPhone",tbPhone.Text);
            command.Parameters.AddWithValue("@studentBirthday",pickDate.SelectedDate);
            connection.Open();
            int result = command.ExecuteNonQuery();
            if(result > 0) {
                MessageBox.Show("Cập nhật thành công");
                selectedStudent.StudentId = tbId.Text;
                selectedStudent.StudentName = tbName.Text;
                selectedStudent.StudentGender = cbGender.Text;
                selectedStudent.StudentAddress = tbAddress.Text;
                selectedStudent.StudentEmail = tbEmail.Text;
                selectedStudent.StudentPhone = tbPhone.Text;
                selectedStudent.StudentBirthday = pickDate.SelectedDate.Value;
                clear();
                dgStudent.Items.Refresh();
            } else {
                MessageBox.Show("Cập nhật thất bại");
            }
        }
    }
}
