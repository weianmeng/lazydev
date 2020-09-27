using System.Reflection;

namespace LazyDev.Core.Dependency
{
    public interface IAllAssemblyFinder
    {
        Assembly[] GetAllAssemblies();
    }
}