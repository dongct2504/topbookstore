using System.ComponentModel.DataAnnotations.Schema;

namespace TopBookStore.Domain.Entities;

[Table("Customers")]
public partial class Customer
{
    [NotMapped]
    public string Role { get; set; } = string.Empty;
}