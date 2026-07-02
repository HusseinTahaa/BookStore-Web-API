using BookStoreAPI.Models;

namespace BookStoreAPI.Repositories.Orders
{
    public interface IOrderRepository
    {

        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<IEnumerable<Order>> GetOrdersByUserIdAsync(string userId);
        Task AddOrderAsync(Order order);
        Task SaveChangesAsync();
    }
}
