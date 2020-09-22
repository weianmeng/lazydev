using System.Collections.Generic;

namespace LazyDev.AspNetCore
{
    public class LazyResult:ILazyResult
    {
        public bool IsSuccess => Code == 0;
        public int Code { get; set; }
        public string Msg { get; set; }
        public IDictionary<string, string> MsgDetail { get; set; }

        public static LazyResult Success()
        {
            return new LazyResult();
        }

        public static LazyResult Failed(int code,string msg=null,IDictionary<string,string> msgDetail=null)
        {
            return new LazyResult {Code = code, Msg = msg, MsgDetail = msgDetail};
        }
    }

    public class LazyResult<T> : LazyResult
    {
        public T Data { get; set; }

        public static LazyResult<T> Success(T data)
        {
            return new LazyResult<T> {Data = data};
        }
    }

}
