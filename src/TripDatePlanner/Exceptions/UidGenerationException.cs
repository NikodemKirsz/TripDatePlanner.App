using Microsoft.EntityFrameworkCore.Metadata;

namespace TripDatePlanner.Exceptions;

[Serializable]
public sealed class UidGenerationException : TripDatePlannerException
{
    private const string MessageTemplate = "Failed to generate UId for {0} table! Run out of tries limit: {1}!";
    
    public IEntityType EntityType { get; }
    public int MaxTries { get; }
    
    public UidGenerationException(IEntityType entityType, int maxTries)
        : base(String.Format(MessageTemplate, entityType.Name, maxTries))
    {
        EntityType = entityType;
        MaxTries = maxTries;
    }
}