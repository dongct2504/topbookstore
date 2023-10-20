namespace TopBookStore.Domain.Interfaces;

public interface ITopBookStoreUnitOfWork : IDisposable
{
    IBookRepository Books { get; }
    ICategoryRepository Categories { get; }
    IAuthorRepository Authors { get; }
    IPublisherRepository Publishers { get; }
    ICustomerRepository Customers { get; }
    ICartItemRepository CartItems { get; }
    ICartRepository Carts { get; }
    IOrderDetailRepository OrderDetails { get; }
    IOrderRepository Orders { get; }

    Task SaveAsync();
}