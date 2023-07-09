using TripDatePlanner.Entities.Enums;
using TripDatePlanner.Entities.Interfaces;
using TripDatePlanner.Models.DateRange;

namespace TripDatePlanner.Entities;

public record Range : IEntity<int>
{
    public int Id { get; set; }
    public DateRange DateRange { get; set; }
    public RangeType RangeType { get; set; }
    public int ParticipantId { get; set; }
    
    public virtual Participant? Participant { get; set; }
}