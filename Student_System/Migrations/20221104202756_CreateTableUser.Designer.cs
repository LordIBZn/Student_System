﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Student_System.Data;

#nullable disable

namespace Student_System.Migrations
{
    [DbContext(typeof(Student_SystemContext))]
    [Migration("20221104202756_CreateTableUser")]
    partial class CreateTableUser
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Student_System.Models.Courses", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<float>("price")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.ToTable("Courses", (string)null);
                });

            modelBuilder.Entity("Student_System.Models.Homework", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("ContentType")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("CoursesId")
                        .HasColumnType("int");

                    b.Property<int>("StudentsId")
                        .HasColumnType("int");

                    b.Property<DateTime>("SubmissionDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CoursesId");

                    b.HasIndex("StudentsId");

                    b.ToTable("Homework", (string)null);
                });

            modelBuilder.Entity("Student_System.Models.Resources", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("typeOfResource")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.ToTable("Resources", (string)null);
                });

            modelBuilder.Entity("Student_System.Models.StudentCourses", b =>
                {
                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.HasKey("StudentId", "CourseId");

                    b.HasIndex("CourseId");

                    b.ToTable("StudentCourses", (string)null);
                });

            modelBuilder.Entity("Student_System.Models.Students", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime?>("BirthDay")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<DateTime>("RegitrationDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Students", (string)null);
                });

            modelBuilder.Entity("Student_System.Models.User", b =>
                {
                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Roles")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("Student_System.Models.Homework", b =>
                {
                    b.HasOne("Student_System.Models.Courses", null)
                        .WithMany("Homeworks")
                        .HasForeignKey("CoursesId");

                    b.HasOne("Student_System.Models.Students", "Students")
                        .WithMany("Homework")
                        .HasForeignKey("StudentsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Students");
                });

            modelBuilder.Entity("Student_System.Models.Resources", b =>
                {
                    b.HasOne("Student_System.Models.Courses", "Course")
                        .WithMany("Resources")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");
                });

            modelBuilder.Entity("Student_System.Models.StudentCourses", b =>
                {
                    b.HasOne("Student_System.Models.Courses", "Course")
                        .WithMany()
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Student_System.Models.Students", "Student")
                        .WithMany()
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("Student_System.Models.Courses", b =>
                {
                    b.Navigation("Homeworks");

                    b.Navigation("Resources");
                });

            modelBuilder.Entity("Student_System.Models.Students", b =>
                {
                    b.Navigation("Homework");
                });
#pragma warning restore 612, 618
        }
    }
}
