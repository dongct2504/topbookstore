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
    private readonly IRepository<Book> _data;

    public BookService(IRepository<Book> data)
    {
        _data = data;
    }

    public async Task<IEnumerable<Book>> GetAllBooksAsync(GridDTO values)
    {
        QueryOptions<Book> options = new()
        {
            Includes = "Authors, Category",
            OrderByDirection = values.SortDirection,
            PageSize = values.PageSize,
            PageNumber = values.PageNumber
        };

        return await _data.ListAllAsync(options);
    }

    public async Task<BookListDTO> GetBooksByCategoryAsync(GridDTO values, string? id)
    {
        QueryOptions<Book> options = new()
        {
            Includes = "Authors, Category",
            OrderByDirection = values.SortDirection,
            PageSize = values.PageSize,
            PageNumber = values.PageNumber
        };

        if (id is not null)
        {
            options.Where = b => b.CategoryId == id;
        };

        BookListDTO dto = new()
        {
            Books = await _data.ListAllAsync(options),
            TotalCount = await _data.CountAsync()
        };

        return dto;
    }

    public async Task<BookListDTO> FilterBooksAsync(GridDTO values)
    {
        QueryOptions<Book> options = new()
        {
            Includes = "Authors, Category",
            OrderByDirection = values.SortDirection,
            PageNumber = values.PageNumber,
            PageSize = values.PageSize
        };

        RouteDictionary route = new(values);

        // filter
        if (route.IsFilterByCategory)
        {
            options.Where = b => b.CategoryId == route.CategoryFilter;
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
                options.Where = b => b.Authors.Any(a => a.AuthorId == authorId);
            }
        }

        // sort
        if (route.IsSortByCategory)
        {
            options.OrderBy = b => b.Category.Name;
        }
        else if (route.IsSortByPrice)
        {
            options.OrderBy = b => b.Price;
        }

        BookListDTO dto = new()
        {
            Books = await _data.ListAllAsync(options),
            TotalCount = await _data.CountAsync()
        };

        return dto;
    }

    public async Task<Book> GetBookByIdAsync(int id)
    {
        Book book = await _data.GetAsync(new QueryOptions<Book>
        {
            Where = b => b.BookId == id,
            Includes = "Authors, Category"
        }) ?? new Book();

        return book;
    }

    public Task CreateBook(Book book)
    {
        throw new NotImplementedException();
    }

    public Task DeleteBook(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateBook(Book book)
    {
        throw new NotImplementedException();
    }
}