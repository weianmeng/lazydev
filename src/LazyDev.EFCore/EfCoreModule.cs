using LazyDev.Core;
using LazyDev.Core.Dependency;
using LazyDev.Core.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace LazyDev.EFCore
{
    [ModuleDependOn(typeof(LazyCoreModule))]
    public class EfCoreModule : Module
    {
        public override void Register(IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<,>), typeof(RepositoryBase<,>));
        }
    }


}
