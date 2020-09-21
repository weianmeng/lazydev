using FluentValidation.AspNetCore;
using LazyDev.Assemblies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LazyDev.AspNetCore
{
    public static class MvcBuilderExtension
    {
        public static IMvcBuilder AddLazyDev(this IMvcBuilder mvcBuilder,Action<LazyDevAspNetCoreOptions> setupAction)
        {
            if (setupAction == null)
            {
                throw new ArgumentNullException(nameof(setupAction));
            }
            var options = new LazyDevAspNetCoreOptions();
            setupAction(options);

            var builder = mvcBuilder.AddMvcOptions(mvcOptions =>
            {
                mvcOptions.Filters.Add<LazyResultFilter>();
                mvcOptions.Filters.Add<LazyDevExceptionFilter>();
            });

            //使用FluentValidation
            if (options.FluentValidationAssemblies != null 
                && options.FluentValidationAssemblies.Any())
            {
                builder.AddFluentValidation(c =>
                    c.RegisterValidatorsFromAssemblies(options.FluentValidationAssemblies));
            }

            //使用使用内置json   
            if (options.UseDefaultJsonOptions)
            {
                builder.AddNewtonsoftJson(c =>
                {
                    c.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    c.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    c.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                });
            }

            //验证返回统一的格式
            InvalidReturnGlobalResult(mvcBuilder);

            //扫描注册服务
            mvcBuilder.Services.Register(options.ServiceAssemblies);

            return mvcBuilder;
        }


        /// <summary>
        /// 验证返回统一的格式
        /// </summary>
        /// <param name="mvcBuilder"></param>
        private static void InvalidReturnGlobalResult(IMvcBuilder mvcBuilder)
        {
            mvcBuilder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var result = new LazyResult
                    {
                        Success = false, Code = "400", Msg = "参数验证失败", MsgDetail = new Dictionary<string, string>()
                    };
                    foreach (var key in context.ModelState.Keys)
                    {
                        var state = context.ModelState[key];
                        var errors = string.Join(",", state.Errors.Select(x => x.ErrorMessage).ToList());
                        result.MsgDetail.Add(key,errors);
                    }

                    return new ObjectResult(result);
                };
            });
        }

    }
}