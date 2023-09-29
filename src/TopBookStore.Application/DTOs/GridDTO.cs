namespace TopBookStore.Application.DTOs;

public class GridDTO
{
    // Only transfer data like this from the route to the controller

    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 4;
    public string SortField { get; set; } = string.Empty;
    public string SortDirection { get; set; } = string.Empty;
}