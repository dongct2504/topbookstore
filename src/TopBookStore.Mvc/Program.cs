using Microsoft.EntityFrameworkCore;
using TopBookStore.Infrastructure.Persistence;

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

// https://localhost:xxxxx/author/list/page-1/size-10/sort-lastname-desc
// app.MapControllerRoute(
//     name: "page_sort",
//     pattern: "{controller}/{action}/page-{pagenumber}/size-{pagesize}/sort-{sortfield}-{sortdirection}"
// );
app.MapControllerRoute(
    name: "paging",
    pattern: "{controller}/{action}/page-{pagenumber}/size-{pagesize}"
);
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();
