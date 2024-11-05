using Ecommerce.Domain.src.Entities.ProductAggregate;
using Ecommerce.Domain.src.Interfaces;
using Microsoft.EntityFrameworkCore;
using Ecommerce.Infrastructure.src.Database;
using Ecommerce.Domain.Enums;
using Ecommerce.Infrastructure.src.Repository;

namespace Ecommerce.Infrastructure.Repositories
{
    public class ProductSizeRepository : BaseRepository<ProductSize>, IProductSizeRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductSizeRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductSize>> GetSizesByProductIdAsync(Guid productId)
        {
            return await _context.ProductSizes.AsNoTracking().Where(ps => ps.Product != null && ps.Product.Id == productId).ToListAsync();
        }

        public async Task<ProductSize> GetSizeByValueAsync(SizeValue size)
        {
            var productSize = await _context.ProductSizes
                .AsNoTracking()
                .FirstOrDefaultAsync(ps => ps.SizeValue == size);

            if (productSize == null)
            {
                throw new InvalidOperationException("No ProductSize found with the specified SizeValue.");
            }
            return productSize;
        }
    }
}

