using CollegeManager.Data.Entities.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CollegeManager.ViewModel
{
    public class TeacherViewModel : Person
    {
        public int? TeacherId { get; set; }
        public int SubjectId { get; set; }

        public float Salaray { get; set; }
    }
}