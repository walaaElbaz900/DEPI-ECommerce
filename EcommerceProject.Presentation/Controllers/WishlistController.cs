using EcommerceProject.Presentation.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Security.Claims;
using EcommerceProject.DAL.UnitOfWork;

namespace EcommerceProject.Presentation.Controllers
{
    [Authorize]
    public class WishlistController : Controller
    {
        private readonly IWishlistService _wishlistService;

        public WishlistController(IWishlistService wishlistService)
        {
            _wishlistService = wishlistService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var wishlist = await _wishlistService.GetUserWishlistAsync(userId);
            return View(wishlist);
        }

        [HttpPost]
        public async Task<IActionResult> AddToWishlist(int carId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _wishlistService.AddToWishlistAsync(userId, carId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromWishlist(int carId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _wishlistService.RemoveFromWishlistAsync(userId, carId);
            return RedirectToAction("Index");
        }
    }
}