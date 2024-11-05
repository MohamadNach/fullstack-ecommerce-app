using Asp.Versioning;
using Ecommerce.Domain.src.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Presentation.src.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[Controller]es")]
    public class UserAddressController : ControllerBase
    {
        private readonly IUserAddressRepository _userAddressRepository;

        public UserAddressController(IUserAddressRepository userAddressRepository)
        {
            _userAddressRepository = userAddressRepository;
        }

        // GET: api/v1/UserAddress/{userId}
        [HttpGet("{userId}")]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> GetUserAddressesByUserIdAsync(Guid userId)
        {
            var addresses = await _userAddressRepository.GetUserAddressesByUserIdAsync(userId);
            if (addresses == null || !addresses.Any())
            {
                return NotFound("User addresses not found.");
            }
            return Ok(addresses);
        }

        [HttpGet("{userId}/default")]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> GetDefaultUserAddressAsync(Guid userId)
        {
            var addresses = await _userAddressRepository.GetUserAddressesByUserIdAsync(userId);
            if (addresses == null || !addresses.Any())
            {
                return NotFound("User addresses not found.");
            }
            return Ok(addresses);
        }

    }
}
