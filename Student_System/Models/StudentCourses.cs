using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Student_System.Models
{
    public class StudentCourses
    {

        public int StudentId { get; set; }

        public int CourseId { get; set; }

        public virtual Students? Student { get; set; }
        public virtual Courses? Course { get; set; }
    }
}
