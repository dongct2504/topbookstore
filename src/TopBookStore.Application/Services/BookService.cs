using TopBookStore.Application.DTOs;
using TopBookStore.Application.Interfaces;
using TopBookStore.Domain.Entities;
using TopBookStore.Domain.Interfaces;
using TopBookStore.Domain.Queries;
using TopBookStore.Domain.Extensions;
using TopBookStore.Application.Routing;
using AutoMapper;

namespace TopBookStore.Application.Services;

public class BookService : IBookService
{
    private readonly ITopBookStoreUnitOfWork _data;
    private readonly IMapper _mapper;

    public BookService(ITopBookStoreUnitOfWork data, IMapper mapper)
    {
        _data = data;
        _mapper = mapper;
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

    public async Task<BookDto?> GetBookDtoByIdAsync(int id)
    {
        QueryOptions<Book> options = new()
        {
            Where = b => b.BookId == id,
            Includes = "Author, Publisher, Categories"
        };

        Book? book = await _data.Books.GetAsync(options);
        if (book is null)
        {
            return null;
        }

        return _mapper.Map<BookDto>(book);
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

    public async Task AddBookAsync(BookDto bookDto)
    {
        Book book = _mapper.Map<Book>(bookDto);

        await _data.Books.AddNewCategoriesAsync(book, bookDto.CategoryIds, _data.Categories);

        _data.Books.Add(book);
        await _data.SaveAsync();
    }

    public async Task UpdateBookAsync(Book book)
    {
        _data.Books.Update(book);
        await _data.SaveAsync();
    }

    public async Task UpdateBookAsync(BookDto bookDto)
    {
        QueryOptions<Book> options = new()
        {
            Where = b => b.BookId == bookDto.BookId,
            Includes = "Categories"
        };

        Book bookFromDb = await _data.Books.GetAsync(options) ?? new Book();
        bookFromDb.BookId = bookDto.BookId;
        bookFromDb.Title = bookDto.Title;
        bookFromDb.Description = bookDto.Description;
        bookFromDb.Isbn13 = bookDto.Isbn13;
        bookFromDb.Inventory = bookDto.Inventory;
        bookFromDb.Price = bookDto.Price;
        bookFromDb.DiscountPercent = bookDto.DiscountPercent;
        bookFromDb.NumberOfPages = bookDto.NumberOfPages;
        bookFromDb.PublicationDate = bookDto.PublicationDate;
        bookFromDb.ImageUrl = bookDto.ImageUrl;
        bookFromDb.AuthorId = bookDto.AuthorId;
        bookFromDb.PublisherId = bookDto.PublisherId;

        await _data.Books.AddNewCategoriesAsync(bookFromDb, bookDto.CategoryIds, _data.Categories);

        // don't need to call _data.Books.Update(bookDto) - db context is tracking changes 
        // because retrieved bookDto with categories from db at the beginning
        await _data.SaveAsync();
    }

    public async Task RemoveBookAsync(Book bookDto)
    {
        _data.Books.Remove(bookDto);
        await _data.SaveAsync();
    }
}