using System.Reflection;
using LazyDev.EFCore;
using Microsoft.EntityFrameworkCore;
using Rock.Core.Entities;

namespace Rock.Infrastructure
{
    public class RockDbContext: DbContextBase
    {
        public RockDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<SysInfo> SysInfos { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountClaim> AccountClaims { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }


    }
}
