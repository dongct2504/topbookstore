namespace TopBookStore.Application.DTOs;

public class BookDTO
{
    public int BookId { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Isbn13 { get; set; } = null!;

    public int Inventory { get; set; }

    public decimal Price { get; set; }

    public decimal DiscountPercent { get; set; }

    public int? NumberOfPages { get; set; }

    public DateTime PublicationDate { get; set; }

    public string? ImageUrl { get; set; }

    public int AuthorId { get; set; }

    public int PublisherId { get; set; }

    // for multiple checkbox
    public int[] CategoryIds { get; set; } = null!;

    // those two are for display
    public string AuthorName { get; set; } = null!;

    public string PublisherName { get; set; } = null!;
}
