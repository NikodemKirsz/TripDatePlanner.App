using System.Diagnostics.CodeAnalysis;
using TripDatePlanner.Exceptions;

namespace TripDatePlanner.Models.DateRange;

public readonly record struct DateRange
{
    private const string DateOnlyFormat = "yyyy-MM-dd";
    private const string Separator = " - ";
    
    public DateOnly Start { get; }
    public DateOnly End { get; }

    public DateRange(DateOnly start, DateOnly end)
    {
        if (end < start)
            throw new ArgumentException("End date is smaller than start date!", nameof(end));

        Start = start;
        End = end;
    }

    public static DateRange Parse(string dateRangeStr) => ParseExact(dateRangeStr, null);

    public static DateRange ParseExact(string dateRangeStr, [StringSyntax("DateOnlyFormat")] string? format)
    {
        try
        {
            string[] splits = dateRangeStr.Split(Separator, StringSplitOptions.RemoveEmptyEntries);
            if (splits.Length != 2)
                throw new ParsingException(dateRangeStr, typeof(DateRange));

            DateOnly start = format is not null
                ? DateOnly.ParseExact(splits[0], format)
                : DateOnly.Parse(splits[0]);
            DateOnly end = format is not null
                ? DateOnly.ParseExact(splits[1], format)
                : DateOnly.Parse(splits[1]);

            return new DateRange(start, end);
        }
        catch (ParsingException)
        {
            throw;
        }
        catch (Exception e)
        {
            throw new ParsingException(dateRangeStr, typeof(DateRange), e);
        }
    }

    public bool Contains(DateRange inner) => Contains(inner, this);

    public bool IsContained(DateRange outer) => Contains(this, outer);

    public bool Overlaps(DateRange other) => Overlaps(this, other);

    public static bool Contains(DateRange inner, DateRange outer)
    {
        return outer.Start <= inner.Start && inner.End <= outer.End;
    }
    
    public static bool Overlaps(DateRange left, DateRange right)
    {
        return IsDateBetween(left.Start, right) || IsDateBetween(left.End, right) ||
            IsDateBetween(right.Start, left) || IsDateBetween(right.End, left);
    }

    public static bool IsDateBetween(DateOnly dateOnly, DateRange dateRange)
    {
        return dateRange.Start <= dateOnly && dateOnly <= dateRange.End;
    }

    public bool Equals(DateRange? other) => other.HasValue && Equals(other.Value);

    public bool Equals(DateRange other) => Start.Equals(other.Start) && End.Equals(other.End);

    public override int GetHashCode() => HashCode.Combine(Start, End);

    public override string ToString() => ToString(DateOnlyFormat);

    public string ToString([StringSyntax("DateOnlyFormat")] string format)
    {
        return $"{Start.ToString(format)}{Separator}{End.ToString(format)}";
    }
}