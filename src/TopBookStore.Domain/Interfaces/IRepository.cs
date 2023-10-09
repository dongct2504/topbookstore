using TopBookStore.Domain.Queries;

namespace TopBookStore.Domain.Interfaces;

public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> ListAllAsync(QueryOptions<T> options);

    Task<int> CountAsync();

    Task<T?> GetAsync(int id);
    Task<T?> GetAsync(string id);
    Task<T?> GetAsync(QueryOptions<T> options);

    void Add(T entity);
    void Remove(T entity);
    void RemoveRange(T entity);
}