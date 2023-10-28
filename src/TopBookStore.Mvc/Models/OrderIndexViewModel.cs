using TopBookStore.Application.DTOs;
using TopBookStore.Domain.Entities;

namespace TopBookStore.Mvc.Models;

public class OrderIndexViewModel
{
    public OrderDto OrderDto { get; set; } = new();
    public Cart Cart { get; set; } = new();
}