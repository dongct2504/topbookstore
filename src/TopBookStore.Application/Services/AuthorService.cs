using TopBookStore.Application.DTOs;
using TopBookStore.Application.Interfaces;
using TopBookStore.Domain.Entities;
using TopBookStore.Domain.Interfaces;
using TopBookStore.Domain.Queries;

namespace TopBookStore.Application.Services;

public class AuthorService : IAuthorService
{
    private readonly ITopBookStoreUnitOfWork _data;

    public AuthorService(ITopBookStoreUnitOfWork data)
    {
        _data = data;
    }

    public async Task<IEnumerable<Author>> GetAllAuthorsAsync()
    {
        QueryOptions<Author> options = new()
        {
            Includes = "Books"
        };

        return await _data.Authors.ListAllAsync(options);
    }

    public async Task<Author?> GetAuthorByIdAsync(int id)
    {
        Author? author = await _data.Authors.GetAsync(new QueryOptions<Author>
        {
            Where = a => a.AuthorId == id,
            Includes = "Books"
        });

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