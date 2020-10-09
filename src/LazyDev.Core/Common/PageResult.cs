using System.Collections.Generic;

namespace LazyDev.Core.Common
{
    public class PageResult<T>
    {
        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 页大小
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 总记录数
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPage
        {
            get
            {
                if (PageSize == 0)
                {
                    PageSize = 10;
                }
                var totalPage = TotalCount / PageSize;
                if (TotalCount % PageSize != 0) totalPage++;

                return totalPage;
            }
        }

        /// <summary>
        /// 获取或设置 分页数据
        /// </summary>
        public IEnumerable<T> Data { get; set; }

        public static PageResult<T> Page(int pageIndex,int pageSize,int totalCount, IEnumerable<T> data)
        {
            return new PageResult<T>()
            {
                 PageIndex = pageIndex,
                 PageSize = pageSize,
                 TotalCount = totalCount,
                 Data = data
            };
        }


    }
}
