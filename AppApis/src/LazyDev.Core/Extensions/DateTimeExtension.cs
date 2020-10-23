using System;

namespace LazyDev.Core.Extensions
{
    public static class DateTimeExtension
    {
        /// <summary>
        /// 时间转时间戳
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static long ToTimeStamp(this DateTime date)
        {
            var dto = new DateTimeOffset(date);
            return dto.ToUnixTimeSeconds();
        }
    }
}
