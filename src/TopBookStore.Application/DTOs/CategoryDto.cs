using System.ComponentModel.DataAnnotations;

namespace TopBookStore.Application.DTOs;

public class CategoryDto
{
    public int CategoryId { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập thể Loại.")]
    [StringLength(80, ErrorMessage = "Thể Loại phải ngắn hơn 80 kí tự.")]
    public string Name { get; set; } = null!;
}