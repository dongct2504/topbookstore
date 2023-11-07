using System.ComponentModel.DataAnnotations;

namespace TopBookStore.Application.DTOs;

public class CartItemDto
{
    // used when storing CartItem data to persistent cookie. Use a DTO so only store the 
    // minimal amount of data needed to restore data from database.
    public int CartItemId { get; set; }

    public int CartId { get; set; }

    public int BookId { get; set; }

    public decimal Price { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập số lượng")]
    [RegularExpression(@"^\d+$", ErrorMessage = "Vui lòng nhập số nguyên lớn hơn 0 cho Số lượng.")]
    public int Quantity { get; set; }
}