namespace TripDatePlanner.Exceptions;

[Serializable]
public sealed class ParsingException : TripDatePlannerException
{
    private const string MessageTemplate = "Parsing exception occured! Could not parse string '{0}' to type '{1}'!";
    
    public string SourceString { get; }
    public Type TargetType { get; }
    
    public ParsingException(string sourceString, Type targetType)
        : this(sourceString, targetType, null)
    { }
    
    public ParsingException(string sourceString, Type targetType, Exception? innerException)
        : base(String.Format(MessageTemplate, sourceString, targetType.FullName), innerException)
    {
        SourceString = sourceString;
        TargetType = targetType;
    }
}