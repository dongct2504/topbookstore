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

    [StringLength(100)]
    public string Title { get; set; } = null!;

    [StringLength(13)]
    [Unicode(false)]
    public string Isbn13 { get; set; } = null!;

    [Column(TypeName = "money")]
    public decimal Price { get; set; }

    public int? NumberOfPages { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? PulicationDate { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string CategoryId { get; set; } = null!;

    public int PublisherId { get; set; }

    [InverseProperty("Book")]
    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    [ForeignKey("CategoryId")]
    [InverseProperty("Books")]
    public virtual Category Category { get; set; } = null!;

    [InverseProperty("Book")]
    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    [ForeignKey("PublisherId")]
    [InverseProperty("Books")]
    public virtual Publisher Publisher { get; set; } = null!;

    [ForeignKey("BookId")]
    [InverseProperty("Books")]
    public virtual ICollection<Author> Authors { get; set; } = new List<Author>();
}
