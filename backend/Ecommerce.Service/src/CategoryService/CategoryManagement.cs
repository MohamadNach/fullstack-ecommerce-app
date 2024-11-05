using Ecommerce.Domain.src.Entities.CategoryAggregate;
using Ecommerce.Domain.src.Interfaces;
using Ecommerce.Service.src.Shared;

namespace Ecommerce.Service.src.CategoryService
{
    public class CategoryManagement : BaseService<Category, CategoryReadDto, CategoryCreateDto, CategoryUpdateDto>, ICategoryManagement
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryManagement(ICategoryRepository categoryRepository) : base(categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<CategoryReadDto> GetCategoryByNameAsync(string categoryName)
        {
            var category = await _categoryRepository.GetCategoryByNameAsync(categoryName);

            if (category == null)
            {
                return null;
                // or return empty DTO instead of null
                //return new CategoryReadDto(); 
            }

            var categoryDto = new CategoryReadDto();
            categoryDto.FromEntity(category);
            return categoryDto;
        }

        public async Task<IEnumerable<CategoryReadDto>> GetCategoriesWithSubcategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllCategoriesWithSubcategoriesAsync();

            // Convert categories to CategoryReadDto and handle subcategories
            return categories.Select(category => new CategoryReadDto
            {
                Id = category.Id,
                CategoryName = category.CategoryName,
                ParentCategoryId = category.ParentCategoryId,
                SubCategories = category.SubCategories.Select(sub => new CategoryReadDto
                {
                    Id = sub.Id,
                    CategoryName = sub.CategoryName,
                    ParentCategoryId = sub.ParentCategoryId
                }).ToList()
            });
        }

    }
}