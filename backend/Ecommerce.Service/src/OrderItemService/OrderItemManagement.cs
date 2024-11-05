using Ecommerce.Domain.src.Entities.OrderAggregate;
using Ecommerce.Domain.src.Interfaces;
using Ecommerce.Service.src.OrderService;
using Ecommerce.Service.src.Shared;

namespace Ecommerce.Service.src.OrderItemService
{
    public class OrderItemManagement : BaseService<OrderItem, OrderItemReadDto, OrderItemCreateDto, OrderItemUpdateDto>, IOrderItemManagement
    {
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;

        public OrderItemManagement(IOrderItemRepository orderItemRepository, IOrderRepository orderRepository, IProductRepository productRepository) : base(orderItemRepository)
        {
            _orderItemRepository = orderItemRepository;
            _orderRepository = orderRepository;
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<OrderItemReadDto>> GetOrderItemsByOrderIdAsync(Guid orderId)
        {
            try
            {
                var order = await _orderRepository.GetAsync(o => o.Id == orderId);
                if (order == null)
                    throw new ArgumentException("Order not found.");

                var orderitems = await _orderItemRepository.GetOrderItemsByOrderIdAsync(orderId);
                var orderItemDtos = orderitems.Select(item =>
                {
                    var dto = new OrderItemReadDto();
                    dto.FromEntity(item);
                    return dto;
                });

                return orderItemDtos;

            }
            catch
            {
                throw new Exception("Error Retrieving Order items!.");
            }
        }

        public async Task<IEnumerable<OrderItemReadDto>> GetOrderItemsByProductIdAsync(Guid productId)
        {
            try
            {
                var product = await _productRepository.GetAsync(ad => ad.Id == productId);
                if (product == null)
                    throw new ArgumentException("Invalid Product.");

                var orderitems = await _orderItemRepository.GetOrderItemsByProductIdAsync(productId);
                var orderItemDtos = orderitems.Select(item =>
                {
                    var dto = new OrderItemReadDto();
                    dto.FromEntity(item);
                    return dto;
                });

                return orderItemDtos;
            }
            catch
            {
                throw new Exception("Error Retrieving Order items!.");
            }
        }

        public async Task<int> GetTotalQuantityByOrderIdAsync(Guid orderId)
        {
            try
            {
                var order = await _orderRepository.GetAsync(o => o.Id == orderId);
                if (order == null)
                    throw new ArgumentException("Order not found.");

                var orderItems = await _orderItemRepository.GetOrderItemsByOrderIdAsync(orderId);
                return orderItems.Sum(item => item.Quantity);
            }
            catch
            {
                throw new Exception("Error Retrieving total quantity!.");
            }
        }

        Task<IEnumerable<OrderReadDto>> IOrderItemManagement.GetOrderItemsByOrderIdAsync(Guid orderId)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<OrderReadDto>> IOrderItemManagement.GetOrderItemsByProductIdAsync(Guid productId)
        {
            throw new NotImplementedException();
        }
    }
}