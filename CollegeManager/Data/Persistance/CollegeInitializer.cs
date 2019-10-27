using CollegeManager.Data.Entities;
using CollegeManager.Data.Persistance;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CollegeManager.Data
{
    public class CollegeInitializer : DropCreateDatabaseIfModelChanges<CollegeDbContext>
    {
        protected override void Seed(CollegeDbContext context)
        {
            var courses = new List<Course>()
            {
                new Course("Matematica"),
                new Course("Fisica"),
                new Course("Quimica")
            };

            courses.ForEach(c => context.Courses.Add(c));
            context.SaveChanges();

            var subjects = new List<Subject>()
            {
                new Subject(1, new Teacher("Manel", DateTime.Now, 1000), "Probabilidades"),
                new Subject(2, new Teacher("Antonio", DateTime.Now, 1100), "Ondas"),
                new Subject(3, new Teacher("Joaquim", DateTime.Now, 1200), "Organica")
            };

            subjects.ForEach(s => context.Subjects.Add(s));
            context.SaveChanges();

            var students = new List<Student>
            {
                new Student(1, "A1", "Joao", DateTime.Now),
                new Student(2, "A2", "Pedro", DateTime.Now),
                new Student(3, "A3", "Manuel", DateTime.Now)
            };

            students.ForEach(s => context.Students.Add(s));
            context.SaveChanges();

            var grades = new List<Grade>
            {
                new Grade(1, 10),
                new Grade(1, 15),
                new Grade(2, 11),
                new Grade(2, 14),
                new Grade(3, 12),
                new Grade(1, 13)
            };

            grades.ForEach(g => context.Grades.Add(g));
            context.SaveChanges();

        }

    }
}