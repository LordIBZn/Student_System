namespace Student_System.Models
{
    public class User
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public List<string> Roles { get; set; }
    }
}
