using TopBookStore.Application.DTOs;
using TopBookStore.Domain.Entities;
using TopBookStore.Domain.Queries;
using TopBookStore.Infrastructure.Repositories;
using TopBookStore.Mvc.Extensions;

namespace TopBookStore.Mvc.SessionCookie;

public class CartSessionCookie
{
    private const string CartKey = "mycart";
    private const string CountKey = "mycount";

    private readonly ISession _session;
    private readonly IRequestCookieCollection _requestCookies;
    private readonly IResponseCookies _responseCookies;

    private List<CartItem> ListCartSession { get; set; } = null!;
    private List<CartDto> ListCartCookie { get; set; } = null!;

    public CartSessionCookie(HttpContext httpContext)
    {
        _session = httpContext.Session;
        _requestCookies = httpContext.Request.Cookies;
        _responseCookies = httpContext.Response.Cookies;
    }

    public async void Load(Repository<Book> data)
    {
        ListCartSession = _session.GetObject<List<CartItem>>(CartKey) ?? new List<CartItem>();
        ListCartCookie = _requestCookies.GetObject<List<CartDto>>(CartKey) ?? new List<CartDto>();

        if (ListCartCookie.Count > ListCartSession.Count)
        {
            ListCartSession.Clear();

            foreach (CartDto storedItem in ListCartCookie)
            {
                Book? book = await data.GetAsync(new QueryOptions<Book>
                {
                    Where = b => b.BookId == storedItem.BookId,
                    Includes = "Authors, Category"
                });

                // if that book is null then it have been deleted
                if (book is not null)
                {
                    CartItem cartItem = new()
                    {
                        Quantity = 1
                    };
                }
            }
            Save();
        }
    }

    public void Save()
    {
        if (ListCartSession.Count == 0)
        {
            _session.Remove(CartKey);
            _session.Remove(CountKey);
            _responseCookies.Delete(CartKey);
            _responseCookies.Delete(CartKey);
        }
        else
        {
            _session.SetObject(CartKey, ListCartSession);
            _session.SetInt32(CountKey, ListCartSession.Count);
            _responseCookies.SetObject(CartKey, ListCartCookie);
            _responseCookies.SetInt32(CountKey, ListCartCookie.Count);
        }
    }

    public int? Count => _session.GetInt32(CountKey) ?? _requestCookies.GetInt32(CountKey);
    public IEnumerable<CartItem> List => ListCartSession;
}