using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using TopBookStore.Domain.Entities;

namespace TopBookStore.Application.DTOs;

public class OrderDto
{
    public int OrderId { get; set; }

    public int CustomerId { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập tên người nhận.")]
    [StringLength(128)]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "Vui lòng nhập số điện thoại.")]
    [StringLength(15)]
    [Unicode(false)]
    public string PhoneNumber { get; set; } = null!;

    [Required(ErrorMessage = "Vui lòng nhập địa chỉ.")]
    [StringLength(50)]
    public string Address { get; set; } = null!;

    [Required(ErrorMessage = "Vui lòng nhập tên phường.")]
    [StringLength(50)]
    public string Ward { get; set; } = null!;

    [Required(ErrorMessage = "Vui lòng nhập tên quận.")]
    [StringLength(30)]
    public string District { get; set; } = null!;

    [Required(ErrorMessage = "Vui lòng nhập tên thành phố.")]
    [StringLength(30)]
    public string City { get; set; } = null!;

    public List<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
