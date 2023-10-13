using TopBookStore.Domain.Entities;

namespace TopBookStore.Domain.Interfaces;

public interface IBookRepository : IRepository<Book>
{
    Task AddNewCategoriesAsync(Book book, int[] categoryIds, ICategoryRepository categoryData);
    void Update(Book book);
}
