using Newtonsoft.Json;

namespace LazyDev.Core.Extensions
{
    public static class ObjectExtension
    {
        public static string ToJson(this object obj, JsonSerializerSettings settings=null)
        {
            if (obj == null)
            {
                return default;
            }

            if (settings == null)
            {
                return JsonConvert.SerializeObject(obj);
            }

            return JsonConvert.SerializeObject(obj, settings);
        }


        /// <summary>
        /// 转为格式化的json
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToIndentedJson(this object obj)
        {
            return obj == null ? string.Empty : JsonConvert.SerializeObject(obj,new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented
            });
        }
    }
}
