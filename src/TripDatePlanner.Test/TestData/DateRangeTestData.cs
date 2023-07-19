using TripDatePlanner.Models;

namespace TripDatePlanner.Test.TestData;

public static class DateRangeTestData
{
    public static IEnumerable<object[]> ParseTestSuccessData => new[]
    {
        new object[] { "2023-03-02 - 2023-04-03", new DateRange(new DateOnly(2023, 03, 02), new DateOnly(2023, 04, 03)) },
        new object[] { "2023/03/02 - 2023/04/03", new DateRange(new DateOnly(2023, 03, 02), new DateOnly(2023, 04, 03)) },
        new object[] { "2023.03.02 - 2023.04.03", new DateRange(new DateOnly(2023, 03, 02), new DateOnly(2023, 04, 03)) },
    };
    
    public static IEnumerable<object[]> ParseExactTestSuccessData => new[]
    {
        new object[] { "20230302 - 20230403", "yyyyMMdd", new DateRange(new DateOnly(2023, 03, 02), new DateOnly(2023, 04, 03)) },
        new object[] { "02.03.2023 - 03.04.2023", "dd.MM.yyyy", new DateRange(new DateOnly(2023, 03, 02), new DateOnly(2023, 04, 03)) },
        new object[] { "03--02//2023 - 04--03//2023", "MM--dd//yyyy", new DateRange(new DateOnly(2023, 03, 02), new DateOnly(2023, 04, 03)) },
    };
    
    public static IEnumerable<object[]> ParseExactTestFailureData => new[]
    {
        new object[] { "2023-03-02 - 2023-04-03 - 2023-05-04" },
        new object[] { "2023-03-02" },
    };

    public static IEnumerable<object[]> ContainsTestData => new[]
    {
        new object[] { DateRange.Parse("2023-03-02 - 2023-04-03"), DateRange.Parse("2023-03-05 - 2023-04-02"), true },
        new object[] { DateRange.Parse("2023-03-02 - 2023-04-03"), DateRange.Parse("2023-03-02 - 2023-04-03"), true },
        new object[] { DateRange.Parse("2023-01-01 - 2023-04-01"), DateRange.Parse("2023-03-31 - 2023-03-31"), true },
        new object[] { DateRange.Parse("2023-01-01 - 2023-04-01"), DateRange.Parse("2023-03-31 - 2023-04-01"), true },
        new object[] { DateRange.Parse("2023-01-01 - 2023-04-01"), DateRange.Parse("2023-04-02 - 2023-04-04"), false },
        new object[] { DateRange.Parse("2023-01-01 - 2023-04-01"), DateRange.Parse("2023-03-02 - 2023-04-04"), false },
        new object[] { DateRange.Parse("2023-03-05 - 2023-04-01"), DateRange.Parse("2023-03-01 - 2023-04-04"), false },
        new object[] { DateRange.Parse("2023-03-05 - 2023-04-01"), DateRange.Parse("2023-03-01 - 2023-03-15"), false },
    };

    public static IEnumerable<object[]> OverlapsTestData => new[]
    {
        new object[] { DateRange.Parse("2023-03-02 - 2023-04-03"), DateRange.Parse("2023-03-05 - 2023-04-02"), true },
        new object[] { DateRange.Parse("2023-03-02 - 2023-04-03"), DateRange.Parse("2023-03-02 - 2023-04-03"), true },
        new object[] { DateRange.Parse("2023-01-01 - 2023-04-01"), DateRange.Parse("2023-03-31 - 2023-03-31"), true },
        new object[] { DateRange.Parse("2023-01-01 - 2023-04-01"), DateRange.Parse("2023-03-31 - 2023-04-01"), true },
        new object[] { DateRange.Parse("2023-03-02 - 2023-04-03"), DateRange.Parse("2023-04-02 - 2023-05-02"), true },
        new object[] { DateRange.Parse("2023-03-02 - 2023-04-03"), DateRange.Parse("2023-02-01 - 2023-03-03"), true },
        new object[] { DateRange.Parse("2023-03-02 - 2023-04-03"), DateRange.Parse("2023-01-01 - 2023-06-03"), true },
        new object[] { DateRange.Parse("2023-03-02 - 2023-04-03"), DateRange.Parse("2023-04-03 - 2023-06-03"), true },
        new object[] { DateRange.Parse("2023-03-02 - 2023-04-03"), DateRange.Parse("2023-01-01 - 2023-03-02"), true },
        new object[] { DateRange.Parse("2023-03-02 - 2023-04-03"), DateRange.Parse("2023-01-01 - 2023-03-01"), false },
        new object[] { DateRange.Parse("2023-03-02 - 2023-04-03"), DateRange.Parse("2023-04-04 - 2023-05-01"), false },
    };

