using TopBookStore.Domain.Entities;

namespace TopBookStore.Domain.Interfaces;

public interface ICategoryRepository : IRepository<Category>
{
    void Update(Category category);
}
