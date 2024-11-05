using Asp.Versioning;
using Ecommerce.Domain.src.Entities.ProductAggregate;
using Ecommerce.Service.src.ProductColorService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Presentation.src.Controllers

{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[Controller]s")]
    public class ProductColorController : AppController<ProductColor, ProductColorReadDto, ProductColorCreateDto, ProductColorUpdateDto>
    {
        private readonly IProductColorManagement _productColorManagement;

        public ProductColorController(IProductColorManagement productColorManagement, ILogger<ProductColorController> logger) : base(productColorManagement, logger)
        {
            _productColorManagement = productColorManagement;
        }

        [Authorize(Roles = "Admin")]
        public override async Task<ActionResult<ProductColorReadDto>> CreateAsync(ProductColorCreateDto entity)
        {
            return await base.CreateAsync(entity);
        }

        [Authorize(Roles = "Admin")]
        public override async Task<ActionResult<ProductColorReadDto>> UpdateAsync(Guid id, ProductColorUpdateDto entity)
        {
            return await base.UpdateAsync(id, entity);
        }

        [Authorize(Roles = "Admin")]
        public override async Task<ActionResult> DeleteAsync(Guid id)
        {
            return await base.DeleteAsync(id);
        }

        [HttpGet("{productId}")]
        public async Task<ActionResult<IEnumerable<ProductColorReadDto>>> GetColorsByProductIdAsync(Guid productId)
        {
            try
            {
                var entities = await _productColorManagement.GetColorsByProductIdAsync(productId);
                return Ok(entities);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error getting entities!.");
            }
        }

        [HttpGet("{colorName}")]
        public async Task<ActionResult<ProductColorReadDto>> GetColorByNameAsync(string colorName)
        {
            try
            {
                var entity = await _productColorManagement.GetColorByNameAsync(colorName);
                return Ok(entity);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error getting entity!.");
            }
        }
    }
}
