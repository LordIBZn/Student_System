using Microsoft.AspNetCore.Identity;

namespace Student_System.Models
{
    public class AspNetUsers : IdentityUser
    {
        public int StudentsId { get; set; }

    }
}
