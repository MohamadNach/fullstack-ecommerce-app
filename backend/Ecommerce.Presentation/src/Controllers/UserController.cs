using Ecommerce.Domain.src.Entities.UserAggregate;
using Microsoft.AspNetCore.Mvc;
using Ecommerce.Service.src.UserService;
using Microsoft.AspNetCore.Authorization;
using Asp.Versioning;
using Ecommerce.Domain.Enums;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Presentation.src.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[Controller]s")]
    public class UserController : AppController<User, UserReadDto, UserCreateDto, UserUpdateDto>
    {
        private readonly IUserManagement _userManagement;

        public UserController(IUserManagement userManagement, ILogger<UserController> logger)
            : base(userManagement, logger)
        {
            _userManagement = userManagement;
        }

        [AllowAnonymous]
        [HttpPost]
        [MapToApiVersion("1.0")]
        public override async Task<ActionResult<UserReadDto>> CreateAsync([FromBody] UserCreateDto userCreateDto)
        {
            if (userCreateDto == null)
            {
                return BadRequest("Invalid data.");
            }

            // Check if the user already exists
            var existingUser = await _userManagement.GetUserByEmailAsync(userCreateDto.Email);
            if (existingUser != null)
            {
                return Conflict(new { message = "A user with this email address already exists." });
            }

            // Automatically assign "User" role to the new user
            userCreateDto.Role = UserRole.User;

            var createdUser = await _userManagement.CreateAsync(userCreateDto);
            return Ok(createdUser);
            // return CreatedAtAction(nameof(GetByIdAsync), new { id = createdUser.Id, version = "1.0" }, createdUser);
        }


        [AllowAnonymous]
        [HttpGet("email/{userEmail}")]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> GetUserByEmailAsync(string userEmail)
        {
            var user = await _userManagement.GetUserByEmailAsync(userEmail);
            if (user == null)
            {
                return NotFound("User not found.");
            }
            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPut("{userId}/{newPassword}")]
        public async Task<IActionResult> UpdateUserPassword(Guid userId, string newPassword)
        {
            var result = await _userManagement.UpdatePasswordAsync(userId, newPassword);
            if (!result)
            {
                return NotFound("User not found.");
            }
            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<UserReadDto>> CreateAdminAsync([FromBody] UserCreateDto userCreateDto)
        {
            if (userCreateDto == null)
            {
                return BadRequest("Invalid data.");
            }

            // Check if the user already exists
            var existingUser = await _userManagement.GetUserByEmailAsync(userCreateDto.Email);
            if (existingUser != null)
            {
                return Conflict(new { message = "A user with this email address already exists." });
            }

            // Assign the "Admin" role to the new user
            userCreateDto.Role = UserRole.Admin;

            var createdAdmin = await _userManagement.CreateAsync(userCreateDto);
            return Ok(createdAdmin);
            // return CreatedAtAction(nameof(GetByIdAsync), new { id = createdAdmin.Id, version = "1.0" }, createdAdmin);
        }

    }
}
