using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CollegeManager.Data.Entities
{
    public class Grade
    {
        public Grade()
        {

        }
        public Grade(int studentId, int value)
        {
            StudentId = studentId;
            Value = value;
        }

        public int GradeId { get; private set; }
        public int StudentId { get; private set; }
        public int Value { get; set; }
    }
}