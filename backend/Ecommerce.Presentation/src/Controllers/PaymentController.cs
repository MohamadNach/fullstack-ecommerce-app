using Asp.Versioning;
using Ecommerce.Domain.src.Entities.PaymentAggregate;
using Ecommerce.Service.src.PaymentService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Presentation.src.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[Controller]s")]
    public class PaymentController : AppController<Payment, PaymentReadDto, PaymentCreateDto, PaymentUpdateDto>
    {
        private readonly IPaymentManagement _paymentManagement;

        public PaymentController(IPaymentManagement paymentManagement, ILogger<PaymentController> logger) : base(paymentManagement, logger)
        {
            _paymentManagement = paymentManagement;
        }

        // GET: api/v1/Payment/User/{userId}
        [HttpGet("User/{userId:guid}")]
        [Authorize]
        public async Task<IActionResult> GetAllUserPaymentAsync(Guid userId)
        {
            var payments = await _paymentManagement.GetAllUserPaymentAsync(userId);
            if (payments == null)
            {
                return NotFound("No payments found for this user.");
            }
            return Ok(payments);
        }
    }
}
