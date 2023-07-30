namespace TripDatePlanner.Exceptions;

public class ConversionException : TripDatePlannerException
{
    private const string MessageTemplate = "Failed to convert value '{0}' of type {1}, to type {2}!";

    public Type SourceType { get; }
    public Type DestinationType { get; }
    public object? SourceValue { get; }

    public ConversionException(object? sourceValue, Type sourceType, Type destinationType)
        : this(sourceValue, sourceType, destinationType, null)
    { }

    public ConversionException(object? sourceValue, Type sourceType, Type destinationType, Exception? innerException)
        : base(
            String.Format(MessageTemplate, sourceValue, sourceType.FullName, destinationType.FullName),
            innerException)
    {
        SourceType = sourceType;
        DestinationType = destinationType;
        SourceValue = sourceValue;
    }
}