using System.Text.Json;

namespace TopBookStore.Mvc.Sessions;

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

    public static void SetListObject<T>(this ISession session, string key, List<T> value) =>
        session.SetString(key, JsonSerializer.Serialize(value));
    
    public static List<T>? GetListObject<T>(this ISession session, string key)
    {
        string? value = session.GetString(key);

        if (value is not null)
        {
            return JsonSerializer.Deserialize<List<T>>(value);
        }
        else
        {
            return default;
        }
    }
}