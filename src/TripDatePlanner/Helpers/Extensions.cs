namespace TripDatePlanner.Helpers;

public static class Extensions
{
    public static bool Between(this int number, int minInclusive, int maxInclusive)
    {
        return minInclusive <= number && number <= maxInclusive;
    }

    public static bool IsWeekend(this DateTime dateTime) => dateTime.DayOfWeek.IsWeekend();
    
    public static bool IsWeekend(this DateOnly date) => date.DayOfWeek.IsWeekend();
    
    public static bool IsWeekend(this DayOfWeek day) => day is DayOfWeek.Saturday or DayOfWeek.Sunday;
}