using Microsoft.EntityFrameworkCore.Storage;
using TripDatePlanner.Entities.Interfaces;

namespace TripDatePlanner.Services.Interfaces;

public interface ICrudService<TEntity, in TId>
    where TEntity : class, IEntity<TId>
    where TId : IConvertible, IComparable, IComparable<TId>, IEquatable<TId>
{
    Task<TEntity[]> GetAll(CancellationToken token = default);
    Task<TEntity> Get(TId id, bool full = false, CancellationToken token = default);
    Task<TEntity> Create(TEntity entity, bool save = true, CancellationToken token = default);
    Task CreateRange(ICollection<TEntity> entities, bool save = true, CancellationToken token = default);
    Task<TEntity> Update(TId id, TEntity newEntity, bool save = true, CancellationToken token = default);
    Task<TEntity> Update(TId id, Action<TEntity> update, bool save = true, CancellationToken token = default);
    Task<TEntity> Delete(TId id, bool save = true, CancellationToken token = default);
    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken token = default);
    IDbContextTransaction BeginTransaction();
    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken token = default);
    void CommitTransaction();
    Task CommitTransactionAsync(CancellationToken token = default);
    void RollbackTransaction();
    Task RollbackTransactionAsync(CancellationToken token = default);
}