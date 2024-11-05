
using Ecommerce.Domain.src.Entities.CartAggregate;
using Ecommerce.Service.src.Shared;

namespace Ecommerce.Service.src.CartService
{
    public class CartReadDto : BaseReadDto<Cart>
    {
        public Guid UserId { get; set; }
        public override void FromEntity(Cart entity)
        {
            base.FromEntity(entity);
            UserId = entity.UserId;
        }

    }
    public class CartCreateDto : ICreateDto<Cart>
    {
        public Guid UserId { get; set; }

        public Cart CreateEntity()
        {
            return new Cart
            {
                UserId = UserId,
            };
        }
    }
    public class CartUpdateDto : IUpdateDto<Cart>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }

        public Cart UpdateEntity(Cart entity)
        {
            entity.UserId = UserId;
            return entity;
        }
    }
}