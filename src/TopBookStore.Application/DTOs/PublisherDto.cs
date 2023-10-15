using System.ComponentModel.DataAnnotations;

namespace TopBookStore.Application.DTOs;

public class PublisherDto
{
    public int PublisherId { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập Tên của nhà xuất bản.")]
    [StringLength(80, ErrorMessage = "Nhập Tên của nhà xuất bản ngắn hơn 80 kí tự.")]
    public string Name { get; set; } = null!;
}