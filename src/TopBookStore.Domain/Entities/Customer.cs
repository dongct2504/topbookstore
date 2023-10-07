using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TopBookStore.Domain.Entities;

public partial class Customer
{
    [Key]
    public string CustomerId { get; set; } = null!;

    [StringLength(80)]
    public string FirstName { get; set; } = null!;

    [StringLength(80)]
    public string LastName { get; set; } = null!;

    [StringLength(15)]
    [Unicode(false)]
    public string? PhoneNumber { get; set; }

    [Column(TypeName = "money")]
    public decimal Debt { get; set; }

    [StringLength(80)]
    public string? Street { get; set; }

    [StringLength(50)]
    public string? District { get; set; }

    [StringLength(30)]
    public string? City { get; set; }

    [StringLength(30)]
    public string? Country { get; set; }

    public int CartId { get; set; }

    [InverseProperty("Customer")]
    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    [InverseProperty("Customer")]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    [InverseProperty("Customer")]
    public virtual ICollection<Receipt> Receipts { get; set; } = new List<Receipt>();
}
