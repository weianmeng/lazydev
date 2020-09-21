using System.Reflection;

namespace LazyDev.Assemblies
{
    public interface IAllAssemblyFinder
    {
        Assembly[] GetAllAssemblies();
    }
}