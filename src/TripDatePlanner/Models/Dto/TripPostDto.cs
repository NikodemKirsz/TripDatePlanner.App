namespace TripDatePlanner.Models.Dto;

public sealed class TripPostDto
{
    public string Name { get; set; } = null!;
    public string? Password { get; set; }
    
    public DateRange AllowedRange { get; set; }
    public int MinDays { get; set; }
    public int MaxDays { get; set; }
}