namespace TripDatePlanner.Models.Dto;

public sealed record RangeDto
{
    public int Id { get; set; }
    public DateRange DateRange { get; set; }
    public int ParticipantId { get; set; }
}