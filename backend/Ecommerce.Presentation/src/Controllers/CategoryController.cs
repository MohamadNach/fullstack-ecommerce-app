using Asp.Versioning;
using Ecommerce.Domain.src.Entities.CategoryAggregate;
using Ecommerce.Service.src.CategoryService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Presentation.src.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/categories")]
    public class CategoryController : AppController<Category, CategoryReadDto, CategoryCreateDto, CategoryUpdateDto>
    {
        private readonly ICategoryManagement _categoryManagement;


        public CategoryController(
            ICategoryManagement categoryManagement,
            ILogger<CategoryController> logger)
            : base(categoryManagement, logger)
        {
            _categoryManagement = categoryManagement;
        }

        // New endpoint to get all categories with subcategories
        [HttpGet("with-subcategories")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<IEnumerable<CategoryReadDto>>> GetCategoriesWithSubcategoriesAsync()
        {
            try
            {
                var categories = await _categoryManagement.GetCategoriesWithSubcategoriesAsync();
                return Ok(categories);
            }
            catch (Exception ex)
            {
                // Log the error and return an appropriate response
                return StatusCode(500, $"Error getting categories with subcategories: {ex.Message}");
            }
        }

    }
}
