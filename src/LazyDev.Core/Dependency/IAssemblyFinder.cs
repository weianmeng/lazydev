using System.Reflection;

namespace LazyDev.Core.Dependency
{
    public interface IAssemblyFinder
    {
        Assembly[] GetAllAssemblies();
    }
}