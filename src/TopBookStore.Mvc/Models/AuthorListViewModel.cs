using TopBookStore.Application.Routing;
using TopBookStore.Domain.Entities;

namespace TopBookStore.Mvc.Models;

public class AuthorListViewModel
{
    public IEnumerable<Author> Authors { get; set; } = new List<Author>();
    public RouteDictionary CurrentRoute { get; set; } = new();
    public int TotalPages { get; set; }
}