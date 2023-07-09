using Microsoft.AspNetCore.Mvc;
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

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken token = default)
    {
        TEntity[] entities = await _entityService.GetAll(token);
        
        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(TId id, [FromQuery] bool full = false, CancellationToken token = default)
    {
        TEntity? entity = await _entityService.Get(id, full, token: token);
        
        return Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TEntityPost entityPost, CancellationToken token = default)
    {
        TEntity entity = _entityMapper.Map(entityPost);
        TEntity createdEntity = await _entityService.Create(entity, token: token);
        
        return Ok(createdEntity);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(TId id, [FromBody] TEntityPost entityPost, CancellationToken token = default)
    {
        TEntity entity = _entityMapper.Map(entityPost);
        TEntity? updatedEntity = await _entityService.Update(id, entity, token: token);
        
        return Ok(updatedEntity);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Remove(TId id, CancellationToken token = default)
    {
        TEntity removedEntity = await _entityService.Remove(id, token: token);
        
        return Ok(removedEntity);
    }
}