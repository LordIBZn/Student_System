using System.ComponentModel.DataAnnotations;

namespace Student_System.Models
{
    public class EditRolViewModel
    {
        public string Id { get; set; }
        public EditRolViewModel()
        {
            User = new List<string>();
        }
        [Required(ErrorMessage = "The name of Rol is obligatory")]
        public string RolName { get; set; }

        public List<string> User { get; set; }
    }
}
