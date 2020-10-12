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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SysInfo>().ToTable("sys_info");
            modelBuilder.Entity<SysInfo>().Property(x => x.Id).HasColumnName("id");
            modelBuilder.Entity<SysInfo>().Property(x => x.Version).HasColumnName("version").HasMaxLength(20);
            base.OnModelCreating(modelBuilder);
        }


    }
}
