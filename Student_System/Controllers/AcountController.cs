using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Student_System.Data;
using Student_System.Models;

namespace Student_System.Controllers
{
    [AllowAnonymous]
    public class AcountController : Controller
    {
        private readonly Student_SystemContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AspNetUsers> _userManager;

        public AcountController(RoleManager<IdentityRole> roleManager,
            UserManager<AspNetUsers> userManager, Student_SystemContext context)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _context = context;
        }

        [HttpGet]
        [Route("Acount/Index")]
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("Acount/EmailConfirmation")]
        public async Task<IActionResult> EmailConfirmation(string userId, string token)
        {

            if (userId == null || token == null)
            {
                return RedirectToAction("index", "home");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"El usuario con id {userId} es invalido";
                return View("Error");
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return View();
            }
            return View("Error");
        }
    }
}
