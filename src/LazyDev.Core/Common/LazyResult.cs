namespace LazyDev.Core.Common
{
    public class LazyResult:ILazyResult
    {
        public bool IsSucceed { get; set; } = true;
        public string Msg { get; set; }

        public static LazyResult Success()
        {
            return new LazyResult();
        }

        public static LazyResult Failed(int code,string msg=null)
        {
            return new LazyDevFailedResult { ErrorCode = code, Msg = msg, IsSucceed=false};
        }
    }

    public class LazyDevFailedResult: LazyResult
    {
        public int ErrorCode { get; set; }
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
