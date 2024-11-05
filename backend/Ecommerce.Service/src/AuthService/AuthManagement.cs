using Ecommerce.Domain.Enums;
using Ecommerce.Domain.src.Auth;
using Ecommerce.Domain.src.Interfaces;
using Ecommerce.Service.src.UserService;


namespace Ecommerce.Service.src.AuthService
{
    public class AuthManagement : IAuthManagement
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly IPasswordHasher _passwordHasher;

        public AuthManagement(IUserRepository userRepository, ITokenService tokenService, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _passwordHasher = passwordHasher;

        }

        public async Task<AuthResponseDto> LoginAsync(UserCredentials userCredentials)
        {
            if (userCredentials == null)
            {
                throw new ArgumentNullException(nameof(userCredentials));
            }
            if (string.IsNullOrEmpty(userCredentials.Email))
            {
                throw new ArgumentException("Email cannot be null or empty", nameof(userCredentials.Email));
            }
            if (string.IsNullOrEmpty(userCredentials.Password))
            {
                throw new ArgumentException("Password cannot be null or empty", nameof(userCredentials.Password));
            }

            var foundUserByEmail = await _userRepository.GetUserByEmailAsync(userCredentials.Email);
            if (foundUserByEmail == null)
            {
                throw new Exception("User not found");
            }

            var isVerified = _passwordHasher.VerifyPassword(userCredentials.Password, foundUserByEmail.PasswordHash, foundUserByEmail.Salt);
            if (isVerified)
            {
                var token = new Token
                {
                    Id = foundUserByEmail.Id,
                    Email = foundUserByEmail.Email,
                    Role = foundUserByEmail.Role
                };
                var userId = foundUserByEmail.Id.ToString();
                var userRole = foundUserByEmail.Role.ToString();
                var generatedToken = _tokenService.GenerateToken(token);
                return new AuthResponseDto
                {
                    UserId = userId,
                    Token = generatedToken,
                    UserRole = userRole
                };
            }
            else
            {
                throw new Exception("Invalid password");
            }
        }

        public async Task LogoutAsync()
        {
            await Task.CompletedTask;
        }
    }
}