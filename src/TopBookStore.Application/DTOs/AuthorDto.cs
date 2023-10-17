using System.ComponentModel.DataAnnotations;

namespace TopBookStore.Application.DTOs;

public class AuthorDto
{
    public int AuthorId { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập tên của tác giả.")]
    [StringLength(60, ErrorMessage = "Tên của tác giả phải ngắn hơn 60 kí tự.")]
    public string FirstName { get; set; } = null!;

    [Required(ErrorMessage = "Vui lòng nhập họ của tác giả.")]
    [StringLength(60, ErrorMessage = "Họ của tác giả phải ngắn hơn 60 kí tự.")]
    public string LastName { get; set; } = null!;

    public string? PhoneNumber { get; set; }
}