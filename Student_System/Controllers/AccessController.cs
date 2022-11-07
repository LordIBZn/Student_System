using Microsoft.AspNetCore.Mvc;
using Student_System.Models;
using Student_System.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;


namespace Student_System.Controllers
{
    public class AccessController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(User _user)
        {
             UserAccess userAccess = new UserAccess();

            var user = userAccess.UserValidate(_user.Email, _user.Password);
            
            if(user != null)
            {
                var Claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim("Email", user.Email),
                };

                foreach(string rol in user.Roles) { 
                    Claims.Add(new Claim(ClaimTypes.Role, rol));
                }

                var ClaimsIdentity = new ClaimsIdentity(Claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(ClaimsIdentity));

                return RedirectToAction("Index","Home");
            }
            else
            {
                return View();
            }

        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Access");
        }
    }
}
