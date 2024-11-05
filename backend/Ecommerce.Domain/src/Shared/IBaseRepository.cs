using System.Linq.Expressions;
using Ecommerce.Domain.src.Model;
using Ecommerce.Domain.src.Shared;

namespace Ecommerce.Domain.src.Interface
{
    public interface IBaseRepository<T>
        where T : BaseEntity
    {
        public Task<T> CreateAsync(T entity);
        public Task<bool> UpdateByIdAsync(T entity);
        public Task<bool> DeleteByIdAsync(Guid id);
        public Task<T?> GetAsync(Expression<Func<T, bool>>? filter = null);
        public Task<PaginatedResult<T>> GetAllAsync(PaginationOptions paginationOptions);
    }
}