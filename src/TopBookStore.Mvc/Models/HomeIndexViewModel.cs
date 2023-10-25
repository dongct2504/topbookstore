using TopBookStore.Application.Routing;
using TopBookStore.Domain.Entities;

namespace TopBookStore.Mvc.Models;

public class HomeIndexViewModel
{
    public IEnumerable<Book> Books { get; set; } = new List<Book>();

    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }

    public RouteDictionary CurrentRoute { get; set; } = new();

    // for filtering category
    public int Id { get; set; }
}