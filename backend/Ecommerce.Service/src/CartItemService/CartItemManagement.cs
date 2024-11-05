using Ecommerce.Domain.src.Entities.CartAggregate;
using Ecommerce.Domain.src.Entities.UserAggregate;
using Ecommerce.Domain.src.Interfaces;
using Ecommerce.Service.src.Shared;

namespace Ecommerce.Service.src.CartItemService
{
    public class CartItemManagement : BaseService<CartItem, CartItemReadDto, CartItemCreateDto, CartItemUpdateDto>, ICartItemManagement
    {
        private readonly ICartItemRepository _cartItemRepository;
        public CartItemManagement(ICartItemRepository cartItemRepository) : base(cartItemRepository)
        {
            _cartItemRepository = cartItemRepository;
        }
    }
}