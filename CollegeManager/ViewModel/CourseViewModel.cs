using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CollegeManager.ViewModel
{
    public class CourseViewModel
    {
        public int? CourseId { get; set; }
        public string Title { get; set; }

        public int? TeacherCount { get; set; }
        public int? StudentCount { get; set; }

    }
}