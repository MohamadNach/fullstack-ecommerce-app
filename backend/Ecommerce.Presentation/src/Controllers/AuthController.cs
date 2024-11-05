using Asp.Versioning;
using Ecommerce.Domain.src.Auth;
using Ecommerce.Service.src.AuthService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Presentation.src.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[Controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthManagement _authManagement;
        private readonly ILogger<AuthController> _logger;


        public AuthController(IAuthManagement authManagement, ILogger<AuthController> logger)
        {
            _authManagement = authManagement;
            _logger = logger;

        }

        [AllowAnonymous]
        [HttpPost("login")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult> LoginAsync([FromBody] UserCredentials userCredentials)
        {
            try
            {
                var token = await _authManagement.LoginAsync(userCredentials);
                Console.WriteLine("token  ", token);
                return Ok(token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during login.");

                if (ex.Message == "Invalid password" || ex.Message == "User not found")
                {
                    return Unauthorized("Invalid email or password.");
                }
                return StatusCode(500, "Error logging in.");
            }
        }

        [AllowAnonymous]
        [HttpPost("logout")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult> LogoutAsync()
        {
            try
            {
                await _authManagement.LogoutAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during logout.");
                return StatusCode(500, "Error logging out.");
            }
        }

    }
}