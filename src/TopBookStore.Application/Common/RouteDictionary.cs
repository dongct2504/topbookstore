using TopBookStore.Application.DTOs;

namespace TopBookStore.Application.Commond;

public class RouteDictionary : Dictionary<string, string>
{
    // get, set these DTOs properties for filtering
    public string CategoryFilter
    {
        get => Get(nameof(GridDTO.CategoryId)) ?? GridDTO.DefaultFilter;
        set => this[nameof(GridDTO.CategoryId)] = value;
    }

    public string PriceFilter
    {
        get => Get(nameof(GridDTO.Price)) ?? GridDTO.DefaultFilter;
        set => this[nameof(GridDTO.Price)] = value;
    }

    public string NumberOfPagesFilter
    {
        get => Get(nameof(GridDTO.NumberOfPages)) ?? GridDTO.DefaultFilter;
        set => this[nameof(GridDTO.NumberOfPages)] = value;
    }

    public string AuthorFilter
    {
        // get => GetIntValue(nameof(GridDTO.AuthorId));
        get => Get(nameof(GridDTO.AuthorId)) ?? GridDTO.DefaultFilter;
        set => this[nameof(GridDTO.AuthorId)] = value;
    }

    public RouteDictionary() { }

    public RouteDictionary(GridDTO values)
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
        CategoryFilter = PriceFilter = NumberOfPagesFilter = AuthorFilter = GridDTO.DefaultFilter;


    // filter flags
    public bool IsFilterByCategory => CategoryFilter != GridDTO.DefaultFilter;
    public bool IsFilterByPrice => PriceFilter != GridDTO.DefaultFilter;
    public bool IsFilterByNumberOfPages => NumberOfPagesFilter != GridDTO.DefaultFilter;
    public bool IsFilterByAuthor => AuthorFilter != GridDTO.DefaultFilter;
}
