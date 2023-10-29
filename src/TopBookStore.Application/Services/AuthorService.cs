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
        return await _data.Authors.ListAllAsync(new QueryOptions<Author>());
    }

    public async Task<Author?> GetAuthorByIdAsync(int id)
    {
        QueryOptions<Author> options = new()
        {
            Where = a => a.AuthorId == id,
            Includes = "Books"
        };

        return await _data.Authors.GetAsync(options);
    }

    public async Task<IEnumerable<Author>> GetAuthorsByTermAsync(string term)
    {
        QueryOptions<Author> options = new()
        {
            Where = a => a.FirstName.Contains(term) || a.LastName.Contains(term)
        };

        return await _data.Authors.ListAllAsync(options);
    }

    public async Task AddAuthorAsync(Author author)
    {
        _data.Authors.Add(author);
        await _data.SaveAsync();
    }

    public async Task UpdateAuthorAsync(Author author)
    {
        _data.Authors.Update(author);
        await _data.SaveAsync();
    }

    public async Task RemoveAuthorAsync(Author author)
    {
        _data.Authors.Remove(author);
        await _data.SaveAsync();
    }
}