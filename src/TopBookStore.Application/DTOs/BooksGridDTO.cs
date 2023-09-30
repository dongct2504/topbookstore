namespace TopBookStore.Application.DTOs;

public class BooksGridDTO : GridDTO
{
    // only for filter
    public string AuthorId { get; set; } = "all";
    public string CategoryId { get; set; } = "all";
    public string PriceId { get; set; } = "all";
}