using System.Text.Json;

namespace BloodMuAPI.Extensions
{
    public static class JsonConverter
    {
        public static string ToJson<T>(this T source)
        {
            return JsonSerializer.Serialize(source);
        }
    }
}
