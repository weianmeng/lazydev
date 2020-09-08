using System.Text.Json;

namespace LazyDev.Utilities.Extensions
{
    public static class ObjectExtension
    {
        public static string ToJson(this object obj)
        {
            return obj == null ? string.Empty : JsonSerializer.Serialize(obj);
        }
    }
}
