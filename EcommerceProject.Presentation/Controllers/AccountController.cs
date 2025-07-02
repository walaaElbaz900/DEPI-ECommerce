using EcommerceProject.DAL.IdentityApplication;
using EcommerceProject.DAL.Interfaces;
using EcommerceProject.Presentation.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EcommerceProject.Presentation.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        // it's being put on the post methods
        // searches in request in form for a key "_RequestVerificationToken" it's a hidden that it's created using tag helper and it prevents bad requests
        // from look alike pages like(me creating a facebook form and send it to my friends and when i take the username,password i redirect them to the real facebook page)
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserViewModel model)
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
                applicationUser.age = model.age;
                applicationUser.Gender = model.Gender == Gender.Male ? false : true; // male = 0 , female = 1
                applicationUser.Image = "DefaultProfilePicture.jpg";
                // save Db
                IdentityResult identityResult = await userManager.CreateAsync(applicationUser, model.Password); // if any errors happend returns list of errors
                if (identityResult.Succeeded) // means there is no errors
                {
                    // assign user role
                    await userManager.AddToRoleAsync(applicationUser, "User");


                    //Cookie
                    await signInManager.SignInAsync(applicationUser, false); // false means it's not a presistent cookie it's for the seesion why=> to make the user make login
                    return RedirectToAction("LogIn", "Account");
                    //return Content("Successful");
                }

                // send errors to end-user to resolve them
                foreach (var item in identityResult.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return View(model);
        }


        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        // it's being put on the post methods
        // searches in request in form for a key "_RequestVerificationToken" it's a hidden that it's created using tag helper and it prevents bad requests
        // from look alike pages like(me creating a facebook form and send it to my friends and when i take the username,password i redirect them to the real facebook page)
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogIn(LoginUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Check if Found in Db
                ApplicationUser applicationUser = await userManager.FindByEmailAsync(model.Email);

                if (applicationUser != null)
                {
                    bool CorrectPassword = await userManager.CheckPasswordAsync(applicationUser, model.Password);
                    if (CorrectPassword)
                    {
                        // Create Cookie
                        // this creates the cookie with only id,name,role
                        //await signInManager.SignInAsync(applicationUser, model.RememberMe);
                        // if i want to add claims like address to the cookie i will use this function
                        // SignInWithClaimsAsync(appUser,presistent,IEnumerable of extra Claims)
                        List<Claim> ListOfExtraClaims = new List<Claim>();
                        ListOfExtraClaims.Add(new Claim("UserAddress", applicationUser.Address));
                        ListOfExtraClaims.Add(new Claim("PhoneNumber", applicationUser.PhoneNumber));
                        await signInManager.SignInWithClaimsAsync(applicationUser, model.RememberMe, ListOfExtraClaims);



                        return RedirectToAction("Index", "Home");
                    }
                }
                ModelState.AddModelError("", "Email Or Password Is Incorrect");
            }
            return View(model);
        }



        public async Task<IActionResult> SignOut()
        {
            await signInManager.SignOutAsync();
            return View(nameof(LogIn));
        }


        // if i want to get values from the cookie i will access it using User Property
        public IActionResult GetValuesFromCookie()
        {
            if (User.Identity.IsAuthenticated == true) // means user got cookie(user loged in)
            {
                Claim IdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier); // id is in NameIdentifier
                string id = IdClaim.Value;
                string name = User.Identity.Name;
                Claim EmailClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);
                string email = EmailClaim.Value;
                return Content($"Id => {id} \nName => {name} \nEmail => {email} ");
            }
            else
            {
                return Content("You Are Not Loged In");
            }
        }
    }
}
