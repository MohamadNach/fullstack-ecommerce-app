using Ecommerce.Domain.src.Entities.OrderAggregate;
using Ecommerce.Service.src.Shared;

namespace Ecommerce.Service.src.OrderItemService
{
    public class OrderItemCreateDto : ICreateDto<OrderItem>
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public OrderItem CreateEntity()
        {
            return new OrderItem
            {
                ProductId = ProductId,
                Quantity = Quantity
            };
        }
    }
    public class OrderItemUpdateDto : IUpdateDto<OrderItem>
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public OrderItem UpdateEntity(OrderItem entity)
        {
            entity.OrderId = OrderId;
            entity.ProductId = ProductId;
            entity.Quantity = Quantity;
            return entity;
        }
    }
    public class OrderItemReadDto : BaseReadDto<OrderItem>
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }

        public override void FromEntity(OrderItem entity)
        {
            OrderId = entity.OrderId;
            ProductId = entity.ProductId;
            Quantity = entity.Quantity;
        }
    }
}