using BookStoreAPI.DTOs.Authors;
using BookStoreAPI.Models;
using BookStoreAPI.Repositories.Authors;

namespace BookStoreAPI.Services.Author
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _repository;

        public AuthorService(IAuthorRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<AuthorDto>> GetAllAsync()
        {
            var authors = await _repository.GetAllAsync();
            return authors.Select(a => new AuthorDto { Id = a.Id, Name = a.Name, Bio = a.Bio });
        }

        public async Task<AuthorDto?> GetByIdAsync(int id)
        {
            var author = await _repository.GetByIdAsync(id);
            if (author == null) return null;
            return new AuthorDto { Id = author.Id, Name = author.Name, Bio = author.Bio };
        }

        public async Task<AuthorDto> CreateAsync(CreateAuthorDto dto)
        {
            var author = new BookStoreAPI.Models.Author { Name = dto.Name, Bio = dto.Bio };
            await _repository.AddAsync(author);
            await _repository.SaveChangesAsync();
            return new AuthorDto { Id = author.Id, Name = author.Name, Bio = author.Bio };
        }

        public async Task<bool> UpdateAsync(int id, CreateAuthorDto dto)
        {
            var author = await _repository.GetByIdAsync(id);
            if (author == null) return false;

            author.Name = dto.Name;
            author.Bio = dto.Bio;
            _repository.Update(author);
            await _repository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var author = await _repository.GetByIdAsync(id);
            if (author == null) return false;

            _repository.Delete(author);
            await _repository.SaveChangesAsync();
            return true;
        }

    }
}