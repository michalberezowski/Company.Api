using System.Text.Json;

namespace Company.Api.Utils.Extensions;

public static class ObjectExtensions
{
    public static string Serialize(this object obj) => 
        JsonSerializer.Serialize(obj);

    public static T? Deserialize<T>(this string json)
    {
        //todo: make sure we are getting the right type/null
        return JsonSerializer.Deserialize<T>(json) ?? default;
    }
}