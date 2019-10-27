using CollegeManager.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CollegeManager.Data.Persistance
{
    public class CollegeDbContext : DbContext
    {

        public CollegeDbContext() : base("CollegeDbContext")
        {

        }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Grade> Grades { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            //Courses
            modelBuilder.Entity<Course>().ToTable("Courses");
            modelBuilder.Entity<Course>().HasKey(p=>p.CourseId);
            modelBuilder.Entity<Course>().HasMany(p => p.Subjects);

            //Subjects
            modelBuilder.Entity<Subject>().ToTable("Subjects");
            modelBuilder.Entity<Subject>().HasKey(p => p.SubjectId);
            modelBuilder.Entity<Subject>().HasMany(p => p.Students);

            //Teachers
            modelBuilder.Entity<Teacher>().ToTable("Teachers");
            modelBuilder.Entity<Teacher>().HasKey(p => p.TeacherId);
            modelBuilder.Entity<Teacher>()
                        .HasRequired(t => t.Subject).WithMany()
                        .HasForeignKey(s => s.SubjectId);

            //Students
            modelBuilder.Entity<Student>().ToTable("Students");
            modelBuilder.Entity<Student>().HasKey(p => p.StudentId);
            modelBuilder.Entity<Student>().HasMany(p => p.Grades);

            modelBuilder.Entity<Grade>().ToTable("Grades");
            modelBuilder.Entity<Grade>().HasKey(p => p.GradeId);

        }
    }
}