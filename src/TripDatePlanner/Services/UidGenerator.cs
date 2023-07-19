using Microsoft.EntityFrameworkCore;
using TripDatePlanner.Data;
using TripDatePlanner.Entities.Interfaces;
using TripDatePlanner.Exceptions;
using TripDatePlanner.Helpers;
using TripDatePlanner.Services.Interfaces;

namespace TripDatePlanner.Services;

public sealed class UidGenerator<TEntity> : IUidGenerator<TEntity>
    where TEntity : class, IEntity<string>
{
    private const int Length = 4;
    private const int MaxTries = 4;
    
    private readonly ILogger<UidGenerator<TEntity>> _logger;
    private readonly DbSet<TEntity> _entitySet;
    private readonly SemaphoreSlim _semaphore = new(1);

    public UidGenerator(
        ILogger<UidGenerator<TEntity>> logger,
        DataContext dataContext)
    {
        _logger = logger;
        _entitySet = dataContext.Set<TEntity>()
            ?? throw new ArgumentException($"No table exists for entity {nameof(TEntity)}", nameof(dataContext));;
    }

    public async Task<string> Generate(CancellationToken token = default)
    {
        await _semaphore.WaitAsync(token);
        
        int tries = 0;
        while (tries < MaxTries)
        {
            tries++;
            
            string potentialUid = UidUtils.CreateRandomId(Length);
            bool exists = await Exists(potentialUid, token);

            if (exists)
                continue;
            
            _logger.LogInformation(
                "Successfully generated Uid for {Type} table (tries: {Tries})", 
                _entitySet.EntityType.Name,
                tries
            );
            
            _semaphore.Release(1);
            return potentialUid;
        }
        
        _semaphore.Release(1);
        throw new UidGenerationException(_entitySet.EntityType, MaxTries);
    }

    private async Task<bool> Exists(string id, CancellationToken token = default)
    {
        return await _entitySet.FindAsync(new object[] { id }, cancellationToken: token) is not null;
    }
}