using System.Reflection;

namespace LazyDev.AspNetCore
{
    public class LazyDevAspNetCoreOptions
    {
        public bool UseDefaultJsonOptions { get; set; } = true;

        internal Assembly[] ServiceAssemblies { get; set; }
        internal Assembly[] FluentValidationAssemblies { get; set; }


        public void RegisterValidations(params Assembly[] assemblies)
        {
            FluentValidationAssemblies = assemblies;
        }
        public void RegisterServices(params Assembly[] assemblies)
        {
            ServiceAssemblies = assemblies;
        }
    }
}
