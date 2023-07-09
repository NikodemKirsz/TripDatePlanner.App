using Microsoft.EntityFrameworkCore;
using TripDatePlanner.Data;
using TripDatePlanner.Services.Interfaces;
using Range = TripDatePlanner.Entities.Range;

namespace TripDatePlanner.Services;

public sealed class RangeService : CrudService<Range, int>, IRangeService
{
    private readonly ILogger<RangeService> _logger;
    private readonly DbSet<Range> _ranges;
    
    public RangeService(ILogger<RangeService> logger, DataContext context)
        : base(logger, context, context.Ranges)
    {
        _logger = logger;
        _ranges = context.Ranges;
    }

    protected override IQueryable<Range> IncludeDependencies(IQueryable<Range> query)
    {
        return query
            .Include(e => e.Participant);
    }
}