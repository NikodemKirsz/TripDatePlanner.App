using System.Diagnostics;

namespace TripDatePlanner.Utilities;

public static class PolandDateTime
{
    private static readonly TimeZoneInfo PolandTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");
    private static readonly TimeSpan CacheTime = TimeSpan.FromSeconds(5);
    private static readonly Stopwatch Watch = Stopwatch.StartNew();
    
    public static DateTime Now
    {
        get
        {
            if (Watch.Elapsed < CacheTime) return _cachedNow;

            Watch.Restart();
            return _cachedNow = ConvertedNow;
        }
    }

    private static DateTime ConvertedNow => TimeZoneInfo.ConvertTime(DateTime.UtcNow, PolandTimeZone);
    private static DateTime _cachedNow = ConvertedNow;
}