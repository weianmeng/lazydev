using Microsoft.AspNetCore.Http;

namespace LazyDev.Log.Trace
{
    public class DefaultTraceSession : ITraceSession
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DefaultTraceSession(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        private string GetHeadValue(string name)
        {
            _httpContextAccessor?.HttpContext?.Request?.Headers.TryGetValue(name, out var value);
            return value;
        }

        public string ChainId => GetHeadValue(TraceConst.ChainId);
        public string TraceId => GetHeadValue(TraceConst.TraceId);
        public string ParentTraceId => GetHeadValue(TraceConst.ParentTraceId);
    }
}
