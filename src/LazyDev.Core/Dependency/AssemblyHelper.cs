using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace LazyDev.Core.Dependency
{
    /// <summary>
    /// 程序集扫描
    /// </summary>
    public class AssemblyHelper
    {
        public static IEnumerable<Type> FindTypes<T>(Assembly[] assemblies)
        {
            return assemblies.SelectMany(x => x.GetExportedTypes().Where(t =>
                typeof(T).IsAssignableFrom(t) && t.IsClass && !t.IsGenericType && !t.IsAbstract));
        }
    }
}
