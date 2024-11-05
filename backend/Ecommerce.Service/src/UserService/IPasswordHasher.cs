
namespace Ecommerce.Service.src.UserService
{
    public interface IPasswordHasher
    {
        public void HashPassword(string originalPassword, out string hashPassword, out byte[] salt);
        public bool VerifyPassword(string inputPassword, string storedPassword, byte[] salt);
    }
}