using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace LazyDev.Core.Dependency
{
    public  class ModuleRegister
    {
        public static void Register(IServiceCollection service, Assembly[] assemblies)
        {
            var types = AssemblyHelper.FindTypes<Module>(assemblies);
            var listModules = types.Select(type => (Module) Activator.CreateInstance(type)).ToList();

            foreach (var module in listModules.OrderBy(x=>x.Order))
            {
                module.Register(service);
            }
        }
    }
}
