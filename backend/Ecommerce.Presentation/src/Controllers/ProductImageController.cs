using Ecommerce.Domain.src.Entities.ProductAggregate;
using Microsoft.AspNetCore.Mvc;
using Ecommerce.Service.src.ProductImageService;
using Microsoft.AspNetCore.Authorization;
using Asp.Versioning;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Presentation.src.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[Controller]s")]
    public class ProductImageController : AppController<ProductImage, ProductImageReadDto, ProductImageCreateDto, ProductImageUpdateDto>
    {
        private readonly IProductImageManagement _productImageManagement;

        public ProductImageController(
            IProductImageManagement productImageManagement,
            ILogger<ProductImageController> logger)
            : base(productImageManagement, logger)
        {
            _productImageManagement = productImageManagement;
        }

        [Authorize(Roles = "Admin")]
        public override async Task<ActionResult<ProductImageReadDto>> CreateAsync(ProductImageCreateDto entity)
        {
            return await base.CreateAsync(entity);
        }

        [Authorize(Roles = "Admin")]
        public override async Task<ActionResult<ProductImageReadDto>> UpdateAsync(Guid id, ProductImageUpdateDto entity)
        {
            return await base.UpdateAsync(id, entity);
        }

        [Authorize(Roles = "Admin")]
        public override async Task<ActionResult> DeleteAsync(Guid id)
        {
            return await base.DeleteAsync(id);
        }

        [HttpGet("images/{productId}")]
        public async Task<ActionResult<IEnumerable<ProductImageReadDto>>> GetImagesByProductIdAsync(Guid productId)
        {
            try
            {
                var entities = await _productImageManagement.GetImagesByProductIdAsync(productId);
                return Ok(entities);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error getting entities!.");
            }
        }

        [HttpGet("main/{productId}")]
        public async Task<ActionResult<ProductImageReadDto>> GetMainImageForProductAsync(Guid productId)
        {
            try
            {
                var entity = await _productImageManagement.GetMainImageForProductAsync(productId);
                return Ok(entity);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error getting entity!.");
            }
        }

        [HttpGet("count/{productId}")]
        public async Task<ActionResult<int>> GetImageCountByProductIdAsync(Guid productId)
        {
            try
            {
                var count = await _productImageManagement.GetImageCountByProductIdAsync(productId);
                return Ok(count);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error getting entity!.");
            }
        }

        [HttpDelete("delete/{productId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<bool>> DeleteImagesByProductIdAsync(Guid productId)
        {
            try
            {
                var result = await _productImageManagement.DeleteImagesByProductIdAsync(productId);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error getting entity!.");
            }
        }
    }
}
