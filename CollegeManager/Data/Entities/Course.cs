using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CollegeManager.Data.Entities
{
    public class Course
    {
        public Course(string title)
        {
            Title = title;
        }
        public int Id { get; private set; }
        public string Title { get; private set; }
        public ICollection<Subject> Subjects { get; private set; }
    }
}