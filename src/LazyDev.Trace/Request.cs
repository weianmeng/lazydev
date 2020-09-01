using System.Collections.Generic;

namespace LazyDev.Trace
{
    internal class Request
    {
        public string Path { get; set; }
        public string Method { get; set; }
        public string Body { get; set; }
        public Dictionary<string, string> Headers { get; set; }
    }
}
