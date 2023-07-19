using Microsoft.EntityFrameworkCore;
using TripDatePlanner.Data;
using TripDatePlanner.Entities;
using TripDatePlanner.Services.Interfaces;
using Range = TripDatePlanner.Entities.Range;

namespace TripDatePlanner.Services;

public sealed class ParticipantService : CrudService<Participant, int>, IParticipantService
{
    private readonly ILogger<ParticipantService> _logger;
    private readonly DbSet<Participant> _participants;
    private readonly DbSet<Range> _ranges;

    public ParticipantService(ILogger<ParticipantService> logger, DataContext context)
        : base(logger, context, context.Participants)
    {
        _logger = logger;
        _participants = context.Participants;
        _ranges = context.Ranges;
    }

    protected override IQueryable<Participant> IncludeDependencies(IQueryable<Participant> query)
    {
        return query
            .Include(e => e.Trip)
            .Include(e => e.Ranges);
    }
}