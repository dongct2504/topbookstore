using TopBookStore.Domain.Entities;
using TopBookStore.Domain.Interfaces;
using TopBookStore.Domain.Queries;
using TopBookStore.Mvc.Extensions;

namespace TopBookStore.Mvc.Middleware;

public class CategoriesMiddlewar
{
    private readonly RequestDelegate _next;
    private bool categorySet = false;

    public CategoriesMiddlewar(RequestDelegate request)
    {
        _next = request;
    }

    public async Task InvokeAsync(HttpContext context, IRepository<Category> data)
    {
        if (!categorySet)
        {
            IEnumerable<Category> categories = await data.ListAllAsync(new QueryOptions<Category>()
            {
                OrderBy = c => c.Name
            });
            context.Session.SetObject("categories", categories);
            
            categorySet = true;
        }

        await _next(context);
    }
}