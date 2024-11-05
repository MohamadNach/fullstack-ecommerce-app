using Ecommerce.Domain.src.Interfaces;
using Ecommerce.Domain.src.Entities.UserAggregate;
using Ecommerce.Service.src.Shared;

namespace Ecommerce.Service.src.UserService
{
    public class UserManagement : BaseService<User, UserReadDto, UserCreateDto, UserUpdateDto>, IUserManagement
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;

        public UserManagement(IUserRepository userRepository, IPasswordHasher passwordHasher) : base(userRepository)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }
        public override async Task<UserReadDto> CreateAsync(UserCreateDto createDto)
        {
            try
            {
                _passwordHasher.HashPassword(createDto.PasswordHash, out string hashedPassword, out byte[] salt);
                var user = createDto.CreateEntity();
                user.PasswordHash = hashedPassword;
                user.Salt = salt;
                await _userRepository.CreateAsync(user);
                var userReadDto = new UserReadDto();
                userReadDto.FromEntity(user);
                return userReadDto;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to create user. Error: " + ex.Message);

            }

        }

        public async Task<bool> UpdatePasswordAsync(Guid userId, string newPassword)
        {
            return await _userRepository.UpdatePasswordAsync(userId, newPassword);
        }


        public async Task<UserReadDto?> GetUserByEmailAsync(string email)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);
            if (user == null)
            {
                return null;
            }
            var userReadDto = new UserReadDto();
            userReadDto.FromEntity(user);
            return userReadDto;
        }
    }
}