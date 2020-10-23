﻿using System;
using Microsoft.Extensions.DependencyInjection;

namespace LazyDev.Core.Dependency
{

    [AttributeUsage(AttributeTargets.Class)]
    public class ComponentAttribute:Attribute
    {
        public ServiceLifetime LifeCycle { get; internal set; }
        public Type ServiceType { get; internal set; }
        public Type ImplType { get; internal set; }
        public ComponentAttribute(Type serviceType, ServiceLifetime lifeCycle)
        {
            LifeCycle = lifeCycle;
            ServiceType = serviceType;
        }

        public ComponentAttribute(ServiceLifetime lifeCycle = ServiceLifetime.Scoped)
        {
            LifeCycle = lifeCycle;
        }
    }

   
}
