namespace LazyDev.Core.Common
{
    public class LazyResult:ILazyResult
    {
        public bool IsSuccess => Code == 0;
        public int Code { get; set; }
        public string Msg { get; set; }

        public static LazyResult Success()
        {
            return new LazyResult();
        }

        public static LazyResult Failed(int code,string msg=null)
        {
            return new LazyResult {Code = code, Msg = msg};
        }
    }

    public class LazyResult<T> : LazyResult, ILazyResult<T>
    {
        public T Data { get; set; }

        public static LazyResult<T> Success(T data)
        {
            return new LazyResult<T> {Data = data};
        }
    }

}
