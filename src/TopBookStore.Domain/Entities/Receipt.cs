using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TopBookStore.Domain.Entities;

public partial class Receipt
{
    [Key]
    public int ReceiptId { get; set; }

    [Column(TypeName = "money")]
    public decimal Amount { get; set; }

    [StringLength(450)]
    public string CustomerId { get; set; } = null!;

    [ForeignKey("CustomerId")]
    [InverseProperty("Receipts")]
    public virtual Customer Customer { get; set; } = null!;
}
