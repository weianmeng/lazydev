using FluentValidation.AspNetCore;
using LazyDev.Core;
using LazyDev.Core.Common;
using LazyDev.Core.Dependency;
using LazyDev.Core.Extensions;
using LazyDev.Core.Runtime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LazyDev.AspNetCore
{
    public static class MvcBuilderExtension
    {
        public static IMvcBuilder AddLazyDev(this IMvcBuilder mvcBuilder,Action<LazyDevAspNetCoreOptions> setupAction=null)
        {

            var options = new LazyDevAspNetCoreOptions();
            setupAction?.Invoke(options);

            var builder = mvcBuilder.AddMvcOptions(mvcOptions =>
            {
                mvcOptions.Filters.Add<LazyResultFilter>();
                mvcOptions.Filters.Add<LazyDevExceptionFilter>();
            });

            var allAssemblyFinder = new AllAssemblyFinder();
            var assemblies = allAssemblyFinder.GetAllAssemblies();

            //注册核心
            LazyDevCore.Initialize(mvcBuilder.Services,allAssemblyFinder);
            //注册Session
            builder.Services.RemoveAll<ILazyDevSession>();
            builder.Services.AddScoped<ILazyDevSession, AspNetCoreSession>();
            //使用FluentValidation
            builder.AddFluentValidation(c =>
                c.RegisterValidatorsFromAssemblies(assemblies));

            //使用json   
            builder.AddNewtonsoftJson(c =>
            {
                c.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                c.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                c.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            });

            //验证返回统一的格式
            InvalidReturnGlobalResult(mvcBuilder);

            //Swagger
            mvcBuilder.Services.AddSwaggerDocument();
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
                    var msgDetails = new Dictionary<string, string>();
                    foreach (var key in context.ModelState.Keys)
                    {
                        var state = context.ModelState[key];
                        var errors = string.Join(",", state.Errors.Select(x => x.ErrorMessage).ToList());
                        msgDetails.Add(key,errors);
                    }
                    return new ObjectResult(LazyResult.Failed(400, msgDetails.ToJson()));
                };
            });
        }

    }
}