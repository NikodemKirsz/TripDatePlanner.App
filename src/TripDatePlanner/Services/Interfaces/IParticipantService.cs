using TripDatePlanner.Entities;

namespace TripDatePlanner.Services.Interfaces;

public interface IParticipantService : ICrudService<Participant, int>
{
    Task<Participant[]> GetMultipleWithRanges(ICollection<int> ids, CancellationToken token = default);
    Task<Participant[]> GetMultipleWithRangesByTripId(string tripId, CancellationToken token = default);
}