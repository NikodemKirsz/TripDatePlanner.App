namespace TripDatePlanner.Utilities.Extensions;

public static class DateExtensions
{
    public static bool IsWeekend(this DateTime dateTime) => dateTime.DayOfWeek.IsWeekend();
    
    public static bool IsWeekend(this DateOnly date) => date.DayOfWeek.IsWeekend();
    
    public static bool IsWeekend(this DayOfWeek day) => day is DayOfWeek.Saturday or DayOfWeek.Sunday;
    
}