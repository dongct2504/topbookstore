using TopBookStore.Domain.Entities;
using TopBookStore.Domain.Interfaces;
using TopBookStore.Infrastructure.Persistence;

namespace TopBookStore.Infrastructure.Repositories;

public class AuthorRepository : Repository<Author>, IAuthorRepository
{
    public AuthorRepository(TopBookStoreContext context) : base(context)
    {
        
    }

    public void Update(Author author)
    {
        throw new NotImplementedException();
    }
}
