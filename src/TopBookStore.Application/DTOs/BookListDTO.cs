using TopBookStore.Domain.Entities;

namespace TopBookStore.Application.DTOs;

public class BookListDTO
{
    public IEnumerable<Book> Books { get; set; } = new List<Book>();
    public int TotalCount { get; set; }
}