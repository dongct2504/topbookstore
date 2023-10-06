using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TopBookStore.Domain.Entities;

public partial class Category
{
    [Key]
    [StringLength(30)]
    [Unicode(false)]
    public string CategoryId { get; set; } = null!;

    [StringLength(80)]
    public string Name { get; set; } = null!;

    [ForeignKey("CategoryId")]
    [InverseProperty("Categories")]
    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
