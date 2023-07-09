using TripDatePlanner.Entities.Interfaces;

namespace TripDatePlanner.Services.Interfaces;

public interface IUidGenerator<TEntity>
    where TEntity : class, IEntity<string>
{
    Task<string> Generate(CancellationToken token = default);
}