using CollegeManager.Data.Entities.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CollegeManager.Data.Entities
{
    public class Student : Person
    {
        public Student()
        {

        }

        public Student(int subjectId, string registrationNumber, string name, DateTime birthday)
        {
            SubjectId = subjectId;
            RegistrationNumber = registrationNumber;
            Name = name;
            Birthday = birthday;
        }

        public int StudentId { get; set; }
        public int SubjectId { get; private set; }
        public string RegistrationNumber { get; private set; }
        public ICollection<Grade> Grades { get; private set; }

        public virtual Subject Subject { get; set; }
    }
}