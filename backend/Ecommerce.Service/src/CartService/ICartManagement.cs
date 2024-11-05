using Ecommerce.Domain.src.Entities.CartAggregate;
using Ecommerce.Service.src.Shared;

namespace Ecommerce.Service.src.CartService
{
    public interface ICartManagement : IBaseService<Cart, CartReadDto, CartCreateDto, CartUpdateDto>
    {

    }
}