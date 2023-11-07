using TopBookStore.Application.DTOs;
using TopBookStore.Domain.Entities;

namespace TopBookStore.Mvc.Models;

public class BookDetailsViewModel
{
    public CartItemDto CartItemDto { get; set; } = new();
    public Book Book { get; set; } = new();
}
