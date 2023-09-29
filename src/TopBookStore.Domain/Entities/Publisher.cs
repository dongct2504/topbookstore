using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TopBookStore.Domain.Entities;

public partial class Publisher
{
    [Key]
    public int PublisherId { get; set; }

    [StringLength(100)]
    public string Name { get; set; } = null!;

    [StringLength(15)]
    [Unicode(false)]
    public string? PhoneNumber { get; set; }

    [InverseProperty("Publisher")]
    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
