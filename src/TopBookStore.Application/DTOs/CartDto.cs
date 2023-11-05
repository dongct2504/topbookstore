using TopBookStore.Domain.Entities;

namespace TopBookStore.Application.DTOs;

public class CartDto
{
    public int CartId { get; set; }

    public decimal TotalAmount { get; set; }

    public IEnumerable<CartItem> CartItems { get; set; } = new List<CartItem>();
}
