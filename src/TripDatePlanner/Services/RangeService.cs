using Microsoft.EntityFrameworkCore;
using TripDatePlanner.Data;
using TripDatePlanner.Entities.Enums;
using TripDatePlanner.Models;
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

    public async Task<Range[]> CreateRangeNormalized(ICollection<Range> ranges, bool save = true, CancellationToken token = default)
    {
        if (ranges.Count < 1)
            return Array.Empty<Range>();
        
        token.ThrowIfCancellationRequested();

        int participantId = ranges.First().ParticipantId;
        Dictionary<RangeType, DateRange[]> splitDateRanges = SplitRangesByTypes(ranges);
        
        Range[] normalizedRanges = splitDateRanges.SelectMany(x =>
        {
            List<DateRange> normalizedDateRanges = DateRange.Normalize(x.Value);
            return normalizedDateRanges.Select(y => new Range()
            {
                RangeType = x.Key,
                DateRange = y,
                ParticipantId = participantId,
            });
        })
            .ToArray();

        await CreateRange(normalizedRanges, save, token);

        return normalizedRanges;
    }

    public async Task<int> DeleteMultipleByParticipantId(int participantId, CancellationToken token = default)
    {
        token.ThrowIfCancellationRequested();

        int rowsDeleted = await _ranges
            .Where(r => r.ParticipantId == participantId)
            .ExecuteDeleteAsync(token);
        return rowsDeleted;
    }

    protected override IQueryable<Range> IncludeDependencies(IQueryable<Range> query)
    {
        return query
            .Include(e => e.Participant);
    }

    private static Dictionary<RangeType, DateRange[]> SplitRangesByTypes(IEnumerable<Range> ranges)
    {
        return ranges.GroupBy(
            x => x.RangeType,
            x => x.DateRange
        )
            .ToDictionary(
            x => x.Key,
            x => x.ToArray()
        );
    }
}