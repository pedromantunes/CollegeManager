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
            modelBuilder.Entity<Course>().HasKey(p=>p.Id);
            modelBuilder.Entity<Course>().HasMany(p => p.Subjects);

            //Subjects
            modelBuilder.Entity<Subject>().ToTable("Courses");
            modelBuilder.Entity<Subject>().HasKey(p => p.Id);
            modelBuilder.Entity<Subject>().HasMany(p => p.Students);
            modelBuilder.Entity<Subject>().HasRequired(p => p.Teacher);

            //Teachers
            modelBuilder.Entity<Teacher>().ToTable("Teachers");
            modelBuilder.Entity<Teacher>().HasKey(p => p.Id);

            //Students
            modelBuilder.Entity<Student>().ToTable("Students");
            modelBuilder.Entity<Student>().HasKey(p => p.Id);
            modelBuilder.Entity<Student>().HasMany(p => p.Grades);

        }
    }
}