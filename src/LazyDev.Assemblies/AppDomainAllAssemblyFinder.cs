using Microsoft.Extensions.DependencyModel;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace LazyDev.Assemblies
{
    public class AppDomainAllAssemblyFinder : IAllAssemblyFinder
    {
        public Assembly[] GetAllAssemblies()
        {
            string[] filters =
            {
                "mscorlib",
                "netstandard",
                "dotnet",
                "api-ms-win-core",
                "runtime.",
                "System",
                "Microsoft",
                "Window",
            };
            var context = DependencyContext.Default;
            if (context == null)
            {
                var path = Directory.GetCurrentDirectory();
                var files = Directory.GetFiles(path, "*.dll", SearchOption.TopDirectoryOnly)
                    .Concat(Directory.GetFiles(path, "*.exe", SearchOption.TopDirectoryOnly))
                    .ToArray();

                return files.Where(file => filters.All(token => Path.GetFileName(file)?.StartsWith(token) != true))
                    .Select(Assembly.LoadFrom).ToArray();

            }

            var names = new List<string>();
            var dllNames = context.CompileLibraries.SelectMany(m => m.Assemblies).Distinct().Select(m => m.Replace(".dll", ""))
                .OrderBy(m => m).ToArray();
            if (dllNames.Length>0)
            {
                names.AddRange(from dllName in dllNames
                    let i = dllName.LastIndexOf('/') + 1
                    select dllName.Substring(i, dllName.Length - i)
                    into name
                    where !filters.Any(name.StartsWith)
                    select name);
            }
            else
            {
                foreach (var library in context.CompileLibraries)
                {
                    var name = library.Name;
                    if (filters.Any(name.StartsWith))
                    {
                        continue;
                    }
                    if (!names.Contains(name))
                    {
                        names.Add(name);
                    }
                }
            }
            return LoadAssemblies(names);
        }

        private static Assembly[] LoadAssemblies(IEnumerable<string> files)
        {
            var assemblies = new List<Assembly>();
            foreach (var file in files)
            {
                var name = new AssemblyName(file);
                try
                {
                    assemblies.Add(Assembly.Load(name));
                }
                catch (FileNotFoundException)
                { }
            }
            return assemblies.ToArray();
        }
    }
}
