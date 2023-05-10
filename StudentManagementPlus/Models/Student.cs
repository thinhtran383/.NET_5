using System;

namespace StudentManagementPlus.Models {
    public class Student {
        private string studentId;
        private string studentName;
        private string studentGender;
        private string studentAddress;
        private string studentEmail;
        private string studentPhone;
        private DateTime studentBirthday;

        public Student(string studentId,string studentName,string studentGender,string studentAddress,string studentEmail,string studentPhone,DateTime studentBirthday) {
            this.studentId = studentId;
            this.studentName = studentName;
            this.studentGender = studentGender;
            this.studentAddress = studentAddress;
            this.studentEmail = studentEmail;
            this.studentPhone = studentPhone;
            this.studentBirthday = studentBirthday;
        }


        public string StudentId {
            get => studentId;
            set => studentId = value;
        }

        public string StudentName {
            get => studentName; 
            set => studentName = value;
        }

        public string StudentGender {
            get => studentGender;
            set => studentGender = value;
        }

        public string StudentAddress {
            get => studentAddress;
            set => studentAddress = value;
        }

        public string StudentEmail {
            get => studentEmail;
            set => studentEmail = value;
        }

        public string StudentPhone {
            get => studentPhone;
            set => studentPhone = value;
        }

        public DateTime StudentBirthday {
            get => studentBirthday;
            set => studentBirthday = value;
        }
    }
}
