using BookStoreAPI.DTOs.Books;
using BookStoreAPI.Helper;

namespace BookStoreAPI.Services.Books
{
    public interface IBookService
    {
        Task<object> GetAllAsync(BookQueryParameters parameters);
        Task<BookDto?> GetByIdAsync(int id);
        Task<BookDto> CreateAsync(CreateBookDto dto);
        Task<bool> UpdateAsync(int id, CreateBookDto dto);
        Task<bool> DeleteAsync(int id);

    }
}
