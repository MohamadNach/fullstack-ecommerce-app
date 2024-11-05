using Ecommerce.Domain.src.Entities.CartAggregate;
using Ecommerce.Domain.src.Entities.UserAggregate;
using Ecommerce.Service.src.Shared;

namespace Ecommerce.Service.src.CartItemService
{
    public class CartItemReadDto : BaseReadDto<CartItem>
    {
        public Guid CartId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }

        public override void FromEntity(CartItem entity)
        {
            base.FromEntity(entity);
            CartId = entity.CartId;
            ProductId = entity.ProductId;
            Quantity = entity.Quantity;
            UnitPrice = entity.UnitPrice;
            TotalPrice = entity.TotalPrice;
        }

    }
    public class CartItemCreateDto : ICreateDto<CartItem>
    {
        public Guid CartId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }

        public CartItem CreateEntity()
        {
            return new CartItem
            {
                CartId = CartId,
                ProductId = ProductId,
                Quantity = Quantity,
                UnitPrice = UnitPrice,
                TotalPrice = TotalPrice
            };
        }
    }
    public class CartItemUpdateDto : IUpdateDto<CartItem>
    {
        public Guid Id { get; set; }
        public Guid CartId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }

        public CartItem UpdateEntity(CartItem entity)
        {
            entity.CartId = CartId;
            entity.ProductId = ProductId;
            entity.Quantity = Quantity;
            entity.UnitPrice = UnitPrice;
            entity.TotalPrice = TotalPrice;
            return entity;
        }
    }
}