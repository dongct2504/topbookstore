using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TopBookStore.Domain.Entities;

public partial class BookCategory
{
    [Key]
    public int BookId { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string CategoryId { get; set; } = null!;

    [ForeignKey("BookId")]
    [InverseProperty("BookCategory")]
    public virtual Book Book { get; set; } = null!;

    [ForeignKey("CategoryId")]
    [InverseProperty("BookCategories")]
    public virtual Category Category { get; set; } = null!;
}
