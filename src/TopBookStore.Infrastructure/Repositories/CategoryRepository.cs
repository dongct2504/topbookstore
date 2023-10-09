using TopBookStore.Domain.Entities;
using TopBookStore.Domain.Interfaces;
using TopBookStore.Infrastructure.Persistence;

namespace TopBookStore.Infrastructure.Repositories;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    public CategoryRepository(TopBookStoreContext context) : base(context)
    {
    }

    public void Update(Category category)
    {
        _context.Update(category);
    }
}
