namespace TopBookStore.Application.DTOs;

public class CartDto
{
    // used when storing CartItem data to persistent cookie. Use a DTO so only store the 
    // minimal amount of data needed to restore data from database.

    public int BookId { get; set; }
    public int Quantity { get; set; }
}