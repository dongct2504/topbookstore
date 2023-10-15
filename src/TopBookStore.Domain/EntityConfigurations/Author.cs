using System.ComponentModel.DataAnnotations.Schema;

namespace TopBookStore.Domain.Entities;

[Table("Authors")]
public partial class Author
{
    public string FullName => $"{LastName} {FirstName}";
}