using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace TripDatePlanner.Models.DateRange;

public sealed class DateRangeStringConverter : ValueConverter<DateRange, string>
{
    public DateRangeStringConverter()
        : base(
            range => range.ToString(),
            str => DateRange.Parse(str))
    { }
}