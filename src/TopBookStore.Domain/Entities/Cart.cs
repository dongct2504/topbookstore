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

    [InverseProperty("Cart")]
    public virtual ICollection<Book> Books { get; set; } = new List<Book>();

    [InverseProperty("Cart")]
    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();
}
