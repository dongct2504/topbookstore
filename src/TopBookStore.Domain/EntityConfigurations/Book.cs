using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TopBookStore.Domain.Entities;

[Table("Books")]
public partial class Book
{
    public decimal DiscountAmount => Price * DiscountPercent;

    public decimal DiscountPrice => Price - DiscountAmount;
}