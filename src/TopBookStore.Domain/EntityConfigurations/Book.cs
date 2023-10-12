using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TopBookStore.Domain.Entities;

[Table("Books")]
[MetadataType(typeof(BookMetadata))]
public partial class Book
{

}

public class BookMetadata
{
    // [ValidateNever]
    // [ForeignKey("AuthorId")]
    // [InverseProperty("Books")]
    // public virtual Author Author { get; set; } = null!;

    // [ValidateNever]
    // [ForeignKey("PublisherId")]
    // [InverseProperty("Books")]
    // public virtual Publisher Publisher { get; set; } = null!;
}