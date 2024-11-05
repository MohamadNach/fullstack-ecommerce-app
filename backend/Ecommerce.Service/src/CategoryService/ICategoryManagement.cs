using Ecommerce.Domain.src.Entities.CategoryAggregate;
using Ecommerce.Service.src.Shared;

namespace Ecommerce.Service.src.CategoryService
{
    public interface ICategoryManagement : IBaseService<Category, CategoryReadDto, CategoryCreateDto, CategoryUpdateDto>
    {
        Task<IEnumerable<CategoryReadDto>> GetCategoriesWithSubcategoriesAsync();

    }
}