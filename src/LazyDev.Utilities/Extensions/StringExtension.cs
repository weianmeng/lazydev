using Newtonsoft.Json;

namespace LazyDev.Utilities.Extensions
{

    public static class StringExtension
    {

        /// <summary>
        /// 字符串反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str"></param>
        /// <returns></returns>
        public static T ToObject<T>(this string str)
        {
            return string.IsNullOrEmpty(str) ? default : JsonConvert.DeserializeObject<T>(str);
        }

        /// <summary>
        /// 字符串为空判断
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }
    }
}
