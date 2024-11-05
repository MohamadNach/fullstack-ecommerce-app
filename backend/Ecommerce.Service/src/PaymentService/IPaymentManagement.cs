using Ecommerce.Domain.src.Entities.PaymentAggregate;
using Ecommerce.Service.src.Shared;

namespace Ecommerce.Service.src.PaymentService
{
    public interface IPaymentManagement : IBaseService<Payment, PaymentReadDto, PaymentCreateDto, PaymentUpdateDto>
    {
        Task<IEnumerable<PaymentReadDto>> GetAllUserPaymentAsync(Guid UserId);

    }
}