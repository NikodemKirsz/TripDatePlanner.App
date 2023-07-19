using System.Diagnostics.CodeAnalysis;
using TripDatePlanner.Exceptions;
using TripDatePlanner.Helpers.Extensions;

namespace TripDatePlanner.Models;

[Serializable]
public readonly struct DateRange : IEquatable<DateRange>
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

    public static DateRange ParseExact(string dateRangeStr, [StringSyntax("DateOnlyFormat")] string? dateOnlyFormat)
    {
        try
        {
            string[] splits = dateRangeStr.Split(Separator, StringSplitOptions.RemoveEmptyEntries);
            if (splits.Length != 2)
                throw new ParsingException(dateRangeStr, typeof(DateRange));

            DateOnly start = dateOnlyFormat is not null
                ? DateOnly.ParseExact(splits[0], dateOnlyFormat)
                : DateOnly.Parse(splits[0]);
            DateOnly end = dateOnlyFormat is not null
                ? DateOnly.ParseExact(splits[1], dateOnlyFormat)
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

    public bool Contains(DateRange inner) => DoRangeContain(this, inner);
    
    public bool Contains(DateOnly date) => IsDateBetween(date, this);

    public bool IsContained(DateRange outer) => DoRangeContain(outer, this);

    public bool Overlaps(DateRange other) => DoRangesOverlap(this, other);

    public bool IsMergable(DateRange other) => AreRangesMergable(this, other);
    
    public static DateRange[] Normalize(IEnumerable<DateRange> ranges)
    {
        DateRange[] orderedRanges = ranges.OrderBy(r => r.Start).ToArray();
        List<DateRange> normalizedRanges = new(orderedRanges.Length);
        List<DateRange> mergableRanges = new(orderedRanges.Length);
        
        for (int i = 0; i < orderedRanges.Length; i++)
        {
            int j;
            
            mergableRanges.Clear();
            mergableRanges.Add(orderedRanges[i]);

            for (j = i + 1; j < orderedRanges.Length; j++)
            {
                DateRange range = orderedRanges[j];
                if (mergableRanges.Any(r => r.IsMergable(range)))
                    mergableRanges.Add(range);
                else
                    break;
            }
            
            DateRange? mergedRange = Merge(mergableRanges);
            if (mergedRange is null)
                continue;
                    
            normalizedRanges.Add(mergedRange.Value);
            i = j - 1;
        }
        
        normalizedRanges.Sort((left, right) => left.Start.CompareTo(right.Start));

        return normalizedRanges.ToArray();
    }

    public static bool DoRangeContain(DateRange outer, DateRange inner)
    {
        return outer.Start <= inner.Start && inner.End <= outer.End;
    }
    
    public static bool DoRangesOverlap(DateRange left, DateRange right)
    {
        return IsDateBetween(left.Start, right) || IsDateBetween(left.End, right) ||
            IsDateBetween(right.Start, left) || IsDateBetween(right.End, left);
    }

    public static bool AreRangesMergable(DateRange left, DateRange right)
    {
        return IsDateBetween(left.Start.AddDays(1), right) || IsDateBetween(left.Start.AddDays(-1), right) ||
            IsDateBetween(left.End.AddDays(1), right) || IsDateBetween(left.End.AddDays(-1), right) ||
            IsDateBetween(right.Start.AddDays(1), left) || IsDateBetween(right.Start.AddDays(-1), left) ||
            IsDateBetween(right.End.AddDays(1), left) || IsDateBetween(right.End.AddDays(-1), left);
    }

    public static bool IsDateBetween(DateOnly dateOnly, DateRange dateRange)
    {
        return dateRange.Start <= dateOnly && dateOnly <= dateRange.End;
    }

    public static DateRange? Merge(ICollection<DateRange> ranges)
    {
        if (!ranges.Any())
            return null;
        
        var (minStart, maxEnd) = ranges.MinMax(r => r.Start, r => r.End);
        return new DateRange(minStart, maxEnd);
    }

    public bool Equals(DateRange? other)
    {
        return other.HasValue && Equals(other.Value);
    }

    public bool Equals(DateRange other)
    {
        return Start.Equals(other.Start) && End.Equals(other.End);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Start, End);
    }

    public override string ToString() => ToString(DateOnlyFormat);

    public string ToString([StringSyntax("DateOnlyFormat")] string format)
    {
        return $"{Start.ToString(format)}{Separator}{End.ToString(format)}";
    }
}