using TripDatePlanner.Entities.Interfaces;
using TripDatePlanner.Models;

namespace TripDatePlanner.Entities;

public record Participant : IEntity<int>
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string TripId { get; set; } = null!;

    public virtual Trip? Trip { get; set; }
    public virtual ICollection<Range> Ranges { get; set; } = new List<Range>();
}