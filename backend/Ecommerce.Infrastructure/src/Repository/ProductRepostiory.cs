
using Ecommerce.Domain.src.ProductAggregate;
using Ecommerce.Domain.src.Interfaces;
using Microsoft.EntityFrameworkCore;
using Ecommerce.Infrastructure.src.Database;

namespace Ecommerce.Infrastructure.src.Repository
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(Guid categoryId)
        {
            return await _context.Products.AsNoTracking()
                .Where(p => p.CategoryId == categoryId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByTitleAsync(string title)
        {
            return await _context.Products.AsNoTracking()
                .Where(p => p.ProductTitle.Contains(title))
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByPriceRangeAsync(decimal minPrice, decimal maxPrice)
        {
            return await _context.Products.AsNoTracking()
                .Where(p => p.Price >= minPrice && p.Price <= maxPrice)
                .ToListAsync();
        }

        // public async Task<IEnumerable<Product>> GetTopSellingProductsAsync(int count)
        // {
        //     return await _context.Products
        //         .OrderByDescending(p => p.SalesCount)
        //         .Take(count)
        //         .ToListAsync();
        // }

        public async Task<IEnumerable<Product>> GetInStockProductsAsync()
        {
            return await _context.Products.AsNoTracking()
                .Where(p => p.Quantity > 0)
                .ToListAsync();
        }

        public Task<IEnumerable<Product>> GetTopSellingProductsAsync(int count)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }
    }
}
