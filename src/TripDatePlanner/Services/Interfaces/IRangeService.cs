using Range = TripDatePlanner.Entities.Range;

namespace TripDatePlanner.Services.Interfaces;

public interface IRangeService : ICrudService<Range, int>
{
    Task<Range[]> CreateRangeNormalized(ICollection<Range> ranges, bool save = true, CancellationToken token = default);
    Task<int> DeleteMultipleByParticipantId(int participantId, CancellationToken token = default);
}