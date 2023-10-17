using System.ComponentModel.DataAnnotations;

namespace TopBookStore.Application.DTOs;

public class PublisherDto
{
    public int PublisherId { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập tên của nhà xuất bản.")]
    [StringLength(80, ErrorMessage = "Tên của nhà xuất bản phải ngắn hơn 80 kí tự.")]
    public string Name { get; set; } = null!;
}