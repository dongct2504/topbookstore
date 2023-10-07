using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TopBookStore.Domain.Entities;

public partial class Cart
{
    [Key]
    public int CartId { get; set; }

    public int Quantity { get; set; }

    [StringLength(450)]
    public string CustomerId { get; set; } = null!;

    [InverseProperty("Cart")]
    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    [ForeignKey("CustomerId")]
    [InverseProperty("Carts")]
    public virtual Customer Customer { get; set; } = null!;
}
