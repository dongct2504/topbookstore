using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TopBookStore.Domain.Entities;

[Table("Authors")]
[MetadataType(typeof(AuthorMetadata))]
public partial class Author
{
    public string FullName => $"{LastName} {FirstName}";
}

public class AuthorMetadata
{
    [StringLength(60)]
    [Required(ErrorMessage = "Vui lòng nhập Tên của tác giả.")]
    public string FirstName { get; set; } = null!;

    [StringLength(60)]
    [Required(ErrorMessage = "Vui lòng nhập Họ của tác giả.")]
    public string LastName { get; set; } = null!;
}