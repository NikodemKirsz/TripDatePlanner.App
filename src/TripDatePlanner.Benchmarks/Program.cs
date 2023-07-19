using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;
using TripDatePlanner.Benchmarks;

Summary summary = BenchmarkRunner.Run<TripDateEvaluatorBenchmarks>();

Console.ReadKey();