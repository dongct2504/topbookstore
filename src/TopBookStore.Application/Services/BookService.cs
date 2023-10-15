using TopBookStore.Application.DTOs;
using TopBookStore.Application.Interfaces;
using TopBookStore.Domain.Entities;
using TopBookStore.Domain.Interfaces;
using TopBookStore.Domain.Queries;
using TopBookStore.Domain.Extensions;
using TopBookStore.Application.Routing;

namespace TopBookStore.Application.Services;

public class BookService : IBookService
{
    private readonly ITopBookStoreUnitOfWork _data;

    public BookService(ITopBookStoreUnitOfWork data)
    {
        _data = data;
    }

    public async Task<IEnumerable<Book>> GetAllBooksAsync()
    {
        QueryOptions<Book> options = new()
        {
            Includes = "Author, Categories, Publisher"
        };

        return await _data.Books.ListAllAsync(options);
    }

    public async Task<Book?> GetBookByIdAsync(int id)
    {
        QueryOptions<Book> options = new()
        {
            Where = b => b.BookId == id,
            Includes = "Author, Publisher, Categories"
        };

        return await _data.Books.GetAsync(options);
    }

    public async Task<IEnumerable<Book>> GetBooksByCategoryAsync(int id)
    {
        QueryOptions<Book> options = new()
        {
            Where = b => b.Categories.Any(c => c.CategoryId == id),
            Includes = "Author, Categories"
        };

        return await _data.Books.ListAllAsync(options);
    }

    public async Task<IEnumerable<Book>> FilterBooksAsync(GridDto values)
    {
        QueryOptions<Book> options = new()
        {
            Includes = "Author, Categories",
        };

        RouteDictionary route = new(values);

        // filter
        if (route.IsFilterByCategory)
        {
            _ = int.TryParse(route.CategoryFilter, out int categoryId) ? categoryId : 0;
            options.Where = b => b.Categories.Any(c => c.CategoryId == categoryId);
        }
        if (route.IsFilterByPrice)
        {
            if (route.PriceFilter.EqualsNoCase("under50"))
            {
                options.Where = b => b.Price < 50_000;
            }
            else if (route.PriceFilter.EqualsNoCase("50to150"))
            {
                options.Where = b => b.Price >= 50_000 && b.Price <= 150_000;
            }
            else if (route.PriceFilter.EqualsNoCase("150to500"))
            {
                options.Where = b => b.Price > 150_000 && b.Price <= 500_000;
            }
            else if (route.PriceFilter.EqualsNoCase("500to1000"))
            {
                options.Where = b => b.Price > 500_000 && b.Price <= 1_000_000;
            }
            else if (route.PriceFilter.EqualsNoCase("over1000"))
            {
                options.Where = b => b.Price > 1_000_000;
            }
        }
        if (route.IsFilterByNumberOfPages)
        {
            if (route.NumberOfPagesFilter.EqualsNoCase("under100"))
            {
                options.Where = b => b.NumberOfPages < 100;
            }
            else if (route.NumberOfPagesFilter.EqualsNoCase("100to500"))
            {
                options.Where = b => b.NumberOfPages >= 100 && b.NumberOfPages <= 500;
            }
            else if (route.NumberOfPagesFilter.EqualsNoCase("over500"))
            {
                options.Where = b => b.NumberOfPages > 500;
            }
        }
        if (route.IsFilterByAuthor)
        {
            _ = int.TryParse(route.AuthorFilter, out int authorId) ? authorId : 0;
            if (authorId > 0)
            {
                // to filter the books by author, use the LINQ Any() method. 
                options.Where = b => b.AuthorId == authorId;
            }
        }

        return await _data.Books.ListAllAsync(options);
    }

    public async Task AddBookAsync(Book book)
    {
        _data.Books.Add(book);
        await _data.SaveAsync();
    }

    public async Task UpdateBookAsync(Book book)
    {
        _data.Books.Update(book);
        await _data.SaveAsync();
    }

    public async Task RemoveBookAsync(Book book)
    {
        _data.Books.Remove(book);
        await _data.SaveAsync();
    }

    public async Task SaveAsync()
    {
        await _data.SaveAsync();
    }
}