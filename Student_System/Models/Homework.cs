using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Student_System.Models
{
    public class Homework
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(100)]
        [Required]
        public string FileName { get; set; }

        [StringLength(100)]
        public string Content { get; set; }

        [StringLength(100)]
        public string ContentType { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime SubmissionDate { get; set; }

        public string? Path { get; set; }

        public int StudentsId { get; set; }

        public virtual Students? Students { get; set; }

        [NotMapped]
        public IFormFile? File { get; set; }
    }
}
