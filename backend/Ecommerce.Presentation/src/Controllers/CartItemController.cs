using Asp.Versioning;
using Ecommerce.Domain.src.Entities.CartAggregate;
using Ecommerce.Service.src.CartItemService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Presentation.src.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[Controller]s")]
    public class CartItemController : AppController<CartItem, CartItemReadDto, CartItemCreateDto, CartItemUpdateDto>
    {
        private readonly ICartItemManagement _cartItemManagement;

        public CartItemController(ICartItemManagement cartItemManagement, ILogger<CartItemController> logger) : base(cartItemManagement, logger)
        {
            _cartItemManagement = cartItemManagement;
        }

    }

}