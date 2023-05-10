using System.Windows;
using StudentManagementPlus.Windows;
namespace StudentManagementPlus {
    /// <summary>
    /// Interaction logic for Panel.xaml
    /// </summary>
    public partial class Panel:Window {
        public Panel() {
            InitializeComponent();
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
    }
}