    public static IEnumerable<object[]> IsMergableTestData => new[]
    {
        new object[] { DateRange.Parse("2023-03-02 - 2023-04-03"), DateRange.Parse("2023-03-05 - 2023-04-02"), true },
        new object[] { DateRange.Parse("2023-03-02 - 2023-04-03"), DateRange.Parse("2023-03-02 - 2023-04-03"), true },
        new object[] { DateRange.Parse("2023-01-01 - 2023-04-01"), DateRange.Parse("2023-03-31 - 2023-03-31"), true },
        new object[] { DateRange.Parse("2023-01-01 - 2023-04-01"), DateRange.Parse("2023-03-31 - 2023-04-01"), true },
        new object[] { DateRange.Parse("2023-03-02 - 2023-04-03"), DateRange.Parse("2023-04-02 - 2023-05-02"), true },
        new object[] { DateRange.Parse("2023-03-02 - 2023-04-03"), DateRange.Parse("2023-02-01 - 2023-03-03"), true },
        new object[] { DateRange.Parse("2023-03-02 - 2023-04-03"), DateRange.Parse("2023-01-01 - 2023-06-03"), true },
        new object[] { DateRange.Parse("2023-03-02 - 2023-04-03"), DateRange.Parse("2023-04-03 - 2023-06-03"), true },
        new object[] { DateRange.Parse("2023-03-02 - 2023-04-03"), DateRange.Parse("2023-01-01 - 2023-03-02"), true },
        new object[] { DateRange.Parse("2023-03-02 - 2023-04-03"), DateRange.Parse("2023-04-04 - 2023-05-02"), true },
        new object[] { DateRange.Parse("2023-03-02 - 2023-04-03"), DateRange.Parse("2023-03-01 - 2023-04-04"), true },
        new object[] { DateRange.Parse("2023-03-02 - 2023-04-03"), DateRange.Parse("2023-03-03 - 2023-04-02"), true },
        new object[] { DateRange.Parse("2023-03-02 - 2023-04-03"), DateRange.Parse("2023-02-01 - 2023-03-01"), true },
        new object[] { DateRange.Parse("2023-03-02 - 2023-03-02"), DateRange.Parse("2023-03-03 - 2023-03-03"), true },
        new object[] { DateRange.Parse("2023-03-02 - 2023-04-03"), DateRange.Parse("2023-02-01 - 2023-02-28"), false },
        new object[] { DateRange.Parse("2023-03-02 - 2023-03-02"), DateRange.Parse("2023-03-04 - 2023-03-04"), false },
    };

    public static IEnumerable<object[]> IsDateBetweenTestData => new[]
    {
        new object[] { DateOnly.Parse("2023-03-02"), DateRange.Parse("2023-02-02 - 2023-05-02"), true },
        new object[] { DateOnly.Parse("2023-02-02"), DateRange.Parse("2023-02-02 - 2023-05-02"), true },
        new object[] { DateOnly.Parse("2023-05-02"), DateRange.Parse("2023-02-02 - 2023-05-02"), true },
        new object[] { DateOnly.Parse("2023-05-03"), DateRange.Parse("2023-02-02 - 2023-05-02"), false },
        new object[] { DateOnly.Parse("2023-02-01"), DateRange.Parse("2023-02-02 - 2023-05-02"), false },
    };

