using System.ComponentModel.DataAnnotations;

namespace Student_System.Models.ViewModels
{
    public class StudentNumberCourseViewModel
    {
        [StringLength(100)]
        public string StudentName { get; set; }
        public int NumCourse { get; set; }

        [DataType(DataType.Currency)]
        public float TotalPrice { get; set; }
        [DataType(DataType.Currency)]
        public float AvaregePrice { get; set; }
    }
}
