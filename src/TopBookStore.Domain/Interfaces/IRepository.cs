namespace TopBookStore.Domain.Interfaces;

using TopBookStore.Domain.Queries;

public interface IRepository<T> where T : class
{
    Task<List<T>> ListAllAsync(QueryOptions<T> options);

    int Count { get; }

    Task<T?> GetAsync(int id);
    Task<T?> GetAsync(string id);
    Task<T?> GetAsync(QueryOptions<T> options);

    void Insert(T entity);
    void Update(T entity);
    void Delete(T entity);

    void Save();
}