    public static IEnumerable<object?[]> MergeTestData => new[]
    {
        new object[]
        {
            new[]
            {
                DateRange.Parse("2023-02-02 - 2023-02-08"),
                DateRange.Parse("2023-02-08 - 2023-02-15"),
                DateRange.Parse("2023-02-19 - 2023-02-21"),
            },
            DateRange.Parse("2023-02-02 - 2023-02-21")
        },
        new object[]
        {
            new[]
            {
                DateRange.Parse("2023-02-02 - 2023-02-02"),
                DateRange.Parse("2023-03-02 - 2023-03-02"),
            },
            DateRange.Parse("2023-02-02 - 2023-03-02")
        },
        new object[]
        {
            new[]
            {
                DateRange.Parse("2023-02-02 - 2023-02-03"),
            },
            DateRange.Parse("2023-02-02 - 2023-02-03")
        },
        new object?[]
        {
            Array.Empty<DateRange>(), null
        },
    };

    public static IEnumerable<object[]> NormalizeTestData => new[]
    {
        new object[]
        {
            new[]
            {
                DateRange.Parse("2023-02-02 - 2023-02-08"),
                DateRange.Parse("2023-02-07 - 2023-02-15"),
                DateRange.Parse("2023-02-14 - 2023-02-21"),
            },
            new[]
            {
                DateRange.Parse("2023-02-02 - 2023-02-21"),
            },
        },
        new object[]
        {
            new[]
            {
                DateRange.Parse("2023-02-02 - 2023-02-08"),
                DateRange.Parse("2023-02-08 - 2023-02-15"),
                DateRange.Parse("2023-02-18 - 2023-02-21"),
            },
            new[]
            {
                DateRange.Parse("2023-02-02 - 2023-02-15"),
                DateRange.Parse("2023-02-18 - 2023-02-21"),
            },
        },
        new object[]
        {
            new[]
            {
                DateRange.Parse("2023-02-02 - 2023-02-08"),
                DateRange.Parse("2023-02-08 - 2023-02-15"),
                DateRange.Parse("2023-02-18 - 2023-02-23"),
                DateRange.Parse("2023-02-21 - 2023-02-28"),
            },
            new[]
            {
                DateRange.Parse("2023-02-02 - 2023-02-15"),
                DateRange.Parse("2023-02-18 - 2023-02-28"),
            },
        },
        new object[]
        {
            new[]
            {
                DateRange.Parse("2023-02-02 - 2023-02-15"),
                DateRange.Parse("2023-02-08 - 2023-02-12"),
                DateRange.Parse("2023-01-20 - 2023-02-01"),
                DateRange.Parse("2023-02-21 - 2023-02-28"),
            },
            new[]
            {
                DateRange.Parse("2023-01-20 - 2023-02-15"),
                DateRange.Parse("2023-02-21 - 2023-02-28"),
            },
        },
        new object[]
        {
            new[]
            {
                DateRange.Parse("2023-02-02 - 2023-02-15"),
                DateRange.Parse("2023-02-08 - 2023-02-12"),
                DateRange.Parse("2023-01-20 - 2023-02-01"),
                DateRange.Parse("2023-02-21 - 2023-02-28"),
                DateRange.Parse("2023-02-25 - 2023-03-05"),
                DateRange.Parse("2023-03-15 - 2023-03-18"),
                DateRange.Parse("2023-03-25 - 2023-03-25"),
                DateRange.Parse("2023-03-26 - 2023-03-30"),
                DateRange.Parse("2023-03-01 - 2023-03-07"),
                DateRange.Parse("2023-04-01 - 2023-04-12"),
                DateRange.Parse("2023-04-10 - 2023-04-15"),
                DateRange.Parse("2023-04-16 - 2023-04-16"),
                DateRange.Parse("2023-04-19 - 2023-04-23"),
                DateRange.Parse("2023-04-26 - 2023-04-27"),
                DateRange.Parse("2023-04-26 - 2023-04-29"),
                DateRange.Parse("2023-04-30 - 2023-05-02"),
            },
            new[]
            {
                DateRange.Parse("2023-01-20 - 2023-02-15"),
                DateRange.Parse("2023-02-21 - 2023-03-07"),
                DateRange.Parse("2023-03-15 - 2023-03-18"),
                DateRange.Parse("2023-03-25 - 2023-03-30"),
                DateRange.Parse("2023-04-01 - 2023-04-16"),
                DateRange.Parse("2023-04-19 - 2023-04-23"),
                DateRange.Parse("2023-04-26 - 2023-05-02"),
            },
        }
    };
}