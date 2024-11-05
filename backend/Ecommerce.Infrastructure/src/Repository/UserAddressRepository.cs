using Ecommerce.Domain.src.Entities.UserAggregate;
using Ecommerce.Domain.src.Interfaces;
using Ecommerce.Infrastructure.src.Database;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.src.Repository
{
    public class UserAddressRepository : BaseRepository<UserAddress>, IUserAddressRepository
    {
        private readonly ApplicationDbContext _context;

        public UserAddressRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<UserAddress>> GetUserAddressesByUserIdAsync(Guid userId)
        {
            return await _context.UserAddress.AsNoTracking().Where(ua => ua.UserId == userId).ToListAsync();
        }

        public async Task<UserAddress> GetDefaultUserAddressAsync(Guid userId)
        {
            var userAddress = await _context.UserAddress.AsNoTracking().FirstOrDefaultAsync(ua => userId == ua.UserId && ua.IsDefault == true);

            if (userAddress == null)
            {
                throw new InvalidOperationException("No default user address found.");

            }
            return userAddress;
        }

    }
}
