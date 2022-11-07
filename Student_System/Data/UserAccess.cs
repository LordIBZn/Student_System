using Student_System.Models;

namespace Student_System.Data
{
    public class UserAccess
    {
        public List<User> ListUsers()
        {
            return new List<User>
            {
                new User { Name = "Edgar", Email = "edgar@usuario.com", Password = "12345", Roles = new List<string> {"Administrador" } },
                new User { Name = "Alhy",  Email = "alhy@user.com", Password = "12345", Roles = new List<string> {"Employee" } },
                new User { Name = "Ivett", Email = "ivett@user.com", Password = "12345", Roles = new List<string> {"Supervisor" } }
            };

        }

        public User UserValidate(string _email, string _password)
        {
            return ListUsers().Where(item => item.Email == _email && item.Password == _password).FirstOrDefault();
        }

    }
}
