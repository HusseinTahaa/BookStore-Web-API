using BookStoreAPI.Helper;
using BookStoreAPI.Models;

namespace BookStoreAPI.Repositories.Books
{
    public interface IBookRepository
    {
        Task<(IEnumerable<Book> Books, int TotalCount)> GetAllAsync(BookQueryParameters parameters);
        Task<Book?> GetByIdAsync(int id);
        Task AddAsync(Book book);
        void Update(Book book);
        void Delete(Book book);
        Task SaveChangesAsync();
    }
}
