namespace LazyDev.Core.Common
{
    public class LazyDevResult:ILazyDevResult
    {
        public bool IsSucceed { get; set; } = true;
        public string Msg { get; set; }

        public static LazyDevResult Success()
        {
            return new LazyDevResult();
        }

        public static LazyDevResult Failed(int code,string msg=null)
        {
            return new LazyDevFailedResult { ErrorCode = code, Msg = msg, IsSucceed=false};
        }
    }

    public class LazyDevFailedResult: LazyDevResult
    {
        public int ErrorCode { get; set; }
    }


    public class LazyResult<T> : LazyDevResult, ILazyResult<T>
    {
        public T Data { get; set; }

        public static LazyResult<T> Success(T data)
        {
            return new LazyResult<T> {Data = data};
        }
    }

}
