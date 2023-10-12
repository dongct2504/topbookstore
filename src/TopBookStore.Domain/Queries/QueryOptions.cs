using System.Linq.Expressions;

namespace TopBookStore.Domain.Queries;

public class QueryOptions<T>
{
    // private field, public properties for Include strings.
    private string[] includes = Array.Empty<string>();

    // write-only property for Include strings.
    // Includes = "Book, Author";
    public string Includes
    {
        set => includes = value.Replace(" ", "").Split(",");
    }

    // get method for Include strings.
    public string[] GetIncludes() => includes;

    // public properties for filtering.
    public List<Expression<Func<T, bool>>> WhereClauses { get; set; } = null!;
    public Expression<Func<T, bool>> Where
    {
        set
        {
            // if WhereClauses is null then create a new one.
            WhereClauses ??= new List<Expression<Func<T, bool>>>();
            WhereClauses.Add(value);
        }
    }

    public Expression<Func<T, object>> OrderBy { get; set; } = null!;
    public string OrderByDirection { get; set; } = "asc"; // only if have orderby to invoke this.

    // flags.
    public bool HasInclude => includes != Array.Empty<string>();
    public bool HasWhere => WhereClauses is not null;
    public bool HasOrderBy => OrderBy is not null;
}