using System.Runtime.Serialization;

namespace TripDatePlanner.Exceptions;

public class TripDatePlannerException : Exception
{
    private const string MessageTemplate = $$"""{{Consts.AppName}} Exception!""";
    
    public TripDatePlannerException()
        : this(null)
    {
    }

    public TripDatePlannerException(string? message)
        : this(message, null)
    {
    }

    public TripDatePlannerException(string? message, Exception? innerException)
        : base(String.Join(' ', MessageTemplate, message), innerException)
    {
    }
}