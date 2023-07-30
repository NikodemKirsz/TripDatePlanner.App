using System.Text.Json;
using System.Text.Json.Serialization;
using TripDatePlanner.Exceptions;

namespace TripDatePlanner.Models;

public class DateRangeJsonConverter : JsonConverter<DateRange>
{
    public override DateRange Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string? valueStr = reader.GetString();
        if (valueStr is null || typeToConvert != typeof(DateRange))
            throw new ConversionException(valueStr, typeToConvert, typeof(string));
        
        return DateRange.Parse(valueStr);
    }

    public override void Write(Utf8JsonWriter writer, DateRange value, JsonSerializerOptions options)
    {
        string dateRangeStr = value.ToString();
        writer.WriteStringValue(dateRangeStr);
    }
}