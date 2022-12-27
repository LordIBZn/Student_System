using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Student_System.Models;
namespace Student_System.Data
{
    public class DbInitializer
    {
        public static void Initialize(Student_SystemContext context)
        {
            context.Database.EnsureCreated();

            if (context.Roles.Any())
            {
                return;
            }

            //create roles
            List<IdentityRole> roles = new List<IdentityRole>() {
                 new IdentityRole{ Name = "Admin", NormalizedName = "Admin"},
                new IdentityRole{ Name = "Student", NormalizedName = "Student"}
            };

            //var identityRole = new IdentityRole[]
            //{
            //    new IdentityRole{ Name = "Admin", NormalizedName = "Admin"},
            //    new IdentityRole{ Name = "Student", NormalizedName = "Student"}
            //};

            foreach (IdentityRole ir in roles)
            {
                context.Roles.Add(ir);
            }
            context.SaveChanges();

            //if (context.Users.Any())
            //{
            //    return;
            //}

            //create users
            List<AspNetUsers> users = new List<AspNetUsers>() {
                
                new AspNetUsers
                {
                    UserName ="ibzedgarovillalobos@gmail.com",
                    NormalizedUserName = "Admin",
                    Email = "ibzedgarovillalobos@gmail.com",
                    NormalizedEmail = "IBZEDGARDOVILLALOBOS@GMAIL.COM"
                },
                new AspNetUsers 
                {
                    UserName = "edgar@usuario.com",
                    NormalizedUserName = "Student",
                    Email = "edgar@usuario.com",
                    NormalizedEmail = "EDGAR@USUARIO.COM"
                }
            };
            foreach (AspNetUsers us in users)
            {
                context.Add(us);
            }
            context.SaveChanges();

            //var users = new AspNetUsers[]
            //{
            //};
            //context.Users.AddRange(users);

            //password
            var password = new PasswordHasher<AspNetUsers>();
            users[0].PasswordHash = password.HashPassword(users[0], "Aq!123456");
            users[1].PasswordHash = password.HashPassword(users[1], "Aq!123456");

            //add role to user
            List<IdentityUserRole<string>> userRoles = new List<IdentityUserRole<string>>();
            userRoles.Add(new IdentityUserRole<string>
            {
                UserId = users[0].Id,
                RoleId = roles.First(q => q.Name == "Admin").Id
            });
            userRoles.Add(new IdentityUserRole<string>
            {
                UserId = users[1].Id,
                RoleId = roles.First(q => q.Name == "Student").Id
            });

            if (context.Students.Any())
            {
                return;
            }

            var students = new Students[]
            {
                new Students
                {
                    Name="Pepe",
                    PhoneNumber="",
                    RegitrationDate = DateTime.Parse("2022-10-21"),
                    BirthDay = DateTime.Parse("1997-08-30"),
                    Email = "pepe@user.com"
                },
                new Students
                {
                    Name = "Alhy",
                    PhoneNumber ="",
                    RegitrationDate = DateTime.Parse("2022-10-21"),
                    BirthDay = DateTime.Parse("2000-05-22"),
                    Email = "alhy@user.com"
                },
                new Students
                {
                    Name = "Luis",
                    PhoneNumber = "",
                    RegitrationDate =DateTime.Parse("2022-10-21"),
                    BirthDay=DateTime.Parse("1998-02-10"),
                    Email = "luis@user.com"


                }
            };
            foreach (Students s in students)
            {
                context.Students.Add(s);
            }
            context.SaveChanges();

            if (context.Courses.Any())
            {
                return;
            }

            var courses = new Courses[]
            {
                new Courses
                {
                   
                    Name = "Astrofisica",
                    Description = "",
                    StartDate = DateTime.Parse("2022-03-10"),
                    EndDate = DateTime.Parse("2022-04-10"),
                    price = 15.4f

                },
                new Courses
                {
                    
                    Name = "Calculo",
                    Description="",
                    StartDate = DateTime.Parse("2022-03-10"),
                    EndDate = DateTime.Parse("2022-04-10"),
                    price = 25.5f
                },
                new Courses
                {
                    
                    Name = "Matematicas",
                    Description ="",
                    StartDate = DateTime.Parse("2022-03-10"),
                    EndDate = DateTime.Parse("2022-04-10"),
                    price = 10.99f
                }
            };
            foreach (Courses c in courses)
            {
                context.Courses.Add(c);
            }
            context.SaveChanges();

            if (context.Resources.Any())
            {
                return;
            }

           

            //var resources = new Resources[]
            //{
            //    new Resources
            //    {
                    
            //        Name = "ASP.NET",
            //        typeOfResource ="document",
            //        Url ="https://learn.microsoft.com/es-es/aspnet/core/data/ef-mvc/complex-data-model?view=aspnetcore-6.0#prerequisites"
            //    },
            //    new Resources
            //    {
                    
            //        Name= "C#",
            //        typeOfResource ="presentation",
            //        Url = "https://profesorezequielruizgarcia.files.wordpress.com/2013/08/c-sharp-para-estudiantes.pdf"
            //    },
            //    new Resources
            //    {
                    
            //        Name = "Base de datos SQLServer",
            //        typeOfResource = "video",
            //        Url = "https://www.youtube.com/watch?v=uUdKAYl-F7g&t=6356s"
            //    }
            //};
            //foreach (Resources r in resources)
            //{
            //    context.Resources.Add(r);
            //}
            //context.SaveChanges();

            //if (context.Homework.Any())
            //{
            //    return;
            //}

            //var homework = new Homework[]
            //{
            //    new Homework
            //    {
                    
            //        Content="C# para estudiante",
            //        ContentType = "pdf",
            //        SubmissionDate = DateTime.Parse("2022-10-25")
            //    },
            //    new Homework
            //    {
                    
            //        Content = "Student System",
            //        ContentType = "pdf",
            //        SubmissionDate = DateTime.Parse("2022-10-21")
            //    },
            //    new Homework
            //    {
                    
            //        Content = "Create Models",
            //        ContentType = "pdf",
            //        SubmissionDate = DateTime.Parse("2022-10-21")
            //    }
            //};
            //foreach (Homework h in homework)
            //{
            //    context.Homework.Add(h);
            //}
            //context.SaveChanges();

            //if (context.StudentCourses.Any())
            //{
            //    return;
            //}

            var studentCourse = new StudentCourses[]
            {
                new StudentCourses
                {
                    StudentId = students.Single(s => s.Name =="pepe").Id,
                    CourseId = courses.Single(c => c.Name == "Astrofisica").Id,
                },
                new StudentCourses
                {
                    StudentId = students.Single(s => s.Name =="Alhy").Id,
                    CourseId = courses.Single(c => c.Name =="Calculo").Id
                },
                new StudentCourses
                {
                    StudentId = students.Single(s => s.Name =="Luis").Id,
                    CourseId = courses.Single(c => c.Name =="Matematicas").Id
                }
            };
            foreach (StudentCourses sc in studentCourse)
            {
                context.StudentCourses.Add(sc);
            }
            context.SaveChanges();
        }

    }
}
