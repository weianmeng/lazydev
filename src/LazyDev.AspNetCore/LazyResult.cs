using System.Collections.Generic;

namespace LazyDev.AspNetCore
{
    public class LazyResult:ILazyResult
    {
        public bool Success { get; set; }
        public string Code { get; set; } = "0";
        public string Msg { get; set; }
        public IDictionary<string, string> MsgDetail { get; set; }
        public object Data { get; set; }

    }

}
