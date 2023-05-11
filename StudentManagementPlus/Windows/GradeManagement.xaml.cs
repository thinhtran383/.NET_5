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
    /// Interaction logic for GradeManagement.xaml
    /// </summary>
    public partial class GradeManagement:Window {
        private ObservableCollection<Grade> grades = new ObservableCollection<Grade>();
        public GradeManagement() {
            InitializeComponent();
            loadGrade();
        }

        private void clear() {
            tbAttendace.Text = "";
            tbFinal.Text = "";
            tbMid.Text = "";
        }

        private void loadGrade() {
            string sql =
                "SELECT students.studentName, grades.studentId, grades.subjectId, subjects.subjectName ,grades.attendanceGrade, grades.midTermGrade, grades.finalGrade\r\nFROM grades\r\nJOIN students ON grades.studentId = students.studentId\r\nJOIN subjects ON subjects.subjectId = grades.subjectId\r\nORDER BY students.studentName;";
            SqlConnection connection = new SqlConnection(ConnectionString.connectionString);
            SqlCommand command = new SqlCommand(sql,connection);
            connection.Open();
            var data = command.ExecuteReader();
            while(data.Read()) {
                string studentName = data.GetString(data.GetOrdinal("studentName"));
                string studentId = data.GetString(data.GetOrdinal("studentId"));
                string subjectId = data.GetString(data.GetOrdinal("subjectId"));
                string subjectName = data.GetString(data.GetOrdinal("subjectName"));
                float attendanceGrade = (float) data.GetDouble(data.GetOrdinal("attendanceGrade"));
                float midTermGrade = (float) data.GetDouble(data.GetOrdinal("midTermGrade"));
                float finalGrade = (float) data.GetDouble(data.GetOrdinal("finalGrade"));
                grades.Add(new Grade(studentName,studentId,subjectId,subjectName,attendanceGrade,midTermGrade,finalGrade));
            }
        }

        private void dgGrade_Loaded(object sender,RoutedEventArgs e) {
            dgGrade.ItemsSource = grades;
        }

        private void dgGrade_SelectionChanged(object sender,SelectionChangedEventArgs e) {
            if(dgGrade.SelectedIndex != -1) {
                btnUpdate.IsEnabled = true;
            }

            Grade grade = (Grade) dgGrade.SelectedItem;
            tbAttendace.Text = grade.AttendanceGrade.ToString();
            tbMid.Text = grade.MidTermGrade.ToString();
            tbFinal.Text = grade.FinalGrade.ToString();

        }

        private void Button_Click(object sender,RoutedEventArgs e) {
            Grade grade = (Grade) dgGrade.SelectedItem;

            float attendanceGrade;
            float midTermGrade;
            float finalGrade;

            try {
                attendanceGrade = float.Parse(tbAttendace.Text);
                midTermGrade = float.Parse(tbMid.Text);
                finalGrade = float.Parse(tbFinal.Text);
            }
            catch (Exception exception) {
                MessageBox.Show("Điểm không hợp lệ");
                return;
            }

            string sql = "Update grades set attendanceGrade = @attendanceGrade, midTermGrade = @midTermGrade, finalGrade = @finalGrade where studentId = @studentId and subjectId = @subjectId";
            SqlConnection connection = new SqlConnection(ConnectionString.connectionString);
            SqlCommand command = new SqlCommand(sql,connection);
            command.Parameters.AddWithValue("@attendanceGrade",attendanceGrade);
            command.Parameters.AddWithValue("@midTermGrade",midTermGrade);
            command.Parameters.AddWithValue("@finalGrade",finalGrade);
            command.Parameters.AddWithValue("@studentId",grade.StudentId);
            command.Parameters.AddWithValue("@subjectId",grade.SubjectId);
            connection.Open();
            int resuilt= command.ExecuteNonQuery();

            if(resuilt == 1) {
                MessageBox.Show("Cập nhật thành công");
                grade.AttendanceGrade = attendanceGrade;
                grade.MidTermGrade = midTermGrade;
                grade.FinalGrade = finalGrade;
                clear();

                dgGrade.Items.Refresh();

            }
            else {
                MessageBox.Show("Cập nhật thất bại");
            }

        }
    }
}
