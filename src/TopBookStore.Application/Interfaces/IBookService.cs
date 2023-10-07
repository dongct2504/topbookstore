using TopBookStore.Application.DTOs;
using TopBookStore.Domain.Entities;

namespace TopBookStore.Application.Interfaces;

public interface IBookService
{
    Task<IEnumerable<Book>> GetAllBooksAsync(GridDTO values);
    Task<BookListDTO> GetBooksByCategoryAsync(GridDTO values, string? id);
    Task<BookListDTO> FilterBooksAsync(GridDTO values);
    Task<Book> GetBookByIdAsync(int id);

    Task CreateBookAsync(Book book);
    Task UpdateBookAsync(Book book);
    Task DeleteBookAsync(int id);
}