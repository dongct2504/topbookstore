using TopBookStore.Domain.Entities;
using TopBookStore.Domain.Interfaces;
using TopBookStore.Domain.Queries;

namespace TopBookStore.Application.Services;

public class PublisherService : IPublisherService
{
    private readonly ITopBookStoreUnitOfWork _data;

    public PublisherService(ITopBookStoreUnitOfWork data)
    {
        _data = data;
    }

    public async Task<IEnumerable<Publisher>> GetAllPublishersAsync()
    {
        return await _data.Publishers.ListAllAsync(new QueryOptions<Publisher>());
    }

    public async Task<Publisher?> GetPublisherByIdAsync(int id)
    {
        QueryOptions<Publisher> options = new()
        {
            Includes = "Books",
            Where = p => p.PublisherId == id
        };

        return await _data.Publishers.GetAsync(options);
    }

    public async Task AddPublisherAsync(Publisher publisher)
    {
        _data.Publishers.Add(publisher);
        await _data.SaveAsync();
    }

    public async Task UpdatePublisherAsync(Publisher publisher)
    {
        _data.Publishers.Update(publisher);
        await _data.SaveAsync();
    }

    public async Task RemovePublisherAsync(Publisher publisher)
    {
        _data.Publishers.Remove(publisher);
        await _data.SaveAsync();
    }

}