using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Reflection;

namespace LazyDev.Core.Dependency
{
    public class ConventionRegister
    {
        public static void Register(IServiceCollection service, Assembly[] assemblies)
        {   
            //Transient
            var transientTypes = DependencyTypeHelper.FindDependencyTypes<ITransientDependency>(assemblies);
            foreach (var type in transientTypes)
            {
                var interfaceTypes = type.GetInterfaces();
                foreach (var interfaceType in interfaceTypes)
                {
                    service.TryAddTransient(interfaceType, type);
                }
            }

            //Scoped
            var scopeTypes = DependencyTypeHelper.FindDependencyTypes<IScopedDependency>(assemblies);
            foreach (var type in scopeTypes)
            {
                var interfaceTypes = type.GetInterfaces();
                foreach (var interfaceType in interfaceTypes)
                {
                    service.TryAddScoped(interfaceType, type);
                }
            }

            //Singleton
            var singletonTypes = DependencyTypeHelper.FindDependencyTypes<ISingletonDependency>(assemblies);
            foreach (var type in singletonTypes)
            {
                var interfaceTypes = type.GetInterfaces();
                foreach (var interfaceType in interfaceTypes)
                {
                    service.TryAddSingleton(interfaceType, type);
                }
            }
        }
    }
}
