namespace LazyDev.Core.Common
{
    public class PageRequest
    {
        /// <summary>
        /// 获取或设置 页索引
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 获取或设置 页大小
        /// </summary>
        public int PageSize { get; set; }

        public int Skip => (PageIndex - 1) * PageSize;

        public PageRequest()
        {
            PageIndex = 1;
            PageSize = 20;
        }
    }
}
