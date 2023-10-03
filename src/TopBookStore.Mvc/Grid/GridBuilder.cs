using TopBookStore.Application.DTOs;
using TopBookStore.Domain.Entities;
using TopBookStore.Domain.Extensions;
using TopBookStore.Mvc.Sessions;

namespace TopBookStore.Mvc.Grid;

// store route in session and retrieve it
public class GridBuilder
{
    private const string RouteKey = "route";

    private readonly ISession _session;

    protected RouteDictionary Routes { get; set; }

    public GridBuilder(ISession session)
    {
        _session = session;
        Routes = _session.GetObject<RouteDictionary>(RouteKey) ?? new RouteDictionary();
    }

    // use this constructor when you need to add new route
    public GridBuilder(ISession session, GridDTO values)
    {
        _session = session;

        Routes = new RouteDictionary
        {
            PageNumber = values.PageNumber,
            PageSize = values.PageSize,
            SortField = values.SortField,
            SortDirection = values.SortDirection,

            // set filter segments
            CategoryFilter = values.CategoryId,
            PriceFilter = values.Price,
            NumberOfPagesFilter = values.NumberOfPages,
            AuthorFilter = values.AuthorId
        };

        SaveRouteDirection();
    }

    public void SaveRouteDirection()
    {
        _session.SetObject(RouteKey, Routes);
    }

    public int GetTotalPages(int count)
    {
        int size = Routes.PageSize;
        return (count + size - 1) / size; // (10 books + 4 - 1) / 4 = 3.25 = 3
    }

    public RouteDictionary CurrentRoute => Routes;

    public void LoadFilterSegments(string[] filter)
    {
        Routes.CategoryFilter = filter[0];
        Routes.PriceFilter = filter[1];
        Routes.NumberOfPagesFilter = filter[2];
        Routes.AuthorFilter = filter[3];
    }

    public void ClearFilterSegments() => Routes.ClearFilters();

    // filter flags
    public bool IsFilterByCategory => Routes.CategoryFilter != GridDTO.DefaultFilter;
    public bool IsFilterByPrice => Routes.PriceFilter != GridDTO.DefaultFilter;
    public bool IsFilterByNumberOfPages => Routes.NumberOfPagesFilter != GridDTO.DefaultFilter;
    public bool IsFilterByAuthor => Routes.AuthorFilter != GridDTO.DefaultFilter;

    // sort flags
    public bool IsSortByCategory => Routes.SortField.EqualsNoCase(nameof(Category));
    public bool IsSortByPrice => Routes.SortField.EqualsNoCase(nameof(Book.Price));
}