using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Student_System.Models
{
    public class StudentCourses
    {

        public int StudentId { get; set; }

        public int CourseId { get; set; }

        public Students Students { get; set; }
        public Courses Courses  { get; set; }
    }
}
