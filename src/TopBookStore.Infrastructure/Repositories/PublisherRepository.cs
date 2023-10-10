using TopBookStore.Domain.Entities;
using TopBookStore.Domain.Interfaces;
using TopBookStore.Infrastructure.Persistence;

namespace TopBookStore.Infrastructure.Repositories;

public class PublisherRepository : Repository<Publisher>, IPublisherRepository
{
    public PublisherRepository(TopBookStoreContext context) : base(context)
    {
    }

    public void Update(Publisher publisher)
    {
        _context.Update(publisher);
    }
}