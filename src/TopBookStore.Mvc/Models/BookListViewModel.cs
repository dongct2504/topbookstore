using TopBookStore.Domain.Entities;
using TopBookStore.Mvc.Grid;

namespace TopBookStore.Mvc.Models;

public class BookListViewModel
{
    public List<Book> Books { get; set; } = new();
    public List<Category> Categories { get; set; } = new();

    public RouteDictionary CurrentRoute { get; set; } = new();
    public int TotalPages { get; set; }
    public int Id { get; set; }
}