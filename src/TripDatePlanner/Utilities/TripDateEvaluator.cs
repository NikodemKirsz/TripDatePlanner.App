using TripDatePlanner.Entities;
using TripDatePlanner.Models;
using TripDatePlanner.Utilities.Extensions;

namespace TripDatePlanner.Utilities;

public sealed class TripDateEvaluator
{
    private readonly DateRange _tripRange;
    private readonly EvaluationOptions _options;
    private readonly Dictionary<int, EvalParticipant> _participants;

    public TripDateEvaluator(Trip trip, EvaluationOptions options)
    {
        _tripRange = trip.AllowedRange;
        _options = options;
        _participants = trip.Participants
            .Select(participant => EvalParticipant.FromParticipant(participant, _tripRange))
            .ToDictionary(
                x => x.Id,
                x => x
            );
    }

    public DateRange[] EvaluateAsync(CancellationToken token = default)
    {
        token.ThrowIfCancellationRequested();

        return Array.Empty<DateRange>();
    }

    public readonly struct EvalParticipant
    {
        public required int Id { get; init; }
        public required DateRange[] PreferredRanges { get; init; }
        public required DateRange[] NeutralRanges { get; init; }
        public required DateRange[] RejectedRanges { get; init; }

        public static EvalParticipant FromParticipant(Participant participant, DateRange tripRange)
        {
            var (preferredRanges, rejectedRanges) = participant.SplitRanges();
            DateRange[] neutralRanges = tripRange.Except(preferredRanges.Concat(rejectedRanges).ToArray());

            return new EvalParticipant()
            {
                Id = participant.Id,
                PreferredRanges = preferredRanges,
                NeutralRanges = neutralRanges,
                RejectedRanges = rejectedRanges,
            };
        }
    }
}

public sealed record EvaluationOptions
{
    public bool ExcludeWeekdays { get; set; } = false;
    public bool ExcludeWeekends { get; set; } = false;
}