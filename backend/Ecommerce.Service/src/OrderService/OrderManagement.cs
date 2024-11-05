using System.Transactions;
using Ecommerce.Domain.Enums;
using Ecommerce.Domain.src.Entities.OrderAggregate;
using Ecommerce.Domain.src.Interfaces;
using Ecommerce.Service.src.Shared;

namespace Ecommerce.Service.src.OrderService
{
    public class OrderManagement : BaseService<Order, OrderReadDto, OrderCreateDto, OrderUpdateDto>, IOrderManagement
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUserRepository _userRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IProductRepository _productRepository;


        public OrderManagement(
            IOrderRepository orderRepository,
            IUserRepository userRepository,
            IAddressRepository addressRepository,
            IOrderItemRepository orderItemRepository,
            IProductRepository productRepository) : base(orderRepository)
        {
            _orderRepository = orderRepository;
            _userRepository = userRepository;
            _addressRepository = addressRepository;
            _orderItemRepository = orderItemRepository;
            _productRepository = productRepository;

        }

        public override async Task<OrderReadDto> CreateAsync(OrderCreateDto orderCreateDto)
        {
            try
            {
                // Step 1: Validate user and shipping address
                var user = await _userRepository.GetAsync(u => u.Id == orderCreateDto.UserId);
                if (user == null) throw new ArgumentException("Invalid user.");

                var address = await _addressRepository.GetAsync(a => a.Id == orderCreateDto.ShippingAddressId);
                if (address == null) throw new ArgumentException("Invalid shipping address.");

                // Step 2: Create the Order entity (TotalPrice will be calculated later)
                var newOrder = orderCreateDto.CreateEntity();
                newOrder.OrderDate = DateTime.UtcNow;  // Set order date to the current date

                // Step 3: Begin a transaction to ensure atomicity of order and order items creation
                using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    // Save the order
                    newOrder = await _orderRepository.CreateAsync(newOrder);

                    // Step 4: Create the associated order items and calculate total price
                    if (orderCreateDto.OrderItems != null && orderCreateDto.OrderItems.Any())
                    {
                        foreach (var itemDto in orderCreateDto.OrderItems)
                        {
                            // Fetch the product to get its price
                            var product = await _productRepository.GetAsync(p => p.Id == itemDto.ProductId);
                            if (product == null) throw new ArgumentException("Invalid product.");

                            // Set the price for the order item based on the product price
                            var orderItem = itemDto.CreateEntity();
                            orderItem.OrderId = newOrder.Id;  // Link order item to the newly created order
                            orderItem.Price = product.Price;  // Set the product price for the order item

                            await _orderItemRepository.CreateAsync(orderItem);

                            // Add order item to the order and recalculate total price
                            newOrder.AddOrderItem(orderItem);
                        }
                    }

                    // Save the updated total price for the order
                    await _orderRepository.UpdateByIdAsync(newOrder);

                    // Commit transaction if all operations succeed
                    transaction.Complete();
                }

                // Step 5: Map the order entity to OrderReadDto and return
                var orderReadDto = new OrderReadDto();
                orderReadDto.FromEntity(newOrder);
                return orderReadDto;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to create order. Error: " + ex.Message);
            }
        }


        public async Task<IEnumerable<OrderReadDto>> GetOrdersByUserIdAsync(Guid userId)
        {
            var user = await _userRepository.GetAsync(u => u.Id == userId);
            if (user == null) throw new ArgumentException("Invalid user.");

            var orders = await _orderRepository.GetOrdersByUserIdAsync(userId);
            return orders.Select(order =>
            {
                var dto = new OrderReadDto();
                dto.FromEntity(order);
                return dto;
            });
        }

        public async Task<IEnumerable<OrderReadDto>> GetOrdersByStatusAsync(OrderStatus status)
        {
            var orders = await _orderRepository.GetOrdersByStatusAsync(status);
            return orders.Select(order =>
            {
                var dto = new OrderReadDto();
                dto.FromEntity(order);
                return dto;
            });
        }

        public async Task<IEnumerable<OrderReadDto>> GetOrdersByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            var orders = await _orderRepository.GetOrdersByDateRangeAsync(startDate, endDate);
            return orders.Select(order =>
            {
                var dto = new OrderReadDto();
                dto.FromEntity(order);
                return dto;
            });
        }

        public async Task<decimal> GetTotalPriceByOrderIdAsync(Guid orderId)
        {
            var order = await _orderRepository.GetAsync(o => o.Id == orderId);
            if (order == null) throw new ArgumentException("Order not found.");

            return await _orderRepository.GetTotalPriceByOrderIdAsync(orderId);
        }
    }
}
