using CollegeManager.Data.Entities.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CollegeManager.Data.Entities
{
    public class Teacher : Person
    {
        public Teacher(string name, DateTime birthday, float salary)
        {
            Name = name;
            Birthday = birthday;
            Salary = salary;
        }

        public int Id { get; private set; }
        public float Salary { get; private set; }
    }
}