using BenchmarkDotNet.Attributes;
using TripDatePlanner.Models;

namespace TripDatePlanner.Benchmarks;

/*
|             Method |    DateRanges |       Mean |   Error |  StdDev | Allocated |
|------------------- |-------------- |-----------:|--------:|--------:|----------:|
| NormalizeBenchmark | DateRange[16] | 1,455.0 ns | 5.81 ns | 4.86 ns |   3.23 KB |
| NormalizeBenchmark |  DateRange[4] |   423.2 ns | 3.75 ns | 3.33 ns |   1.02 KB |
*/

[MemoryDiagnoser(false)]
public class TripDateEvaluatorBenchmarks
{
    public static IEnumerable<DateRange[]> DateRangesSource() => new[]
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
    };
    
    [ParamsSource(nameof(DateRangesSource))]
    public DateRange[] DateRanges = null!;

    public volatile DateRange[] Results = null!;
    
    [Benchmark]
    public void NormalizeBenchmark()
    {
        Results = DateRange.Normalize(DateRanges);
    }
}