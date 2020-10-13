using System.Text.Json.Serialization;

namespace LazyDev.Core.Common
{
    public interface IPageRequest
    {
        /// <summary>
        /// 获取或设置 页索引
        /// </summary>
        int PageIndex { get; set; }

        /// <summary>
        /// 获取或设置 页大小
        /// </summary>
        int PageSize { get; set; }

        [JsonIgnore]
        int Skip { get; }
    }
}