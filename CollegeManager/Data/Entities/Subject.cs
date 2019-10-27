using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CollegeManager.Data.Entities
{
    public class Subject
    {
        public Subject()
        {

        }

        public Subject(int courseiId, string title)
        {
            CourseId = courseiId;
            Title = title;
        }

        public int SubjectId { get; set; }
        public string Title { get; private set; }
        public int CourseId { get; private set; }

        public ICollection<Student> Students { get; private set; }

        public Teacher Teacher { get; set; }

        public virtual Course Course { get; set; }
    }
}