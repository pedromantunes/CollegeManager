using CollegeManager.Data.Entities.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CollegeManager.ViewModel
{
    public class StudentViewModel : Person
    {
        public int? StudentId { get; set; }
        public int SubjectId { get; set; }
        public string RegistrationNumber { get; set; }
    }
}