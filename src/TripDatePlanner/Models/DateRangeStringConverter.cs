using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace TripDatePlanner.Models;

public sealed class DateRangeStringConverter : ValueConverter<DateRange, string>
{
    public DateRangeStringConverter()
        : base(
            range => range.ToString(),
            str => DateRange.Parse(str))
    { }
}