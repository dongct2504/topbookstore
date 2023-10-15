using TopBookStore.Application.Commond;
using TopBookStore.Application.DTOs;
using TopBookStore.Domain.Entities;

namespace TopBookStore.Mvc.Models;

public class BookListViewModel
{
    public BookDto BookDto { get; set; } = new();
    public IEnumerable<Book> Books { get; set; } = new List<Book>();

    public RouteDictionary CurrentRoute { get; set; } = new();
    public int TotalPages { get; set; }
    public int Id { get; set; }

    // data for dropdowns both for filter and display
    public IEnumerable<Author> Authors { get; set; } = new List<Author>();
    public IEnumerable<Publisher> Publishers { get; set; } = new List<Publisher>();
    public IEnumerable<Category> Categories { get; set; } = new List<Category>();
    public Dictionary<string, string> Prices =>
        new()
        {
            { "under50", "Dưới 50,000 VND" },
            { "50to150", "50,000 tới 150,000 VND" },
            { "150to500", "Từ 150,000 tới 500,000 VND" },
            { "500to1000", "Từ 500,000 tới 1,000,000 VND" },
            { "over1000", "1,000,000 VND trở lên" }
        };
    public Dictionary<string, string> NumberOfPages =>
        new()
        {
            { "under100", "Dưới 100 trang" },
            { "100to500", "Từ 100 đến 500 trang" },
            { "over500", "500 trang trở lên" }
        };
}