using System.ComponentModel.DataAnnotations;

namespace TopBookStore.Application.DTOs;

public class AuthorDto
{
    public int AuthorId { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập Tên của tác giả.")]
    [StringLength(60, ErrorMessage = "Nhập Tên của tác giả ngắn hơn 60 kí tự.")]
    public string FirstName { get; set; } = null!;

    [StringLength(60, ErrorMessage = "Nhập Tên của tác giả ngắn hơn 60 kí tự.")]
    [Required(ErrorMessage = "Vui lòng nhập Họ của tác giả.")]
    public string LastName { get; set; } = null!;

    public string? PhoneNumber { get; set; }
}