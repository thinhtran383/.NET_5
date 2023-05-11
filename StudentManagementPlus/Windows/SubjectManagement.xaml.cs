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
using System.Xml.Linq;
using StudentManagementPlus.Models;

namespace StudentManagementPlus.Windows {
    /// <summary>
    /// Interaction logic for SubjectManagement.xaml
    /// </summary>
    public partial class SubjectManagement : Window {
        private ObservableCollection<Subject> subjectList = new ObservableCollection<Subject>();

        public SubjectManagement() {
            InitializeComponent();
            loadSubject();
        }


        private void clear() {
            tbSubjectCredit.Text = "";
            tbSubjectId.Text = "";
            tbSubjectName.Text = "";
        }

        private void loadSubject() {
            try {
                string sql = "Select * from subjects";
                SqlConnection connection = new SqlConnection(ConnectionString.connectionString);
                SqlCommand command = new SqlCommand(sql, connection);
                connection.Open();
                var data = command.ExecuteReader();
                while (data.Read()) {
                    string subjectId = data.GetString(data.GetOrdinal("subjectId"));
                    string subjectName = data.GetString(data.GetOrdinal("subjectName"));
                    int subjectCredit = data.GetInt32(data.GetOrdinal("subjectCredit"));

                    Subject subject = new Subject(subjectId, subjectName, subjectCredit);
                    subjectList.Add(subject);
                }
            }
            catch (SqlException E) {
                MessageBox.Show("Lỗi khi truy vấn cớ sở dữ liệu ");
                throw;
            }
        }

        private void dgSubject_Loaded(object sender, RoutedEventArgs e) {
            dgSubject.ItemsSource = subjectList;
        }

        private void dgSubject_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (dgSubject.SelectedItem != null) {
                btnDelete.IsEnabled = true;
                btnUpdate.IsEnabled = true;
            }

            Subject subject = dgSubject.SelectedItem as Subject;
            if (subject != null) {
                tbSubjectId.Text = subject.SubjectId;
                tbSubjectName.Text = subject.SubjectName;
                tbSubjectCredit.Text = subject.SubjectCredit.ToString();
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e) {
            // kiem tra neu trong thi khong lam gi
            if (tbSubjectId.Text == "" || tbSubjectName.Text == "" || tbSubjectCredit.Text == "") {
                return;
            }

            // kiem tra neu trung id thi thong bao
            foreach (Subject subject in subjectList) {
                if (subject.SubjectId == tbSubjectId.Text) {
                    MessageBox.Show("Mã môn học đã tồn tại");
                    return;
                }
            }

            // kiem tra so tin chi la mot so nguyen duong
            // kiem tra so tin chi la mot so nguyen duong
            int subjectCredit;
            if (!Int32.TryParse(tbSubjectCredit.Text, out subjectCredit) || subjectCredit <= 0) {
                MessageBox.Show("Số tín chỉ phải là một số nguyên dương");
                return;
            }


            // them vao csdl
            string sql = "Insert into subjects values(@subjectId,@subjectName,@subjectCredit)";
            SqlConnection connection = new SqlConnection(ConnectionString.connectionString);
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@subjectId", tbSubjectId.Text);
            command.Parameters.AddWithValue("@subjectName", tbSubjectName.Text);
            command.Parameters.AddWithValue("@subjectCredit", tbSubjectCredit.Text);

            connection.Open();
            int result = command.ExecuteNonQuery();
            if (result > 0) {
                MessageBox.Show("Thêm môn học thành công");
            }
            else {
                MessageBox.Show("Thêm môn học thất bại");
            }

            Subject subject1 = new Subject(tbSubjectId.Text, tbSubjectName.Text, int.Parse(tbSubjectCredit.Text));
            subjectList.Add(subject1);
            clear();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e) {
            string sql = "Delete from subjects where subjectId = @subjectId";
            SqlConnection connection = new SqlConnection(ConnectionString.connectionString);
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@subjectId", tbSubjectId.Text);

            connection.Open();
            int result = command.ExecuteNonQuery();
            if (result > 0) {
                MessageBox.Show("Xóa môn học thành công");
                Subject subject = dgSubject.SelectedItem as Subject;
                subjectList.Remove(subject);
                clear();
            }
        }

        private void btnUpdate_Click(object sender,RoutedEventArgs e) {
            var subject = dgSubject.SelectedItem as Subject;


            string oldId = subject.SubjectId;
            string sql = "Update subjects set subjectId = @subjectIdNew ,subjectName = @subjectName, subjectCredit = @subjectCredit where subjectId = @subjectId";
            SqlConnection connection = new SqlConnection(ConnectionString.connectionString);
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@subjectId", oldId);
            command.Parameters.AddWithValue("@subjectIdNew", tbSubjectId.Text);
            command.Parameters.AddWithValue("@subjectName", tbSubjectName.Text);
            command.Parameters.AddWithValue("@subjectCredit", tbSubjectCredit.Text);
            connection.Open();
            int result = command.ExecuteNonQuery();
            if (result > 0) {
                MessageBox.Show("Cập nhật môn học thành công");
                subject.SubjectName = tbSubjectName.Text;
                subject.SubjectCredit = int.Parse(tbSubjectCredit.Text);
                subject.SubjectId = tbSubjectId.Text;
                dgSubject.Items.Refresh();
                clear();
            }

            
        }

        private void tbSearch_TextChanged(object sender,TextChangedEventArgs e) {
            string search = tbSearch.Text.ToLower();
            if (search == "") {
                dgSubject.ItemsSource = subjectList;
            }
            else {
                var list = subjectList.Where(s => s.SubjectId.ToLower().Contains(search) || s.SubjectName.ToLower().Contains(search));
                dgSubject.ItemsSource = list;
            }
        }
    }
}
