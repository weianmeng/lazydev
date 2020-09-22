using System.Collections.Generic;

namespace LazyDev.AspNetCore
{
    /// <summary>
    /// 返回结果统一格式
    /// </summary>
    public interface ILazyResult
    {
        public bool IsSuccess { get; }
        int Code { get; set; }
        string Msg { get; set; }
        IDictionary<string, string> MsgDetail { get; set; }
    }


}
