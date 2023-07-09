using Microsoft.EntityFrameworkCore;
using TripDatePlanner.Data;
using TripDatePlanner.Entities;
using TripDatePlanner.Services.Interfaces;

namespace TripDatePlanner.Services;

public sealed class ParticipantService : CrudService<Participant, int>, IParticipantService
{
    private readonly ILogger<ParticipantService> _logger;
    private readonly DbSet<Participant> _participants;

    public ParticipantService(ILogger<ParticipantService> logger, DataContext context)
        : base(logger, context, context.Participants)
    {
        _logger = logger;
        _participants = context.Participants;
    }

    protected override IQueryable<Participant> IncludeDependencies(IQueryable<Participant> query)
    {
        return query
            .Include(e => e.Trip)
            .Include(e => e.Ranges);
    }
}