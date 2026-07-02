using BookStoreAPI.DTOs.Orders;
using BookStoreAPI.Models;
using BookStoreAPI.Repositories.Books;
using BookStoreAPI.Repositories.Orders;

namespace BookStoreAPI.Services.Orders
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IBookRepository _bookRepository;
        private readonly ILogger<OrderService> _logger;

        public OrderService(IOrderRepository orderRepository, IBookRepository bookRepository, ILogger<OrderService> logger)
        {
            _orderRepository = orderRepository;
            _bookRepository = bookRepository;
            _logger = logger;
        }

        public async Task<OrderDto> CreateOrderAsync(string userId, CreateOrderDto dto)
        {
            if (dto.Items == null || !dto.Items.Any())
                throw new Exception("Order must contain at least one item.");

            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.UtcNow,
                Status = OrderStatus.Pending,
                OrderItems = new List<OrderItem>()
            };

            decimal totalAmount = 0;

            foreach (var item in dto.Items)
            {
                var book = await _bookRepository.GetByIdAsync(item.BookId);
                if (book == null)
                    throw new Exception($"Book with ID {item.BookId} not found.");

                if (book.StockQuantity < item.Quantity)
                    throw new Exception($"Not enough stock for book '{book.Title}'. Available: {book.StockQuantity}");

                book.StockQuantity -= item.Quantity;
                _bookRepository.Update(book);

                var orderItem = new OrderItem
                {
                    BookId = book.Id,
                    Quantity = item.Quantity,
                    UnitPrice = book.Price
                };

                order.OrderItems.Add(orderItem);
                totalAmount += (item.Quantity * book.Price);
            }

            order.TotalAmount = totalAmount;

            await _orderRepository.AddOrderAsync(order);
            await _orderRepository.SaveChangesAsync(); 

            _logger.LogInformation($"Order {order.Id} created successfully by User {userId} with Total {totalAmount:C}.");

            return MapToDto(order);
        }

        public async Task<IEnumerable<OrderDto>> GetUserOrdersAsync(string userId)
        {
            var orders = await _orderRepository.GetOrdersByUserIdAsync(userId);
            return orders.Select(MapToDto);
        }

        public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync()
        {
            var orders = await _orderRepository.GetAllOrdersAsync();
            return orders.Select(MapToDto);
        }

        private OrderDto MapToDto(Order order)
        {
            return new OrderDto
            {
                Id = order.Id,
                UserId = order.UserId,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                Status = order.Status.ToString(),
                Items = order.OrderItems.Select(oi => new OrderItemDto
                {
                    BookId = oi.BookId,
                    BookTitle = oi.Book?.Title ?? "Unknown",
                    Quantity = oi.Quantity,
                    UnitPrice = oi.UnitPrice
                }).ToList()
            };
        }


    }
}