using Ecommerce.Domain.src.Entities.PaymentAggregate;
using Ecommerce.Domain.src.Interfaces;
using Ecommerce.Service.src.Shared;

namespace Ecommerce.Service.src.PaymentService
{
    public class PaymentManagement : BaseService<Payment, PaymentReadDto, PaymentCreateDto, PaymentUpdateDto>, IPaymentManagement
    {
        private readonly IPaymentRepository _paymentRepository;

        public PaymentManagement(IPaymentRepository paymentRepository) : base(paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<IEnumerable<PaymentReadDto>> GetAllUserPaymentAsync(Guid userId)
        {
            try
            {
                var payments = await _paymentRepository.GetAllUserPaymentAsync(userId);
                return payments.Select(p =>
                {
                    var paymentDto = new PaymentReadDto();
                    paymentDto.FromEntity(p);
                    return paymentDto;
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}