using LazyDev.Utilities.Json;
using System.Text.Json;

namespace LazyDev.Utilities.Extensions
{
    public static class ObjectExtension
    {
        public static string ToJson(this object obj, JsonSerializerOptions options = null)
        {
            if (options == null)
            {
                options = new JsonSerializerOptions();
                options.Converters.Add(new DateTimeConverter());
                options.Converters.Add(new DateTimeNullableConverter());
                options.WriteIndented = true;
            }

            return obj == null ? string.Empty : JsonSerializer.Serialize(obj, options);
        }

        

        /// <summary>
        /// 转为格式化的json
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToIndentedJson(this object obj,JsonSerializerOptions options =null)
        {
            if (options == null)
            {
                options = new JsonSerializerOptions();
                options.Converters.Add(new DateTimeConverter());
                options.Converters.Add(new DateTimeNullableConverter());
                options.WriteIndented = true;
            }

            return obj == null ? string.Empty : JsonSerializer.Serialize(obj, options);
        }
    }
}
