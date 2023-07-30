using TripDatePlanner.Entities.Enums;

namespace TripDatePlanner.Models.Dto;

public sealed record RangePostDto
{
    public int? Id { get; set; }
    public DateRange DateRange { get; set; }
    public RangeType RangeType { get; set; }
    public int ParticipantId { get; set; }
}