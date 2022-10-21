using System.ComponentModel.DataAnnotations;

namespace Student_System.Models
{
    public class Homework
    {
        [Key]
        public int Id { get; set; }
        
        [StringLength(100)]
        public string Content { get; set; }

        [StringLength(100)]
        public string ContentType { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime SubmissionDate { get; set; }

        public Students Students { get; set; }
    }
}
