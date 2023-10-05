using System.Text.Json;

namespace TopBookStore.Mvc.Extensions;

public static class CookieExtensions
{
    public static string GetString(this IRequestCookieCollection requestCookies, string key) =>
        requestCookies[key] ?? string.Empty;

    public static int? GetInt32(this IRequestCookieCollection requestCookies, string key)
    {
        string valueString = requestCookies.GetString(key);

        // it will automatically cast to int? if null
        return int.TryParse(valueString, out int value) ? value : null;
    }

    public static T? GetObject<T>(this IRequestCookieCollection requestCookies, string key)
    {
        string valueString = requestCookies.GetString(key);

        return string.IsNullOrEmpty(valueString) ? default : JsonSerializer.Deserialize<T>(valueString);
    }

    public static void SetString(this IResponseCookies responseCookies, string key, string value,
        int days = 30)
    {
        responseCookies.Delete(key);

        if (days == 0)
        {
            responseCookies.Append(key, value);
        }
        else
        {
            CookieOptions options = new()
            {
                Expires = DateTime.Now.AddDays(days)
            };
            responseCookies.Append(key, value, options);
        }
    }

    public static void SetInt32(this IResponseCookies responseCookies, string key, int value,
        int days = 30)
    {
        string valueString = value.ToString();

        SetString(responseCookies, key, valueString, days);
    }

    public static void SetObject<T>(this IResponseCookies responseCookies, string key, T value,
        int days = 30)
    {
        string valueString = JsonSerializer.Serialize(value);

        SetString(responseCookies, key, valueString, days);
    }
}