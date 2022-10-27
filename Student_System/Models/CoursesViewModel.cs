using Student_System.Models;
namespace Student_System.Models
{
    public class CoursesViewModel
    {
        
        public string CourseName { get; set; }
        public DateTime StarDate { get; set; }
        public DateTime EndDate { get; set; }
        public  TimeSpan CourseDuration { get; set; }
        public int StudentCount { get; set; }
    }
}
