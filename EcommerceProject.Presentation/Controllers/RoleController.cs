using EcommerceProject.DAL.IdentityApplication;
using EcommerceProject.Presentation.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceProject.Presentation.Controllers
{
    [Authorize(Roles ="Admin")] // this searches in the cookie for role = admin, if true it can access the RoleController 
    public class RoleController : Controller
    {
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;

        public RoleController(RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }
        public IActionResult AddRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddRole(AddRoleViewModel roleViewModel)
        {
            if (ModelState.IsValid)
            {
                ApplicationRole applicationRole = new ApplicationRole();
                applicationRole.Name = roleViewModel.RoleName;

                IdentityResult result = await roleManager.CreateAsync(applicationRole);
                if (result.Succeeded)
                {
                    ViewBag.success = true;
                    return View();
                }
                ViewBag.success = false;
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);

                }
            }
            return View(roleViewModel);
        }


        public IActionResult AddAdmin()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddAdmin(RegisterUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Maping
                ApplicationUser applicationUser = new ApplicationUser();
                applicationUser.UserName = model.UserName;
                applicationUser.Email = model.Email;
                //applicationUser.PasswordHash = model.Password;
                applicationUser.Address = model.Address;
                applicationUser.PhoneNumber = model.PhoneNumber;
                applicationUser.Gender = model.Gender == Gender.Male ? false : true; // male = 0 , female = 1
                // save Db
                IdentityResult identityResult = await userManager.CreateAsync(applicationUser, model.Password); // if any errors happend returns list of errors
                if (identityResult.Succeeded) // means there is no errors
                {
                    // assign admin role
                    await userManager.AddToRoleAsync(applicationUser, "Admin");

                    return RedirectToAction("Index", "Home");
                }

                // send errors to end-user to resolve them
                foreach (var item in identityResult.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return View(model);
        }
    }
}
