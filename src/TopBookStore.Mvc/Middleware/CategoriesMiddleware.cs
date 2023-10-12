using TopBookStore.Domain.Entities;
using TopBookStore.Domain.Interfaces;
using TopBookStore.Domain.Queries;
using TopBookStore.Mvc.Extensions;

namespace TopBookStore.Mvc.Middleware;

public class CategoriesMiddleware
{
    private readonly RequestDelegate _next;
    private bool categorySet = false;

    public CategoriesMiddleware(RequestDelegate request)
    {
        _next = request;
    }

    public async Task InvokeAsync(HttpContext context, ITopBookStoreUnitOfWork data)
    {
        if (!categorySet)
        {
            IEnumerable<Category> categories = await data.Categories.ListAllAsync(
                new QueryOptions<Category>()
                {
                    OrderBy = c => c.Name
                });
            context.Session.SetObject("categories", categories);

            categorySet = true;
        }

        await _next(context);
    }
}