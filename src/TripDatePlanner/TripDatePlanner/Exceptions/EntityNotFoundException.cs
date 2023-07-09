namespace TripDatePlanner.Exceptions;

[Serializable]
public sealed class EntityNotFoundException : TripDatePlannerException
{
    private const string MessageTemplate = "Entity of type '{0}' with '{1}' = '{2}' was not found!";

    public Type EntityType { get; }
    public string KeyName { get; }
    public object Value { get; }

    public EntityNotFoundException(Type entityType, string keyName, object value)
        : base(String.Format(MessageTemplate, entityType.Name, keyName, value))
    {
        EntityType = entityType;
        KeyName = keyName;
        Value = value;
    }
}