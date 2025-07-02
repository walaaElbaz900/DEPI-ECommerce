using System.Threading.Tasks;
using ECommerce.Models;

namespace EcommerceProject.Presentation.Services.Interfaces
{
    public interface IReviewService
    {
        Task AddReviewAsync(string userId, int carId, int rating, string comment);
        Task<IEnumerable<Review>> GetReviewsByCarAsync(int carId);
        Task<Review> GetReviewByIdAsync(int reviewId);
        Task DeleteReviewAsync(int reviewId);
    }
}