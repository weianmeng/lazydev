using System;

namespace LazyDev.Core.Dependency
{
    [AttributeUsage(AttributeTargets.Class,AllowMultiple = true)]
    public class ModuleDependOnAttribute: Attribute
    {
        public Type ModuleType { get; }
        public ModuleDependOnAttribute(Type type)
        {
            ModuleType = type;
        }
    }
}
