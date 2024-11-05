using Ecommerce.Domain.src.Entities.UserAggregate;
using Ecommerce.Service.src.Shared;

namespace Ecommerce.Service.src.UserAddressService
{
    public class UserAddressReadDto : BaseReadDto<UserAddress>
    {
        public Guid UserId { get; set; }
        public Guid AddressId { get; set; }
        public bool IsDefault { get; set; }

        public override void FromEntity(UserAddress entity)
        {
            base.FromEntity(entity);
            UserId = entity.UserId;
            AddressId = entity.AddressId;
            IsDefault = entity.IsDefault;

        }
    }
    public class UserAddressCreateDto : ICreateDto<UserAddress>
    {
        public Guid UserId { get; set; }
        public Guid AddressId { get; set; }
        public bool IsDefault { get; set; }

        public UserAddress CreateEntity()
        {
            return new UserAddress
            {
                UserId = UserId,
                AddressId = AddressId,
                IsDefault = IsDefault
            };
        }
    }

    public class UserAddressUpdateDto : IUpdateDto<UserAddress>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid AddressId { get; set; }
        public bool IsDefault { get; set; }

        public UserAddress UpdateEntity(UserAddress entity)
        {
            entity.UserId = UserId;
            entity.AddressId = AddressId;
            entity.IsDefault = IsDefault;
            return entity;
        }
    }
}