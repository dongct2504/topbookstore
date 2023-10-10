using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TopBookStore.Domain.Entities;

[MetadataType(typeof(BookMetadata))]
public partial class Book
{

}

public class BookMetadata
{
    [StringLength(80)]
    [Required(ErrorMessage = "Vui lòng nhập tiêu đề")]
    public string Title { get; set; } = null!;

    [Required(ErrorMessage = "Vui lòng nhập mô tả sách")]
    public string Description { get; set; } = null!;

    [Required(ErrorMessage = "Vui lòng nhập lượng tồn kho")]
    public int Inventory { get; set; }

    [Column(TypeName = "money")]
    [Required(ErrorMessage = "Vui lòng nhập giá tiền")]
    [Range(1, 999_999_999)]
    public decimal Price { get; set; }

    [Column(TypeName = "decimal(3, 2)")]
    [Required(ErrorMessage = "Vui lòng nhập giảm giá")]
    [Range(0, 1)]
    public decimal DiscountPercent { get; set; }

    [Column(TypeName = "datetime")]
    [Required(ErrorMessage = "Vui lòng nhập ngày phát hành")]
    public DateTime PublicationDate { get; set; }
}