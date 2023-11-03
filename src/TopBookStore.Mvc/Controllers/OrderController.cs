using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TopBookStore.Application.DTOs;
using TopBookStore.Application.Interfaces;
using TopBookStore.Domain.Entities;
using TopBookStore.Domain.Interfaces;
using TopBookStore.Domain.Queries;
using TopBookStore.Infrastructure.Identity;
using TopBookStore.Infrastructure.Persistence;

namespace TopBookStore.Mvc.Controllers;

[Authorize]
public class OrderController : Controller
{
    private readonly TopBookStoreContext _context;
    private readonly ITopBookStoreUnitOfWork _data;
    private readonly IOrderService _service;
    private readonly IMapper _mapper;

    public OrderController(TopBookStoreContext context, ITopBookStoreUnitOfWork data,
        IOrderService service, IMapper mapper)
    {
        _context = context;
        _data = data;
        _service = service;
        _mapper = mapper;
    }

    public async Task<IActionResult> Index(int id)
    {
        ClaimsIdentity? claimsIdentity = User.Identity as ClaimsIdentity;
        Claim? claim = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier);
        IdentityTopBookStoreUser user = await _context.Users.FindAsync(claim?.Value) ??
            throw new Exception("User not found.");

        Customer customer = await _data.Customers.GetAsync(user.CustomerId) ??
            throw new Exception("Customer not found.");

        Cart? cart = await _data.Carts.GetAsync(new QueryOptions<Cart>
        {
            Includes = "CartItems.Book",
            Where = c => c.CartId == id
        });
        if (cart is null)
        {
            return NotFound();
        }

        Order? order = await _service.GetOrderByCustomerIdAsync(customer.CustomerId);

        if (order is null)
        {
            order = new Order()
            {
                Name = customer.FullName,
                PhoneNumber = user.PhoneNumber ?? string.Empty,
                OrderDate = DateTime.Now,
                ShippingDate = DateTime.Now.AddDays(2),
                Address = customer.Address,
                Ward = customer.Ward,
                District = customer.District,
                City = customer.City,
                CustomerId = customer.CustomerId
            };

            await _service.AddOrderAsync(order);
        }

        // Add or Update order total amount here
        order.TotalAmount = cart.TotalAmount;

        // Add or Update order details
        List<OrderDetail> orderDetails = new();

        foreach (CartItem cartItem in cart.CartItems)
        {
            // this is also the same as cart in Details action in Book controller
            // first check the order detail belong to what order
            // then get the order details has associate with a book
            OrderDetail? existingOrderDetail = await _data.OrderDetails.GetAsync(
                new QueryOptions<OrderDetail>
                {
                    Where = od => od.OrderId == order.OrderId && od.BookId == cartItem.BookId
                });

            if (existingOrderDetail is null)
            {
                // Create new order detail
                OrderDetail orderDetail = new()
                {
                    BookId = cartItem.BookId,
                    Book = cartItem.Book,
                    Quantity = cartItem.Quantity,
                    Price = cartItem.Price,
                    OrderId = order.OrderId
                };

                orderDetails.Add(orderDetail);
            }
            else
            {
                // Update existing order detail
                existingOrderDetail.Quantity = cartItem.Quantity;
                existingOrderDetail.Price = cartItem.Price;
            }
        }

        // Remove order details for cart items that were removed
        List<OrderDetail> removedOrderDetails = order.OrderDetails
            .Where(od => !cart.CartItems.Any(ci => ci.BookId == od.BookId))
            .ToList();

        _data.OrderDetails.RemoveRange(removedOrderDetails);

        _data.OrderDetails.AddRange(orderDetails);
        await _data.SaveAsync();

        OrderDto orderDto = _mapper.Map<OrderDto>(order);

        return View(orderDto);
    }
}