using TopBookStore.Domain.Interfaces;
using TopBookStore.Infrastructure.Persistence;
using TopBookStore.Infrastructure.Repositories;

namespace TopBookStore.Infrastructure.UnitOfWork;

public class TopBookStoreUnitOfWork : ITopBookStoreUnitOfWork
{
    private readonly TopBookStoreContext _context;

    public IBookRepository Books { get; private set; }

    public ICategoryRepository Categories { get; private set; }

    public IAuthorRepository Authors { get; private set; }

    public TopBookStoreUnitOfWork(TopBookStoreContext context)
    {
        _context = context;
        Books = new BookRepository(_context);
        Categories = new CategoryRepository(_context);
        Authors = new AuthorRepository(_context);
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}