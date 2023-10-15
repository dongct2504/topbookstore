using System.ComponentModel.DataAnnotations;
using TopBookStore.Domain.CustomValidations;

namespace TopBookStore.Application.DTOs;

public class BookDto
{
    public int BookId { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập Tiêu đề.")]
    [StringLength(80, ErrorMessage = "Nhập Tiêu đề ngắn hơn 80 kí tự.")]
    public string Title { get; set; } = null!;

    [Required(ErrorMessage = "Vui lòng nhập Mô Tả.")]
    public string Description { get; set; } = null!;

    [Required(ErrorMessage = "Vui lòng nhập Isbn13.")]
    [StringLength(13, ErrorMessage = "Vui lòng nhập Isbn13 hợp lệ.")]
    public string Isbn13 { get; set; } = null!;

    [Required(ErrorMessage = "Vui lòng nhập Hàng tồn kho.")]
    [RegularExpression(@"^\d+$", ErrorMessage = "Vui lòng nhập số nguyên lớn hơn 0 cho Hàng tồn kho.")]
    public int Inventory { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập Giá.")]
    [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Vui lòng nhập số nguyên lớn hơn 0 cho Giá.")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập Giảm giá.")]
    [RegularExpression(@"^\d+(\.\d{1,2})?$",
        ErrorMessage = "Vui lòng nhập số nguyên lớn hơn 0 cho Giảm giá.")]
    [Range(0.00, 1.0, ErrorMessage = "Nhập Giảm giá từ 0.00 đến 1.")]
    public decimal DiscountPercent { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập Số trang.")]
    [RegularExpression(@"^\d+$", ErrorMessage = "Vui lòng nhập số nguyên lớn hơn 0 cho Số trang.")]
    public int NumberOfPages { get; set; }

    [DateRange(ErrorMessage = "Ngày xuất bản phải từ ngày 1/1/1000 đến hiện tại.")]
    public DateTime PublicationDate { get; set; }

    public string? ImageUrl { get; set; }

    public int AuthorId { get; set; }

    public int PublisherId { get; set; }

    // for multiple checkbox
    [Required(ErrorMessage = "Vui lòng chọn Thể loại.")]
    public int[] CategoryIds { get; set; } = null!;

    // those two are for display
    [Required(ErrorMessage = "Vui lòng nhập Tác giả.")]
    public string AuthorName { get; set; } = null!;

    [Required(ErrorMessage = "Vui lòng nhập Nhà xuất bản.")]
    public string PublisherName { get; set; } = null!;
}
