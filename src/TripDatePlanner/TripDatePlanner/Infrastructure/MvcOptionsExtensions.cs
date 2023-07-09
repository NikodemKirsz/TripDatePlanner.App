using Microsoft.AspNetCore.Mvc;
using TripDatePlanner.Filters;

namespace TripDatePlanner.Infrastructure;

public static class MvcOptionsExtensions
{
    public static void AddFilters(this MvcOptions options)
    {
        var filters = options.Filters;

        filters.Add<ExceptionsFilter>();
    }
}