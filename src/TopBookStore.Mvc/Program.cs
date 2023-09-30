using Microsoft.EntityFrameworkCore;
using TopBookStore.Domain.Entities;
using TopBookStore.Domain.Interfaces;
using TopBookStore.Infrastructure.Persistence;
using TopBookStore.Infrastructure.Repositories;
using TopBookStore.Mvc.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddRouting(options =>
{
    options.LowercaseUrls = true;
    options.AppendTrailingSlash = true;
});

builder.Services.AddMemoryCache();
builder.Services.AddSession();

builder.Services.AddControllersWithViews();

var TopBookStoreCS = builder.Configuration.GetConnectionString("TopBookStoreCS");
builder.Services.AddDbContext<TopBookStoreContext>(options =>
    options.UseSqlServer(TopBookStoreCS));

builder.Services.AddScoped<IRepository<Category>, Repository<Category>>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.UseMiddleware<CategoriesMiddlewar>();

// //localhost:xxxxx/author/list/page-1/size-10/sort-lastname-desc
// app.MapControllerRoute(
//     name: "page_sort",
//     pattern: "{controller}/{action}/page-{pagenumber}/size-{pagesize}/sort-{sortfield}-{sortdirection}/{id?}"
// );
app.MapControllerRoute(
    name: "paging_books",
    pattern: "{controller}/{action}/{page-{pagenumber}/size-{pagesize}/filter-{authorid}-{categoryid}-{priceid}"
);
app.MapControllerRoute(
    name: "paging",
    pattern: "{controller}/{action}/page-{pagenumber}/size-{pagesize}/{id?}"
);
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();
