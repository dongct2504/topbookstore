using TopBookStore.Application.DTOs;
using TopBookStore.Domain.Extensions;

namespace TopBookStore.Mvc.Grid;

public class RouteDictionary : Dictionary<string, string>
{
    public int PageNumber
    {
        get => GetIntValue(nameof(GridDTO.PageNumber));
        set => this[nameof(GridDTO.PageNumber)] = value.ToString();
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

    private int GetIntValue(string key) =>
        int.TryParse(Get(key), out int intValue) ? intValue : 0;

    private string? Get(string key) => ContainsKey(key) ? this[key] : null;

    public void SetSortAndDirection(string fieldName, RouteDictionary current)
    {
        this[nameof(GridDTO.SortField)] = fieldName;

        if (current.SortField.EqualNoCase(fieldName) && current.SortField == "asc")
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

    public RouteDictionary Clone()
    {
        RouteDictionary clone = new();

        foreach (string key in Keys)
        {
            clone.Add(key, this[key]);
        }

        return clone;
    }
}
