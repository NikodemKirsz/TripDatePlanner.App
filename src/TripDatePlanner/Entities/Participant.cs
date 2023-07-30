using System.ComponentModel.DataAnnotations;
using TripDatePlanner.Entities.Interfaces;

namespace TripDatePlanner.Entities;

public record Participant : IEntity<int>
{
    public int Id { get; set; }

    [StringLength(Consts.MaxNameLength)]
    public string Name { get; set; } = null!;

    [StringLength(Consts.MaxTripIdLength)]
    public string TripId { get; set; } = null!;


    public virtual Trip? Trip { get; set; }

    public virtual IList<Range> Ranges { get; set; } = new List<Range>();
}