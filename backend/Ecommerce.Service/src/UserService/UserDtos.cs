using Ecommerce.Domain.Enums;
using Ecommerce.Domain.src.Entities.UserAggregate;
using Ecommerce.Service.src.Shared;

namespace Ecommerce.Service.src.UserService
{
    public class UserReadDto : BaseReadDto<User>
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = string.Empty;
        public UserRole Role { get; set; }

        public override void FromEntity(User entity)
        {
            base.FromEntity(entity);
            FirstName = entity.FirstName;
            LastName = entity.LastName;
            Email = entity.Email;
            PhoneNumber = entity.PhoneNumber;
            Role = entity.Role;
        }
    }
    public class UserCreateDto : ICreateDto<User>
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string PhoneNumber { get; set; } = string.Empty;
        public UserRole Role { get; set; }

        public User CreateEntity()
        {
            return new User
            {
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                PasswordHash = PasswordHash,
                PhoneNumber = PhoneNumber,
                Role = Role
            };
        }
    }

    public class UserUpdateDto : IUpdateDto<User>
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = string.Empty;
        public UserRole Role { get; set; }

        public User UpdateEntity(User entity)
        {
            entity.FirstName = FirstName;
            entity.LastName = LastName;
            entity.Email = Email;
            entity.PhoneNumber = PhoneNumber;
            entity.Role = Role;
            return entity;
        }
    }
}