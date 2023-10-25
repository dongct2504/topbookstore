using TopBookStore.Application.DTOs;
using TopBookStore.Domain.Extensions;

namespace TopBookStore.Application.Routing;

public class RouteDictionary : Dictionary<string, string>
{
    public int PageNumber
    {
        get => Get(nameof(GridDto.PageNumber))?.SafeToInt() ?? 0;
        set => this[nameof(GridDto.PageNumber)] = value.ToString();
    }

    public int PageSize
    {
        get => Get(nameof(GridDto.PageSize))?.SafeToInt() ?? 0;
        set => this[nameof(GridDto.PageSize)] = value.ToString();
    }

    // get, set these DTOs properties for filtering
    public string CategoryFilter
    {
        get => Get(nameof(BookGridDto.CategoryId)) ?? BookGridDto.DefaultFilter;
        set => this[nameof(BookGridDto.CategoryId)] = value;
    }

    public string PriceFilter
    {
        get => Get(nameof(BookGridDto.Price)) ?? BookGridDto.DefaultFilter;
        set => this[nameof(BookGridDto.Price)] = value;
    }

    public string NumberOfPagesFilter
    {
        get => Get(nameof(BookGridDto.NumberOfPages)) ?? BookGridDto.DefaultFilter;
        set => this[nameof(BookGridDto.NumberOfPages)] = value;
    }

    public string AuthorFilter
    {
        // get => GetIntValue(nameof(BookGridDto.AuthorId));
        get => Get(nameof(BookGridDto.AuthorId)) ?? BookGridDto.DefaultFilter;
        set => this[nameof(BookGridDto.AuthorId)] = value;
    }

    public RouteDictionary()
    {
    }

    public RouteDictionary(GridDto values)
    {
        PageNumber = values.PageNumber;
        PageSize = values.PageSize;
    }

    public RouteDictionary(BookGridDto values)
    {
        PageNumber = values.PageNumber;
        PageSize = values.PageSize;

        // set filter segments
        CategoryFilter = values.CategoryId;
        PriceFilter = values.Price;
        NumberOfPagesFilter = values.NumberOfPages;
        AuthorFilter = values.AuthorId;
    }

    private string? Get(string key) => ContainsKey(key) ? this[key] : null;

    // // does not create new instances of referenced objects (and mutable objects), these for paging
    // public RouteDictionary Clone() => (RouteDictionary)MemberwiseClone();

    // manually clone.
    public RouteDictionary Clone()
    {
        RouteDictionary clone = new();

        foreach (string key in Keys)
        {
            clone.Add(key, this[key]);
        }

        return clone;
    }

    public void LoadFilterSegments(string[] filter)
    {
        CategoryFilter = filter[0];
        PriceFilter = filter[1];
        NumberOfPagesFilter = filter[2];
        AuthorFilter = filter[3];
    }

    public void ClearFilters() =>
        CategoryFilter = PriceFilter = NumberOfPagesFilter = AuthorFilter = BookGridDto.DefaultFilter;


    // filter flags
    public bool IsFilterByCategory => CategoryFilter != BookGridDto.DefaultFilter;
    public bool IsFilterByPrice => PriceFilter != BookGridDto.DefaultFilter;
    public bool IsFilterByNumberOfPages => NumberOfPagesFilter != BookGridDto.DefaultFilter;
    public bool IsFilterByAuthor => AuthorFilter != BookGridDto.DefaultFilter;
}
