using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TopBookStore.Domain.Entities;

[Index("CustomerId", Name = "ORDERCUSTOMERS_FK")]
public partial class Order
{
    [Key]
    public int OrderId { get; set; }

    public int CustomerId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime OrderDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime ShippingDate { get; set; }

    [StringLength(128)]
    public string? Name { get; set; }

    [StringLength(15)]
    [Unicode(false)]
    public string PhoneNumber { get; set; } = null!;

    [Column(TypeName = "money")]
    public decimal TotalAmount { get; set; }

    [StringLength(128)]
    [Unicode(false)]
    public string? TrackingNumber { get; set; }

    [StringLength(128)]
    [Unicode(false)]
    public string? Carrier { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? OrderStatus { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? PaymentStatus { get; set; }

    [StringLength(256)]
    [Unicode(false)]
    public string TransactionId { get; set; } = null!;

    [StringLength(128)]
    public string? Address { get; set; }

    [StringLength(50)]
    public string? Ward { get; set; }

    [StringLength(30)]
    public string? District { get; set; }

    [StringLength(30)]
    public string? City { get; set; }

    [ForeignKey("CustomerId")]
    [InverseProperty("Orders")]
    public virtual Customer Customer { get; set; } = null!;

    [InverseProperty("Order")]
    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
