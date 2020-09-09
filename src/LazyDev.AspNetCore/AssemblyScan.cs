using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace LazyDev.AspNetCore
{
    /// <summary>
    /// 程序集扫描
    /// </summary>
    public class AssemblyScan
    {
        public static IEnumerable<ComponentAttribute> FindComponentsInAssemblies(params Assembly[] assemblies)
        {
            var scanComponents = new List<ComponentAttribute>();
            var types = assemblies.SelectMany(x => x.GetExportedTypes().Where(t => t.IsClass && !t.IsGenericType && !t.IsAbstract).Distinct());
            foreach (var type in types)
            {
               var components = type.GetCustomAttributes<ComponentAttribute>().ToArray();

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
