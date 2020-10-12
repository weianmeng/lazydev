using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace LazyDev.Core.Dependency
{
    public  class ModuleRegister
    {
        public static void Register(IServiceCollection service, Assembly[] assemblies)
        {
            var types = DependencyTypeHelper.FindModuleTypes(assemblies).ToArray();
            //排序
            var moduleInfos = new List<ModuleInfo>();
            //模块依赖排序
            foreach (var type in types)
            {
                var module = (Module)Activator.CreateInstance(type);
                var dependOnModuleTypes = type.GetCustomAttributes<ModuleDependOnAttribute>().Select(x => x.ModuleType)
                    .ToList();
                // 根模块
                if (!dependOnModuleTypes.Any())
                {   
                    module.Order = 1;
                }
                moduleInfos.Add(new ModuleInfo()
                {
                    ModuleType = type,
                    Module = module,
                    DependOnModuleTypes = dependOnModuleTypes
                });
            }

            var rootModuleInfos = moduleInfos.Where(x => x.Module.Order == 1);
            foreach (var rootModuleInfo in rootModuleInfos)
            {
                //解决循环依赖
                rootModuleInfo.Processed = true;
                OrderModule(rootModuleInfo, moduleInfos);
            }

            var modules = moduleInfos.Select(x => x.Module).OrderBy(x => x.Order).ToList();
            modules.ForEach(x=>x.Register(service));
        }
        private static void OrderModule(ModuleInfo parentModuleInfo,List<ModuleInfo> moduleInfos)
        {
            foreach (var moduleInfo in moduleInfos)
            {
                if (moduleInfo.Processed)
                {
                   continue; 
                }
                if (!moduleInfo.DependOnModuleTypes.Contains(parentModuleInfo.ModuleType)) continue;

                if (moduleInfo.Module.Order < parentModuleInfo.Module.Order)
                {
                    moduleInfo.Module.Order = parentModuleInfo.Module.Order + 1;
                }
                moduleInfo.Processed = true;
                OrderModule(moduleInfo, moduleInfos);
            }
        }


        public class ModuleInfo
        {
            public Type ModuleType { get; set; }
            public Module Module { get; set; }
            public bool Processed { get; set; }
            public List<Type> DependOnModuleTypes { get; set; }
        }
    }
}
