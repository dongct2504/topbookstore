using TopBookStore.Domain.Entities;

namespace TopBookStore.Domain.Interfaces;

public interface IPublisherRepository : IRepository<Publisher>
{
    void Update(Publisher publisherCompany);
}