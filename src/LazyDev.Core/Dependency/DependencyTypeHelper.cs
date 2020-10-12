using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using LazyDev.Core.Extensions;

namespace LazyDev.Core.Dependency
{
    /// <summary>
    /// 程序集类型查找帮助类
    /// </summary>
    public class DependencyTypeHelper
    {
        /// <summary>
        /// 约定类型查找
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assemblies"></param>
        /// <returns></returns>
        public static IEnumerable<Type> FindDependencyTypes<T>(Assembly[] assemblies)
        {
            return assemblies.SelectMany(x => x.GetExportedTypes().Where(t =>
                typeof(T).IsAssignableFrom(t)
                && !t.HasAttribute<IgnoreRegisterAttribute>()
                && t.IsClass
                && !t.IsGenericType 
                && !t.IsAbstract));
        }

        /// <summary>
        /// 根据 Attribute 查找类型集合
        /// </summary>
        /// <param name="assemblies"></param>
        /// <returns></returns>
        public static IEnumerable<Type> FindComponentRegisterTypes(Assembly[] assemblies)
        {
            return assemblies.SelectMany(x =>
                x.GetExportedTypes().Where(t =>
                    t.HasAttribute<ComponentAttribute>()
                    && !t.HasAttribute<IgnoreRegisterAttribute>()
                    && t.IsClass 
                    && !t.IsGenericType &&
                    !t.IsAbstract));
        }

        /// <summary>
        /// 查找所有依赖模块
        /// </summary>
        /// <param name="assemblies"></param>
        /// <returns></returns>
        public static IEnumerable<Type> FindModuleTypes(Assembly[] assemblies)
        {
            return assemblies.SelectMany(x => x.GetExportedTypes().Where(t =>
                typeof(Module).IsAssignableFrom(t)
                && !t.HasAttribute<IgnoreRegisterAttribute>()
                && t.IsClass
                && !t.IsGenericType
                && !t.IsAbstract));
        }
    }
}
