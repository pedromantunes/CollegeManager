using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CollegeManager.Data.Entities
{
    public class Subject
    {
        public Subject(int courseiId, Teacher teacher, string title)
        {
            CourseId = courseiId;
            Teacher = teacher;
            Title = title;
        }
        public int Id { get; private set; }
        public string Title { get; private set; }
        public int CourseId { get; private set; }
        public Teacher Teacher { get; private set; }
        public ICollection<Student> Students { get; private set; }

        public virtual Course Course { get; set; }
    }
}