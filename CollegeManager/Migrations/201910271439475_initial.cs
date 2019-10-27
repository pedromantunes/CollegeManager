namespace CollegeManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        CourseId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.CourseId);
            
            CreateTable(
                "dbo.Subjects",
                c => new
                    {
                        SubjectId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        CourseId = c.Int(nullable: false),
                        Teacher_TeacherId = c.Int(),
                    })
                .PrimaryKey(t => t.SubjectId)
                .ForeignKey("dbo.Teachers", t => t.Teacher_TeacherId)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .Index(t => t.CourseId)
                .Index(t => t.Teacher_TeacherId);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        StudentId = c.Int(nullable: false, identity: true),
                        SubjectId = c.Int(nullable: false),
                        RegistrationNumber = c.String(),
                        Name = c.String(),
                        Birthday = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.StudentId)
                .ForeignKey("dbo.Subjects", t => t.SubjectId, cascadeDelete: true)
                .Index(t => t.SubjectId);
            
            CreateTable(
                "dbo.Grades",
                c => new
                    {
                        GradeId = c.Int(nullable: false, identity: true),
                        StudentId = c.Int(nullable: false),
                        Value = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.GradeId)
                .ForeignKey("dbo.Students", t => t.StudentId, cascadeDelete: true)
                .Index(t => t.StudentId);
            
            CreateTable(
                "dbo.Teachers",
                c => new
                    {
                        TeacherId = c.Int(nullable: false, identity: true),
                        SubjectId = c.Int(nullable: false),
                        Salary = c.Single(nullable: false),
                        Name = c.String(),
                        Birthday = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.TeacherId)
                .ForeignKey("dbo.Subjects", t => t.SubjectId, cascadeDelete: true)
                .Index(t => t.SubjectId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Subjects", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.Subjects", "Teacher_TeacherId", "dbo.Teachers");
            DropForeignKey("dbo.Teachers", "SubjectId", "dbo.Subjects");
            DropForeignKey("dbo.Students", "SubjectId", "dbo.Subjects");
            DropForeignKey("dbo.Grades", "StudentId", "dbo.Students");
            DropIndex("dbo.Teachers", new[] { "SubjectId" });
            DropIndex("dbo.Grades", new[] { "StudentId" });
            DropIndex("dbo.Students", new[] { "SubjectId" });
            DropIndex("dbo.Subjects", new[] { "Teacher_TeacherId" });
            DropIndex("dbo.Subjects", new[] { "CourseId" });
            DropTable("dbo.Teachers");
            DropTable("dbo.Grades");
            DropTable("dbo.Students");
            DropTable("dbo.Subjects");
            DropTable("dbo.Courses");
        }
    }
}
