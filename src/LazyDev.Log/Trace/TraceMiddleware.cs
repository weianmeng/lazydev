using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace LazyDev.Log.Trace
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
            var requestBodyString = await FormatRequestAsync(context.Request);

            var originalBody = context.Response.Body;

            using var currentBody = new MemoryStream();
            context.Response.Body = currentBody;
            //读取请求信息
            await _next(context);

            //读取响应信息
            var (responseString, statusCode) = await FormatResponseAsync(context.Response);
            await currentBody.CopyToAsync(originalBody);

            //写入日志
            _logger.LogTrace(LogLevel.Information,new TraceMessage
            {
               Request = new Request
               {
                 Path = $"{context.Request.Path}{context.Request.QueryString.Value}",
                 Method = context.Request.Method,
                 Body = requestBodyString?.Length > 4096 ? $"{requestBodyString.Substring(0, 4096)}..." : requestBodyString,
                 Headers = context.Request?.Headers.ToDictionary(s => s.Key, s => string.Join(",", s.Value.ToArray()))
               },
               Response = new Response
               {
                   Body = responseString,
                   StatusCode = statusCode
               },
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

        /// <summary>
        /// 读取请求Body数据
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private async Task<string> FormatRequestAsync(HttpRequest request)
        {
            try
            {
                if (!request.ContentLength.HasValue || request.ContentLength.Value <= 0) return "";

                request.EnableBuffering();

                using var reader = new StreamReader(request.Body, Encoding.UTF8, false, 1024, true);
                var requestBody = await reader.ReadToEndAsync();
                request.Body.Position = 0;
                return requestBody;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "获取Request Body 错误");
                throw;
            }
        }
        /// <summary>
        /// 读取响应内容
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        private async Task<(string responseString, int statusCode)> FormatResponseAsync(HttpResponse response)
        {
            try
            {
                
                response.Body.Seek(0, SeekOrigin.Begin);
                var responseString = await new StreamReader(response.Body).ReadToEndAsync();
                response.Body.Seek(0, SeekOrigin.Begin);
                return (responseString, response.StatusCode);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "获取Response Body 错误");
                throw;
            }
        }
    }
}
