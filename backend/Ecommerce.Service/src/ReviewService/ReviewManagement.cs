
using Ecommerce.Domain.src.Entities.ReviewAggregate;
using Ecommerce.Domain.src.Interfaces;
using Ecommerce.Service.src.Shared;

namespace Ecommerce.Service.src.ReviewService
{
    public class ReviewManagement : BaseService<Review, ReviewReadDto, ReviewCreateDto, ReviewUpdateDto>, IReviewManagement
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewManagement(IReviewRepository reviewRepository) : base(reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task<IEnumerable<ReviewReadDto>> GetReviewsByProductIdAsync(Guid productId)
        {
            try
            {
                var reviews = await _reviewRepository.GetReviewsByProductIdAsync(productId);
                return reviews.Select(r =>
                {
                    var reviewDto = new ReviewReadDto();
                    reviewDto.FromEntity(r);
                    return reviewDto;
                });
            }
            catch (Exception)
            {
                throw new Exception("Error getting reviews!.");
            }
        }

        public async Task<IEnumerable<ReviewReadDto>> GetReviewsByUserIdAsync(Guid userId)
        {
            try
            {
                var reviews = await _reviewRepository.GetReviewsByUserIdAsync(userId);
                return reviews.Select(r =>
                {
                    var reviewDto = new ReviewReadDto();
                    reviewDto.FromEntity(r);
                    return reviewDto;
                });
            }
            catch (Exception)
            {
                throw new Exception("Error getting reviews!.");
            }
        }
    }
}