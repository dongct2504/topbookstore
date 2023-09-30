using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TopBookStore.Domain.Entities;

public partial class Address
{
    [Key]
    public int AddressId { get; set; }

    [StringLength(100)]
    public string? Street { get; set; }

    [StringLength(50)]
    public string? District { get; set; }

    [StringLength(50)]
    public string? City { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? ZipCode { get; set; }

    [StringLength(50)]
    public string? Country { get; set; }

    [InverseProperty("Address")]
    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();

    [InverseProperty("Address")]
    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
}
