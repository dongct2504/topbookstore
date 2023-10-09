using TopBookStore.Domain.Entities;

namespace TopBookStore.Application.Interfaces;

public interface ICategoryService
{
    Task<IEnumerable<Category>> GetAllCategoriesAsync();
    Task<Category?> GetCategoryByIdAsync(int id);

    Task AddCategoryAsync(Category category);
    Task UpdateCategoryAsync(Category category);
    Task RemoveCategoryAsync(Category category);
}
