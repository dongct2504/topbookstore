using TopBookStore.Domain.Entities;
using TopBookStore.Mvc.Grid;

namespace TopBookStore.Mvc.Models;

public class AuthorListViewModel
{
    public List<Author> Authors { get; set; } = new();
    public RouteDictionary CurrentRoute { get; set; } = new();
    public int TotalPages { get; set; }
}