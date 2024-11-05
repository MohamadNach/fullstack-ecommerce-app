using Asp.Versioning;
using Ecommerce.Domain.src.Entities.ShipmentAggregate;
using Ecommerce.Domain.src.Model;
using Ecommerce.Domain.src.Shared;
using Ecommerce.Service.src.ShipmentService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Presentation.src.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[Controller]s")]
    public class ShipmentController : AppController<Shipment, ShipmentReadDto, ShipmentCreateDto, ShipmentUpdateDto>
    {
        private readonly IShipmentManagement _shipmentManagement;

        public ShipmentController(IShipmentManagement shipmentManagement, ILogger<ShipmentController> logger) : base(shipmentManagement, logger)
        {
            _shipmentManagement = shipmentManagement;
        }

        [Authorize(Roles = "Admin")]
        public override async Task<ActionResult<ShipmentReadDto>> CreateAsync(ShipmentCreateDto entity)
        {
            return await base.CreateAsync(entity);
        }

        [Authorize(Roles = "Admin")]
        public override async Task<ActionResult<ShipmentReadDto>> UpdateAsync(Guid id, ShipmentUpdateDto entity)
        {
            return await base.UpdateAsync(id, entity);
        }

        [Authorize(Roles = "Admin")]
        public override async Task<ActionResult> DeleteAsync(Guid id)
        {
            return await base.DeleteAsync(id);
        }

        [Authorize(Roles = "Admin")]
        public override async Task<ActionResult<PaginatedResult<ShipmentReadDto>>> GetAllAsync(PaginationOptions paginationOptions)
        {
            return await base.GetAllAsync(paginationOptions);
        }

        [Authorize(Roles = "Admin")]
        public override async Task<ActionResult<ShipmentReadDto>> GetByIdAsync(Guid id)
        {
            return await base.GetByIdAsync(id);
        }

        // GET: api/v1/Shipment/Order/{orderId}
        [HttpGet("Order/{orderId:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetShipmentsByOrderId(Guid orderId)
        {
            var shipments = await _shipmentManagement.GetShipmentsByOrderIdAsync(orderId);
            if (shipments == null)
            {
                return NotFound("No shipments found for this order.");
            }
            return Ok(shipments);
        }


    }


}
