namespace TripDatePlanner.Models.Dto;

public sealed class ParticipantWithRangesPostDto : ParticipantPostDto
{
    public RangePostDto[] RangesPostDto { get; set; } = Array.Empty<RangePostDto>();
}