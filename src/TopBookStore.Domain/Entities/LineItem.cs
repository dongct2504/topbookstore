using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TopBookStore.Domain.Entities;

[Table("LineItem")]
public partial class LineItem
{
    [Key]
    public int LineItemId { get; set; }

    public int Quantity { get; set; }

    public int InvoiceId { get; set; }

    public int BookId { get; set; }

    [ForeignKey("BookId")]
    [InverseProperty("LineItems")]
    public virtual Book Book { get; set; } = null!;

    [ForeignKey("InvoiceId")]
    [InverseProperty("LineItems")]
    public virtual Invoice Invoice { get; set; } = null!;
}
