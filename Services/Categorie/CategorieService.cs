using BookStoreAPI.DTOs.Categories;
using BookStoreAPI.Models;
using BookStoreAPI.Repositories.Categories;

namespace BookStoreAPI.Services.Categorie
{
    public class CategorieService
    {
        public class CategoryService : ICategoryService
        {
            private readonly ICategoryRepository _repository;

            public CategoryService(ICategoryRepository repository)
            {
                _repository = repository;
            }

            public async Task<IEnumerable<CategoryDto>> GetAllAsync()
            {
                var categories = await _repository.GetAllAsync();
                return categories.Select(c => new CategoryDto { Id = c.Id, Name = c.Name, Description = c.Description });
            }

            public async Task<CategoryDto?> GetByIdAsync(int id)
            {
                var category = await _repository.GetByIdAsync(id);
                if (category == null) return null;
                return new CategoryDto { Id = category.Id, Name = category.Name, Description = category.Description };
            }

            public async Task<CategoryDto> CreateAsync(CreateCategoryDto dto)
            {
                var category = new Category { Name = dto.Name, Description = dto.Description };
                await _repository.AddAsync(category);
                await _repository.SaveChangesAsync();
                return new CategoryDto { Id = category.Id, Name = category.Name, Description = category.Description };
            }

            public async Task<bool> UpdateAsync(int id, CreateCategoryDto dto)
            {
                var category = await _repository.GetByIdAsync(id);
                if (category == null) return false;

                category.Name = dto.Name;
                category.Description = dto.Description;
                _repository.Update(category);
                await _repository.SaveChangesAsync();
                return true;
            }

            public async Task<bool> DeleteAsync(int id)
            {
                var category = await _repository.GetByIdAsync(id);
                if (category == null) return false;

                _repository.Delete(category);
                await _repository.SaveChangesAsync();
                return true;
            }

        }
    }
}