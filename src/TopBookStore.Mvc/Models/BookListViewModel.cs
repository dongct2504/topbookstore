using TopBookStore.Domain.Entities;
using TopBookStore.Mvc.Grid;

namespace TopBookStore.Mvc.Models;

public class BookListViewModel
{
    public List<Book> Books { get; set; } = new();

    public RouteDictionary CurrentRoute { get; set; } = new();
    public int TotalPages { get; set; }
    public string Id { get; set; } = string.Empty;

    // data for dropdowns
    public List<Category> Categories { get; set; } = new();
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
    public List<Author> Authors { get; set; } = new();
}