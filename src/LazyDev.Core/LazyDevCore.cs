using LazyDev.Core.Dependency;
using Microsoft.Extensions.DependencyInjection;

namespace LazyDev.Core
{
    public static class LazyDevCore
    {
        public static void Initialize(IServiceCollection services, IAssemblyFinder assemblyFinder)
        {

            var assemblies = assemblyFinder.GetAllAssemblies();
            //约定注册
            ConventionRegister.Register(services, assemblies);
            //模块注册
            ModuleRegister.Register(services, assemblyFinder.GetAllAssemblies());
            
        }     
    }
}
