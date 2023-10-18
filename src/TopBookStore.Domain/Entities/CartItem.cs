using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TopBookStore.Domain.Entities;

[Index("BookId", Name = "CARTITEMBOOK_FK")]
[Index("CartId", Name = "CARTITEMCARTS_FK")]
public partial class CartItem
{
    [Key]
    public int CartItemId { get; set; }

    public int CartId { get; set; }

    public int BookId { get; set; }

    [Column(TypeName = "money")]
    public decimal Price { get; set; }

    public int Quantity { get; set; }

    [ForeignKey("BookId")]
    [InverseProperty("CartItems")]
    public virtual Book Book { get; set; } = null!;

    [ForeignKey("CartId")]
    [InverseProperty("CartItems")]
    public virtual Cart Cart { get; set; } = null!;
}
