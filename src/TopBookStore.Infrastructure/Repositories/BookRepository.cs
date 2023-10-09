using TopBookStore.Domain.Entities;
using TopBookStore.Domain.Interfaces;
using TopBookStore.Infrastructure.Persistence;

namespace TopBookStore.Infrastructure.Repositories;

public class BookRepository : Repository<Book>, IBookRepository
{
    public BookRepository(TopBookStoreContext context) : base(context)
    {
    }

    public void Update(Book book)
    {
        _context.Update(book);
    }
}
