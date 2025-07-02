using EcommerceProject.DAL.IdentityApplication;
using EcommerceProject.Presentation.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EcommerceProject.Presentation.Controllers
{
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        // for image uploading 
        IWebHostEnvironment _environment;
        public ProfileController(UserManager<ApplicationUser> userManager , IWebHostEnvironment environment)
        {
            this.userManager = userManager;
            _environment = environment;
        }
        

        [Authorize]
        public async Task<IActionResult> EditProfileData()
        {
            if (User.Identity.IsAuthenticated == true)
            {
                string Id = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

                ApplicationUser applicationUser = await userManager.FindByIdAsync(Id);

                ProfileViewModel profileViewModel = new ProfileViewModel();
                profileViewModel.Id = Id;
                profileViewModel.Gender = applicationUser.Gender == false ? Gender.Male : Gender.Female; // male = 0 , female = 1
                profileViewModel.Address = applicationUser.Address;
                profileViewModel.PhoneNumber = applicationUser.PhoneNumber;
                profileViewModel.Email = applicationUser.Email;
                profileViewModel.age = applicationUser.age;
                profileViewModel.UserName = applicationUser.UserName;
                profileViewModel.ImageFromDB = applicationUser.Image;
                return View(profileViewModel);
            }
            return Redirect("Account/LogIn");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProfileData(ProfileViewModel profileViewModel)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser applicationUser = await userManager.FindByIdAsync(profileViewModel.Id);
                applicationUser.PhoneNumber = profileViewModel.PhoneNumber;
                applicationUser.Email = profileViewModel.Email;
                applicationUser.Address = profileViewModel.Address;
                applicationUser.age = profileViewModel.age;
                applicationUser.Gender = profileViewModel.Gender == Gender.Male ? false : true; // Male 0, Female 1
                applicationUser.UserName = profileViewModel.UserName;

                applicationUser.Image = ImagePath(profileViewModel.ProfilePicture);

                IdentityResult result = await userManager.UpdateAsync(applicationUser);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                return View(applicationUser);
            }
            return View(profileViewModel);
            
        }

        public string ImagePath(IFormFile image)
        {
            if(image != null)
            {
                string Path = _environment.WebRootPath + "/ProfilePicture/"; // _environment.WebRootPath == wwwroot
                string FileName = image.FileName;
                // if path doesn't exist create it 
                if(!Directory.Exists(Path))
                {
                    Directory.CreateDirectory(Path);
                }
                FileStream fileStream = System.IO.File.Create(Path+FileName);

                image.CopyTo(fileStream);
                fileStream.Flush(); // to clear the buffer
                return FileName;
            }
            else
            {
                return "DefaultProfilePicture.jpg";
            }
        }
    }
}