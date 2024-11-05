using Ecommerce.Domain.src.Entities.CartAggregate;
using Ecommerce.Domain.src.Interfaces;
using Ecommerce.Infrastructure.src.Database;

namespace Ecommerce.Infrastructure.src.Repository
{
    public class CartRepository : BaseRepository<Cart>, ICartRepository
    {
        private readonly ApplicationDbContext _context;

        public CartRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}