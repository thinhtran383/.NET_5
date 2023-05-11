using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementPlus.Models {
    public class Grade {
        private string subjectName;
        private string subjectId;
        private float attendanceGrade;
        private float midTermGrade;
        private float finalGrade;

        private string studentId;
        private string studentName;


      

        public Grade(string studentName, string studentId, string subjectId, string subjectName, float attendanceGrade, float midTermGrade, float finalGrade){
            this.studentName = studentName;
            this.studentId = studentId;
            this.subjectId = subjectId;
            this.subjectName = subjectName;
            this.attendanceGrade = attendanceGrade;
            this.midTermGrade = midTermGrade;
            this.finalGrade = finalGrade;
        }

        public string SubjectName {
            get => subjectName;
            set => subjectName = value;
        }

        public string SubjectId {
            get => subjectId;
            set => subjectId = value;
        }

        public float AttendanceGrade {
            get => attendanceGrade;
            set => attendanceGrade = value;
        }

        public float MidTermGrade {
            get => midTermGrade;
            set => midTermGrade = value;
        }

        public float FinalGrade {
            get => finalGrade;
            set => finalGrade = value;
        }


        public string StudentId {
            get => studentId;
            set => studentId = value;
        }

        public string StudentName {
            get => studentName;
            set => studentName = value;
        }
    }
}
