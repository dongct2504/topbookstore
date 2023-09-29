using TopBookStore.Application.DTOs;
using TopBookStore.Mvc.Sessions;

namespace TopBookStore.Mvc.Grid;

// store route in session and retrieve it
public class GridBuilder
{
    private const string routeKey = "route";

    private readonly ISession _session;

    protected RouteDictionary Routes { get; set; }

    public GridBuilder(ISession session)
    {
        _session = session;
        Routes = _session.GetObject<RouteDictionary>(routeKey) ?? new RouteDictionary();
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
            SortDirection = values.SortDirection
        };

        SaveRouteDirection();
    }

    public void SaveRouteDirection()
    {
        _session.SetObject(routeKey, Routes);
    }

    public int GetTotalPages(int count)
    {
        int size = Routes.PageSize;
        return (count + size - 1) / size; // (10 books + 4 - 1) / 4 = 3.25 = 3
    }

    public RouteDictionary CurrentRoute => Routes;
}