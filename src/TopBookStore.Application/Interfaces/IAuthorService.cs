using TopBookStore.Application.DTOs;
using TopBookStore.Domain.Entities;

namespace TopBookStore.Application.Interfaces;

public interface IAuthorService
{
    Task<IEnumerable<Author>> GetAllAuthorsAsync();
    Task<Author?> GetAuthorByIdAsync(int id);
    Task<IEnumerable<Author>> GetAuthorsByTermAsync(string term);

    Task AddAuthorAsync(Author author);
    Task UpdateAuthorAsync(Author author);
    Task RemoveAuthorAsync(Author author);
    Task SaveAsync();
}