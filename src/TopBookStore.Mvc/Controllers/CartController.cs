using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TopBookStore.Domain.Entities;
using TopBookStore.Domain.Interfaces;
using TopBookStore.Infrastructure.Persistence;
using TopBookStore.Infrastructure.Repositories;

namespace TopBookStore.Mvc.Controllers;

[Authorize]
public class CartController : Controller
{
    private readonly IRepository<Book> _data;

    public CartController(TopBookStoreContext context) => _data = new Repository<Book>(context);

    public async Task<ViewResult> Index()
    {
        return View();
    }

    // public Task<ViewResult> Add(int id)
    // {
        
    // }
}