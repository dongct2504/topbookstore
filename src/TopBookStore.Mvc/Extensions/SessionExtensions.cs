using System.Text.Json;

namespace TopBookStore.Mvc.Extensions;

public static class SessionExtensions
{
    public static void SetObject<T>(this ISession session, string key, T value) =>
        session.SetString(key, JsonSerializer.Serialize(value));

    public static T? GetObject<T>(this ISession session, string key)
    {
        string? value = session.GetString(key);

        if (value is not null)
        {
            return JsonSerializer.Deserialize<T>(value);
        }
        else
        {
            return default;
        }
    }
}