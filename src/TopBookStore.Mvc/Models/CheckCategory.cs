using TopBookStore.Domain.Entities;

namespace TopBookStore.Mvc.Models;

public static class CheckCategory
{
    public static bool IsSelected(Book book, Category category)
    {
        return book.Categories.Any(c => c.CategoryId == category.CategoryId);
    }
}