using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Student_System.Models
{
    public class Resources
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public string typeOfResource { get; set; }

        [DataType(DataType.Url)]
        public string Url { get; set; }

    }
}
