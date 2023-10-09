namespace TopBookStore.Domain.Interfaces;

public interface ITopBookStoreUnitOfWork : IDisposable
{
    IBookRepository Books { get; }
    ICategoryRepository Categories { get; }
    IAuthorRepository Authors { get; }

    Task SaveAsync();
}