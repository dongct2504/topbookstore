using TopBookStore.Application.DTOs;

namespace TopBookStore.Mvc.Models;

public class OrderIndexViewModel
{
    public OrderDto OrderDto { get; set; } = new();
    public CartDto CartDto { get; set; } = new();
}
