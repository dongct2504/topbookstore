using Microsoft.EntityFrameworkCore;
using TopBookStore.Domain.Interfaces;
using TopBookStore.Infrastructure.Persistence;
using TopBookStore.Mvc.Middleware;
using TopBookStore.Infrastructure.Identity;
using TopBookStore.Application.Interfaces;
using TopBookStore.Application.Services;
using TopBookStore.Infrastructure.UnitOfWork;
using System.Text.Json.Serialization;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddRouting(options =>
{
    options.LowercaseUrls = true;
    options.AppendTrailingSlash = true;
});

builder.Services.AddMemoryCache();
builder.Services.AddSession();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddControllersWithViews().AddJsonOptions(x =>
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddRazorPages();

var TopBookStoreCS = builder.Configuration.GetConnectionString("TopBookStoreCS");
// builder.Services.AddDbContext<TopBookStoreContext>(options =>
//     options.UseSqlServer(TopBookStoreCS));

// var ApplicationDbContextCS = builder.Configuration.GetConnectionString("ApplicationDbContextConnection");
builder.Services.AddDbContext<IdentityTopBookStoreDbContext>(options =>
    options.UseSqlServer(TopBookStoreCS));
builder.Services.AddDbContext<TopBookStoreContext>(options =>
    options.UseSqlServer(TopBookStoreCS));

builder.Services.AddDefaultIdentity<IdentityTopBookStoreUser>(options => 
        options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddDefaultTokenProviders()
    .AddEntityFrameworkStores<IdentityTopBookStoreDbContext>();

builder.Services.AddTransient<ITopBookStoreUnitOfWork, TopBookStoreUnitOfWork>();
builder.Services.AddTransient<IBookService, BookService>();
builder.Services.AddTransient<IAuthorService, AuthorService>();
builder.Services.AddTransient<ICategoryService, CategoryService>();
builder.Services.AddTransient<IPublisherService, PublisherService>();
builder.Services.AddTransient<ICustomerService, CustomerService>();

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

app.UseMiddleware<CategoriesMiddleware>();

app.MapAreaControllerRoute(
    name: "admin",
    areaName: "Admin",
    pattern: "Admin/{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();