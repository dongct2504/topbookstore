using TopBookStore.Domain.Entities;

namespace TopBookStore.Application.Services;

public interface IPublisherService
{
    Task<IEnumerable<Publisher>> GetAllPublishersAsync();
    Task<Publisher?> GetPublisherByIdAsync(int id);
    Task<IEnumerable<Publisher>> GetPublishersByTermAsync(string term);

    Task AddPublisherAsync(Publisher category);
    Task UpdatePublisherAsync(Publisher category);
    Task RemovePublisherAsync(Publisher category);
    Task SaveAsync();
}