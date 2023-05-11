using System.Windows;
using StudentManagementPlus.Windows;
namespace StudentManagementPlus {
    /// <summary>
    /// Interaction logic for Panel.xaml
    /// </summary>
    public partial class Panel:Window {
        private string username;

        public Panel(string username) {
            InitializeComponent();
            this.username = username;
        }

        private void btnDangXuat_Click(object sender,RoutedEventArgs e) {
            MessageBoxResult result = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất không?","Thông báo",MessageBoxButton.YesNo,MessageBoxImage.Question);
            if(result == MessageBoxResult.Yes) {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            } else {
                return;
            }
        }

        private void btnThongTin_Click(object sender,RoutedEventArgs e) {
            InfoManagement info = new InfoManagement();
            info.ShowDialog();
        }

        private void btnDoiMatKhau_Click(object sender,RoutedEventArgs e) {
            ChangePassword change = new ChangePassword(username);
            change.ShowDialog();
        }

        private void btnDiem_Click(object sender,RoutedEventArgs e) {
            GradeManagement grade = new GradeManagement();
            grade.ShowDialog();

        }

        private void btnMonHoc_Click(object sender,RoutedEventArgs e) {
            SubjectManagement subject = new SubjectManagement();
            subject.ShowDialog();
        }
    }
}
