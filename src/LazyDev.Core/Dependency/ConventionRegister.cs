using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Linq;
using System.Reflection;

namespace LazyDev.Core.Dependency
{
    public class ConventionRegister
    {
        public static void Register(IServiceCollection service, Assembly[] assemblies)
        {   
            //Transient
            var transientTypes = AssemblyHelper.FindTypes<ITransientDependency>(assemblies);
            foreach (var type in transientTypes)
            {
                var interfaceTypes = type.GetInterfaces().Where(x => !x.IsGenericType);
                foreach (var interfaceType in interfaceTypes)
                {
                    service.TryAddTransient(interfaceType, type);
                }
            }

            //Scoped
            var scopeTypes = AssemblyHelper.FindTypes<IScopedDependency>(assemblies);
            foreach (var type in scopeTypes)
            {
                var interfaceTypes = type.GetInterfaces().Where(x => !x.IsGenericType);
                foreach (var interfaceType in interfaceTypes)
                {
                    service.TryAddScoped(interfaceType, type);
                }
            }
            //Singleton
            var singletonTypes = AssemblyHelper.FindTypes<IScopedDependency>(assemblies);
            foreach (var type in singletonTypes)
            {
                var interfaceTypes = type.GetInterfaces().Where(x => !x.IsGenericType);
                foreach (var interfaceType in interfaceTypes)
                {
                    service.TryAddSingleton(interfaceType, type);
                }
            }
        }
    }
}
