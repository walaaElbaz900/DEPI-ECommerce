using System.Threading.Tasks;
using ECommerce.Models;

namespace EcommerceProject.Presentation.Services.Interfaces
{
    public interface IWishlistService
    {
        Task AddToWishlistAsync(string userId, int carId);
        Task RemoveFromWishlistAsync(string userId, int carId);
        Task<IEnumerable<Wishlist>> GetUserWishlistAsync(string userId);
    }
}