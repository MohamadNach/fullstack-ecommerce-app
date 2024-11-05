using Ecommerce.Domain.src.Entities.ReviewAggregate;
using Ecommerce.Service.src.Shared;

namespace Ecommerce.Service.src.ReviewService
{
    public interface IReviewManagement : IBaseService<Review, ReviewReadDto, ReviewCreateDto, ReviewUpdateDto>
    {
        Task<IEnumerable<ReviewReadDto>> GetReviewsByProductIdAsync(Guid productId);
        Task<IEnumerable<ReviewReadDto>> GetReviewsByUserIdAsync(Guid userId);
    }
}