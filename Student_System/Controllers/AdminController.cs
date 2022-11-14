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
        private readonly UserManager<Student> _userManager;

        public AdminController(RoleManager<IdentityRole> roleManager, 
            UserManager<Student> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
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

        [HttpGet]
        [Route("Admin/EditRol")]
        public async Task<IActionResult> EditRol(string id)
        {
            //buscar por id
            var rol = await _roleManager.FindByIdAsync(id);

            if (rol == null)
            {
                ViewBag.ErrorMessage = $"Rol con el id = {id} no fue econtrado";
                return View("Error");
            }

            var model = new EditRolViewModel
            {
                Id = rol.Id,
                RolName = rol.Name,
            };

            //ontenemos todos los usuarios
            foreach (var user in _userManager.Users)
            {
                if (await _userManager.IsInRoleAsync(user,rol.Name))
                {
                    model.User.Add(user.UserName);
                }
            }
            return View(model);
        }

        [HttpPost]
        [Route("Admin/EditRol")]
        public async Task<IActionResult> EditRol(EditRolViewModel model)
        {
            var rol = await _roleManager.FindByIdAsync(model.Id);

            if (rol == null)
            {
                ViewBag.ErrorMessage = $"Rol con el id = {model.Id} no fue econtrado";
                return View("Error");
            }
            else
            {
                rol.Name = model.RolName;

                var result = await _roleManager.UpdateAsync(rol);

                if (result.Succeeded)
                {
                    return RedirectToAction("GetRoles");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(result);
            }
        }

        [HttpGet]
        [Route("Admin/EditUserRol")]
        public async Task<IActionResult> EditUserRol(string rolId)
        {
            ViewBag.roleId = rolId;

            var rol = await _roleManager.FindByIdAsync(rolId);

            if (rol == null)
            {
                ViewBag.ErrorMessage = $"Rol con el id = {rolId} no fue econtrado";
                return View("Error");
            }

            var model = new List<UserRol>();

            foreach (var user in _userManager.Users)
            {
                var UserRol = new UserRol
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                };

                if (await _userManager.IsInRoleAsync(user, rol.Name))
                {
                    UserRol.IsSelected = true;
                }
                else
                {
                    UserRol.IsSelected = false;
                }
                model.Add(UserRol);
            }
            return View(model);
        }

        [HttpPost]
        [Route("Admin/EditUserRol")]
        public async Task<IActionResult> EditUserRol(List<UserRol> model, string rolId)
        {
            var rol = await _roleManager.FindByIdAsync(rolId);


            if (rol == null)
            {
                ViewBag.ErrorMessage = $"Rol con el id = {rolId} no fue econtrado";
                return View("Error");
            }

            for (int i = 0; i < model.Count; i++)
            {
                var user = await _userManager.FindByIdAsync(model[i].UserId);

                IdentityResult result = null;

                if (model[i].IsSelected && !(await _userManager.IsInRoleAsync(user, rol.Name)))
                {
                    result = await _userManager.AddToRoleAsync(user, rol.Name); 
                }
                else if (!model[i].IsSelected && await _userManager.IsInRoleAsync(user, rol.Name))
                {
                    result = await _userManager.RemoveFromRoleAsync(user, rol.Name);    
                }
                else
                {
                    continue;
                }

                if (result.Succeeded)
                {
                    if (i < (model.Count - 1))
                        continue;
                    else
                        return RedirectToAction("EditRol", new { Id = rolId });

                }
            }
            return RedirectToAction("EditRol", new { Id = rolId });
        }


    }
}
