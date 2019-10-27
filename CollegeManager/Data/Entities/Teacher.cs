using CollegeManager.Data.Entities.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CollegeManager.Data.Entities
{
    public class Teacher : Person
    {
        public Teacher()
        {

        }

        public Teacher(string name, DateTime birthday, float salary, int subjectId)
        {
            Name = name;
            Birthday = birthday;
            Salary = salary;
            SubjectId = subjectId;
        }

        public int TeacherId { get; set; }
        public int SubjectId { get; private set; }
        public float Salary { get; private set; }

        public Subject Subject { get; set; }
    }
}