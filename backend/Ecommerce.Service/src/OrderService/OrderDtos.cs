using Ecommerce.Domain.Enums;
using Ecommerce.Domain.src.Entities.OrderAggregate;
using Ecommerce.Service.src.CartItemService;
using Ecommerce.Service.src.OrderItemService;
using Ecommerce.Service.src.Shared;

namespace Ecommerce.Service.src.OrderService
{
    public class OrderReadDto : BaseReadDto<Order>
    {
        public Guid UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public Guid ShippingAddressId { get; set; }
        public OrderStatus OrderStatus { get; set; }

        public override void FromEntity(Order entity)
        {
            UserId = entity.UserId;
            OrderDate = entity.OrderDate;
            TotalPrice = entity.TotalPrice;
            ShippingAddressId = entity.ShippingAddressId;
            OrderStatus = entity.OrderStatus;
            base.FromEntity(entity);
        }
    }
    public class OrderCreateDto : ICreateDto<Order>
    {
        public Guid UserId { get; set; }
        public Guid ShippingAddressId { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public List<OrderItemCreateDto> OrderItems { get; set; } = new List<OrderItemCreateDto>();

        public Order CreateEntity()
        {
            var order = new Order
            {
                UserId = UserId,
                ShippingAddressId = ShippingAddressId,
                OrderStatus = OrderStatus,
            };
            foreach (var item in OrderItems)
            {
                var orderItem = item.CreateEntity();
                order.AddOrderItem(orderItem);
            }
            return order;
        }
    }
    public class OrderUpdateDto : IUpdateDto<Order>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public Guid ShippingAddressId { get; set; }
        public OrderStatus OrderStatus { get; set; }

        public Order UpdateEntity(Order entity)
        {
            entity.UserId = UserId;
            entity.OrderDate = OrderDate;
            entity.TotalPrice = TotalPrice;
            entity.ShippingAddressId = ShippingAddressId;
            entity.OrderStatus = OrderStatus;
            return entity;
        }
    }
}