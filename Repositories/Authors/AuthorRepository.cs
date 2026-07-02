using BookStoreAPI.Data;
using BookStoreAPI.Models;
using Microsoft.EntityFrameworkCore;
namespace BookStoreAPI.Repositories.Authors
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly AppDbContext _context;

        public AuthorRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Author>> GetAllAsync() => await _context.Authors.ToListAsync();

        public async Task<Author?> GetByIdAsync(int id) => await _context.Authors.FindAsync(id);

        public async Task AddAsync(Author author) => await _context.Authors.AddAsync(author);

        public void Update(Author author) => _context.Authors.Update(author);

        public void Delete(Author author) => _context.Authors.Remove(author);

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }

}

