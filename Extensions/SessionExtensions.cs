using System.Text.Json;

namespace QL_CUA_HANG_BAN_XE_DAP.Extensions
{
    public static class SessionExtensions
    {
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        public static T? GetObjectFromJson<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return string.IsNullOrEmpty(value) ? default : JsonSerializer.Deserialize<T>(value);
        }
    }
}
