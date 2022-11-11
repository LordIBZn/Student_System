using System.ComponentModel.DataAnnotations;

namespace Student_System.Models
{
    public class CreateRolViewModel
    {
        [Required(ErrorMessage = "This fild is Obligatory")]
        [Display(Name ="Rol")]
        public string RolName { get; set; }
    }
}
