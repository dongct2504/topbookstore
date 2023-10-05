using TopBookStore.Application.DTOs;
using TopBookStore.Application.Interfaces;
using TopBookStore.Domain.Entities;
using TopBookStore.Domain.Interfaces;
using TopBookStore.Domain.Queries;

namespace TopBookStore.Application.Services;

public class AuthorService : IAuthorService
{
    private readonly IRepository<Author> _data;

    public AuthorService(IRepository<Author> data)
    {
        _data = data;
    }

    public async Task<AuthorListDTO> GetAllAuthorsAsync(GridDTO values)
    {
        QueryOptions<Author> options = new()
        {
            Includes = "Books",
            OrderByDirection = values.SortDirection,
            PageNumber = values.PageNumber,
            PageSize = values.PageSize,
        };

        AuthorListDTO dto = new()
        {
            Authors = await _data.ListAllAsync(options),
            TotalCount = await _data.CountAsync()
        };

        return dto;
    }

    public async Task<Author> GetAuthorByIdAsync(int id)
    {
        Author author = await _data.GetAsync(new QueryOptions<Author>
        {
            Where = a => a.AuthorId == id,
            Includes = "Books"
        }) ?? new Author();

        return author;
    }

    public Task AddAuthorAsync(Author author)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAuthorAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAuthorAsync(Author author)
    {
        throw new NotImplementedException();
    }
}