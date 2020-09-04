namespace LazyDev.Log.Trace
{
    internal class TraceMessage:LogMessage
    {
        public Request Request { get; set; }

        public Response Response { get; set; }

        public long TimeSpan { get; set; }

    }

}
