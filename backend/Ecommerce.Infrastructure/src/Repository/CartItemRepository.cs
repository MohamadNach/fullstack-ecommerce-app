using Ecommerce.Domain.src.Entities.CartAggregate;
using Ecommerce.Domain.src.Interfaces;
using Ecommerce.Infrastructure.src.Database;

namespace Ecommerce.Infrastructure.src.Repository
{
    public class CartItemRepository : BaseRepository<CartItem>, ICartItemRepository
    {
        private readonly ApplicationDbContext _context;

        public CartItemRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}