using Microsoft.EntityFrameworkCore;
using TripDatePlanner.Data;
using TripDatePlanner.Entities;
using TripDatePlanner.Services.Interfaces;

namespace TripDatePlanner.Services;

public sealed class TripService : CrudService<Trip, string>, ITripService
{
    private readonly ILogger<TripService> _logger;
    private readonly DbSet<Trip> _trips;
    private readonly IUidGenerator<Trip> _uidGenerator;

    public TripService(ILogger<TripService> logger, DataContext context, IUidGenerator<Trip> uidGenerator)
        : base(logger, context, context.Trips)
    {
        _logger = logger;
        _trips = context.Trips;
        _uidGenerator = uidGenerator;
    }

    public new async Task<Trip> Create(Trip entity, bool save = true, CancellationToken token = default)
    {
        token.ThrowIfCancellationRequested();

        string uid = await _uidGenerator.Generate(token);

        entity.Id = uid;
        return await base.Create(entity, save, token);
    }

    protected override IQueryable<Trip> IncludeDependencies(IQueryable<Trip> query)
    {
        return query
            .Include(e => e.Participants);
    }
}