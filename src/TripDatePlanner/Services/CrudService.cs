using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using TripDatePlanner.Data;
using TripDatePlanner.Entities.Interfaces;
using TripDatePlanner.Exceptions;
using TripDatePlanner.Services.Interfaces;

namespace TripDatePlanner.Services;

public abstract class CrudService<TEntity, TId> : ICrudService<TEntity, TId >
    where TEntity : class, IEntity<TId>, new()
    where TId : IConvertible, IComparable, IComparable<TId>, IEquatable<TId>
{
    private readonly ILogger<CrudService<TEntity, TId>> _logger;
    private readonly DataContext _context;
    private readonly DbSet<TEntity> _dbSet;

    protected CrudService(ILogger<CrudService<TEntity, TId>> logger, DataContext context, DbSet<TEntity>? dbSet = default)
    {
        _logger = logger;
        _context = context;
        _dbSet = dbSet ?? context.Set<TEntity>();
    }

    public virtual async Task<TEntity[]> GetAll(CancellationToken token = default)
    {
        return await _dbSet.ToArrayAsync(token);
    }

    public virtual async Task<TEntity?> Get(TId id, bool full = false, CancellationToken token = default)
    {
        TEntity? entity = full
            ? await IncludeDependencies(_dbSet).FirstOrDefaultAsync(e => id.Equals(e.Id), cancellationToken: token)
            : await _dbSet.FindAsync(new object?[] { id }, cancellationToken: token);
        
        return entity ?? throw new EntityNotFoundException(typeof(TEntity), nameof(id), id);
    }

    public virtual async Task<TEntity> Create(TEntity entity, bool save = true, CancellationToken token = default)
    {
        EntityEntry<TEntity> entityEntry = await _dbSet.AddAsync(entity, token);

        if (save)
            await SaveChangesAsync(token);

        return entityEntry.Entity;
    }
    
    public virtual async Task CreateRange(ICollection<TEntity> entities, bool save = true, CancellationToken token = default)
    {
        await _dbSet.AddRangeAsync(entities, token);

        if (save)
            await SaveChangesAsync(token);
    }

    public virtual async Task<TEntity?> Update(TId id, Action<TEntity> update, bool save = true, CancellationToken token = default)
    {
        TEntity? entity = await Get(id, token: token);
        if (entity is null)
            return null;
        
        update(entity);

        return await Update(id, entity, token: token);
    }

    public virtual async Task<TEntity?> Update(TId id, TEntity newEntity, bool save = true, CancellationToken token = default)
    {
        newEntity.Id = id;
        EntityEntry<TEntity>? entityEntry = null;

        await Task.Run(() => entityEntry = _dbSet.Update(newEntity), token);
        if (save)
            await SaveChangesAsync(token);

        return entityEntry?.Entity;
    }

    public virtual async Task<TEntity> Remove(TId id, bool save = true, CancellationToken token = default)
    {
        TEntity entity = new() { Id = id };

        _dbSet.Attach(entity);
        EntityEntry<TEntity> entityEntry = _dbSet.Remove(entity);

        if (save)
            await SaveChangesAsync(token);

        return entityEntry.Entity;
    }

    public int SaveChanges() => _context.SaveChanges();
    
    public Task<int> SaveChangesAsync(CancellationToken token = default) => _context.SaveChangesAsync(token);

    public IDbContextTransaction BeginTransaction() => _context.Database.BeginTransaction();
    
    public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken token = default) => _context.Database.BeginTransactionAsync(token);

    public void CommitTransaction() => _context.Database.CommitTransaction();
    
    public Task CommitTransactionAsync(CancellationToken token = default) => _context.Database.CommitTransactionAsync(token);

    public void RollbackTransaction() => _context.Database.RollbackTransaction();
    
    public Task RollbackTransactionAsync(CancellationToken token = default) => _context.Database.RollbackTransactionAsync(token);

    protected abstract IQueryable<TEntity> IncludeDependencies(IQueryable<TEntity> query);
}