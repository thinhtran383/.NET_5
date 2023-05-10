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
    }
}
