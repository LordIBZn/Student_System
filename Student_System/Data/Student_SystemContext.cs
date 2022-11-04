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
        public DbSet<Student_System.Models.User> Users { get; set; } = default;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Students>().ToTable("Students");
            modelBuilder.Entity<Courses>().ToTable("Courses");
            modelBuilder.Entity<Resources>().ToTable("Resources");
            modelBuilder.Entity<Homework>().ToTable("Homework");
            modelBuilder.Entity<StudentCourses>().ToTable("StudentCourses");
            modelBuilder.Entity<User>().ToTable("Users");

            modelBuilder.Entity<User>().HasNoKey();
            modelBuilder.Entity<StudentCourses>().HasKey(c => new { c.StudentId, c.CourseId });
        }
    }
}
