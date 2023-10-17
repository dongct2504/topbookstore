using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TopBookStore.Domain.Entities;

public partial class OrderDetail
{
    [Key]
    public int OrderDetailId { get; set; }

    [Column(TypeName = "money")]
    public decimal Price { get; set; }

    public int Quantity { get; set; }

    public int BookId { get; set; }

    public int OrderId { get; set; }

    [ForeignKey("BookId")]
    [InverseProperty("OrderDetails")]
    public virtual Book Book { get; set; } = null!;

    [ForeignKey("OrderId")]
    [InverseProperty("OrderDetails")]
    public virtual Order Order { get; set; } = null!;
}
