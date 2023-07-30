using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using TripDatePlanner.Entities.Interfaces;
using TripDatePlanner.Mappers.Interfaces;
using TripDatePlanner.Services.Interfaces;

namespace TripDatePlanner.Controllers;

public abstract class CrudController<TId, TEntity, TEntityPost> : ControllerBase
    where TEntity : class, IEntity<TId>, new()
    where TId : IConvertible, IComparable, IComparable<TId>, IEquatable<TId>
{
    private readonly ILogger<CrudController<TId, TEntity, TEntityPost>> _logger;
    private readonly ICrudService<TEntity, TId> _entityService;
    private readonly IMapper<TEntityPost, TEntity> _entityMapper;

    protected CrudController(
        ILogger<CrudController<TId, TEntity, TEntityPost>> logger,
        ICrudService<TEntity, TId> entityService,
        IMapper<TEntityPost, TEntity> entityMapper)
    {
        _logger = logger;
        _entityMapper = entityMapper;
        _entityService = entityService;
    }

    [HttpGet("[action]")]
    public virtual async Task<ActionResult<TEntity[]>> GetAll(CancellationToken token = default)
    {
        TEntity[] entities = await _entityService.GetAll(token);
        
        return Ok(entities);
    }

    [HttpGet("{id}/[action]")]
    public virtual async Task<ActionResult<TEntity>> Get(TId id, [FromQuery] bool full = false, CancellationToken token = default)
    {
        TEntity entity = await _entityService.Get(id, full, token: token);
        
        return Ok(entity);
    }

    [HttpPost("[action]")]
    public virtual async Task<ActionResult<TEntity>> Create([FromBody] TEntityPost entityPost, CancellationToken token = default)
    {
        TEntity entity = _entityMapper.Map(entityPost);
        TEntity createdEntity = await _entityService.Create(entity, token: token);
        
        return CreatedAtAction(
            nameof(Get),
            typeof(TEntity).Name,
            new { createdEntity.Id },
            createdEntity
        );
    }

    [HttpPut("{id}/[action]")]
    public virtual async Task<ActionResult<TEntity>> Update(TId id, [FromBody] TEntityPost entityPost, CancellationToken token = default)
    {
        TEntity entity = _entityMapper.Map(entityPost);
        TEntity updatedEntity = await _entityService.Update(id, entity, token: token);
        if (updatedEntity is null)
            return NotFound();
        
        return Ok(updatedEntity);
    }

    [HttpDelete("{id}/[action]")]
    public virtual async Task<ActionResult<TEntity>> Delete(TId id, CancellationToken token = default)
    {
        TEntity deleteEntity = await _entityService.Delete(id, token: token);
        
        return Ok(deleteEntity);
    }
    
    protected int SaveChanges() => _entityService.SaveChanges();
    
    protected Task<int> SaveChangesAsync(CancellationToken token = default) => _entityService.SaveChangesAsync(token);
    
    protected IDbContextTransaction BeginTransaction() => _entityService.BeginTransaction();

    protected Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken token = default) => _entityService.BeginTransactionAsync(token);

    protected void CommitTransaction() => _entityService.CommitTransaction();
    
    protected Task CommitTransactionAsync(CancellationToken token = default) => _entityService.CommitTransactionAsync(token);

    protected void RollbackTransaction() => _entityService.RollbackTransaction();
    
    protected Task RollbackTransactionAsync(CancellationToken token = default) => _entityService.RollbackTransactionAsync(token);
}