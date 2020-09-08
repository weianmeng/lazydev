using System.Text.Json;

namespace LazyDev.Utilities.Extensions
{
    public static class ObjectExtension
    {
        public static string ToJson(this object obj)
        {
            return obj == null ? string.Empty : JsonSerializer.Serialize(obj);
        }

        

        /// <summary>
        /// 转为格式化的json
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToIndentedJson(this object obj)
        {
            return obj == null ? string.Empty : JsonSerializer.Serialize(obj, new JsonSerializerOptions
            {
                WriteIndented = true
            });
        }
    }
}
