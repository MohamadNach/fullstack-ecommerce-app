using Ecommerce.Domain.src.ProductAggregate;
using Microsoft.AspNetCore.Mvc;
using Ecommerce.Service.src.ProductService;
using Microsoft.AspNetCore.Authorization;
using Asp.Versioning;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Presentation.src.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[Controller]s")]
    public class ProductController : AppController<Product, ProductReadDto, ProductCreateDto, ProductUpdateDto>
    {
        private readonly IProductManagement _productManagement;

        public ProductController(IProductManagement productManagement, ILogger<ProductController> logger) : base(productManagement, logger)
        {
            _productManagement = productManagement;
        }

        [Authorize(Roles = "Admin")]
        public override async Task<ActionResult<ProductReadDto>> CreateAsync(ProductCreateDto entity)
        {
            return await base.CreateAsync(entity);
        }

        [Authorize(Roles = "Admin")]
        public override async Task<ActionResult<ProductReadDto>> UpdateAsync(Guid id, ProductUpdateDto entity)
        {
            return await base.UpdateAsync(id, entity);
        }

        [Authorize(Roles = "Admin")]
        public override async Task<ActionResult> DeleteAsync(Guid id)
        {
            return await base.DeleteAsync(id);
        }

        [HttpGet("category/{categoryId}")]
        public async Task<IActionResult> GetProductsByCategory(Guid categoryId)
        {
            var products = await _productManagement.GetProductsByCategoryAsync(categoryId);
            return Ok(products);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchProducts([FromQuery] string title)
        {
            var products = await _productManagement.SearchProductsByTitleAsync(title);
            return Ok(products);
        }

        [HttpGet("price-range")]
        public async Task<IActionResult> GetProductsByPriceRange([FromQuery] decimal minPrice, [FromQuery] decimal maxPrice)
        {
            var products = await _productManagement.GetProductsByPriceRangeAsync(minPrice, maxPrice);
            return Ok(products);
        }

        // [HttpGet("top-selling")]
        // public async Task<IActionResult> GetTopSellingProducts([FromQuery] int count)
        // {
        //     var products = await _productManagement.GetTopSellingProductsAsync(count);
        //     return Ok(products);
        // }

        [HttpGet("in-stock")]
        public async Task<IActionResult> GetInStockProducts()
        {
            var products = await _productManagement.GetInStockProductsAsync();
            return Ok(products);
        }

    }
}
