using Microsoft.Extensions.DependencyInjection;

namespace LazyDev.Core.Dependency
{
    public abstract class Module
    {
        public int Order => -1;

        public virtual void Register(IServiceCollection services)
        {

        }
    }
}
