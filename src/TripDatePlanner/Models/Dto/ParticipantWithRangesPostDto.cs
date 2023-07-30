namespace TripDatePlanner.Models.Dto;

public sealed record ParticipantWithRangesPostDto : ParticipantPostDto
{
    public DateRange[] PreferredRanges { get; set; } = Array.Empty<DateRange>();
    public DateRange[] RejectedRanges { get; set; } = Array.Empty<DateRange>();
}