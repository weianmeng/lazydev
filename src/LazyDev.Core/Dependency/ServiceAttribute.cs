using System;
using Microsoft.Extensions.DependencyInjection;

namespace LazyDev.Core.Dependency
{

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class ServiceAttribute:Attribute
    {
        public ServiceLifetime LifeCycle { get; internal set; }
        public Type ServiceType { get; internal set; }
        public Type ImplType { get; internal set; }
        public ServiceAttribute(Type serviceType, ServiceLifetime lifeCycle)
        {
            LifeCycle = lifeCycle;
            ServiceType = serviceType;
        }

        public ServiceAttribute(ServiceLifetime lifeCycle = ServiceLifetime.Scoped)
        {
            LifeCycle = lifeCycle;
        }
    }

   
}
