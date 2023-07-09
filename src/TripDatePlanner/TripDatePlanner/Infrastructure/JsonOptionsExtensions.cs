using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace TripDatePlanner.Infrastructure;

public static class JsonOptionsExtensions
{
    public static void AddReferenceHandler(this JsonOptions options)
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    }
    
    public static void AddConverters(this JsonOptions options)
    {
        IList<JsonConverter> converters = options.JsonSerializerOptions.Converters;
        
        converters.Add(new JsonStringEnumConverter());
    }
}