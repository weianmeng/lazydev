namespace LazyDev.Core.Common
{
    /// <summary>
    /// 返回结果统一格式
    /// </summary>
    public interface ILazyResult
    {
        public bool IsSucceed { get; }
        string Msg { get; set; }
    }

    public interface ILazyResult<T> : ILazyResult
    {
        public T Data { get; set; }
    }


}
