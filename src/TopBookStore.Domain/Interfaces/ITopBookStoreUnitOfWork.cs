using TopBookStore.Domain.Entities;

namespace TopBookStore.Domain.Interfaces;

public interface ITopBookStoreUnitOfWork
{
    IRepository<Book> Books { get; }
    IRepository<Category> Categories { get; }
    IRepository<Author> Authors { get; }

    void DeleteCurrentBookAuthors(Book book);
    void LoadCurrentBookAuthors(Book book, int[] authorIds);

    void Save();
}