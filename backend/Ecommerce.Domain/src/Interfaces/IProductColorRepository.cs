using Ecommerce.Domain.src.Entities.ProductAggregate;
using Ecommerce.Domain.src.Interface;

namespace Ecommerce.Domain.src.Interfaces
{
    public interface IProductColorRepository : IBaseRepository<ProductColor>
    {
        Task<IEnumerable<ProductColor>> GetColorsByProductIdAsync(Guid productId);
        Task<ProductColor?> GetColorByNameAsync(string colorName);
    }
}