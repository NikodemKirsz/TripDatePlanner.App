using TripDatePlanner.Entities;
using TripDatePlanner.Entities.Enums;
using TripDatePlanner.Models;
using Range = TripDatePlanner.Entities.Range;

namespace TripDatePlanner.Utilities.Extensions;

public static class ParticipantExtensions
{
    public static (DateRange[] preferredRanges, DateRange[] rejectedRanges) SplitRanges(this Participant participant)
    {
        List<DateRange> preferredRanges = new(participant.Ranges.Count);
        List<DateRange> rejectedRanges = new(participant.Ranges.Count);

        foreach (Range range in participant.Ranges)
        {
            List<DateRange> currentRanges = range.RangeType switch
            {
                RangeType.Preferred => preferredRanges,
                RangeType.Rejected => rejectedRanges,
                _ => throw new ArgumentOutOfRangeException(nameof(participant.Ranges)),
            };
            currentRanges.Add(range.DateRange);
        }

        return (preferredRanges.ToArray(), rejectedRanges.ToArray());
    }
}