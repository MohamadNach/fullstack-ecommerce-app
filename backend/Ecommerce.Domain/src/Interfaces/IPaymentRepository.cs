using Ecommerce.Domain.src.Entities.PaymentAggregate;
using Ecommerce.Domain.src.Interface;

namespace Ecommerce.Domain.src.Interfaces
{
    public interface IPaymentRepository : IBaseRepository<Payment>
    {
        Task<IEnumerable<Payment>> GetAllUserPaymentAsync(Guid UserId);
    }
}