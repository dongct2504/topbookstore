using TopBookStore.Domain.Entities;
using TopBookStore.Domain.Interfaces;
using TopBookStore.Infrastructure.Persistence;

namespace TopBookStore.Infrastructure.Repositories;

public class BookRepository : Repository<Book>, IBookRepository
{
    public BookRepository(TopBookStoreContext context) : base(context)
    {
    }

    public async Task AddNewCategoriesAsync(Book book, int[] categoryIds, ICategoryRepository categoryData)
    {
        // first remove any current categories
        book.Categories.Clear();

        // then add new categories
        foreach (int id in categoryIds)
        {
            Category? category = await categoryData.GetAsync(id);
            if (category is not null)
            {
                book.Categories.Add(category);
            }
        }
    }

    public void Update(Book book)
    {
        _context.Update(book);
    }
}
