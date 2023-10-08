using TopBookStore.Domain.Entities;

namespace TopBookStore.Domain.Interfaces;

public interface IBookRepository : IRepository<Book>
{
    void Update(Book book);
}
