namespace TripDatePlanner.Utilities.Extensions;

public static class NumberExtensions
{
    public static bool Between(this int number, int minInclusive, int maxInclusive)
    {
        return minInclusive <= number && number <= maxInclusive;
    }
}