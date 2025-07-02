using EcommerceProject.Presentation.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Security.Claims;
using EcommerceProject.DAL.UnitOfWork;

namespace EcommerceProject.Presentation.Controllers
{
    [Authorize]
    public class ReviewController : Controller
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        public async Task<IActionResult> Index(int carId)
        {
            var reviews = await _reviewService.GetReviewsByCarAsync(carId);
            ViewBag.CarId = carId;
            return RedirectToAction("Details","Car", new { id = carId });
        }

        [HttpPost]
        public async Task<IActionResult> AddReview(int carId, int rating, string comment)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _reviewService.AddReviewAsync(userId, carId, rating, comment);
            return RedirectToAction("Index", new { carId });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteReview(int reviewId, int carId)
        {
            await _reviewService.DeleteReviewAsync(reviewId);
            return RedirectToAction("Index", new { carId });
        }
    }
}