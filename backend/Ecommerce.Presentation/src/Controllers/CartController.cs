using Asp.Versioning;
using Ecommerce.Domain.src.Entities.CartAggregate;
using Ecommerce.Service.src.CartService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Presentation.src.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[Controller]s")]
    public class CartController : AppController<Cart, CartReadDto, CartCreateDto, CartUpdateDto>
    {
        private readonly ICartManagement _cartManagement;

        public CartController(ICartManagement cartManagement, ILogger<CartController> logger) : base(cartManagement, logger)
        {
            _cartManagement = cartManagement;
        }

    }
}