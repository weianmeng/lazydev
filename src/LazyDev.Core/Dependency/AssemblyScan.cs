using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace LazyDev.Core.Dependency
{
    /// <summary>
    /// 程序集扫描
    /// </summary>
    public class AssemblyScan
    {
        public static IEnumerable<ServiceAttribute> FindServiceInAssemblies(params Assembly[] assemblies)
        {
            var scanComponents = new List<ServiceAttribute>();
            var types = assemblies.SelectMany(x => x.GetExportedTypes().Where(t => t.IsClass && !t.IsGenericType && !t.IsAbstract).Distinct());
            foreach (var type in types)
            {
               var components = type.GetCustomAttributes<ServiceAttribute>().ToArray();

               if (!components.Any()) continue;

               foreach (var component in components)
               {
                   component.ImplType = type;
               }
               scanComponents.AddRange(components);
            }

            return scanComponents;

        }
    }
}
