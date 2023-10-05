using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TopBookStore.Domain.Interfaces;
using TopBookStore.Domain.Queries;
using TopBookStore.Infrastructure.Persistence;

namespace TopBookStore.Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    // private backing field bc when filtering (where) count might be less than _dbset.Count()
    private int? count;

    protected readonly TopBookStoreContext _context;
    private readonly DbSet<T> _dbset;

    public Repository(TopBookStoreContext context)
    {
        _context = context;
        _dbset = _context.Set<T>(); // _context.Books()
    }

    public async Task<IEnumerable<T>> ListAllAsync(QueryOptions<T> options) =>
        await BuildQuery(options).ToListAsync();

    // if count is null (where is not use) then use _dbset.CountAsync()
    public async Task<int> CountAsync() => count ?? await _dbset.CountAsync();

    public virtual async Task<T?> GetAsync(int id) =>
        await _dbset.FindAsync(id);
    public virtual async Task<T?> GetAsync(string id) =>
        await _dbset.FindAsync(id);
    public virtual async Task<T?> GetAsync(QueryOptions<T> options) =>
        await BuildQuery(options).FirstOrDefaultAsync();

    public void Insert(T entity) => _dbset.Add(entity);
    public void Update(T entity) => _dbset.Update(entity);
    public void Delete(T entity) => _dbset.Remove(entity);

    public void Save() => _context.SaveChanges();

    private IQueryable<T> BuildQuery(QueryOptions<T> options)
    {
        IQueryable<T> query = _dbset; // ex: _context.Books;

        if (options.HasInclude)
        {
            foreach (string include in options.GetIncludes())
            {
                query = query.Include(include);
            }
        }

        if (options.HasWhere)
        {
            foreach (Expression<Func<T, bool>> expression in options.WhereClauses)
            {
                query = query.Where(expression);
            }
            count = query.Count(); // get filter count
        }

        if (options.HasOrderBy)
        {
            if (options.OrderByDirection == "asc")
            {
                query = query.OrderBy(options.OrderBy);
            }
            else
            {
                query = query.OrderByDescending(options.OrderBy);
            }
        }

        if (options.HasPaging)
        {
            query = query.PageBy(options.PageNumber, options.PageSize);
        }

        return query;
    }
}