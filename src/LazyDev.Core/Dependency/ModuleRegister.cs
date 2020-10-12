using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using LazyDev.Core.Extensions;

namespace LazyDev.Core.Dependency
{
    public  class ModuleRegister
    {
        public static void Register(IServiceCollection service, Assembly[] assemblies)
        {
            var types = DependencyTypeHelper.FindModuleTypes(assemblies).Distinct().ToArray();
            var dictionary = new Dictionary<Type,Module>();
            //模块依赖排序
            foreach (var type in types)
            { 
                if (dictionary.ContainsKey(type))
                {
                      continue;
                }
                var module =  (Module) Activator.CreateInstance(type);
                dictionary.Add(type, module);
            }

            foreach (var keyValuePair in dictionary)
            {
                var type = keyValuePair.Key;
                var module = keyValuePair.Value;
                var dependOnAttrs = type.GetAttributes<ModuleDependOnAttribute>();

                foreach (var attr in dependOnAttrs)
                { 
                    dictionary[attr.ModuleType].Order = module.Order + 1;
                }
            }

            var listModules = dictionary.Select(x => x.Value);
            foreach (var module in listModules.OrderByDescending(x=>x.Order))
            {
                module.Register(service);
            }
        }

        internal class OrderModule
        {
            public Type ModuleType { get; set; }
            public List<Type> DependOnModuleType { get; set; }
        }
    }
}
