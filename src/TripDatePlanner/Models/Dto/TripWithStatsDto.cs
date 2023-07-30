namespace TripDatePlanner.Models.Dto;

public sealed record TripWithStatsDto
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? Passcode { get; set; }
    public DateRange AllowedRange { get; set; }
    public int MinDays { get; set; }
    public int MaxDays { get; set; }
    public ICollection<ParticipantWithStatsDto> Participants { get; set; } = Array.Empty<ParticipantWithStatsDto>();
}