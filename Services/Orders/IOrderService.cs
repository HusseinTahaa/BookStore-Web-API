using BookStoreAPI.DTOs.Orders;

namespace BookStoreAPI.Services.Orders
{
    public interface IOrderService
    {


        Task<OrderDto> CreateOrderAsync(string userId, CreateOrderDto dto);
        Task<IEnumerable<OrderDto>> GetUserOrdersAsync(string userId);
        Task<IEnumerable<OrderDto>> GetAllOrdersAsync();
    }
}
