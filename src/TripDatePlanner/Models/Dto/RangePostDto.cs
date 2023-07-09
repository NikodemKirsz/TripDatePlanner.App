using TripDatePlanner.Entities.Enums;

namespace TripDatePlanner.Models.Dto;

public sealed class RangePostDto
{
    public DateOnly DateRangeStart { get; set; }
    public DateOnly DateRangeEnd { get; set; }
    public RangeType RangeType { get; set; }
    public int ParticipantId { get; set; }
}