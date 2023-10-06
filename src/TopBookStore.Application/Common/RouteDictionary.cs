using TopBookStore.Application.DTOs;
using TopBookStore.Domain.Entities;
using TopBookStore.Domain.Extensions;

namespace TopBookStore.Application.Commond;

public class RouteDictionary : Dictionary<string, string>
{
    public RouteDictionary() { }

    public RouteDictionary(GridDTO values)
    {
        PageNumber = values.PageNumber;
        PageSize = values.PageSize;
        SortField = values.SortField;
        SortDirection = values.SortDirection;

        // set filter segments
        CategoryFilter = values.CategoryId;
        PriceFilter = values.Price;
        NumberOfPagesFilter = values.NumberOfPages;
        AuthorFilter = values.AuthorId;
    }

    // get, set these DTOs properties for routing
    public int PageNumber
    {
        get => GetIntValue(nameof(GridDTO.PageNumber)); // get a value int
        set => this[nameof(GridDTO.PageNumber)] = value.ToString(); // so set it's key a string
    }

    public int PageSize
    {
        get => GetIntValue(nameof(GridDTO.PageSize));
        set => this[nameof(GridDTO.PageSize)] = value.ToString();
    }

    public string SortField
    {
        get => Get(nameof(GridDTO.SortField)) ?? string.Empty;
        set => this[nameof(GridDTO.SortField)] = value;
    }

    public string SortDirection
    {
        get => Get(nameof(GridDTO.SortDirection)) ?? string.Empty;
        set => this[nameof(GridDTO.SortDirection)] = value;
    }

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

    private int GetIntValue(string key) =>
        int.TryParse(Get(key), out int intValue) ? intValue : 0;

    private string? Get(string key) => ContainsKey(key) ? this[key] : null;

    public void SetSortAndDirection(string fieldName, RouteDictionary current)
    {
        this[nameof(GridDTO.SortField)] = fieldName;

        if (current.SortField.EqualsNoCase(fieldName) && current.SortField == "asc")
        {
            this[nameof(GridDTO.SortDirection)] = "desc";
        }
        else
        {
            this[nameof(GridDTO.SortDirection)] = "asc";
        }
    }

    // // does not create new instances of referenced objects (and mutable objects)
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

    // sort flags
    public bool IsSortByPrice => SortField.EqualsNoCase(nameof(Book.Price));
}
