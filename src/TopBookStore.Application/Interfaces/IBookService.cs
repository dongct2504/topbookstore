using TopBookStore.Application.DTOs;
using TopBookStore.Domain.Entities;

namespace TopBookStore.Application.Interfaces;

public interface IBookService
{
    Task<IEnumerable<Book>> GetAllBooksAsync();
    Task<IEnumerable<Book>> GetBooksByCategoryAsync(int id);
    Task<IEnumerable<Book>> FilterBooksAsync(GridDto values);

    Task<Book?> GetBookByIdAsync(int id);
    Task<BookDto?> GetBookDtoByIdAsync(int id);

    Task AddBookAsync(Book book);
    Task AddBookAsync(BookDto bookDto);

    Task UpdateBookAsync(Book book);
    Task UpdateBookAsync(BookDto bookDto);

    Task RemoveBookAsync(Book book);
}