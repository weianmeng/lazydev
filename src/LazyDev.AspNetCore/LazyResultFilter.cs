using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LazyDev.AspNetCore
{
    /// <summary>
    /// 包装统一的Json返回格式
    /// </summary>
    public class LazyResultFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Result is ObjectResult objectResult)
            {
                if (objectResult.Value is ILazyResult)
                {
                  return;  
                }
                context.Result = new ObjectResult(new LazyResult{Success = true,Data = objectResult.Value});
            }
        }

    }
}
