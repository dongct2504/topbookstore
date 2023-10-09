using TopBookStore.Application.Commond;
using TopBookStore.Application.DTOs;
using TopBookStore.Application.Interfaces;
using TopBookStore.Domain.Entities;
using TopBookStore.Domain.Interfaces;
using TopBookStore.Domain.Queries;
using TopBookStore.Domain.Extensions;

namespace TopBookStore.Application.Services;

public class BookService : IBookService
{
    private readonly ITopBookStoreUnitOfWork _data;

    public BookService(ITopBookStoreUnitOfWork data)
    {
        _data = data;
    }

    public async Task<IEnumerable<Book>> GetAllBooksAsync(GridDTO values)
    {
        QueryOptions<Book> options = new()
        {
            Includes = "Author, Categories",
            OrderByDirection = values.SortDirection,
            PageSize = values.PageSize,
            PageNumber = values.PageNumber
        };

        return await _data.Books.ListAllAsync(options);
    }

    public async Task<BookListDTO> GetBooksByCategoryAsync(GridDTO values, int? id)
    {
        QueryOptions<Book> options = new()
        {
            Includes = "Author, Categories",
            OrderByDirection = values.SortDirection,
            PageSize = values.PageSize,
            PageNumber = values.PageNumber
        };

        if (id is not null)
        {
            options.Where = b => b.Categories.Any(c => c.CategoryId == id);
        };

        BookListDTO dto = new()
        {
            Books = await _data.Books.ListAllAsync(options),
            TotalCount = await _data.Books.CountAsync()
        };

        return dto;
    }

    public async Task<BookListDTO> FilterBooksAsync(GridDTO values)
    {
        QueryOptions<Book> options = new()
        {
            Includes = "Author, Categories",
            OrderByDirection = values.SortDirection,
            PageNumber = values.PageNumber,
            PageSize = values.PageSize
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

        // sort
        if (route.IsSortByPrice)
        {
            options.OrderBy = b => b.Price;
        }

        BookListDTO dto = new()
        {
            Books = await _data.Books.ListAllAsync(options),
            TotalCount = await _data.Books.CountAsync()
        };

        return dto;
    }

    public async Task<Book?> GetBookByIdAsync(int id)
    {
        Book? book = await _data.Books.GetAsync(new QueryOptions<Book>
        {
            Where = b => b.BookId == id,
            Includes = "Author, Categories"
        });

        return book;
    }

    public Task AddBookAsync(Book book)
    {
        throw new NotImplementedException();
    }

    public Task DeleteBookAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateBookAsync(Book book)
    {
        throw new NotImplementedException();
    }
}