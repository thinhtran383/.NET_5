

namespace StudentManagementPlus.Models {
    public class Account {
        private string Username { get; set; }
        private string Password { get; set; }

        public Account(string Username, string Password) {
            this.Username = Username;
            this.Password = Password;
        }

    }
}