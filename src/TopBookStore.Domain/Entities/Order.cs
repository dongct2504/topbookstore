using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TopBookStore.Domain.Entities;

public partial class Order
{
    [Key]
    public int OrderId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime OrderDate { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string? State { get; set; }

    public int Quantity { get; set; }

    public int CustomerId { get; set; }

    [InverseProperty("Order")]
    public virtual ICollection<Book> Books { get; set; } = new List<Book>();

    [ForeignKey("CustomerId")]
    [InverseProperty("Orders")]
    public virtual Customer Customer { get; set; } = null!;
}
