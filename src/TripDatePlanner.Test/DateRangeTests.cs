using TripDatePlanner.Exceptions;
using TripDatePlanner.Models;
using TripDatePlanner.Test.TestData;

namespace TripDatePlanner.Test;

public class DateRangeTests
{
    [Fact]
    public void ConstructorTest_Success()
    {
        DateOnly start = DateOnly.MinValue;
        DateOnly end = DateOnly.MaxValue;

        DateRange dateRange = new(start, end);
        
        Assert.NotEqual(default, dateRange);
        Assert.Equal(start, dateRange.Start);
        Assert.Equal(end, dateRange.End);
    }
    
    [Fact]
    public void ConstructorTest_ThrowsException_WhenStartIsAfterEnd()
    {
        DateOnly start = DateOnly.MinValue;
        DateOnly end = DateOnly.MaxValue;

        Assert.Throws<ArgumentException>(() => _ = new DateRange(end, start));
    }

    [Theory]
    [MemberData(nameof(DateRangeTestData.ParseTestSuccessData), MemberType = typeof(DateRangeTestData))]
    public void ParseTest_Success(string dateRangeStr, DateRange dateRange)
    {
        DateRange parsedDateRange = DateRange.Parse(dateRangeStr);
        Assert.Equal(dateRange, parsedDateRange);
    }

    [Theory]
    [MemberData(nameof(DateRangeTestData.ParseExactTestSuccessData), MemberType = typeof(DateRangeTestData))]
    public void ParseExactTest_Success(string dateRangeStr, string format, DateRange dateRange)
    {
        DateRange parsedDateRange = DateRange.ParseExact(dateRangeStr, format);
        Assert.Equal(dateRange, parsedDateRange);
    }

    [Theory]
    [MemberData(nameof(DateRangeTestData.ParseExactTestFailureData), MemberType = typeof(DateRangeTestData))]
    public void ParseTest_ThrowsException_WhenNot2Splits(string dateRangeStr)
    {
        var exception = Assert.Throws<ParsingException>(() => _ = DateRange.Parse(dateRangeStr));
        Assert.Equal(dateRangeStr, exception.SourceString);
        Assert.Equal(typeof(DateRange), exception.TargetType);
    }

    [Theory]
    [MemberData(nameof(DateRangeTestData.ContainsTestData), MemberType = typeof(DateRangeTestData))]
    public void ContainsTest_Success(DateRange outer, DateRange inner, bool success)
    {
        Assert.Equal(success, outer.Contains(inner));
    }
    
    [Theory]
    [MemberData(nameof(DateRangeTestData.ContainsTestData), MemberType = typeof(DateRangeTestData))]
    public void IsContainedTest_Success(DateRange outer, DateRange inner, bool success)
    {
        Assert.Equal(success, inner.IsContained(outer));
    }

    [Theory]
    [MemberData(nameof(DateRangeTestData.OverlapsTestData), MemberType = typeof(DateRangeTestData))]
    public void OverlapsTest_Success(DateRange first, DateRange second, bool success)
    {
        Assert.Equal(success, first.Overlaps(second));
    }

    [Theory]
    [MemberData(nameof(DateRangeTestData.IsMergableTestData), MemberType = typeof(DateRangeTestData))]
    public void IsMergableTest_Success(DateRange first, DateRange second, bool success)
    {
        Assert.Equal(success, first.IsMergable(second));
    }
    
    [Theory]
    [MemberData(nameof(DateRangeTestData.IsDateBetweenTestData), MemberType = typeof(DateRangeTestData))]
    public void IsDateBetweenTest_Success(DateOnly date, DateRange dateRange, bool success)
    {
        Assert.Equal(success, dateRange.Contains(date));
    }
    
    [Theory]
    [MemberData(nameof(DateRangeTestData.MergeTestData), MemberType = typeof(DateRangeTestData))]
    public void MergeTest_Success(DateRange[] ranges, DateRange? range)
    {
        Assert.Equal(range, DateRange.Merge(ranges));
    }
    
    [Theory]
    [MemberData(nameof(DateRangeTestData.NormalizeTestData), MemberType = typeof(DateRangeTestData))]
    public void NormalizeTest_Success(DateRange[] ranges, DateRange[] normalized)
    {
        Assert.Equal(normalized, DateRange.Normalize(ranges));
    }
}