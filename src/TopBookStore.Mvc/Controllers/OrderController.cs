using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TopBookStore.Application.DTOs;
using TopBookStore.Application.Interfaces;
using TopBookStore.Domain.Constants;
using TopBookStore.Domain.Entities;
using TopBookStore.Domain.Interfaces;
using TopBookStore.Domain.Queries;
using TopBookStore.Infrastructure.Identity;
using TopBookStore.Infrastructure.Persistence;
using TopBookStore.Mvc.Models;

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

    [HttpGet]
    public async Task<IActionResult> Payment(int id)
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

        OrderDto orderDto = new()
        {
            Name = customer.FullName,
            PhoneNumber = user.PhoneNumber ?? string.Empty
        };

        OrderIndexViewModel vm = new()
        {
            OrderDto = orderDto,
            CartDto = _mapper.Map<CartDto>(cart)
        };

        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Payment(OrderIndexViewModel vm, string stripeToken)
    {
        if (ModelState.IsValid)
        {
            ClaimsIdentity? claimsIdentity = User.Identity as ClaimsIdentity;
            Claim? claim = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier);
            IdentityTopBookStoreUser user = await _context.Users.FindAsync(claim?.Value) ??
                throw new Exception("User not found.");

            Customer customer = await _data.Customers.GetAsync(user.CustomerId) ??
                throw new Exception("Customer not found.");

            Order order = new()
            {
                Name = customer.FullName,
                PhoneNumber = user.PhoneNumber ?? string.Empty,
                OrderStatus = StatusConstants.StatusPending,
                PaymentStatus = StatusConstants.PaymentStatusPending,
                OrderDate = new DateTime(1999, 1, 1),
                ShippingDate = new DateTime(1999, 1, 1),
                TotalAmount = vm.CartDto.TotalAmount,
                Address = vm.OrderDto.Address,
                Ward = vm.OrderDto.Ward,
                District = vm.OrderDto.District,
                City = vm.OrderDto.City,
                CustomerId = customer.CustomerId
            };

            await _service.AddOrderAsync(order);

            Cart existingCart = await _data.Carts.GetAsync(new QueryOptions<Cart>
            {
                Includes = "CartItems",
                Where = c => c.CartId == vm.CartDto.CartId
            }) ?? throw new Exception("Cart not found.");

            List<OrderDetail> orderDetails = new();
            foreach (CartItem cartItem in existingCart.CartItems)
            {
                // Create new order detail
                OrderDetail orderDetail = new()
                {
                    BookId = cartItem.BookId,
                    Quantity = cartItem.Quantity,
                    Price = cartItem.Price,
                    OrderId = order.OrderId
                };

                orderDetails.Add(orderDetail);
            }
            _data.OrderDetails.AddRange(orderDetails);

            _data.Carts.Remove(existingCart); // remove cart and it's cart items

            await _data.SaveAsync();

            // after add order details, remove existing Cart and save changes, remember
            // to set cart item's quantity in session to 0
            HttpContext.Session.SetInt32(SessionCookieConstants.CartItemQuantityKey, 0);

            if (stripeToken is null)
            {
                throw new ArgumentNullException(nameof(stripeToken));
            }
            else
            {
                // because Stripe namespace, specificly Stripe.Customer is dulicated with
                // entity Customer, I have to do this.
                Stripe.ChargeCreateOptions options = new()
                {
                    Amount = (long)order.TotalAmount,
                    Currency = "VND",
                    Description = "Order Id: " + order.OrderId,
                    Source = stripeToken
                };

                Stripe.ChargeService service = new();
                // will make a call to create transaction
                Stripe.Charge charge = await service.CreateAsync(options);

                if (charge.BalanceTransactionId is null)
                {
                    order.PaymentStatus = StatusConstants.PaymentStatusRejected;
                }
                else
                {
                    order.TransactionId = charge.BalanceTransactionId;
                }

                if (charge.Status.ToLower() == "succeeded")
                {
                    order.PaymentStatus = StatusConstants.PaymentStatusApproved;
                    order.OrderStatus = StatusConstants.StatusApproved;
                    order.OrderDate = DateTime.Now;
                    order.ShippingDate = DateTime.Now.AddDays(2);
                }

                await _data.SaveAsync();
            }

            return RedirectToAction("OrderConfirmation", "Order", new { id = order.OrderId });
        }

        Cart cart = await _data.Carts.GetAsync(new QueryOptions<Cart>
        {
            Includes = "CartItems.Book",
            Where = c => c.CartId == vm.CartDto.CartId
        }) ?? throw new Exception("Cart not found.");

        vm.CartDto = _mapper.Map<CartDto>(cart);

        return View(vm);
    }

    public async Task<IActionResult> OrderConfirmation(int id)
    {
        Order? order = await _service.GetOrderByIdAsync(id);
        if (order is null)
        {
            return NotFound();
        }

        return View(order);
    }
}