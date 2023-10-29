using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TopBookStore.Application.DTOs;

public class OrderDto
{
    public int OrderId { get; set; }

    public int CustomerId { get; set; }

    public DateTime OrderDate { get; set; }

    public DateTime ShippingDate { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập tên người nhận.")]
    [StringLength(128)]
    public string? Name { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập số điện thoại.")]
    [StringLength(15)]
    [Unicode(false)]
    public string PhoneNumber { get; set; } = null!;

    public decimal TotalAmount { get; set; }

    [StringLength(128)]
    [Unicode(false)]
    public string? TrackingNumber { get; set; }

    [StringLength(128)]
    [Unicode(false)]
    public string? Carrier { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? OrderStatus { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? PaymentStatus { get; set; }

    [StringLength(256)]
    [Unicode(false)]
    public string TransactionId { get; set; } = null!;

    [Required(ErrorMessage = "Vui lòng nhập địa chỉ.")]
    [StringLength(50)]
    public string? Address { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập tên phường.")]
    [StringLength(50)]
    public string? Ward { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập tên quận.")]
    [StringLength(30)]
    public string? District { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập tên thành phố.")]
    [StringLength(30)]
    public string? City { get; set; }
}
