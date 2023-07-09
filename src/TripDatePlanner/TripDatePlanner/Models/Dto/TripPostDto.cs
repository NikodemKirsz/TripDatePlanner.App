namespace TripDatePlanner.Models.Dto;

public sealed class TripPostDto
{
    public string Name { get; set; } = null!;
    public string? Password { get; set; }
    public DateOnly AllowedRangeStart { get; set; }
    public DateOnly AllowedRangeEnd { get; set; }
    public int MinDays { get; set; }
    public int MaxDays { get; set; }
}