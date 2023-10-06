using TopBookStore.Domain.Entities;

namespace TopBookStore.Application.DTOs;

public class BookDTO
{
    public int BookId { get; set; }
    public string Title { get; set; } = string.Empty;
    public decimal Price { get; set; }

    public BookDTO() { }

    // accept and load a book
    public BookDTO(Book book)
    {
        BookId = book.BookId;
        Title = book.Title;
        Price = book.Price;
    }
}