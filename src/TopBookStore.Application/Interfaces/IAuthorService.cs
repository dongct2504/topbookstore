using TopBookStore.Application.DTOs;
using TopBookStore.Domain.Entities;

namespace TopBookStore.Application.Interfaces;

public interface IAuthorService
{
    Task<AuthorListDTO> GetAllAuthorsAsync(GridDTO values);
    Task<Author> GetAuthorByIdAsync(int id);

    Task AddAuthorAsync(Author author);
    Task UpdateAuthorAsync(Author author);
    Task DeleteAuthorAsync(int id);
}