using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using TripDatePlanner.Models;

namespace TripDatePlanner.Infrastructure;

public static class SwaggerGenOptionsExtensions
{
    public static SwaggerGenOptions UseDateRangeStringConverter(this SwaggerGenOptions options)
    {
        options.MapType<DateRange>(() => new OpenApiSchema()
        {
            Type = nameof(String).ToLower(),
            Format = "date",
            Example = OpenApiAnyFactory.CreateFromJson("\"1936-06-26 - 2002-01-13\""),
        });

        return options;
    }
}