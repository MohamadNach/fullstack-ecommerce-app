using System.Security.Cryptography;
using Ecommerce.Service.src.UserService;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Ecommerce.Infrastructure.src.Repository.Service
{
    public class PasswordHasher : IPasswordHasher
    {
        public void HashPassword(string originalPassword, out string hashPassword, out byte[] salt)
        {
            salt = RandomNumberGenerator.GetBytes(16);
            hashPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: originalPassword,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 32));
        }

        public bool VerifyPassword(string inputPassword, string storedPassword, byte[] salt)
        {
            string hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: inputPassword,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 32));
            Console.WriteLine($"Stored Hash: {storedPassword}, Generated Hash: {hashedPassword}");

            return storedPassword == hashedPassword;
        }
    }
}