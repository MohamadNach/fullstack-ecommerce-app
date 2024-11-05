using Ecommerce.Domain.src.Entities.ProductAggregate;
using Ecommerce.Domain.src.Interfaces;
using Microsoft.EntityFrameworkCore;
using Ecommerce.Infrastructure.src.Database;
using Ecommerce.Infrastructure.src.Repository;

namespace Ecommerce.Infrastructure.Repositories
{
    public class ProductImageRepository : BaseRepository<ProductImage>, IProductImageRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductImageRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductImage>> GetImagesByProductIdAsync(Guid productId)
        {
            return await _context.ProductImages.AsNoTracking()
                .Where(pi => pi.ProductId == productId)
                .ToListAsync();
        }

        public async Task<ProductImage?> GetMainImageForProductAsync(Guid productId)
        {

            return await _context.ProductImages.AsNoTracking()
                .Where(pi => pi.ProductId == productId && pi.IsDefault)
                .FirstOrDefaultAsync();
        }

        public async Task<int> GetImageCountByProductIdAsync(Guid productId)
        {
            return await _context.ProductImages.AsNoTracking()
                .Where(pi => pi.ProductId == productId)
                .CountAsync();
        }

        public async Task<bool> DeleteImagesByProductIdAsync(Guid productId)
        {
            var images = await _context.ProductImages
                .Where(pi => pi.ProductId == productId)
                .ToListAsync();

            if (images == null)
            {
                return false;
            }

            _context.ProductImages.RemoveRange(images);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
