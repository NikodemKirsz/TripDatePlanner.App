namespace TripDatePlanner.Models.Dto;

public sealed record TripPostDto
{
    public string? Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Passcode { get; set; } 
    public DateRange AllowedRange { get; set; }
    public int MinDays { get; set; }
    public int MaxDays { get; set; }
}