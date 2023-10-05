using TopBookStore.Domain.Entities;

namespace TopBookStore.Application.DTOs;

public class AuthorListDTO
{
    public IEnumerable<Author> Authors { get; set; } = new List<Author>();
    public int TotalCount { get; set; }
}