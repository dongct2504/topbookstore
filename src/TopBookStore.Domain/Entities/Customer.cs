using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TopBookStore.Domain.Entities;

public partial class Customer
{
    [Key]
    public int CustomerId { get; set; }

    [StringLength(50)]
    public string FirstName { get; set; } = null!;

    [StringLength(50)]
    public string LastName { get; set; } = null!;

    [Column(TypeName = "money")]
    public decimal Debt { get; set; }

    [StringLength(50)]
    public string? Street { get; set; }

    [StringLength(30)]
    public string? District { get; set; }

    [StringLength(30)]
    public string? City { get; set; }

    [StringLength(60)]
    public string? Country { get; set; }

    [InverseProperty("Customer")]
    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    [InverseProperty("Customer")]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
