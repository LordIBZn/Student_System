using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Student_System.Models;

namespace Student_System.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        [HttpGet]
        [Route("Admin/CreateRol")]
        public IActionResult CreateRol()
        {
            return View();
        }

        [HttpPost]
        [Route("Admin/CreateRol")]
        public async Task<IActionResult> CreateRol(CreateRolViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = model.RolName
                };

                IdentityResult result = await _roleManager.CreateAsync(identityRole);

                if (result.Succeeded)
                {
                    return RedirectToAction("index", "home");
                }

                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }


        [HttpGet]
        [Route("Admin/GetRoles")]
        public IActionResult GetRoles()
        {
            List<IdentityRole> roles = _roleManager.Roles.ToList();
            return View(roles);
        }
    }
}
