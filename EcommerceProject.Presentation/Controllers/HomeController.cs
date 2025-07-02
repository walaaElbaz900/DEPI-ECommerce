using EcommerceProject.DAL.IdentityApplication;
using EcommerceProject.DAL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EcommerceProject.Presentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUnitOfWork _unitOfWork;
        public HomeController(UserManager<ApplicationUser> userManager,IUnitOfWork unitOfWork)
        {
            this.userManager = userManager;
            _unitOfWork = unitOfWork;
        }
        [Authorize]
        public async Task<IActionResult> Index()
        {

            Claim IdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier); // id is in NameIdentifier
            string id = IdClaim.Value;
            ApplicationUser applicationUser = await userManager.FindByIdAsync(id);
            ViewBag.image = applicationUser.Image;
            ViewBag.Cars = _unitOfWork.Car.GetAll();
            return View();
        }
    }
}
