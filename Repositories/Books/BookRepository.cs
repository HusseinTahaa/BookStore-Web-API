using BookStoreAPI.Data;
using BookStoreAPI.Helper;
using BookStoreAPI.Models;
using Microsoft.EntityFrameworkCore;
namespace BookStoreAPI.Repositories.Books
{
    public class BookRepository : IBookRepository
    {
        private readonly AppDbContext _context;

        public BookRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<(IEnumerable<Book> Books, int TotalCount)> GetAllAsync(BookQueryParameters parameters)
        {
            var query = _context.Books
                .Include(b => b.Category)
                .Include(b => b.Author)
                .AsQueryable();

            // 1. Searching
            if (!string.IsNullOrWhiteSpace(parameters.SearchTerm))
            {
                query = query.Where(b => b.Title.Contains(parameters.SearchTerm) || b.Description!.Contains(parameters.SearchTerm));
            }

            // 2. Filtering
            if (parameters.CategoryId.HasValue)
                query = query.Where(b => b.CategoryId == parameters.CategoryId.Value);

            if (parameters.AuthorId.HasValue)
                query = query.Where(b => b.AuthorId == parameters.AuthorId.Value);

            if (parameters.MinPrice.HasValue)
                query = query.Where(b => b.Price >= parameters.MinPrice.Value);

            if (parameters.MaxPrice.HasValue)
                query = query.Where(b => b.Price <= parameters.MaxPrice.Value);

            // Total count before pagination
            var totalCount = await query.CountAsync();

            // 3. Pagination
            var books = await query
                .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize)
                .ToListAsync();

            return (books, totalCount);
        }

        public async Task<Book?> GetByIdAsync(int id)
        {
            return await _context.Books
                .Include(b => b.Category)
                .Include(b => b.Author)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task AddAsync(Book book) => await _context.Books.AddAsync(book);

        public void Update(Book book) => _context.Books.Update(book);

        public void Delete(Book book) => _context.Books.Remove(book);

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }


}
