using Ecommerce.Domain.src.Entities.CategoryAggregate;
using Ecommerce.Domain.src.Interfaces;
using Ecommerce.Infrastructure.src.Database;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.src.Repository
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Category?> GetCategoryByNameAsync(string categoryName)
        {
            return await _context.Categories.AsNoTracking()
                .FirstOrDefaultAsync(c => c.CategoryName == categoryName);
        }

        public async Task<List<Category>> GetAllCategoriesWithSubcategoriesAsync()
        {
            var categories = await _context.Categories.AsNoTracking().ToListAsync();

            // Create a dictionary to map parent-child relationships
            var categoryDict = categories.ToDictionary(c => c.Id);

            foreach (var category in categories)
            {
                if (category.ParentCategoryId.HasValue && categoryDict.ContainsKey(category.ParentCategoryId.Value))
                {
                    var parentCategory = categoryDict[category.ParentCategoryId.Value];
                    parentCategory.SubCategories.Add(category);
                }
            }

            return categories.Where(c => !c.ParentCategoryId.HasValue).ToList();
        }
    }
}
