using Ecommerce.Domain.src.Entities.AddressAggregate;
using Ecommerce.Domain.src.Interfaces;
using Ecommerce.Infrastructure.src.Database;

namespace Ecommerce.Infrastructure.src.Repository
{
    public class AddressRepository : BaseRepository<Address>, IAddressRepository
    {
        private readonly ApplicationDbContext _context;

        public AddressRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
