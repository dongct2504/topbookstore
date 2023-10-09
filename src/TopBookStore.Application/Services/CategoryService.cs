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
        return await _data.Categories.ListAllAsync(new QueryOptions<Category> { 
            OrderBy = c => c.Name
        });
    }

    public async Task<Category?> GetCategoryByIdAsync(int id)
    {
        return await _data.Categories.GetAsync(id);
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
}
