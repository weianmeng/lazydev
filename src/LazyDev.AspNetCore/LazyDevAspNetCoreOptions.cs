using System.Reflection;

namespace LazyDev.AspNetCore
{
    public class LazyDevAspNetCoreOptions
    {
        public bool UseNewtonsoftJson { get; set; } = true;

        internal Assembly[] ServiceAssemblies { get; set; }
        internal Assembly[] FluentValidationAssemblies { get; set; }
        internal bool UseFluentValidation { get; set; }

        public void RegisterValidatorsFromAssemblies(params Assembly[] assemblies)
        {
            UseFluentValidation = true;
            FluentValidationAssemblies = assemblies;
        }
        public void RegisterServiceFromAssemblies(params Assembly[] assemblies)
        {
            ServiceAssemblies = assemblies;
        }
    }
}
