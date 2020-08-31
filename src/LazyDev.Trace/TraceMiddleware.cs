using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace LazyDev.Trace
{
    public class TraceMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<TraceMiddleware> _logger;
        public TraceMiddleware(RequestDelegate next, ILogger<TraceMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            var sw = new Stopwatch();
            sw.Start();

            //设置头部信息 链路ID, 父亲追踪ParentTraceId,当前TraceId
            BuildHeader(context);
           
            //读取请求信息
            await _next(context);
            //读取响应信息

            //写入日志
           _logger.LogTrace(LogLevel.Information,new TraceMessage()
           {
               TimeSpan = sw.ElapsedMilliseconds
           });
        }

        /// <summary>
        /// 设置调用链信息 
        /// </summary>
        /// <param name="context"></param>
        private static void BuildHeader(HttpContext context)
        {
            context.Request.Headers.TryGetValue(TraceConst.ChainId, out var chainId);
            if (string.IsNullOrEmpty(chainId))
            {
                context.Request.Headers.Add(TraceConst.ChainId, Guid.NewGuid().ToString());
            }

            context.Request.Headers.TryGetValue(TraceConst.TraceId, out var traceId);
            if (string.IsNullOrEmpty(traceId))
            {
                context.Request.Headers.Add(TraceConst.TraceId, Guid.NewGuid().ToString());
            }
            else
            {

                context.Request.Headers.Remove(TraceConst.ParentTraceId);
                context.Request.Headers.Add(TraceConst.ParentTraceId, traceId);

                context.Request.Headers.Remove(TraceConst.TraceId);
                context.Request.Headers.Add(TraceConst.TraceId, Guid.NewGuid().ToString());
            }

        }
    }
}
