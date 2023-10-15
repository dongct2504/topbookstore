using TopBookStore.Application.DTOs;
using TopBookStore.Domain.Entities;

namespace TopBookStore.Mvc.Models;

public static class CheckCategory
{
    public static bool IsSelected(BookDto bookDto, Category category)
    {
        if (bookDto.BookId == 0)
        {
            return false;
        }
        return bookDto.CategoryIds.Contains(category.CategoryId);
    }
}