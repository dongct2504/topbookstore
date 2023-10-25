using System.Text.Json.Serialization;

namespace TopBookStore.Application.DTOs;

public class BookGridDto : GridDto
{
    // only for filter
    [JsonIgnore]
    public const string DefaultFilter = "all";

    public string CategoryId { get; set; } = DefaultFilter;
    public string Price { get; set; } = DefaultFilter;
    public string NumberOfPages { get; set; } = DefaultFilter;
    public string AuthorId { get; set; } = DefaultFilter;
}