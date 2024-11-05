using Ecommerce.Domain.Enums;
using Ecommerce.Domain.src.Entities.ProductAggregate;
using Ecommerce.Domain.src.Interfaces;
using Ecommerce.Infrastructure.src.Database;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.src.Repository
{
    public class ProductColorRepository : BaseRepository<ProductColor>, IProductColorRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductColorRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductColor>> GetColorsByProductIdAsync(Guid productId)
        {
            return await _context.ProductColors.AsNoTracking()
                .Where(pc => pc.ProductId == productId)
                .ToListAsync();
        }

        public async Task<ProductColor?> GetColorByNameAsync(string colorName)
        {
            // Check if the colorName can be parsed to the ColorName enum
            if (Enum.TryParse(typeof(ColorName), colorName, true, out var colorEnum))
            {
                var productColor = await _context.ProductColors.AsNoTracking()
                    .FirstOrDefaultAsync(pc => pc.ColorName == (ColorName)colorEnum);

                return productColor;
            }
            return null;
        }
    }
}
