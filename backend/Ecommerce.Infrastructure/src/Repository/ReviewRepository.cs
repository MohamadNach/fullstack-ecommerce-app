using Ecommerce.Domain.src.Entities.ReviewAggregate;
using Ecommerce.Domain.src.Interfaces;
using Ecommerce.Infrastructure.src.Database;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.src.Repository
{
    public class ReviewRepository : BaseRepository<Review>, IReviewRepository
    {
        private readonly ApplicationDbContext _context;

        public ReviewRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Review>> GetReviewsByProductIdAsync(Guid productId)
        {
            return await _context.Reviews.AsNoTracking().Where(r => r.Product != null && r.Product.Id == productId).ToListAsync();
        }

        public async Task<IEnumerable<Review>> GetReviewsByUserIdAsync(Guid userId)
        {
            return await _context.Reviews.AsNoTracking().Where(r => r.User != null && r.User.Id == userId).ToListAsync();
        }

    }
}
