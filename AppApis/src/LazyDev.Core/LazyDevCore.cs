using LazyDev.Core.Dependency;
using Microsoft.Extensions.DependencyInjection;

namespace LazyDev.Core
{
    public static class LazyDevCore
    {
        public static void Initialize(IServiceCollection services, IAssemblyFinder assemblyFinder)
        {

            var assemblies = assemblyFinder.GetAllAssemblies();
            //模块注册      高
            ModuleRegister.Register(services, assemblies);

            //注解注册      中
            ComponentRegister.Register(services, assemblies);

            //约定注册      低
            ConventionRegister.Register(services, assemblies);

        }     
    }
}
