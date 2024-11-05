using Ecommerce.Domain.src.Entities.CategoryAggregate;
using Ecommerce.Domain.src.Interface;

namespace Ecommerce.Domain.src.Interfaces
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        Task<Category?> GetCategoryByNameAsync(string categoryName);
        Task<List<Category>> GetAllCategoriesWithSubcategoriesAsync();
    }
}
