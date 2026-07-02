using BookStoreAPI.DTOs.Authors;

namespace BookStoreAPI.Services.Author
{
    public interface IAuthorService
    {

        Task<IEnumerable<AuthorDto>> GetAllAsync();
        Task<AuthorDto?> GetByIdAsync(int id);
        Task<AuthorDto> CreateAsync(CreateAuthorDto dto);
        Task<bool> UpdateAsync(int id, CreateAuthorDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
