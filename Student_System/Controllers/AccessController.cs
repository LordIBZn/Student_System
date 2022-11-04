using Microsoft.AspNetCore.Mvc;
using Student_System.Models;
using Student_System.Data;
namespace Student_System.Controllers
{
    public class AccessController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(User _user)
        {
             UserAccess userAccess = new UserAccess();

            var user = userAccess.UserValidate(_user.Email, _user.Password);
            
            if(user != null)
            {
                return RedirectToAction("Index","Home");
            }
            else
            {
                return View();
            }

        }
    }
}
