using TopBookStore.Application.Commond;
using TopBookStore.Application.DTOs;
using TopBookStore.Domain.Entities;
using TopBookStore.Domain.Extensions;
using TopBookStore.Mvc.Extensions;

namespace TopBookStore.Mvc.Grid;

// store route in session and retrieve it
public class GridBuilder
{
    private const string RouteKey = "route";

    private readonly ISession _session;

    public GridBuilder(ISession session)
    {
        _session = session;
        Routes = _session.GetObject<RouteDictionary>(RouteKey) ?? new RouteDictionary();
    }

    protected RouteDictionary Routes { get; set; }

    public RouteDictionary CurrentRoute => Routes;

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

        SaveRouteSegments();
    }

    public void SaveRouteSegments() => _session.SetObject(RouteKey, Routes);

    public int GetTotalPages(int count)
    {
        int size = Routes.PageSize;
        return (count + size - 1) / size; // (10 books + 4 - 1) / 4 = 3.25 = 3
    }

    public void LoadFilterSegments(string[] filter) => Routes.LoadFilterSegments(filter);

    public void ClearFilterSegments() => Routes.ClearFilters();
}