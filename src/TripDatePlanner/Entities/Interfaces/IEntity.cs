namespace TripDatePlanner.Entities.Interfaces;

public interface IEntity<TId>
    where TId : IConvertible, IComparable, IComparable<TId>, IEquatable<TId>
{
    TId Id { get; set; }
}