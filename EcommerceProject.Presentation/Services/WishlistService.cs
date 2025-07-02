using EcommerceProject.DAL.UnitOfWork;
using ECommerce.Models;
using EcommerceProject.Presentation.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcommerceProject.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EcommerceProject.Presentation.Services
{
    public class WishlistService : IWishlistService
    {
        private readonly IUnitOfWork _unitOfWork;

        public WishlistService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddToWishlistAsync(string userId, int carId)
        {
            var existingWishlist = _unitOfWork.Wishlist.GetAll()
                .FirstOrDefault(w => w.UserId == userId && w.CarID == carId);

            if (existingWishlist == null)
            {
                var wishlistItem = new Wishlist
                {
                    UserId = userId,
                    CarID = carId,
                    AddedAt = DateTime.UtcNow
                };
                _unitOfWork.Wishlist.Add(wishlistItem);
                await Task.Run(() => _unitOfWork.Complete());
            }
        }

        public async Task RemoveFromWishlistAsync(string userId, int carId)
        {
            var wishlistItem = _unitOfWork.Wishlist.GetAll()
                .FirstOrDefault(w => w.UserId == userId && w.CarID == carId);

            if (wishlistItem != null)
            {
                _unitOfWork.Wishlist.Delete(wishlistItem.WishlistID);
                await Task.Run(() => _unitOfWork.Complete());
            }
        }

        public async Task<IEnumerable<Wishlist>> GetUserWishlistAsync(string userId)
        {
            return await Task.Run(() => _unitOfWork.Wishlist.QuerableGetAll()
                                                            .Where(w => w.UserId == userId)
                                                            .Include(w => w.Car)
                                                            .ToList());
        }
    }
}
