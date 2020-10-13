using LazyDev.Core.Dependency;
using LazyDev.Core.Runtime;
using Microsoft.Extensions.DependencyInjection;

namespace LazyDev.Core
{
    public class LazyCoreModule : Module
    {
        public override void Register(IServiceCollection services)
        {
            services.AddScoped(typeof(ILazyDevSession<>),typeof(NullSession<>));
            services.AddScoped(typeof(ILazyDevSession), typeof(NullSession));
        }
    }
}
