using TopBookStore.Domain.Entities;

namespace TopBookStore.Application.DTOs;

public class BookDTO
{
    public int BookId { get; set; }
    public string Title { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public Dictionary<int, string> Authors { get; set; } = new();

    public BookDTO() { }

    // accept and load a book
    public BookDTO(Book book)
    {
        BookId = book.BookId;
        Title = book.Title;
        Price = book.Price;

        if (book.Authors.Count > 0)
        {
            foreach (Author author in book.Authors)
            {
                Authors.Add(author.AuthorId, author.FullName);
            }
        }
    }
}