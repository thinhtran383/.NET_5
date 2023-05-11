using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementPlus.Models {
    public class Subject {
        private string subjectId;
        private string subjectName;
        private int subjectCredit;

        public Subject(string subjectId, string subjectName, int subjectCredit) {
            this.subjectId = subjectId;
            this.subjectName = subjectName;
            this.subjectCredit = subjectCredit;
        }

        public string SubjectId {
            get => subjectId;
            set => subjectId = value;
        }

        public string SubjectName {
            get => subjectName;
            set => subjectName = value;
        }

        public int SubjectCredit {
            get => subjectCredit;
            set => subjectCredit = value;
        }
    }
}
