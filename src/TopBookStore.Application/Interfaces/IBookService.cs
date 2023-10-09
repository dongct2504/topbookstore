using TopBookStore.Application.DTOs;
using TopBookStore.Domain.Entities;

namespace TopBookStore.Application.Interfaces;

public interface IBookService
{
    Task<IEnumerable<Book>> GetAllBooksAsync(GridDTO values);
    Task<BookListDTO> GetBooksByCategoryAsync(GridDTO values, int? id);
    Task<BookListDTO> FilterBooksAsync(GridDTO values);
    Task<Book?> GetBookByIdAsync(int id);

    Task AddBookAsync(Book book);
    Task UpdateBookAsync(Book book);
    Task DeleteBookAsync(int id);
}