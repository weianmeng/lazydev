using Newtonsoft.Json;

namespace LazyDev.Core.Common
{
    public class PageRequestBase : IPageRequest
    {
        /// <summary>
        /// 获取或设置 页索引
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 获取或设置 页大小
        /// </summary>
        public int PageSize { get; set; }

        [JsonIgnore]
        public int Skip => (PageIndex - 1) * PageSize;

        public PageRequestBase()
        {
            PageIndex = 1;
            PageSize = 20;
        }
    }
}
