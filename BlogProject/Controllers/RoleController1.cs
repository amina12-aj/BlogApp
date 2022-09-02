using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.Controllers
{
    [Authorize]
    public class RoleController1 : Controller
    {
        private RoleManager<IdentityRole> _roleManager;
        public RoleController1(RoleManager<IdentityRole> roleMgr)
        {
            _roleManager = roleMgr;
        }

        public ViewResult Index() => View(_roleManager.Roles);

        private void Errors(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
                ModelState.AddModelError("", error.Description);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(string Rolename)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(Rolename));
                if (result.Succeeded)
                    return RedirectToAction("Index");
                else
                    Errors(result);
            }
            return View(Rolename);
        }
       
    }
}