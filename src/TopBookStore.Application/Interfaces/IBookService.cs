using TopBookStore.Application.DTOs;
using TopBookStore.Domain.Entities;

namespace TopBookStore.Application.Interfaces;

public interface IBookService
{
    Task<IEnumerable<Book>> GetAllBooksAsync();
    Task<IEnumerable<Book>> GetAllBooksAsync(GridDto values);
    Task<IEnumerable<Book>> GetBooksByCategoryAsync(GridDto values);
    Task<IEnumerable<Book>> FilterBooksAsync(BookGridDto values);
    Task<int> GetBookCountAsync();

    Task<Book?> GetBookByIdAsync(int id);
    Task<BookDto?> GetBookDtoByIdAsync(int id);

    Task AddBookAsync(Book book);
    Task AddBookAsync(BookDto bookDto);

    Task UpdateBookAsync(Book book);
    Task UpdateBookAsync(BookDto bookDto);

    Task RemoveBookAsync(Book book);
}