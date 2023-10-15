using TopBookStore.Application.DTOs;

namespace TopBookStore.Application.Routing;

public class RouteDictionary : Dictionary<string, string>
{
    // get, set these DTOs properties for filtering
    public string CategoryFilter
    {
        get => Get(nameof(GridDto.CategoryId)) ?? GridDto.DefaultFilter;
        set => this[nameof(GridDto.CategoryId)] = value;
    }

    public string PriceFilter
    {
        get => Get(nameof(GridDto.Price)) ?? GridDto.DefaultFilter;
        set => this[nameof(GridDto.Price)] = value;
    }

    public string NumberOfPagesFilter
    {
        get => Get(nameof(GridDto.NumberOfPages)) ?? GridDto.DefaultFilter;
        set => this[nameof(GridDto.NumberOfPages)] = value;
    }

    public string AuthorFilter
    {
        // get => GetIntValue(nameof(GridDto.AuthorId));
        get => Get(nameof(GridDto.AuthorId)) ?? GridDto.DefaultFilter;
        set => this[nameof(GridDto.AuthorId)] = value;
    }

    public RouteDictionary() { }

    public RouteDictionary(GridDto values)
    {
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
        CategoryFilter = PriceFilter = NumberOfPagesFilter = AuthorFilter = GridDto.DefaultFilter;


    // filter flags
    public bool IsFilterByCategory => CategoryFilter != GridDto.DefaultFilter;
    public bool IsFilterByPrice => PriceFilter != GridDto.DefaultFilter;
    public bool IsFilterByNumberOfPages => NumberOfPagesFilter != GridDto.DefaultFilter;
    public bool IsFilterByAuthor => AuthorFilter != GridDto.DefaultFilter;
}
