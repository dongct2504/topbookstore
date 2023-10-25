using TopBookStore.Application.DTOs;
using TopBookStore.Application.Routing;
using TopBookStore.Mvc.Extensions;

namespace TopBookStore.Mvc.Grid;

// store route in session and retrieve it
public class GridBuilder
{
    private const string RouteKey = "route";

    private readonly ISession _session;

    protected RouteDictionary Routes { get; set; }

    public RouteDictionary CurrentRoute => Routes;

    public GridBuilder(ISession session)
    {
        _session = session;
        Routes = _session.GetObject<RouteDictionary>(RouteKey) ?? new RouteDictionary();
    }

    // use this constructor when you need to add new route
    public GridBuilder(ISession session, GridDto values)
    {
        _session = session;

        if (values is BookGridDto bookGridDto)
        {
            Routes = new RouteDictionary
            {
                PageNumber = bookGridDto.PageNumber,
                PageSize = bookGridDto.PageSize,

                // set filter segments
                CategoryFilter = bookGridDto.CategoryId,
                PriceFilter = bookGridDto.Price,
                NumberOfPagesFilter = bookGridDto.NumberOfPages,
                AuthorFilter = bookGridDto.AuthorId
            };
        }
        else
        {
            Routes = new RouteDictionary
            {
                PageNumber = values.PageNumber,
                PageSize = values.PageSize
            };

        }

        SaveRouteSegments();
    }

    public int GetTotalPages(int count)
    {
        return (Routes.PageSize + count - 1) / Routes.PageSize;
    }

    public void SaveRouteSegments() => _session.SetObject(RouteKey, Routes);

    public void LoadFilterSegments(string[] filter) => Routes.LoadFilterSegments(filter);

    public void ClearFilterSegments() => Routes.ClearFilters();
}