using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TopBookStore.Domain.Entities;

public partial class Book
{
    [Key]
    public int BookId { get; set; }

    [StringLength(80)]
    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    [StringLength(13)]
    [Unicode(false)]
    public string Isbn13 { get; set; } = null!;

    public int Inventory { get; set; }

    [Column(TypeName = "money")]
    public decimal Price { get; set; }

    [Column(TypeName = "decimal(3, 2)")]
    public decimal DiscountPercent { get; set; }

    public int? NumberOfPages { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime PulicationDate { get; set; }

    public int AuthorId { get; set; }

    public int PublisherId { get; set; }

    public int OrderId { get; set; }

    public int CartId { get; set; }

    [ForeignKey("AuthorId")]
    [InverseProperty("Books")]
    public virtual Author Author { get; set; } = null!;

    [ForeignKey("CartId")]
    [InverseProperty("Books")]
    public virtual Cart Cart { get; set; } = null!;

    [ForeignKey("OrderId")]
    [InverseProperty("Books")]
    public virtual Order Order { get; set; } = null!;

    [ForeignKey("PublisherId")]
    [InverseProperty("Books")]
    public virtual Publisher Publisher { get; set; } = null!;

    [ForeignKey("BookId")]
    [InverseProperty("Books")]
    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();
}
