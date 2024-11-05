using Microsoft.AspNetCore.Mvc;
using Ecommerce.Service.src.AddressService;
using Asp.Versioning;
using Ecommerce.Domain.src.Entities.AddressAggregate;
using Ecommerce.Service.src.UserAddressService;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Presentation.src.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[Controller]es")]
    public class AddressController : AppController<Address, AddressReadDto, AddressCreateDto, AddressUpdateDto>
    {
        private readonly IAddressManagement _addressManagement;
        private readonly IUserAddressManagement _userAddressManagement;

        public AddressController(
            IAddressManagement addressManagement,
            IUserAddressManagement userAddressManagement,
            ILogger<AddressController> logger) : base(addressManagement, logger)
        {
            _addressManagement = addressManagement;
            _userAddressManagement = userAddressManagement;
        }

        [HttpPost("create-User-address")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<AddressReadDto>> CreateAddressAsync([FromBody] AddressCreateDto addressCreateDto, Guid userId, bool isDefault)
        {
            if (addressCreateDto == null)
            {
                return BadRequest("Invalid data.");
            }

            try
            {
                // Creating the address
                var CreatedAddess = await _addressManagement.CreateAsync(addressCreateDto);
                if (CreatedAddess == null)
                {
                    return StatusCode(500, "Error creating address.");
                }

                // Adding address to UserAddress table
                if (isDefault)
                {

                    var existingUserAddress = await _userAddressManagement.GetDefaultUserAddressAsync(userId);
                    if (existingUserAddress != null)
                    {
                        var userAddressUpdateDto = new UserAddressUpdateDto
                        {
                            Id = existingUserAddress.Id,
                            UserId = existingUserAddress.UserId,
                            AddressId = existingUserAddress.AddressId,
                            IsDefault = false // Set to false since we are making a new address default
                        };
                        existingUserAddress.IsDefault = false;

                        await _userAddressManagement.UpdateAsync(existingUserAddress.Id, userAddressUpdateDto);
                    }

                    // Add the new address to the UserAddress table
                    var userAddressEntity = new UserAddressCreateDto
                    {
                        UserId = userId,
                        AddressId = CreatedAddess.Id,
                        IsDefault = isDefault
                    };
                    // saving the new UserAddress record
                    await _userAddressManagement.CreateAsync(userAddressEntity);

                }

                return Ok(CreatedAddess);

            }
            catch (Exception ex)
            {
                // Log exception details here (consider using a logging framework)
                return StatusCode(500, $"Error creating address: {ex.Message}");
            }
        }
    }
}
