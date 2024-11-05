using Ecommerce.Domain.src.Entities.ProductAggregate;
using Ecommerce.Service.src.Shared;

namespace Ecommerce.Service.src.ProductColorService
{
    public interface IProductColorManagement : IBaseService<ProductColor, ProductColorReadDto, ProductColorCreateDto, ProductColorUpdateDto>
    {
        Task<IEnumerable<ProductColorReadDto>> GetColorsByProductIdAsync(Guid productId);
        Task<ProductColorReadDto> GetColorByNameAsync(string colorName);

    }
}