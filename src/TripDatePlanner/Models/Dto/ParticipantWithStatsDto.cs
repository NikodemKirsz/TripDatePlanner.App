namespace TripDatePlanner.Models.Dto;

public sealed record ParticipantWithStatsDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string TripId { get; set; } = null!;
    public ICollection<DateRange> PreferredRanges { get; set; } = Array.Empty<DateRange>();
    public ICollection<DateRange> RejectedRanges { get; set; } = Array.Empty<DateRange>();
    public int PreferredDays { get; set; }
    public int RejectedDays { get; set; }
}