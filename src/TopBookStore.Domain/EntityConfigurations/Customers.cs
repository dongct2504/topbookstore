using System.ComponentModel.DataAnnotations.Schema;

namespace TopBookStore.Domain.Entities;

[Table("Customers")]
public partial class Customer
{
    public string FullName => $"{LastName} {FirstName}";
}