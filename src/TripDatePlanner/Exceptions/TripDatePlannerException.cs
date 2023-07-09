using System.Runtime.Serialization;

namespace TripDatePlanner.Exceptions;

public class TripDatePlannerException : Exception
{
    public TripDatePlannerException()
    {
    }

    protected TripDatePlannerException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public TripDatePlannerException(string? message) : base(message)
    {
    }

    public TripDatePlannerException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}