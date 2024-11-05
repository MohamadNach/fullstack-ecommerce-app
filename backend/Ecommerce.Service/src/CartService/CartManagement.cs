using Ecommerce.Domain.src.Entities.CartAggregate;
using Ecommerce.Domain.src.Interfaces;
using Ecommerce.Service.src.Shared;

namespace Ecommerce.Service.src.CartService
{
    public class CartManagement : BaseService<Cart, CartReadDto, CartCreateDto, CartUpdateDto>, ICartManagement
    {

        private readonly ICartRepository _cartRepository;
        public CartManagement(ICartRepository cartRepository) : base(cartRepository)
        {
            _cartRepository = cartRepository;
        }

    }
}