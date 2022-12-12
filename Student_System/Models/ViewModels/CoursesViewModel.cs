using System.ComponentModel.DataAnnotations;

namespace Student_System.Models.ViewModels
{
    public class CoursesViewModel
    {

        [StringLength(100)]
        public string CourseName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StarDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd} " + "Days")]
        public TimeSpan CourseDuration { get; set; }
        public int StudentCount { get; set; }
    }
}
