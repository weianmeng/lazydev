using Newtonsoft.Json;

namespace LazyDev.Utilities.Extensions
{
    public static class ObjectExtension
    {
        public static string ToJson(this object obj)
        {
            return obj == null ? string.Empty : JsonConvert.SerializeObject(obj);
        }
    }
}
