using BookStoreAPI.DTOs.Books;
using BookStoreAPI.Exceptions;
using BookStoreAPI.Helper;
using BookStoreAPI.Models;
using BookStoreAPI.Repositories.Books;

namespace BookStoreAPI.Services.Books
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<object> GetAllAsync(BookQueryParameters parameters)
        {
            var (books, totalCount) = await _bookRepository.GetAllAsync(parameters);

            var dtos = books.Select(b => new BookDto
            {
                Id = b.Id,
                Title = b.Title,
                ISBN = b.ISBN,
                Description = b.Description,
                Price = b.Price,
                StockQuantity = b.StockQuantity,
                CategoryName = b.Category?.Name ?? "Unknown",
                AuthorName = b.Author?.Name ?? "Unknown"
            });

            return new
            {
                TotalCount = totalCount,
                PageNumber = parameters.PageNumber,
                PageSize = parameters.PageSize,
                Data = dtos
            };
        }

        public async Task<BookDto?> GetByIdAsync(int id)
        {
            var b = await _bookRepository.GetByIdAsync(id);
            if (b == null) return null;

            return new BookDto
            {
                Id = b.Id,
                Title = b.Title,
                ISBN = b.ISBN,
                Description = b.Description,
                Price = b.Price,
                StockQuantity = b.StockQuantity,
                CategoryName = b.Category?.Name ?? "Unknown",
                AuthorName = b.Author?.Name ?? "Unknown"
            };
        }

        public async Task<BookDto> CreateAsync(CreateBookDto dto)
        {
            var book = new Book
            {
                Title = dto.Title,
                ISBN = dto.ISBN,
                Description = dto.Description,
                Price = dto.Price,
                StockQuantity = dto.StockQuantity,
                CategoryId = dto.CategoryId,
                AuthorId = dto.AuthorId
            };

            await _bookRepository.AddAsync(book);
            await _bookRepository.SaveChangesAsync();

            return await GetByIdAsync(book.Id) ?? throw new NotFoundException("Failed to retrieve created book");
        }

        public async Task<bool> UpdateAsync(int id, CreateBookDto dto)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null) return false;

            book.Title = dto.Title;
            book.ISBN = dto.ISBN;
            book.Description = dto.Description;
            book.Price = dto.Price;
            book.StockQuantity = dto.StockQuantity;
            book.CategoryId = dto.CategoryId;
            book.AuthorId = dto.AuthorId;

            _bookRepository.Update(book);
            await _bookRepository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null) return false;

            _bookRepository.Delete(book);
            await _bookRepository.SaveChangesAsync();
            return true;
        }
    }

}