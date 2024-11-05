using Ecommerce.Domain.src.Entities.PaymentAggregate;
using Ecommerce.Domain.src.Interfaces;
using Ecommerce.Infrastructure.src.Database;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.src.Repository
{
    public class PaymentRepository : BaseRepository<Payment>, IPaymentRepository
    {
        private readonly ApplicationDbContext _context;


        public PaymentRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }


        public async Task<IEnumerable<Payment>> GetAllUserPaymentAsync(Guid userId)
        {
            return await _context.Payments.AsNoTracking().Where(p => p.UserId == userId).ToListAsync();
        }

    }
}
