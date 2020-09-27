using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace LazyDev.Core.Dependency
{
    public static class RegisterService
    {
        /// <summary>
        ///  扫描注册服务(注意：不支持通用泛型服务注册,通用泛型服务请手动注册)
        /// </summary>
        /// <param name="services"></param>
        /// <param name="scanAssemblies"></param>
        /// <returns></returns>
        public static IServiceCollection Register(this IServiceCollection services, Assembly[] scanAssemblies)
        {
            var registerComponents = new List<ServiceAttribute>();
            var components = AssemblyScan.FindServiceInAssemblies(scanAssemblies);
            foreach (var component in components)
            {
                if (component.ServiceType == null)
                {
                    var serviceTypes = component.ImplType.GetInterfaces();

                    registerComponents.AddRange(from service in serviceTypes
                        where !service.IsGenericType
                        select new ServiceAttribute
                            { LifeCycle = component.LifeCycle, ServiceType = service, ImplType = component.ImplType });
                }
                else
                {
                    registerComponents.Add(component);
                }

            }
            registerComponents.ForEach(component =>
            {
                services.TryAdd(new ServiceDescriptor(component.ServiceType, component.ImplType, component.LifeCycle));
            });

            return services;
        }
    }
}
