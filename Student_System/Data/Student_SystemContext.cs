using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Student_System.Models;

namespace Student_System.Data
{
    public class Student_SystemContext : DbContext
    {
        public Student_SystemContext (DbContextOptions<Student_SystemContext> options)
            : base(options)
        {
        }

        public DbSet<Student_System.Models.Students> Students { get; set; } = default!;
        public DbSet<Student_System.Models.Courses> Courses { get; set; } = default!;
        public DbSet<Student_System.Models.Resources> Resources { get; set; } = default!;
        public DbSet<Student_System.Models.Homework> Homework { get; set; } = default!;
        public DbSet<Student_System.Models.StudentCourses> StudentCourses { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Students>().ToTable("Students");
            modelBuilder.Entity<Courses>().ToTable("Courses");
            modelBuilder.Entity<Resources>().ToTable("Resources");
            modelBuilder.Entity<Homework>().ToTable("Homework");
            modelBuilder.Entity<StudentCourses>().ToTable("StudentCourses");

            modelBuilder.Entity<StudentCourses>().HasKey(c => new { c.StudentId, c.CourseId });

            //modelBuilder.Entity<Students>().HasData(
            //         new Students
            //         {
            //             Name = "Pepe",
            //             PhoneNumber = "",
            //             RegitrationDate = DateTime.Parse("2022-10-21"),
            //             BirthDay = DateTime.Parse("1997-08-30")
            //         },
            //        new Students
            //        {
            //            Name = "Alhy",
            //            PhoneNumber = "",
            //            RegitrationDate = DateTime.Parse("2022-10-21"),
            //            BirthDay = DateTime.Parse("2000-05-22")
            //        },
            //        new Students
            //        {
            //            Name = "Luis",
            //            PhoneNumber = "",
            //            RegitrationDate = DateTime.Parse("2022-10-21"),
            //            BirthDay = DateTime.Parse("1998-02-10")

            //        }
            //   );
            //modelBuilder.Entity<Courses>().HasData(
            //        new Courses
            //        {
            //            Id = 1050,
            //            Name = "Astrofisica",
            //            Description = "",
            //            StartDate = DateTime.Parse("2022-03-10"),
            //            EndDate = DateTime.Parse("2022-04-10"),
            //            price = 15.4f

            //        },
            //        new Courses
            //        {
            //            Id = 4022,
            //            Name = "Calculo",
            //            Description = "",
            //            StartDate = DateTime.Parse("2022-03-10"),
            //            EndDate = DateTime.Parse("2022-04-10"),
            //            price = 25.5f
            //        },
            //        new Courses
            //        {
            //            Id = 4041,
            //            Name = "Matematicas",
            //            Description = "",
            //            StartDate = DateTime.Parse("2022-03-10"),
            //            EndDate = DateTime.Parse("2022-04-10"),
            //            price = 10.99f
            //        }
            //    );
            //modelBuilder.Entity<Resources>().HasData(
            //        new Resources
            //        {
            //            Name = "ASP.NET",
            //            typeOfResource = "document",
            //            Url = "https://learn.microsoft.com/es-es/aspnet/core/data/ef-mvc/complex-data-model?view=aspnetcore-6.0#prerequisites"
            //        },
            //        new Resources
            //        {
            //            Name = "C#",
            //            typeOfResource = "presentation",
            //            Url = "https://profesorezequielruizgarcia.files.wordpress.com/2013/08/c-sharp-para-estudiantes.pdf"
            //        },
            //        new Resources
            //        {
            //            Name = "Base de datos SQLServer",
            //            typeOfResource = "video",
            //            Url = "https://www.youtube.com/watch?v=uUdKAYl-F7g&t=6356s"
            //        }
            //    );
            //modelBuilder.Entity<Homework>().HasData(
            //         new Homework
            //         {
            //             Content = "C# para estudiante",
            //             ContentType = "pdf",
            //             SubmissionDate = DateTime.Parse("2022-10-25")
            //         },
            //        new Homework
            //        {
            //            Content = "Student System",
            //            ContentType = "pdf",
            //            SubmissionDate = DateTime.Parse("2022-10-21")
            //        },
            //        new Homework
            //        {
            //            Content = "Create Models",
            //            ContentType = "pdf",
            //            SubmissionDate = DateTime.Parse("2022-10-21")
            //        }
            //    );
            //modelBuilder.Entity<StudentCourses>().HasData(
            //        new StudentCourses
            //        {
            //            StudentId = Students.Single(s => s.Name == "pepe").Id,
            //            CourseId = Courses.Single(c => c.Name == "Astrofisica").Id,
            //        },
            //        new StudentCourses
            //        {
            //            StudentId = Students.Single(s => s.Name == "Alhy").Id,
            //            CourseId = Courses.Single(c => c.Name == "Calculo").Id
            //        },
            //        new StudentCourses
            //        {
            //            StudentId = Students.Single(s => s.Name == "Luis").Id,
            //            CourseId = Courses.Single(c => c.Name == "Matematicas").Id
            //        }
            //    );

        }
    }
}
