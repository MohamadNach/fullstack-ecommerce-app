using Ecommerce.Domain.src.Entities.ReviewAggregate;
using Ecommerce.Domain.src.Interface;

namespace Ecommerce.Domain.src.Interfaces
{
    public interface IReviewRepository : IBaseRepository<Review>
    {
        Task<IEnumerable<Review>> GetReviewsByProductIdAsync(Guid productId);
        Task<IEnumerable<Review>> GetReviewsByUserIdAsync(Guid userId);
    }
}