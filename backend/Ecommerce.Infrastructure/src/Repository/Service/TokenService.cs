using Ecommerce.Domain.src.Auth;
using Ecommerce.Service.src.AuthService;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace Ecommerce.Infrastructure.src.Repository.Service
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GenerateToken(Token token)
        {
            // Get the key from configuration
            var jwtKey = _configuration["Jwt:Key"];
            if (string.IsNullOrWhiteSpace(jwtKey))
            {
                throw new InvalidOperationException("JWT Key is not configured in the app settings.");
            }
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, token.Id.ToString()),
                new Claim(ClaimTypes.Role, token.Role.ToString()),
                new Claim(ClaimTypes.Email, token.Email),
            };
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = signingCredentials
            };
            var tokenObj = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(tokenObj);

        }
    }
}