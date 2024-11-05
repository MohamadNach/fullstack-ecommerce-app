using Ecommerce.Domain.src.Entities.CartAggregate;
using Ecommerce.Service.src.Shared;

namespace Ecommerce.Service.src.CartItemService
{
    public interface ICartItemManagement : IBaseService<CartItem, CartItemReadDto, CartItemCreateDto, CartItemUpdateDto>
    {

    }
}