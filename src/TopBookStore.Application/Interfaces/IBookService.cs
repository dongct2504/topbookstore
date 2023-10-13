using TopBookStore.Application.DTOs;
using TopBookStore.Domain.Entities;

namespace TopBookStore.Application.Interfaces;

public interface IBookService
{
    Task<IEnumerable<Book>> GetAllBooksAsync();
    Task<Book?> GetBookByIdAsync(int id);
    Task<IEnumerable<Book>> GetBooksByCategoryAsync(int id);
    Task<IEnumerable<Book>> FilterBooksAsync(GridDTO values);
    Task UpsertBookAsync(BookDTO bookDTO);

    Task AddBookAsync(Book book);
    Task UpdateBookAsync(Book book);
    Task RemoveBookAsync(Book book);
    Task SaveAsync();
}