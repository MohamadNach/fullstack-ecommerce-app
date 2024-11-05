using Ecommerce.Domain.src.Entities.CategoryAggregate;
using Ecommerce.Service.src.Shared;

namespace Ecommerce.Service.src.CategoryService
{
    public class CategoryReadDto : BaseReadDto<Category>
    {
        public string CategoryName { get; set; } = string.Empty;
        public Guid? ParentCategoryId { get; set; }

        // Add this to hold subcategories
        public List<CategoryReadDto> SubCategories { get; set; } = new List<CategoryReadDto>();

        public override void FromEntity(Category entity)
        {
            base.FromEntity(entity);
            ParentCategoryId = entity.ParentCategoryId;
            CategoryName = entity.CategoryName;

            // Map subcategories
            SubCategories = entity.SubCategories.Select(sub => new CategoryReadDto
            {
                Id = sub.Id,
                CategoryName = sub.CategoryName,
                ParentCategoryId = sub.ParentCategoryId
            }).ToList();
        }
    }
    public class CategoryCreateDto : ICreateDto<Category>
    {
        public string CategoryName { get; set; } = string.Empty;
        public Guid? ParentCategoryId { get; set; }
        public List<Category> SubCategories { get; set; } = [];

        public Category CreateEntity()
        {
            return new Category
            {
                CategoryName = CategoryName,
                ParentCategoryId = ParentCategoryId,
                SubCategories = SubCategories
            };
        }
    }
    public class CategoryUpdateDto : IUpdateDto<Category>
    {
        public Guid Id { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public Guid? ParentCategoryId { get; set; }
        public List<Category> SubCategories { get; set; } = [];

        public Category UpdateEntity(Category entity)
        {
            entity.CategoryName = CategoryName;
            entity.ParentCategoryId = ParentCategoryId;
            entity.SubCategories = SubCategories;
            return entity;
        }
    }
}