using TopBookStore.Domain.Entities;
using TopBookStore.Domain.Interfaces;
using TopBookStore.Infrastructure.Persistence;
using TopBookStore.Infrastructure.Repositories;

namespace TopBookStore.Infrastructure.UnitOfWork;

public class TopBookStoreUnitOfWork : ITopBookStoreUnitOfWork
{
    private IRepository<Book> bookData = null!;
    private IRepository<Category> categoryData = null!;
    private IRepository<Author> authorData = null!;

    private readonly TopBookStoreContext _context;

    public TopBookStoreUnitOfWork(TopBookStoreContext context) => _context = context;

    public IRepository<Book> Books =>
        bookData ??= new Repository<Book>(_context); // if bookData is null

    public IRepository<Category> Categories =>
        categoryData ??= new Repository<Category>(_context);

    public IRepository<Author> Authors =>
        authorData ??= new Repository<Author>(_context);

    public void DeleteCurrentBookAuthors(Book book)
    {
        throw new NotImplementedException();
    }

    public void LoadCurrentBookAuthors(Book book, int[] authorIds)
    {
        throw new NotImplementedException();
    }

    public void Save() => _context.SaveChanges();
}