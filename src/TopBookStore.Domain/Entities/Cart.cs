﻿using System;
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

    public int CustomerId { get; set; }

    public int BookId { get; set; }

    [ForeignKey("BookId")]
    [InverseProperty("Carts")]
    public virtual Book Book { get; set; } = null!;

    [ForeignKey("CustomerId")]
    [InverseProperty("Carts")]
    public virtual Customer Customer { get; set; } = null!;
}
