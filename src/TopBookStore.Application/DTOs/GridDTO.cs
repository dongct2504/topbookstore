using System.Text.Json.Serialization;

namespace TopBookStore.Application.DTOs;

public class GridDTO
{
    // only for transfer data from route to controller
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 4;
    public string SortField { get; set; } = string.Empty;
    public string SortDirection { get; set; } = string.Empty;

    // only for filter
    [JsonIgnore]
    public const string DefaultFilter = "all";

    public string CategoryId { get; set; } = DefaultFilter;
    public string Price { get; set; } = DefaultFilter;
    public string NumberOfPages { get; set; } = DefaultFilter;
    public string AuthorId { get; set; } = DefaultFilter;
}