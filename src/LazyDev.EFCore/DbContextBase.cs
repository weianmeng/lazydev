using Microsoft.EntityFrameworkCore;

namespace LazyDev.EFCore
{
    public abstract class DbContextBase:DbContext
    {
        protected DbContextBase(DbContextOptions options):base(options)
        {
            
        }
    }
}
