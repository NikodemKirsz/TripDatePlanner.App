using TripDatePlanner.Entities.Interfaces;

namespace TripDatePlanner.Services.Interfaces;

public interface ICrudService<TEntity, TId>
    where TEntity : class, IEntity<TId>
    where TId : IConvertible, IComparable, IComparable<TId>, IEquatable<TId>
{
    Task<TEntity[]> GetAll(CancellationToken token = default);
    Task<TEntity?> Get(TId id, bool full = false, CancellationToken token = default);
    Task<TEntity> Create(TEntity entity, bool save = true, CancellationToken token = default);
    Task<TEntity?> Update(TId id, TEntity newEntity, bool save = true, CancellationToken token = default);
    Task<TEntity?> Update(TId id, Action<TEntity> update, bool save = true, CancellationToken token = default);
    Task<TEntity> Remove(TId id, bool save = true, CancellationToken token = default);
    Task<int> SaveChanges();
}