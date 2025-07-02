using EcommerceProject.DAL.UnitOfWork;
using ECommerce.Models;
using EcommerceProject.Presentation.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcommerceProject.DAL.Interfaces;

namespace EcommerceProject.Presentation.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReviewService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddReviewAsync(string userId, int carId, int rating, string comment)
        {
            var review = new Review
            {
                UserId = userId,
                CarID = carId,
                Rating = rating,
                Comment = comment,
                CreatedAt = DateTime.UtcNow
            };
            _unitOfWork.Review.Add(review);
            await Task.Run(() => _unitOfWork.Complete());
        }

        public async Task<IEnumerable<Review>> GetReviewsByCarAsync(int carId)
        {
            return await Task.Run(() => _unitOfWork.Review.GetAll()
                .Where(r => r.CarID == carId)
                .ToList());
        }

        public async Task<Review> GetReviewByIdAsync(int reviewId)
        {
            return await Task.Run(() => _unitOfWork.Review.GetById(reviewId));
        }

        public async Task DeleteReviewAsync(int reviewId)
        {
            _unitOfWork.Review.Delete(reviewId);
            await Task.Run(() => _unitOfWork.Complete());
        }
    }
}