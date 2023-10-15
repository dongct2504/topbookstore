using System.ComponentModel.DataAnnotations;

namespace TopBookStore.Application.DTOs;

public class CategoryDto
{
    public int CategoryId { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập Thể Loại.")]
    [StringLength(80, ErrorMessage = "Nhập Thể Loại ngắn hơn 80 kí tự.")]
    public string Name { get; set; } = null!;
}