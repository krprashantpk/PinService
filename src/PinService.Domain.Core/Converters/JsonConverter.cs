
using System;
using System.Text.Json;

namespace PinService.Domain.Core.Converters;

public static class JsonConverter
{

    public static string Serialize<T>(T value, JsonSerializerOptions? options = null) where T : class
    {
        return JsonSerializer.Serialize<T>(value, options);
    }
    public static T? Deserialize<T>(string value, JsonSerializerOptions? options = null) where T : class
    {
        return JsonSerializer.Deserialize<T>(value, options);
    }
}