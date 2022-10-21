using System.ComponentModel.DataAnnotations;

namespace Student_System.Models
{
    public class Resources
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public string typeOfResource { get; set; }

        [DataType(DataType.Url)]
        public string Url { get; set; }

    }
}
