using TopBookStore.Domain.Entities;
using TopBookStore.Domain.Interfaces;

namespace TopBookStore.Domain.Interfaces;

public interface IAuthorRepository : IRepository<Author>
{
    void Update(Author author);
}
