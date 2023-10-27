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
using Microsoft.AspNetCore.Identity.UI.Services;
using TopBookStore.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddRouting(options =>
{
    options.LowercaseUrls = true;
    options.AppendTrailingSlash = true;
});

builder.Services.AddMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddControllersWithViews().AddJsonOptions(x =>
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddRazorPages();

var TopBookStoreCS = builder.Configuration.GetConnectionString("TopBookStoreCS");
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
builder.Services.AddTransient<ICartItemService, CartItemService>();
builder.Services.AddTransient<ICartService, CartService>();

builder.Services.AddSingleton<IEmailSender, EmailSender>();

builder.Services.Configure<EmailOptions>(builder.Configuration);

// string fbAppId = Environment.
//     GetEnvironmentVariable("FACEBOOK_APP_ID", EnvironmentVariableTarget.User) ?? string.Empty;
// string fbAppSecret = Environment.
//     GetEnvironmentVariable("FACEBOOK_APP_SECRET", EnvironmentVariableTarget.User) ?? string.Empty;

string fbAppId = builder.Configuration["Authentication:Facebook:AppId"] ?? string.Empty;
string fbAppSecret = builder.Configuration["Authentication:Facebook:AppSecret"] ?? string.Empty;

// string ggClientId = Environment.
//     GetEnvironmentVariable("GOOGLE_CLIENT_ID", EnvironmentVariableTarget.User) ?? string.Empty;
// string ggClientSecret = Environment.
//     GetEnvironmentVariable("GOOGLE_CLIENT_SECRET", EnvironmentVariableTarget.User) ?? string.Empty;

string ggClientId = builder.Configuration["Authentication:Google:ClientId"] ?? string.Empty;
string ggClientSecret = builder.Configuration["Authentication:Google:ClientSecret"] ?? string.Empty;

builder.Services.AddAuthentication()
    .AddFacebook(fbOptions =>
    {
        fbOptions.AppId = fbAppId;
        fbOptions.AppSecret = fbAppSecret;
    })
    .AddGoogle(ggOptions =>
    {
        ggOptions.ClientId = ggClientId;
        ggOptions.ClientSecret = ggClientSecret;
    });

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

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<CategoriesMiddleware>();

app.MapAreaControllerRoute(
    name: "admin",
    areaName: "Admin",
    pattern: "Admin/{controller=Home}/{action=Index}/{id?}"
);
app.MapControllerRoute(
    name: "filter",
    pattern: "{controller=Home}/{action=Index}/filter/{categoryId}/{price}/{numberOfPages}/{authorId}"
);
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);
app.MapRazorPages();

app.Run();