using Ecommerce.Domain.Enums;
using Ecommerce.Domain.src.Interface;
using Ecommerce.Domain.src.Entities.ProductAggregate;

namespace Ecommerce.Domain.src.Interfaces
{
    public interface IProductSizeRepository : IBaseRepository<ProductSize>
    {
        Task<IEnumerable<ProductSize>> GetSizesByProductIdAsync(Guid productId);
        Task<ProductSize> GetSizeByValueAsync(SizeValue sizeValue);
    }
}