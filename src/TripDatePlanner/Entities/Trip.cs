using TripDatePlanner.Entities.Interfaces;
using TripDatePlanner.Models;

namespace TripDatePlanner.Entities;

public record Trip : IEntity<string>
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? Password { get; set; }
    public DateRange AllowedRange { get; set; }
    public int MinDays { get; set; }
    public int MaxDays { get; set; }

    public virtual ICollection<Participant> Participants { get; set; } = new List<Participant>();
}