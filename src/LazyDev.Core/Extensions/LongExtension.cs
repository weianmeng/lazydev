using System;

namespace LazyDev.Core.Extensions
{
    public static class LongExtension
    {
        /// <summary>
        ///   时间戳转本地时间
        /// </summary> 
        public static DateTime ToLocalTimeTime(this long unix)
        {
            var dto = DateTimeOffset.FromUnixTimeSeconds(unix);
            return dto.ToLocalTime().DateTime;
        }
    }
}
