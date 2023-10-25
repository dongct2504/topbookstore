using TopBookStore.Domain.Interfaces;
using TopBookStore.Infrastructure.Persistence;
using TopBookStore.Infrastructure.Repositories;

namespace TopBookStore.Infrastructure.UnitOfWork;

public class TopBookStoreUnitOfWork : ITopBookStoreUnitOfWork
{
    private readonly TopBookStoreContext _context;

    public IBookRepository Books { get; private set; }

    public ICategoryRepository Categories { get; private set; }

    public IAuthorRepository Authors { get; private set; }

    public IPublisherRepository Publishers { get; private set; }

    public ICustomerRepository Customers { get; private set; }

    public ICartItemRepository CartItems { get; private set; }

    public ICartRepository Carts { get; private set; }

    public IOrderDetailRepository OrderDetails { get; private set; }

    public IOrderRepository Orders { get; private set; }

    public TopBookStoreUnitOfWork(TopBookStoreContext context)
    {
        _context = context;
        Books = new BookRepository(_context);
        Categories = new CategoryRepository(_context);
        Authors = new AuthorRepository(_context);
        Publishers = new PublisherRepository(_context);
        Customers = new CustomerRepository(_context);
        CartItems = new CartItemRepository(_context);
        Carts = new CartRepository(_context);
        OrderDetails = new OrderDetailRepository(_context);
        Orders = new OrderRepository(_context);
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}