using TopBookStore.Domain.Entities;
using TopBookStore.Domain.Interfaces;
using TopBookStore.Domain.Queries;
using TopBookStore.Mvc.Extensions;

namespace TopBookStore.Mvc.Middleware;

public class CategoriesMiddleware
{
    private readonly RequestDelegate _next;
    private int categoryCount = 0;

    public CategoriesMiddleware(RequestDelegate request)
    {
        _next = request;
    }

    public async Task InvokeAsync(HttpContext context, ITopBookStoreUnitOfWork data)
    {
        if (categoryCount < await data.Categories.CountAsync())
        {
            IEnumerable<Category> categories = await data.Categories.ListAllAsync(
                new QueryOptions<Category>()
                {
                    OrderBy = c => c.Name
                });
            context.Session.SetObject("categories", categories);

            categoryCount = await data.Categories.CountAsync();
        }

        await _next(context);
    }
}