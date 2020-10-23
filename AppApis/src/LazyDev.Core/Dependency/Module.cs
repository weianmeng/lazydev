using Microsoft.Extensions.DependencyInjection;

namespace LazyDev.Core.Dependency
{
    public abstract class Module
    {
        internal int Order { get; set; }
        public abstract void Register(IServiceCollection services);
    }
}
