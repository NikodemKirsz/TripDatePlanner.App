namespace TripDatePlanner.Models.Dto;

public record ParticipantPostDto
{
    public int? Id { get; set; }
    public string Name { get; set; } = null!;
    public string TripId { get; set; } = null!;
}