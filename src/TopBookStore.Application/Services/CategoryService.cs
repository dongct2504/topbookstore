using TopBookStore.Application.Interfaces;
using TopBookStore.Domain.Entities;
using TopBookStore.Domain.Interfaces;
using TopBookStore.Domain.Queries;

namespace TopBookStore.Application.Services;

public class CategoryService : ICategoryService
{
    private readonly ITopBookStoreUnitOfWork _data;

    public CategoryService(ITopBookStoreUnitOfWork data)
    {
        _data = data;
    }

    public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
    {
        QueryOptions<Category> options = new()
        {
            OrderBy = c => c.Name
        };

        return await _data.Categories.ListAllAsync(options);
    }

    public async Task<Category?> GetCategoryByIdAsync(int id)
    {
        QueryOptions<Category> options = new()
        {
            Includes = "Books",
            Where = c => c.CategoryId == id
        };

        return await _data.Categories.GetAsync(options);
    }

    public async Task AddCategoryAsync(Category category)
    {
        _data.Categories.Add(category);
        await _data.SaveAsync();
    }

    public async Task UpdateCategoryAsync(Category category)
    {
        _data.Categories.Update(category);
        await _data.SaveAsync();
    }

    public async Task RemoveCategoryAsync(Category category)
    {
        _data.Categories.Remove(category);
        await _data.SaveAsync();
    }

    public async Task SaveAsync()
    {
        await _data.SaveAsync();
    }
}
