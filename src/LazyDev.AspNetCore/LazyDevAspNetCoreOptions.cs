using System.Reflection;

namespace LazyDev.AspNetCore
{
    public class LazyDevAspNetCoreOptions
    {
        public bool UseDefaultJsonOptions { get; set; } = true;

        internal Assembly[] ServiceAssemblies { get; set; }
        internal Assembly[] FluentValidationAssemblies { get; set; }


        public void RegisterFluentValidation(params Assembly[] assemblies)
        {
            FluentValidationAssemblies = assemblies;
        }
        public void RegisterComponents(params Assembly[] assemblies)
        {
            ServiceAssemblies = assemblies;
        }
    }
}
