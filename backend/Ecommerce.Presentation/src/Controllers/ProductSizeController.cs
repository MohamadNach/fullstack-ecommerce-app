using Microsoft.AspNetCore.Mvc;
using Ecommerce.Domain.Enums;
using Ecommerce.Service.src.ProductSizeService;
using Ecommerce.Domain.src.Entities.ProductAggregate;
using Microsoft.AspNetCore.Authorization;
using Asp.Versioning;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Presentation.src.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[Controller]s")]
    public class ProductSizeController : AppController<ProductSize, ProductSizeReadDto, ProductSizeCreateDto, ProductSizeUpdateDto>
    {
        private readonly IProductSizeManagement _productSizeManagement;

        public ProductSizeController(
            IProductSizeManagement productSizeManagement,
            ILogger<ProductSizeController> logger) : base(productSizeManagement, logger)
        {
            _productSizeManagement = productSizeManagement;
        }

        [Authorize(Roles = "Admin")]
        public override async Task<ActionResult<ProductSizeReadDto>> CreateAsync(ProductSizeCreateDto entity)
        {
            return await base.CreateAsync(entity);
        }

        [Authorize(Roles = "Admin")]
        public override async Task<ActionResult<ProductSizeReadDto>> UpdateAsync(Guid id, ProductSizeUpdateDto entity)
        {
            return await base.UpdateAsync(id, entity);
        }

        [Authorize(Roles = "Admin")]
        public override async Task<ActionResult> DeleteAsync(Guid id)
        {
            return await base.DeleteAsync(id);
        }

        [HttpGet("Product/{productId:guid}")]
        public async Task<IActionResult> GetSizesByProductIdAsync(Guid productId)
        {
            var sizes = await _productSizeManagement.GetSizesByProductIdAsync(productId);
            if (sizes == null)
            {
                return NotFound("No sizes found for this product.");
            }
            return Ok(sizes);
        }

        [HttpGet("Size/{sizeValue}")]
        public async Task<IActionResult> GetSizeByValueAsync(SizeValue sizeValue)
        {
            var size = await _productSizeManagement.GetSizeByValueAsync(sizeValue);
            if (size == null)
            {
                return NotFound("No size found for this value.");
            }
            return Ok(size);
        }
    }
}
