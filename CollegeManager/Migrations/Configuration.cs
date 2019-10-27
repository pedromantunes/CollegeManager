namespace CollegeManager.Migrations
{
    using CollegeManager.Data.Entities;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CollegeManager.Data.Persistance.CollegeDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CollegeManager.Data.Persistance.CollegeDbContext context)
        {
            var courses = new List<Course>
            {
                new Course("Matematica"),
                new Course("Fisica"),
                new Course("Quimica")
            };

            courses.ForEach(c => context.Courses.Add(c));
            context.SaveChanges();

            var subjects = new List<Subject>
            {
                new Subject(1, "Probabilidades"),
                new Subject(2, "Ondas"),
                new Subject(3, "Organica")
            };

            subjects.ForEach(s => context.Subjects.Add(s));
            context.SaveChanges();

            var teachers = new List<Teacher>
            {

                new Teacher("Manel", DateTime.Now, 1000,1),
                new Teacher("Antonio", DateTime.Now, 1100,2),
                new Teacher("Joaquim", DateTime.Now, 1200,3)
            };

            teachers.ForEach(s => context.Teachers.Add(s));
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
