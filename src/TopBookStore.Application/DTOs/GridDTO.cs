namespace TopBookStore.Application.DTOs;

public class GridDto
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 6;

    // For filtering category in home index
    public int? Id { get; set; }
}